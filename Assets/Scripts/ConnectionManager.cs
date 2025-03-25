using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ConnectionManager : MonoBehaviour
{
    public ConnectionStatusEvent onStatusChanged
    {
        get
        {
            return this._onStatusChanged;
        }
    }

    public UnityEvent onConnect
    {
        get
        {
            return this._onConnect;
        }
    }

    public UnityEvent onDisconnect
    {
        get
        {
            return this._onDisconnect;
        }
    }

    public UnityEvent onFinalDisconnect
    {
        get
        {
            return this._onFinalDisconnect;
        }
    }

    public UnityEvent onReconnect
    {
        get
        {
            return this._onReconnect;
        }
    }

    private void Start()
    {
        ConnectionManager.THIS = this;
        this.reconnectButton.gameObject.SetActive(false);
        this.InitEvents();
        this.obvyazka = base.gameObject.GetComponent<Obvyazka>();
        this.obvyazka.AddConnectHandler(new TypedCallback<string>(this.OnConnected));
        this.obvyazka.AddNoConnectHandler(new TypedCallback<string>(this.OnNoConnect));
        this.obvyazka.AddDisconnectHandler(new TypedCallback<string>(this.OnDisconnected));
        if (!this.DEBUG || this.DEBUG_WITH_AUTH)
        {
            this.playButton.gameObject.SetActive(true);
            this.playButton.onClick.AddListener(new UnityAction(this.FirstConnect));
            this.button.gameObject.SetActive(false);
            this.field.gameObject.SetActive(false);
        }
        else
        {
            this.playButton.gameObject.SetActive(false);
            this.button.gameObject.SetActive(true);
            this.field.gameObject.SetActive(true);
            this.button.onClick.AddListener(new UnityAction(this.DebugClick));
        }
        this.obvyazka.OnU("ST", new TypedCallback<string>(this.StatusLogger), false);
        this.obvyazka.OnU("RC", new TypedCallback<string>(this.ReconnectHandler), false);
        this.reconnectButton.onClick.AddListener(new UnityAction(this.ReconnectClick));
    }

    private void ReconnectHandler(ref string msg)
    {
        this.dontReconnect = true;
        ConnectionManager.phrase = "\n\nК вашему аккаунту подключились из другого места.";
    }

    public void StatusLogger(ref string msg)
    {
        this.connectionText.text = "Инициализируем сессию: " + msg;
    }

    private void ReconnectClick()
    {
        if (ConnectionManager.disconnected && !this.tryingToConnect)
        {
            this.tryingToConnect = true;
            this.FirstConnect();
        }
    }

    private void DebugClick()
    {
        AuthManager.debugGid = this.field.text;
        this.button.gameObject.SetActive(false);
        this.field.gameObject.SetActive(false);
        this.FirstConnect();
    }

    private void InitEvents()
    {
        if (this._onConnect == null)
        {
            this._onConnect = new UnityEvent();
        }
        if (this._onDisconnect == null)
        {
            this._onDisconnect = new UnityEvent();
        }
        if (this._onFinalDisconnect == null)
        {
            this._onFinalDisconnect = new UnityEvent();
        }
        if (this._onReconnect == null)
        {
            this._onReconnect = new UnityEvent();
        }
        if (this._onStatusChanged == null)
        {
            this._onStatusChanged = new ConnectionStatusEvent();
        }
    }

    public void ForceDisconnectFull()
    {
        this.fadeIn = true;
        this.fadeOut = false;
        this.obvyazka.ForceDisconnect();
    }

    public void ForceDisconnect()
    {
        this.fadeIn = true;
        this.fadeOut = false;
        this.obvyazka.ForceDisconnect();
        this.connectionText.gameObject.SetActive(true);
        base.Invoke("ReTry", 2f);
    }

    private void ReTry()
    {
        this.fadeOut = false;
        this.fadeIn = true;
        ConnectionManager.disconnected = true;
        this.connectionText.text = ConnectionManager.phrase;
        if (!this.dontReconnect)
        {
            this.reconnectTries++;
            if (this.reconnectTries > 50)
            {
                this.dontReconnect = true;
            }
            this.reconnectButton.gameObject.SetActive(true);
            base.Invoke("ReconnectClick", 1f);
        }
    }

    public void FirstConnect()
    {
        if (this.DEBUG_PORTS && this.DEBUG)
        {
            this.tcpPort = 9094;
            this.ioPort = 9095;
        }
        else if (this.DEBUG_PORTS)
        {
            this.tcpPort = 9090;
            this.ioPort = 9091;
        }
        this.connectionText.text = "Подключаемся к серверу...";
        this.status = "connecting";
        this.obvyazka.Connect(this.tcpPort, this.ioPort, this.host);
        this._onStatusChanged.Invoke(this.status);
        this.playButton.gameObject.SetActive(false);
        this.button.gameObject.SetActive(false);
        this.field.gameObject.SetActive(false);
        this.reconnectButton.gameObject.SetActive(false);
        ServerTime.THIS.clientTimeStart = -1;
    }

    private void SecondConnect()
    {
    }

    private void OnDisconnected(ref string msg)
    {
        this._onDisconnect.Invoke();
        this.fadeIn = true;
        this.connectionText.gameObject.SetActive(true);
        this.reconnectionNum++;
        this.ReTry();
        this.prefix = "Потеря подключения";
    }

    private void OnNoConnect(ref string msg)
    {
        this.tryingToConnect = false;
        this.status = "no_connect";
        this._onFinalDisconnect.Invoke();
        this._onStatusChanged.Invoke(this.status);
        this.ReTry();
    }

    private void OnConnected(ref string msg)
    {
        this.tryingToConnect = false;
        this.status = "connected";
        this._onConnect.Invoke();
        this._onStatusChanged.Invoke(this.status);
        this.connectionText.text = "Подключение выполнено...";
        this.reconnectionNum = 2;
        this.fadeOut = true;
        ConnectionManager.disconnected = false;
    }

    private void Update()
    {
        if (this.reconnectTime > 0f)
        {
            int num = Mathf.FloorToInt(this.reconnectTime - Time.unscaledTime);
            if (num <= 0)
            {
                this.reconnectTime = -1f;
                this.connectionText.text = "Подключаемся к серверу...";
                return;
            }
            this.connectionText.text = this.prefix + "... Повторная попытка через " + num;
        }
    }
    
	public bool DEBUG;

	public bool DEBUG_PORTS;

	public bool DEBUG_WITH_AUTH = true;

	public static string METHOD = "SITE";

	private string status = "not_connected";

	public bool dontReconnect;

	private Obvyazka obvyazka;

	public Button button;

	public Button playButton;

	public InputField field;

	public Text connectionText;

	public Button reconnectButton;

	private ConnectionStatusEvent _onStatusChanged = new ConnectionStatusEvent();

	private UnityEvent _onConnect = new UnityEvent();

	private UnityEvent _onDisconnect = new UnityEvent();

	private UnityEvent _onFinalDisconnect = new UnityEvent();

	private UnityEvent _onReconnect = new UnityEvent();

	public static ConnectionManager THIS;

	private bool tryingToConnect;

	private int tries;

	public static bool disconnected;

	public static string phrase = "\n\nНет подключения к серверу.";

	private int reconnectTries;

	private int tcpPort = 8090;

	private int ioPort = 8082;

	private float reconnectTime = -1f;

	private int reconnectionNum = 2;

	private string prefix = "Сервер не отвечает";

	private bool fadeOut;

	private bool fadeIn;

    private string host = "90.188.7.54";
}
