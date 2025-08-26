using Assets.Scripts.DrugsMod;
using MyUI;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
    public void DailyRewardToggle(bool showReward)
    {
        if (showReward)
        {
            this.blinkDonate = true;
            return;
        }
        this.blinkDonate = false;
        this.DonatePlus.gameObject.SetActive(true);
    }

    public void ShowInventoryGrid(int d, int dx, int dy, int w, int h, string mapStr)
    {
        char[] array = mapStr.ToCharArray();
        int[] array2 = new int[w * h];
        for (int i = 0; i < w * h; i++)
        {
            if (array[i] == '0')
            {
                array2[i] = 0;
            }
            else
            {
                array2[i] = 1;
            }
        }
        if (w * h == 1681)
        {
            array2 = DMGlobalVariables.gunWith3Radiuses;
        }
        OverlayRenderer.THIS.AddGrid(w, h, array2, dx, dy, d);
    }

    public void ChooseInventoryItem(int item, int num, string hint)
    {
        this.inventoryItem = item;
        this.InventoryHint.text = hint;
    }

    public void HideClanIcon()
    {
        this.clanButton.gameObject.SetActive(false);
    }

    public void ShowClanIcon(int id)
    {
        if (id == 0)
        {
            this.HideClanIcon();
            return;
        }
        this.clanButton.gameObject.SetActive(true);
        this.clanIcon.sprite = ClanSpriteScript.sprites[id - 1];
    }

    public void CloseInventoryItem()
    {
        this.inventoryItem = -1;
        this.InventoryHint.text = "";
        OverlayRenderer.THIS.HideGrid();
    }

    public void ChangeProgTo(bool state)
    {
    }

    private void Start()
    {
        this.ChangeProgTo(false);
        this.connectionManager = this.network.GetComponent<ConnectionManager>();
        this.connectionManager.onStatusChanged.AddListener(new UnityAction<string>(this.updateConnectionTF));
        if (GUIManager.THIS != null)
        {
            throw new Exception("Singletone!");
        }
        GUIManager.THIS = this;
        this.m_EventSystem = EventSystem.current;
        this.m_EventSystem.sendNavigationEvents = false;
        this.fontLoader.gameObject.SetActive(false);
        this.agrShow.gameObject.SetActive(false);
        this.autoRemShow.gameObject.SetActive(false);
        this.progOpenButton.onClick.AddListener(new UnityAction(this.OnProgButton));
        this.returnButton.onClick.AddListener(new UnityAction(this.OnReturnButton));
        this.soundButton.onClick.AddListener(new UnityAction(this.OnSound));
        this.musicButton.onClick.AddListener(new UnityAction(this.OnMusic));
        this.mapButton.onClick.AddListener(new UnityAction(this.OnMap));
        this.settingsButton.onClick.AddListener(new UnityAction(this.OnSettings));
        this.DonateButton.onClick.AddListener(new UnityAction(this.OnDonate));
        this.InventoryHint.text = "";
        this.dropboxButton.onClick.AddListener(new UnityAction(this.OnDropbox));
        this.ConsoleButton.onClick.AddListener(new UnityAction(this.OnConsole));
        this.HelpButton.onClick.AddListener(new UnityAction(this.OnHelp));
        this.clanButton.onClick.AddListener(new UnityAction(this.OpenClan));
        this.buildingsButton.onClick.AddListener(new UnityAction(this.OpenBuildings));
    }

    private void OnConsole()
    {
        ClientController.CanGoto = false;
        TutorialNavigation.CheckHide("_CONS");
        ServerTime.THIS.SendTypicalMessage(ClientController.THIS.TimeOfMove(), "Locl", 0, 0, "console");
    }

    private void OnHelp()
    {
        //ClientController.CanGoto = false;
        //TutorialNavigation.CheckHide("_HELP");
        //ServerTime.THIS.SendTypicalMessage(-1, "Help", 0, 0, "_");
        DMPopupManager.THIS.Show();
    }

    private void OpenBuildings()
    {
        ClientController.CanGoto = false;
        TutorialNavigation.CheckHide("_BLDS");
        ServerTime.THIS.SendTypicalMessage(-1, "Blds", 0, 0, "_");
    }

    private void OnSettings()
    {
        ClientController.CanGoto = false;
        TutorialNavigation.CheckHide("_SETT");
        ServerTime.THIS.SendTypicalMessage(-1, "Sett", 0, 0, "_");
    }

    private void OpenClan()
    {
        ClientController.CanGoto = false;
        TutorialNavigation.CheckHide("_CLAN");
        ServerTime.THIS.SendTypicalMessage(-1, "Clan", 0, 0, "_");
    }

    private void OnDropbox()
    {
        ClientController.CanGoto = false;
        TutorialNavigation.CheckHide("_DROP");
        ServerTime.THIS.SendTypicalMessage(-1, "DPBX", 0, 0, "_");
    }

    private void OnInventory()
    {
        if (this.inventoryItem == -1)
        {
            ServerTime.THIS.SendTypicalMessage(-1, "INVN", 0, 0, "_");
            return;
        }
        ServerTime.THIS.SendTypicalMessage(-1, "INCL", 0, 0, "_");
    }

    private void OnDonate()
    {
        ClientController.CanGoto = false;
        TutorialNavigation.CheckHide("_DONATE");
        ServerTime.THIS.SendTypicalMessage(-1, "GDon", 0, 0, ConnectionManager.METHOD);
    }

    private void OnMap()
    {
        ClientController.CanGoto = false;
        TutorialNavigation.CheckHide("_MAP");
        MapViewer.THIS.Show();
    }

    public void SetMusic()
    {
        if (SoundManager.MusicOn)
        {
            this.musicOff.gameObject.SetActive(false);
        }
        else
        {
            this.musicOff.gameObject.SetActive(true);
        }
        SoundManager.THIS.UpdateMusic();
    }

    public void SetSound()
    {
        if (SoundManager.SoundOn)
        {
            this.soundOff.gameObject.SetActive(false);
            return;
        }
        this.soundOff.gameObject.SetActive(true);
    }

    private void OnMusic()
    {
        ClientController.CanGoto = false;
        TutorialNavigation.CheckHide("_Music");
        SoundManager.MusicOn = !SoundManager.MusicOn;
        this.SetMusic();
        ServerTime.THIS.SendTypicalMessage(-1, "Sett", 0, 0, "mus:" + (SoundManager.MusicOn ? "1" : "0"));
    }

    private void OnSound()
    {
        ClientController.CanGoto = false;
        TutorialNavigation.CheckHide("_SOUND");
        SoundManager.SoundOn = !SoundManager.SoundOn;
        this.SetSound();
        ServerTime.THIS.SendTypicalMessage(-1, "Sett", 0, 0, "snd:" + (SoundManager.SoundOn ? "1" : "0"));
    }

    private void OnReturnButton()
    {
        ClientController.CanGoto = false;
        TutorialNavigation.CheckHide("_RESP");
        AYSWindowManager.THIS.Show("ВОЗВРАЩЕНИЕ НА РЕСП", "Вы собираетесь вернуться на респаун.\nВы потеряете 10% груза, остальной груз упакуется в бокс.\nУверены?", delegate
        {
            ServerTime.THIS.SendTypicalMessage(-1, "RESP", 0, 0, "_");
        });
    }

    public void OnProgCloseButton()
    {
        ClientController.CanGoto = false;
       
        ServerTime.THIS.SendTypicalMessage(-1, "pRST", 0, 0, "_");
    }

    private void OnProgButton()
    {
        ClientController.CanGoto = false;
        TutorialNavigation.CheckHide("_PROG");
        if (!this.programmator.activeSelf)
        {
            ServerTime.THIS.SendTypicalMessage(-1, "pRST", 0, 0, "_");
            if (!ProgrammatorView.opened)
            {
                ServerTime.THIS.SendTypicalMessage(-1, "Pope", 0, 0, GUIManager.programToSend);
                return;
            }
            this.programmator.SetActive(true);
            ProgrammatorView.active = true;
            ProgrammatorView.THIS.Show();
        }
    }

    public void UpdateProgramm(int id, string title, string source)
    {
        if (id == -1)
        {
            this.programmatorTitlePanel.text = title;
            this.programmatorTitle.text = title;
            GUIManager.programToSend = source;
            return;
        }
        this.programmator.SetActive(true);
        ProgrammatorView.active = true;
        ProgrammatorView.programId = id;
        ProgrammatorView.title = title;
        this.programmatorTitlePanel.text = title;
        this.programmatorTitle.text = title;
        GUIManager.programToSend = "_";
        if (source.Length > 0)
        {
            ProgrammatorView.THIS.LoadFromString(source);
        }
        else
        {
            ProgrammatorView.THIS.ClearSource();
            ProgrammatorView.THIS.UpdateIconsWithoutSaving();
        }
        ProgrammatorView.THIS.Show();
        this.programmator.SetActive(false);
        ProgrammatorView.active = false;
    }

    public void OpenProgramm(int id, string title, string source)
    {
        if (id == -1)
        {
            this.programmatorTitlePanel.text = title;
            this.programmatorTitle.text = title;
            GUIManager.programToSend = source;
            return;
        }
        this.programmator.SetActive(true);
        ProgrammatorView.active = true;
        ProgrammatorView.programId = id;
        ProgrammatorView.title = title;
        this.programmatorTitlePanel.text = title;
        this.programmatorTitle.text = title;
        GUIManager.programToSend = "_";
        if (source.Length > 0)
        {
            ProgrammatorView.THIS.LoadFromString(source);
            Debug.Log("DA");
        }
        else
        {
            ProgrammatorView.THIS.ClearSource();
            ProgrammatorView.THIS.UpdateIconsWithoutSaving();
            Debug.Log("Net");
        }
        ProgrammatorView.THIS.Show();
    }

    public void ChatHandler(ref string msg)
    {
    }

    public void ClearFocus()
    {
        this.m_EventSystem.SetSelectedGameObject(null);
    }

    private void OnLocalChatButton()
    {
        ServerTime.THIS.SendTypicalMessage(ClientController.THIS.TimeOfMove(), "Locl", 0, 0, this.localChatInput.text);
        this.localChatInput.text = "";
        this.ClearFocus();
        this.localChatInput.gameObject.SetActive(false);
    }

    private void OnChatButton()
    {
        ChatManager.THIS.SendChat();
        ChatManager.THIS.UpdateFocus();
    }

    public void SetCoord(int x, int y)
    {
        string cell = "Null";
        //Debug.Log($"GUIManager.SetCoord ClientController.map: {ClientController.map.}");
        switch (ClientController.StaticDirection) {
            case 0: cell = ClientController.map.GetCell(ClientController.StaticView_x, ClientController.StaticView_y + 1).ToString();break;
            case 1: cell = ClientController.map.GetCell(ClientController.StaticView_x - 1, ClientController.StaticView_y).ToString();break;
            case 2: cell = ClientController.map.GetCell(ClientController.StaticView_x, ClientController.StaticView_y - 1).ToString();break;
            case 3: cell = ClientController.map.GetCell(ClientController.StaticView_x + 1, ClientController.StaticView_y).ToString();break;
        };
        this.CoordTF.text = x.ToString() + ":" + y.ToString() + ":" + cell;
    }

    public void SetMoney(long money, long creds)
    {
        this.MoneyTF.text = " $" + money.ToString("### ### ### ##0");
        this.CredTF.text = creds.ToString("### ### ### ##0");
    }

    public void SetCreds(int creds)
    {
        this.CredTF.text = creds.ToString("### ### ### ##0");
    }

    public void SetOnline(string online, string onprog)
    {
        this.OnlineTF.text = string.Concat(new string[]
        {
            "ОНЛАЙН ",
            online,
            " <color=yellow>(",
            onprog,
            ")</color>"
        });
    }

    public void SetMods(string[] mods)
    {
        this.modsTF.text = "Модули:\n";
        for (int i = 0; i < mods.Length; i++)
        {
            Text text = this.modsTF;
            text.text = text.text + mods[i] + "\n";
        }
    }

    public void SetLevel(int level)
    {
        this.LevelTF.text = " ур." + level;
    }

    public void SetHP(int hp, int hpmax)
    {
        this.HPTF.text = hp + "HP";
        float num = (float)hp / (float)hpmax;
        this.HPTF.color = new Color(Mathf.Sqrt(Mathf.Sqrt(1f - num)), Mathf.Sqrt(Mathf.Sqrt(num)), num * (1f - num));
        this.HpLine.color = new Color(Mathf.Sqrt(Mathf.Sqrt(1f - num)), Mathf.Sqrt(Mathf.Sqrt(num)), num * (1f - num), 0.7f);
        Vector2 sizeDelta = this.HpLine.rectTransform.sizeDelta;
        sizeDelta.x = this.HpWrapper.rectTransform.sizeDelta.x * num;
        this.HpLine.rectTransform.sizeDelta = sizeDelta;
    }

    private void MakeAlpha(Text TF, float alpha)
    {
        Color color = TF.color;
        color.a = alpha;
        TF.color = color;
    }

    private void SetBasketCrys(GameObject TF, long crys)
    {
        Text componentInChildren = TF.GetComponentInChildren<Text>();
        if (crys < 1000L)
        {
            componentInChildren.text = " " + crys.ToString("##0");
        }
        else if (crys < 1000000L)
        {
            componentInChildren.text = " " + crys.ToString("### ##0");
        }
        else if (crys < 10000000000L)
        {
            crys = (long)Mathf.FloorToInt((float)crys / 1000f);
            componentInChildren.text = " " + crys.ToString("### ##0") + " K";
        }
        else
        {
            crys = (long)Mathf.FloorToInt((float)crys / 1000000f);
            componentInChildren.text = " " + crys.ToString("### ##0") + " KK";
        }
        if (crys == 0L)
        {
            this.MakeAlpha(componentInChildren, 0.25f);
            TF.SetActive(false);
            return;
        }
        this.MakeAlpha(componentInChildren, 1f);
        TF.SetActive(true);
    }

    public void SetBasket(long g, long b, long r, long v, long w, long c, long capacity)
    {
        this.SetBasketCrys(this.BasketGTF, g);
        this.SetBasketCrys(this.BasketBTF, b);
        this.SetBasketCrys(this.BasketRTF, r);
        this.SetBasketCrys(this.BasketVTF, v);
        this.SetBasketCrys(this.BasketWTF, w);
        this.SetBasketCrys(this.BasketCTF, c);
        if (g + b + r + v + w + c == 0L)
        {
            this.BasketPanel.SetActive(false);
        }
        else
        {
            this.BasketPanel.SetActive(true);
        }
        float num = (float)capacity / 100f;
        if (num > 1f)
        {
            num = 1f;
        }
        float num2 = (float)(capacity - 100L) / 100f;
        if (num2 > 1f)
        {
            num2 = 1f;
        }
        string str;
        if (capacity < 10L)
        {
            str = " Груз  <color=#777>" + capacity.ToString() + "%";
        }
        else if (capacity < 50L)
        {
            str = " Груз <color=#777>" + capacity.ToString() + "%";
        }
        else if (capacity < 100L)
        {
            str = " Груз <color=#7f7>" + capacity.ToString() + "%";
        }
        else if (capacity < 115L)
        {
            str = "Груз <color=#ff5>" + capacity.ToString() + "%";
        }
        else if (capacity < 200L)
        {
            str = "Груз <color=#f75>" + capacity.ToString() + "%";
        }
        else if (capacity < 1000L)
        {
            str = "Груз <color=#f55>" + capacity.ToString() + "%";
        }
        else if (capacity < 2000L)
        {
            str = " Груз <color=#f57>1k%";
        }
        else if (capacity < 3000L)
        {
            str = " Груз <color=#f5f>2k%";
        }
        else
        {
            str = "Груз <color=#5ff>3k+%";
        }
        this.CapacityTF.text = str + "</color>";
        if (capacity < 50L)
        {
            this.CapacityBar.color = new Color(0.75f, 0.75f, 0.75f);
            this.overload = false;
            Vector2 sizeDelta = this.CapacityBar.rectTransform.sizeDelta;
            sizeDelta.x = 95f * num;
            this.CapacityBar.rectTransform.sizeDelta = sizeDelta;
            sizeDelta = this.CapacityOverloadBar.rectTransform.sizeDelta;
            sizeDelta.x = 0f;
            this.CapacityOverloadBar.rectTransform.sizeDelta = sizeDelta;
            return;
        }
        if (capacity < 100L)
        {
            this.CapacityBar.color = new Color(0.25f, 0.99f, 0.25f);
            this.overload = false;
            Vector2 sizeDelta2 = this.CapacityBar.rectTransform.sizeDelta;
            sizeDelta2.x = 95f * num;
            this.CapacityBar.rectTransform.sizeDelta = sizeDelta2;
            sizeDelta2 = this.CapacityOverloadBar.rectTransform.sizeDelta;
            sizeDelta2.x = 0f;
            this.CapacityOverloadBar.rectTransform.sizeDelta = sizeDelta2;
            return;
        }
        if (capacity < 115L)
        {
            this.CapacityBar.color = new Color(0.95f, 0.95f, 0.25f);
            this.overload = true;
            Vector2 sizeDelta3 = this.CapacityBar.rectTransform.sizeDelta;
            sizeDelta3.x = 95f;
            this.CapacityBar.rectTransform.sizeDelta = sizeDelta3;
            this.CapacityOverloadBar.color = new Color(0.7f, 0.5f, 0.15f);
            sizeDelta3 = this.CapacityOverloadBar.rectTransform.sizeDelta;
            sizeDelta3.x = 95f * num2;
            this.CapacityOverloadBar.rectTransform.sizeDelta = sizeDelta3;
            return;
        }
        if (capacity < 200L)
        {
            this.CapacityBar.color = new Color(0.99f, 0.65f, 0.25f);
            this.overload = true;
            Vector2 sizeDelta4 = this.CapacityBar.rectTransform.sizeDelta;
            sizeDelta4.x = 95f;
            this.CapacityBar.rectTransform.sizeDelta = sizeDelta4;
            this.CapacityOverloadBar.color = new Color(0.7f, 0f, 0.15f);
            sizeDelta4 = this.CapacityOverloadBar.rectTransform.sizeDelta;
            sizeDelta4.x = 95f * num2;
            this.CapacityOverloadBar.rectTransform.sizeDelta = sizeDelta4;
            return;
        }
        this.CapacityBar.color = new Color(0.9f, 0.35f, 0.35f);
        this.overload = true;
        Vector2 sizeDelta5 = this.CapacityBar.rectTransform.sizeDelta;
        sizeDelta5.x = 95f;
        this.CapacityBar.rectTransform.sizeDelta = sizeDelta5;
        this.CapacityOverloadBar.color = new Color(0.7f, 0f, 0.15f);
        sizeDelta5 = this.CapacityOverloadBar.rectTransform.sizeDelta;
        sizeDelta5.x = 95f * num2;
        this.CapacityOverloadBar.rectTransform.sizeDelta = sizeDelta5;
    }

    private void updateConnectionTF(string msg)
    {
    }

    private void њљјїјїїјїљљїњњїљјљљњјјњ()
    {
    }

    private void њјљјњјљљљїњљњїјїјњњїїїљ()
    {
    }

    public bool GlobalChatIsActive()
    {
        return this.m_EventSystem.currentSelectedGameObject != null && this.m_EventSystem.currentSelectedGameObject.name == "ChatField";
    }

    private void Update()
    {
        if (this.blinkDonate)
        {
            this.DonatePlus.gameObject.SetActive(Mathf.Sin(15f * Time.time) > 0f);
        }
        if (this.overload)
        {
            Color color = this.CapacityOverloadBar.color;
            color.a = 0.7f + 0.3f * Mathf.Sin(19f * Time.time);
            this.CapacityOverloadBar.color = color;
        }
        if (Input.GetMouseButtonDown(0))
        {
            if ((this.m_EventSystem.currentSelectedGameObject != null && this.m_EventSystem.currentSelectedGameObject.name == "LocalChat") || this.localChatInput.gameObject.activeSelf)
            {
                ClientController.CanGoto = false;
                this.localChatInput.text = "";
                this.ClearFocus();
                this.localChatInput.gameObject.SetActive(false);
            }
            if (this.fullscreenButton != null)
            {
                Vector3 position = this.fullscreenButton.transform.position;
                if (Vector3.Distance(UnityEngine.Input.mousePosition, position) < 23f)
                {
                    ClientController.CanGoto = false;
                    Screen.fullScreen = !Screen.fullScreen;
                    TerrainRendererScript.needUpdate = true;
                    TutorialNavigation.CheckHide("_FULL");
                }
            }
        }
        if (!ProgrammatorView.active && UnityEngine.Input.GetKeyDown(ClientConfig.LOCALCHAT_KEY) && (this.m_EventSystem.currentSelectedGameObject == null || (this.m_EventSystem.currentSelectedGameObject.name != "LocalChat" && this.m_EventSystem.currentSelectedGameObject.name != "InputField" && this.m_EventSystem.currentSelectedGameObject.name != "ChatField")))
        {
            this.localChatInput.gameObject.SetActive(true);
            this.m_EventSystem.SetSelectedGameObject(this.localChatInput.gameObject, null);
        }
        if (UnityEngine.Input.GetKeyDown(KeyCode.Tab))
        {
            //OnProgButton();
            ChatManager.THIS.OnToggle();
        }
        if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
        {
            if (this.inventoryItem != -1)
            {
                ServerTime.THIS.SendTypicalMessage(-1, "INCL", 0, 0, "-1");
            }
            if (this.m_EventSystem.currentSelectedGameObject != null && this.m_EventSystem.currentSelectedGameObject.name == "LocalChat")
            {
                this.localChatInput.text = "";
                this.ClearFocus();
                this.localChatInput.gameObject.SetActive(false);
            }
        }
        if ((UnityEngine.Input.GetKeyDown(KeyCode.Return) || UnityEngine.Input.GetKeyDown(KeyCode.KeypadEnter)) && !ConnectionManager.disconnected && this.m_EventSystem.currentSelectedGameObject != null)
        {
            if (this.m_EventSystem.currentSelectedGameObject.name == "ChatField")
            {
                this.OnChatButton();
            }
            else if (this.m_EventSystem.currentSelectedGameObject.name == "LocalChat")
            {
                this.OnLocalChatButton();
            }
        }
        if (this.m_EventSystem.currentSelectedGameObject == null || (this.m_EventSystem.currentSelectedGameObject.name != "LocalChat" && this.m_EventSystem.currentSelectedGameObject.name != "InputField" && this.m_EventSystem.currentSelectedGameObject.name != "ChatField"))
        {
            if (UnityEngine.Input.GetKeyDown(ClientConfig.TOGGLE0_KEY))
            {
                ClientConfig.Toggle(0);
            }
            if (UnityEngine.Input.GetKeyDown(ClientConfig.TOGGLE1_KEY))
            {
                ClientConfig.Toggle(1);
            }
            if (UnityEngine.Input.GetKeyDown(ClientConfig.TOGGLE2_KEY))
            {
                ClientConfig.Toggle(2);
            }
            if (UnityEngine.Input.GetKeyDown(ClientConfig.TOGGLE3_KEY))
            {
                ClientConfig.Toggle(3);
            }
            if (UnityEngine.Input.GetKeyDown(ClientConfig.TOGGLE4_KEY))
            {
                ClientConfig.Toggle(4);
            }
            if (UnityEngine.Input.GetKeyDown(ClientConfig.TOGGLE5_KEY))
            {
                ClientConfig.Toggle(5);
            }
            if (UnityEngine.Input.GetKeyDown(ClientConfig.TOGGLE6_KEY))
            {
                ClientConfig.Toggle(6);
            }
            if (UnityEngine.Input.GetKeyDown(ClientConfig.TOGGLE7_KEY))
            {
                ClientConfig.Toggle(7);
            }
            if (UnityEngine.Input.GetKeyDown(ClientConfig.TOGGLE8_KEY))
            {
                ClientConfig.Toggle(8);
            }
            if (UnityEngine.Input.GetKeyDown(ClientConfig.TOGGLE9_KEY))
            {
                ClientConfig.Toggle(9);
            }
        }
    }
    
	public GameObject network;

	public Text statusTF;

	public Button fullscreenButton;

	public Button returnButton;

	public Button mapButton;

	public Button buildingsButton;

	public Text InventoryHint;

	public Button inventoryButton;

	public GameObject currentItemImage;

	public int inventoryItem = -1;

	public Button banHammer;

	public Button dropboxButton;

	public Button settingsButton;

	public Button progOpenButton;

	public Button clanButton;

	public Image clanIcon;

	public Button soundButton;

	public Image soundOff;

	public Button musicButton;

	public Image musicOff;

	public GameObject programmator;

	public Text programmatorTitle;

	public Text programmatorTitlePanel;

	public GameObject agrShow;

	public GameObject autoRemShow;

	public Text GeoTF;

	public Text HPTF;

	public Text LevelTF;

	public RawImage HpWrapper;

	public RawImage HpLine;

	public GameObject BasketPanel;

	public GameObject BasketGTF;

	public GameObject BasketBTF;

	public GameObject BasketRTF;

	public GameObject BasketVTF;

	public GameObject BasketWTF;

	public GameObject BasketCTF;

	public Text CapacityTF;

	public RawImage CapacityBar;

	public RawImage CapacityOverloadBar;

	public Text MoneyTF;

	public Text CredTF;

	public InputField CoordTF;

	public Text OnlineTF;

	public Button DonateButton;

	public Button ConsoleButton;

	public Button HelpButton;

	public StatePanel statePanel;

	public Image DonatePlus;

	public GameObject AccountPanel;

	public GameObject PayloadPanel;

	private bool blinkDonate;

	public static string programToSend = "_";

	private ConnectionManager connectionManager;

	public MyInputField localChatInput;

	public Text chatTF;

	public Text modsTF;

	public Image RightPanel;

	public GameObject fontLoader;

	public EventSystem m_EventSystem;

	public static GUIManager THIS;

	private bool overload;
}

