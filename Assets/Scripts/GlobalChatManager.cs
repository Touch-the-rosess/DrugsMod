using MyUI;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GlobalChatManager : MonoBehaviour
{
    private GlobalChatLineModel DefaultLineModel()
    {
        return new GlobalChatLineModel
        {
            cid = 0,
            id = 0,
            name = "",
            text = "",
            color = 0
        };
    }

    private void Start()
    {
        GlobalChatManager.THIS = this;
        this.chatModeButton.onClick.AddListener(new UnityAction(this.OnModeButton));
        this.chatColors = new uint[]
        {
            16121843u,
            13390926u,
            9211599u,
            15061507u,
            3201644u,
            10071813u,
            7198161u,
            14973696u,
            15721871u,
            14124754u,
            5090557u,
            13421772u,
            16681832u,
            4100608u,
            9400432u
        };
        this.chatInput.gameObject.name = "ChatField";
        this.colorPickButton.onClick.AddListener(new UnityAction(this.OnColorButton));
        this.ChangeChatColor();
        this.chatColor = 2;
        this.chatLines = new GameObject[this.ALL_MESSAGES];
        this.fed_models = new GlobalChatLineModel[this.ALL_MESSAGES];
        this.tor_models = new GlobalChatLineModel[this.ALL_MESSAGES];
        this.dno_models = new GlobalChatLineModel[this.ALL_MESSAGES];
        this.models = this.fed_models;
        for (int i = 0; i < this.ALL_MESSAGES; i++)
        {
            GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.chatLinePrefab);
            this.chatLines[i] = gameObject;
            gameObject.transform.SetParent(this.chatLinesContainer.transform);
            int num = i;
            gameObject.GetComponentInChildren<Button>().onClick.AddListener(delegate ()
            {
                this.CommonNickListener(num);
            });
            this.fed_models[i] = this.DefaultLineModel();
            this.tor_models[i] = this.DefaultLineModel();
            this.dno_models[i] = this.DefaultLineModel();
        }
        this.UpdateView();
        this.OnModeButton();
        this.fedButton.onClick.AddListener(new UnityAction(this.Fedbt));
        this.torButton.onClick.AddListener(new UnityAction(this.Torbt));
        this.dnoButton.onClick.AddListener(new UnityAction(this.Dnobt));
    }

    private void Fedbt()
    {
        this.ChangeChannelTo(0, "FED");
    }

    private void Torbt()
    {
        this.ChangeChannelTo(1, "TOR");
    }

    private void Dnobt()
    {
        this.ChangeChannelTo(2, "DNO");
    }

    public void ChangeChannelTo(int num, string name)
    {
        this.channel = name;
        Vector3 position = this.chatArrow.transform.position;
        position.y = 120f - 20f * (float)num;
        this.chatArrow.transform.position = position;
        switch (num)
        {
            case 0:
                this.models = this.fed_models;
                break;
            case 1:
                this.models = this.tor_models;
                break;
            case 2:
                this.models = this.dno_models;
                break;
        }
        this.UpdateView();
    }

    public void ChatHandler(ref string msg)
    {
        if (msg.IndexOf('#') != -1)
        {
            string[] array = msg.Substring(0, msg.IndexOf('#')).Split(new char[]
            {
                ':'
            });
            string text = msg.Substring(msg.IndexOf('#') + 1);
            string[] array2 = text.Split(new char[]
            {
                ':'
            });
            array2[0] = text.Substring(0, text.IndexOf(':'));
            array2[1] = text.Substring(text.IndexOf(':') + 1);
            string a = array[3];
            GlobalChatLineModel[] array3;
            if (a == "FED")
            {
                array3 = this.fed_models;
            }
            else if (a == "DNO")
            {
                array3 = this.dno_models;
            }
            else
            {
                array3 = this.tor_models;
            }
            if (array3[this.ALL_MESSAGES - 1].id == int.Parse(array[2]) && array3[this.ALL_MESSAGES - 1].text.Length + array2[1].Length < 130)
            {
                string str = ", ";
                if ("!?.,;:*()[]{}".IndexOf(array3[this.ALL_MESSAGES - 1].text.Substring(array3[this.ALL_MESSAGES - 1].text.Length - 1)) != -1)
                {
                    str = " ";
                }
                GlobalChatLineModel[] array4 = array3;
                int num = this.ALL_MESSAGES - 1;
                array4[num].text = array4[num].text + str + array2[1];
            }
            else
            {
                for (int i = 0; i < this.ALL_MESSAGES - 1; i++)
                {
                    array3[i] = array3[i + 1];
                }
                array3[this.ALL_MESSAGES - 1].name = array2[0];
                array3[this.ALL_MESSAGES - 1].text = array2[1];
                array3[this.ALL_MESSAGES - 1].color = int.Parse(array[0]);
                array3[this.ALL_MESSAGES - 1].cid = int.Parse(array[1]);
                array3[this.ALL_MESSAGES - 1].id = int.Parse(array[2]);
            }
            if (array3 == this.models)
            {
                this.UpdateView();
            }
        }
    }

    private void UpdateView()
    {
        for (int i = 0; i < this.ALL_MESSAGES; i++)
        {
            Text[] componentsInChildren = this.chatLines[i].GetComponentsInChildren<Text>();
            componentsInChildren[0].color = this.UIntToColor(this.chatColors[this.models[i].color]);
            componentsInChildren[0].text = " " + this.models[i].name + " : ";
            componentsInChildren[1].color = this.UIntToColor(this.chatColors[this.models[i].color]);
            componentsInChildren[1].text = this.models[i].text;
            Image componentInChildren = this.chatLines[i].GetComponentInChildren<Image>();
            if (this.models[i].cid == 0)
            {
                componentInChildren.sprite = this.emptySprite;
            }
            else
            {
                componentInChildren.sprite = ClanSpriteScript.sprites[this.models[i].cid - 1];
            }
        }
    }

    private Color UIntToColor(uint color)
    {
        byte b = (byte)(color >> 16);
        byte b2 = (byte)(color >> 8);
        byte b3 = (byte)color;
        return new Color
        {
            r = (float)b / 255f,
            g = (float)b2 / 255f,
            b = (float)b3 / 255f,
            a = 1f
        };
    }

    public void SendChat()
    {
        ServerTime.THIS.SendTypicalMessage(ClientController.THIS.TimeOfMove(), "Chat", 0, 0, string.Concat(new object[]
        {
            this.chatColor,
            ":",
            this.channel,
            "#",
            this.chatInput.text
        }));
        this.chatInput.text = "";
    }

    private void OnColorButton()
    {
        this.chatColor++;
        this.chatColor %= this.chatColors.Length;
        this.ChangeChatColor();
    }

    public void ChangeChatColor()
    {
        this.colorPickButton.image.color = this.UIntToColor(this.chatColors[this.chatColor]);
        this.chatInput.textComponent.color = this.UIntToColor(this.chatColors[this.chatColor]);
        this.chatInput.image.color = this.UIntToColor(this.chatColors[this.chatColor]);
    }

    public void SetLinesNum(int num)
    {
        for (int i = 0; i < this.ALL_MESSAGES; i++)
        {
            this.chatLines[i].gameObject.SetActive(i > this.ALL_MESSAGES - 1 - num);
        }
    }

    private void CommonNickListener(int јњљјїњјњњњїїјјљљњїњјљјј)
    {
    }

    private void OnModeButton()
	{
		this.chatMode++;
		this.chatMode %= 3;
		if (this.chatMode == 0)
		{
			this.chatFull.gameObject.SetActive(false);
			this.chatNorm.gameObject.SetActive(true);
			this.chatMin.gameObject.SetActive(false);
			Vector2 sizeDelta = this.chatBack.rectTransform.sizeDelta;
			sizeDelta.y = 155f;
			this.chatBack.rectTransform.sizeDelta = sizeDelta;
			this.chatBack.gameObject.SetActive(true);
			this.SetLinesNum(11);
			return;
		}
		if (this.chatMode == 1)
		{
			this.chatFull.gameObject.SetActive(false);
			this.chatNorm.gameObject.SetActive(false);
			this.chatMin.gameObject.SetActive(true);
			this.chatBack.gameObject.SetActive(false);
			this.SetLinesNum(3);
			return;
		}
		this.chatFull.gameObject.SetActive(true);
		this.chatNorm.gameObject.SetActive(false);
		this.chatMin.gameObject.SetActive(false);
		this.chatBack.gameObject.SetActive(true);
		Vector2 sizeDelta2 = this.chatBack.rectTransform.sizeDelta;
		sizeDelta2.y = 600f;
		this.chatBack.rectTransform.sizeDelta = sizeDelta2;
		this.SetLinesNum(28);
	}

    private void Update()
    {
    }

   
	public static GlobalChatManager THIS;

	public Button chatModeButton;

	public Image chatFull;

	public Image chatNorm;

	public Image chatMin;

	public Sprite emptySprite;

	public Image chatBack;

	public Image chatArrow;

	public GameObject chatLinesContainer;

	public GameObject[] chatLines;

	public GameObject chatLinePrefab;

	private GlobalChatLineModel[] fed_models;

	private GlobalChatLineModel[] tor_models;

	private GlobalChatLineModel[] dno_models;

	private GlobalChatLineModel[] models;

	public MyInputField chatInput;

	public Button colorPickButton;

	public int chatColor = 2;

	public uint[] chatColors;

	public Button fedButton;

	public Button torButton;

	public Button dnoButton;

	private int chatMode;

	private int ALL_MESSAGES = 28;

	private string channel = "FED";

}

