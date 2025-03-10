using Obvyazka3;
using System;
using System.Threading;
using UnityEngine;
using System.Runtime.InteropServices;

public class Obvyazka : MonoBehaviour
{
    public void Connect(int tcpPort, int socketIOPort, string host)
    {
        if (!this._inited)
        {
            this.Init();
        }
        this.client.OnU("_connected", new TypedCallback<string>(this._onTCPConnected), true);
        this.client.OnU("_connectError", new TypedCallback<string>(this._onTCPNoConnect), true);
        this.client.OnU("_disconnect", new TypedCallback<string>(this._onTCPDisconnect), true);
        this.client.Connect(tcpPort, host);
    }

    private void _onTCPDisconnect(ref string msg)
    {
        this._needDisconnect = true;
    }

    private void _onTCPNoConnect(ref string msg)
    {
        this._needNoConnect = true;
    }

    private void _onTCPConnected(ref string msg)
    {
        this._needConnected = true;
    }

    private void Init()
    {
        if (!this._inited)
        {
            this._tcpOperationMutex = new Mutex();
            this.client = new TCPConnection(this._tcpOperationMutex);
        }
        this._inited = true;
    }

    private void Update()
    {
        if (!this._inited)
        {
            return;
        }
        if (this._tcpOperationMutex != null && this._tcpOperationMutex.WaitOne(10))
        {
            if (this._needConnected)
            {
                this._needConnected = false;
                this.Connected();
            }
            if (this._needNoConnect)
            {
                this._needNoConnect = false;
                this.NoConnect();
            }
            if (this._needDisconnect)
            {
                this._needDisconnect = false;
                this.Disconnected();
            }
            this.client.Update();
            this._tcpOperationMutex.ReleaseMutex();
        }
    }

    private void Start()
    {
        this.Init();
    }

    public void ForceDisconnect()
    {
        if (this.client != null)
        {
            this.client.ForceDisconnect();
        }
    }

    public void AddDisconnectHandler(TypedCallback<string> cb)
    {
        this._stringCallbackSet.addEventCallback("disconnect", cb, false);
    }

    public void AddNoConnectHandler(TypedCallback<string> cb)
    {
        this._stringCallbackSet.addEventCallback("noconnect", cb, false);
    }

    public void AddConnectHandler(TypedCallback<string> cb)
    {
        this._stringCallbackSet.addEventCallback("connect", cb, false);
    }

    public void SendJ(string eventName, JSONNode message)
    {
        this.Init();
        this.client.SendJ(eventName, message);
    }

    public void SendU(string eventName, string message)
    {
        this.Init();
        this.client.SendU(eventName, message);
    }

    public void SendB(string eventName, byte[] message)
    {
        this.Init();
        this.client.SendB(eventName, message);
    }

    public void OnJ(string eventName, TypedCallback<JSONNode> cb, bool once = false)
    {
        this.Init();
        this.client.OnJ(eventName, cb, once);
    }

    public void OnU(string eventName, TypedCallback<string> cb, bool once = false)
    {
        this.Init();
        this.client.OnU(eventName, cb, once);
    }

    public void OnB(string eventName, TypedCallback<byte[]> cb, bool once = false)
    {
        this.Init();
        this.client.OnB(eventName, cb, once);
    }

    private void Connected()
    {
        string text = "connect";
        this._stringCallbackSet.emitEvent("connect", ref text);
    }

    private void Disconnected()
    {
        string text = "disconnect";
        this._stringCallbackSet.emitEvent("disconnect", ref text);
    }

    private void NoConnect()
    {
        string text = "noconnect";
        this._stringCallbackSet.emitEvent("noconnect", ref text);
    }

    private void OnApplicationQuit()
    {
        if (this.client != null)
        {
            this.client.Destroy();
        }
    }

    public GameConnection client;

	private bool _inited;

	private EventCallbackSet<string> _stringCallbackSet = new EventCallbackSet<string>();

	private Mutex _tcpOperationMutex;

	private bool _needDisconnect;

	private bool _needNoConnect;

	private bool _needConnected;

	public string vk_access_token = "";

	public string vk_access_token2 = "";
}

