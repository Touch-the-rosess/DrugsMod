namespace Obvyazka3
{
	public abstract class GameConnection
	{
		public abstract void Connect(int port, string host);

		public abstract void Destroy();

		public abstract void Translate(string type, string eventName, string data);

		public abstract void Update();

		public abstract void ForceDisconnect();

		public abstract void SendJ(string eventName, JSONNode message);

		public abstract void SendU(string eventName, string message);

		public abstract void SendB(string eventName, byte[] message);

		public abstract void OnJ(string eventName, TypedCallback<JSONNode> cb, bool once = false);

		public abstract void OnU(string eventName, TypedCallback<string> cb, bool once = false);

		public abstract void OnB(string eventName, TypedCallback<byte[]> cb, bool once = false);
	}
}
