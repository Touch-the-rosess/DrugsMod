using MyUI;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ChatManager : MonoBehaviour
{
    public string getLasts()
    {
        string text = "";
        bool flag = true;
        foreach (KeyValuePair<string, int> keyValuePair in this.LastIDs)
        {
            if (!flag)
            {
                text += "#";
            }
            flag = false;
            text = string.Concat(new object[]
            {
                text,
                keyValuePair.Key,
                "#",
                keyValuePair.Value
            });
        }
        return text;
    }

    public string getCurrentChat()
    {
        return this.currentChat;
    }

    private void Start()
    {
        ChatManager.THIS = this;
        this.UpdateChatMode();
        this.ChatToggle.onClick.AddListener(new UnityAction(this.ctog));
        this.RightChatToggle.onClick.AddListener(new UnityAction(this.rctog));
        this.ChatsButton.onClick.AddListener(new UnityAction(this.OnMenu));
        this.ChatSettings.onClick.AddListener(new UnityAction(this.OnSettings));
    }

    private void ctog()
    {
        this.OnToggle();
        ClientController.CanGoto = false;
    }

    private void rctog()
    {
        this.OnToggle();
        ClientController.CanGoto = false;
    }

    private void OnSettings()
    {
        ClientController.CanGoto = false;
        TutorialNavigation.CheckHide("_CHATSET");
        ServerTime.THIS.SendTypicalMessage(ClientController.THIS.TimeOfMove(), "Cset", 0, 0, "_");
    }

    private void OnMenu()
    {
        ClientController.CanGoto = false;
        TutorialNavigation.CheckHide("_CHATMENU");
        ServerTime.THIS.SendTypicalMessage(ClientController.THIS.TimeOfMove(), "Cmen", 0, 0, "_");
    }

    public void OnToggle()
    {
        TutorialNavigation.CheckHide("_CHATTOGGLE");
        this.chatmode++;
        if (this.chatmode == 3)
        {
            this.chatmode = 0;
        }
        this.UpdateChatMode();
        base.Invoke("UpdateFocus", 0.01f);
    }

    public void UpdateFocus()
    {
        if (this.chatmode == 1)
        {
            GUIManager.THIS.m_EventSystem.SetSelectedGameObject(this.ChatInput.gameObject, null);
            return;
        }
        GUIManager.THIS.ClearFocus();
    }

    private void SetPanelMode(int mode)
    {
        if (mode == 0)
        {
            this.RightChatToggle.gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            this.ChatsButton.gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.5f);
        }
        if (mode == 1)
        {
            this.RightChatToggle.gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.5f);
            this.ChatsButton.gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        }
    }

    public void mnHandler(ref string msg)
    {
        if (short.Parse(msg) == 0)
        {
            this.Notification.SetActive(false);
            return;
        }
        this.Notification.SetActive(true);
        this.Notification.GetComponentInChildren<Text>().text = msg;
    }

    public void mlHandler(ref string msg)
    {
        if (msg == "")
        {
            return;
        }
        this.ChatInput.gameObject.SetActive(false);
        this.TitleTF.text = "СПИСОК ЧАТОВ";
        foreach (object obj in this.ChatContainer.transform)
        {
            UnityEngine.Object.Destroy(((Transform)obj).gameObject);
        }
        string[] array = msg.Split(new char[]
        {
            '#'
        });
        for (int i = 0; i < array.Length; i++)
        {
            string[] subs = array[i].Split(new char[]
            {
                '±'
            });
            string text = "#dddd88";
            if (subs[0].StartsWith("_"))
            {
                subs[3] = subs[3].Substring(subs[3].IndexOf(':') + 1);
                text = "#88aa88";
            }
            GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ChatMenuLinePrefab);
            gameObject.transform.SetParent(this.ChatContainer.transform, false);
            gameObject.GetComponent<Text>().text = ((subs[1] == "1") ? "  " : "");
            gameObject.GetComponentInChildren<Image>().gameObject.SetActive(subs[1] == "1");
            if (subs[2].Length > 20)
            {
                subs[2] = subs[2].Substring(0, 18) + "...";
            }
            Text component = gameObject.GetComponent<Text>();
            component.text = string.Concat(new string[]
            {
                component.text,
                "<color=",
                text,
                ">",
                subs[2],
                "</color>"
            });
            if (subs[3].Length > 22)
            {
                subs[3] = subs[3].Substring(0, 20) + "...";
            }
            Text component2 = gameObject.GetComponent<Text>();
            component2.text = component2.text + "\n<color=#888888>" + subs[3] + "</color>";
            gameObject.GetComponent<Button>().onClick.AddListener(delegate ()
            {
                string str = subs[0];
                ServerTime.THIS.SendTypicalMessage(ClientController.THIS.TimeOfMove(), "Choo", 0, 0, str);
            });
        }
    }

    public void moHandler(ref string msg)
    {
        this.ChatInput.gameObject.SetActive(true);
        int num = msg.IndexOf(':');
        string text = msg.Substring(0, num);
        string text2 = msg.Substring(num + 1);
        if (text.StartsWith("_"))
        {
            if (text2.Length > 20)
            {
                text2 = text2.Substring(0, 18) + "...";
            }
            this.TitleTF.text = "ЛС – " + text2;
        }
        else
        {
            this.TitleTF.text = text + " – " + text2;
        }
        this.currentChat = text;
        this.SetPanelMode(0);
        if (!this.History.ContainsKey(text))
        {
            this.History.Add(text, new List<GCMessage>());
            this.LastIDs.Add(text, -1);
        }
        foreach (object obj in this.ChatContainer.transform)
        {
            UnityEngine.Object.Destroy(((Transform)obj).gameObject);
        }
        this.lastAdded = default(GCMessage);
        List<GCMessage> list = this.History[text];
        for (int i = 0; i < list.Count; i++)
        {
            GCMessage message = list[i];
            this.AddLine(message);
        }
        this.UpdateMini();
        this.DelayedForcedScrollDown();
    }

    public void UpdateChatStyle()
    {
        foreach (object obj in this.ChatContainer.transform)
        {
            ChatLineInfo component = ((Transform)obj).gameObject.GetComponent<ChatLineInfo>();
            if (component != null)
            {
                component.SetMessage(component.msg);
            }
        }
    }

    private void UpdateMini()
    {
        foreach (object obj in this.DownChatContainer.transform)
        {
            UnityEngine.Object.Destroy(((Transform)obj).gameObject);
        }
        for (int i = Math.Max(0, this.History[this.currentChat].Count - 3); i < this.History[this.currentChat].Count; i++)
        {
            this.AddMiniLine(this.History[this.currentChat][i]);
        }
    }

    private void AddMiniLine(GCMessage message)
    {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ChatLineMiniPrefab);
        gameObject.transform.SetParent(this.DownChatContainer.transform, false);
        if (message.cid != 0)
        {
            gameObject.GetComponentInChildren<Image>().sprite = ClanSpriteScript.sprites[message.cid - 1];
            gameObject.GetComponent<Text>().text = "      " + message.nick + ": " + message.text;
        }
        else
        {
            gameObject.GetComponentInChildren<Image>().gameObject.SetActive(false);
            gameObject.GetComponent<Text>().text = message.nick + ": " + message.text;
        }
        gameObject.GetComponent<Text>().color = this.colorFromCode(message.color, true);
    }

    private Color colorFromCode(int code, bool mini = false)
    {
        if (code != 50)
        {
            return Color.HSVToRGB((float)code / 20f, 0.3f, (code % 2 == 0) ? 1f : 0.86f);
        }
        if (mini)
        {
            return new Color(1f, 1f, 1f, 1f);
        }
        return new Color(0.5f, 0.5f, 0.5f, 1f);
    }

    private void AddLine(GCMessage message)
    {
        string str = ", ";
        string text = "!?.,;:*()[]{}";
        GCMessage GCMessage = this.lastAdded;
        this.lastAdded = message;
        if (GCMessage.id >= message.id)
        {
            return;
        }
        if (this.History[this.currentChat].Count > 0)
        {
            int childCount = this.ChatContainer.transform.childCount;
            int length = this.ChatContainer.transform.GetChild(childCount - 1).gameObject.GetComponent<Text>().text.Length;
            if (GCMessage.gid > 0 && GCMessage.gid == message.gid && length < 100 && GCMessage.text.Length > 0)
            {
                if (text.IndexOf(GCMessage.text.Substring(GCMessage.text.Length - 1)) != -1)
                {
                    str = " ";
                }
                GCMessage.text = GCMessage.text + str + message.text;
                string text2 = this.ChatContainer.transform.GetChild(childCount - 1).gameObject.GetComponent<Text>().text;
                this.ChatContainer.transform.GetChild(childCount - 1).gameObject.GetComponent<Text>().text = text2 + str + message.text;
                return;
            }
        }
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ChatLinePrefab);
        gameObject.transform.SetParent(this.ChatContainer.transform, false);
        gameObject.GetComponent<ChatLineInfo>().SetMessage(message);
        gameObject.GetComponent<Button>().onClick.AddListener(delegate ()
        {
            int gid = message.gid;
            ServerTime.THIS.SendTypicalMessage(ClientController.THIS.TimeOfMove(), "Cpri", 0, 0, gid.ToString());
        });
        gameObject.GetComponent<Text>().color = this.colorFromCode(message.color, false);
        if (this.ChatContainer.transform.childCount > 30)
        {
            int num = this.ChatContainer.transform.childCount - 30;
            for (int i = 0; i < num; i++)
            {
                UnityEngine.Object.Destroy(this.ChatContainer.transform.GetChild(i).gameObject);
            }
        }
    }

    public void SendChat()
    {
        if (!this.ChatInput.gameObject.activeSelf)
        {
            return;
        }
        if (this.ChatInput.text != "")
        {
            ServerTime.THIS.SendTypicalMessage(ClientController.THIS.TimeOfMove(), "Chat", 0, 0, this.ChatInput.text);
        }
        this.ChatInput.text = "";
    }

    public void mcHandler(ref string msg)
    {
        int num = (int)short.Parse(msg);
        this.ChatInput.GetComponentInChildren<Text>().color = Color.HSVToRGB((float)num / 20f, 0.3f, (num % 2 == 0) ? 1f : 0.86f);
    }

    public void muHandler(ref string msg)
    {
        ChatManager.MuPacket MuPacket = JsonUtility.FromJson<ChatManager.MuPacket>(msg);
        for (int i = 0; i < MuPacket.h.Length; i++)
        {
            string[] array = MuPacket.h[i].Split(new char[]
            {
                '±'
            });
            if (array.Length == 7)
            {
                GCMessage GCMessage = default(GCMessage);
                GCMessage.id = int.Parse(array[0]);
                GCMessage.color = int.Parse(array[1]);
                GCMessage.cid = int.Parse(array[2]);
                GCMessage.time = int.Parse(array[3]);
                GCMessage.nick = array[4];
                GCMessage.text = array[5];
                GCMessage.gid = int.Parse(array[6]);
                GCMessage.realtxt = "";
                if (MuPacket.ch == this.currentChat)
                {
                    this.AddLine(GCMessage);
                }
                if (!this.History.ContainsKey(MuPacket.ch))
                {
                    this.History.Add(MuPacket.ch, new List<GCMessage>());
                    this.LastIDs.Add(MuPacket.ch, -1);
                }
                if (GCMessage.id > this.LastIDs[MuPacket.ch])
                {
                    this.History[MuPacket.ch].Add(GCMessage);
                    this.LastIDs[MuPacket.ch] = GCMessage.id;
                }
            }
        }
        while (this.History[MuPacket.ch].Count > 50)
        {
            this.History[MuPacket.ch].RemoveAt(0);
        }
        if (MuPacket.ch == this.currentChat)
        {
            this.UpdateMini();
        }
        if (MuPacket.ch == this.currentChat)
        {
            this.ScrollDown();
        }
    }

    private void DelayedForcedScrollDown()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(this.DownChatContainer.GetComponent<RectTransform>());
        LayoutRebuilder.ForceRebuildLayoutImmediate(this.ChatScroll.GetComponent<RectTransform>());
        base.Invoke("ForcedScrollDown", 0.2f);
    }



    public void ScrollDown()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(this.DownChatContainer.GetComponent<RectTransform>());
        LayoutRebuilder.ForceRebuildLayoutImmediate(this.ChatScroll.GetComponent<RectTransform>());
        this.ScrollDown2(false);
    }

    private void ForcedScrollDown()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(this.DownChatContainer.GetComponent<RectTransform>());
        LayoutRebuilder.ForceRebuildLayoutImmediate(this.ChatScroll.GetComponent<RectTransform>());
        this.ChatScroll.verticalNormalizedPosition = 0f;
    }

    private void ScrollDown2(bool force = false)
    {
        if (force || this.ChatScroll.verticalNormalizedPosition < 0.2f)
        {
            this.ChatScroll.verticalNormalizedPosition = 0f;
        }
    }

    private void UpdateChatMode()
    {
        switch (this.chatmode)
        {
            case 0:
                this.LeftGUI.transform.localPosition = new Vector3(0f, 0f, 0f);
                this.ChatPanel.gameObject.SetActive(false);
                this.ChatToggleOff.gameObject.SetActive(false);
                this.ChatToggle.gameObject.SetActive(true);
                this.DownChatContainer.gameObject.SetActive(true);
                return;
            case 1:
                this.LeftGUI.transform.localPosition = new Vector3(213f, 0f, 0f);
                this.ChatPanel.gameObject.SetActive(true);
                this.ChatToggleOff.gameObject.SetActive(false);
                this.ChatToggle.gameObject.SetActive(false);
                this.DownChatContainer.gameObject.SetActive(false);
                this.ScrollDown();
                base.Invoke("ScrollDown", 0.1f);
                return;
            case 2:
                this.LeftGUI.transform.localPosition = new Vector3(0f, 0f, 0f);
                this.ChatPanel.gameObject.SetActive(false);
                this.ChatToggleOff.gameObject.SetActive(true);
                this.ChatToggle.gameObject.SetActive(true);
                this.DownChatContainer.gameObject.SetActive(false);
                this.ChatInput.text = "";
                return;
            default:
                return;
        }
    }
    
    private void Update()
    {
    }
    
    public GameObject LeftGUI;

	public Button ChatToggle;

	public Image ChatToggleOff;

	public Button RightChatToggle;

	public Button ChatsButton;

	public Button ChatSettings;

	public GameObject ChatContainer;

	public GameObject ChatPanel;

	public GameObject Notification;

	public MyInputField ChatInput;

	public Text TitleTF;

	public GameObject ChatMenuLinePrefab;

	public GameObject ChatLinePrefab;

	public GameObject ChatLineMiniPrefab;

	public ScrollRect ChatScroll;

	public GameObject DownChatContainer;

	public static ChatManager THIS;

	private Dictionary<string, int> LastIDs = new Dictionary<string, int>();

	private Dictionary<string, List<GCMessage>> History = new Dictionary<string, List<GCMessage>>();

    private int chatmode;

	private string currentChat = "";

	private GCMessage lastAdded;

	public struct MuPacket
	{
		public string[] h;

		public string ch;
	}
}

