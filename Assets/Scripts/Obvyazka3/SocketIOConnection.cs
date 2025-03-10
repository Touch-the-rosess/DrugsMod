using System;
using UnityEngine;
using System.Runtime.InteropServices;
using System.Text;

namespace Obvyazka3
{
    public class SocketIOConnection : GameConnection
    {
        public EventCallbackSet<JSONNode> jcbs;
        public EventCallbackSet<byte[]> bcbs;
        public EventCallbackSet<string> ucbs;
        public string _host;
        public int _port;
        
        [DllImport("__Internal")]
        private static extern void ConnectSocketIO(int port, string host);
		
		[DllImport("__Internal")]
        private static extern void SendSocketIOJ(string name, string json);
		
		[DllImport("__Internal")]
        private static extern void SendSocketIOU(string name, string str);
		
		[DllImport("__Internal")]
        private static extern void SendSocketIOB(string name, string buffer);
		
		[DllImport("__Internal")]
        private static extern void SocketIOPleaseDisconnect();
		
        public SocketIOConnection()
        {
			this.jcbs = new EventCallbackSet<JSONNode>();
			this.bcbs = new EventCallbackSet<byte[]>();
			this.ucbs = new EventCallbackSet<string>();
        }
        public override void SendJ(string eventName, JSONNode message)
        {
			SendSocketIOJ(eventName, message);
        }
        public override void SendB(string eventName, byte[] message)
        {
			SendSocketIOB(eventName, Convert.ToBase64String(message));
        }
        public override void SendU(string eventName, string message)
        {
			SendSocketIOU(eventName, message);
        }
        public override void OnJ(string eventName, TypedCallback<JSONNode> cb, bool once)
        {
			this.jcbs.addEventCallback(eventName, cb, once);
        }
        public override void OnB(string eventName, TypedCallback<byte[]> cb, bool once)
        {
			this.bcbs.addEventCallback(eventName, cb, once);
        }
        public override void OnU(string eventName, TypedCallback<string> cb, bool once)
        {
			this.ucbs.addEventCallback(eventName, cb, once);
        }
        public override void Translate(string type, string eventName, string data)
        {
			switch(type) {
				case "U":
					this.ucbs.emitEvent(eventName, ref data);
				break;
				case "B":
					byte[] message = Convert.FromBase64String(data);
					this.bcbs.emitEvent(eventName, ref message);
				break;
				case "J":
					JSONNode message2 = JSON.Parse(data);
					this.jcbs.emitEvent(eventName, ref message2);
				break;
			}
        }
        public override void Connect(int port, string host)
        {
			ConnectSocketIO(port, host);
        }
		public override void Update()
		{
			try
			{
				this.jcbs.emitFromQueue();
				this.ucbs.emitFromQueue();
				this.bcbs.emitFromQueue();
			}
			catch (Exception arg)
			{
				Debug.Log("TRANSLATION ERROR! " + arg);
			}
		}
        public override void ForceDisconnect()
        {
			SocketIOPleaseDisconnect();
        }
        public override void Destroy()
        {
			SocketIOPleaseDisconnect();
			this.jcbs.cbs.Clear();
			this.bcbs.cbs.Clear();
			this.ucbs.cbs.Clear();
        }
    
    }

}