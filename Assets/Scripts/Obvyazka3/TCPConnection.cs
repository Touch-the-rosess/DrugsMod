using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

namespace Obvyazka3
{
	public class TCPConnection : GameConnection
	{
		private Socket _client;

		private byte[] _segmentBuffer;

		private byte[] _mergedBuffer;

		private byte[] _typeBuffer;

		private byte[] _eventNameBuffer;

		private EventCallbackSet<JSONNode> _jsonCallbackSet;

		private EventCallbackSet<byte[]> _bufferCallbackSet;

		private EventCallbackSet<string> _stringCallbackSet;

		private int _mergedLen;

		private int _maxSegmentLen = 68000;

		private int _maxBufferLen = 1000000;

		private const int LEN_PREFIX_LENGTH = 4;

		private const int TYPE_PREFIX_LENGTH = 1;

		private const int EVENTNAME_PREFIX_LENGTH = 2;

		private const int PREFIX_LENGTH = 7;

		private string _host;

		private int _port;

		private Mutex _tcpOperationMutex;

		private bool canConnect = true;

		private void recreateSocket()
		{
			_client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			_client.NoDelay = true;
			canConnect = true;
		}

		public TCPConnection(Mutex tcpOperationMutex)
		{
			_tcpOperationMutex = tcpOperationMutex;
			recreateSocket();
			_segmentBuffer = new byte[_maxSegmentLen];
			_mergedBuffer = new byte[_maxBufferLen];
			_typeBuffer = new byte[1];
			_eventNameBuffer = new byte[2];
			_jsonCallbackSet = new EventCallbackSet<JSONNode>();
			_bufferCallbackSet = new EventCallbackSet<byte[]>();
			_stringCallbackSet = new EventCallbackSet<string>();
		}

		public override void Connect(int port, string host)
		{
			_port = port;
			_host = host;
			SocketState state = new SocketState();
			_client.BeginConnect(_host, _port, _connectHandler, state);
			canConnect = false;
		}

		private void _connectHandler(IAsyncResult asyncResult)
		{
			_tcpOperationMutex.WaitOne();
			try
			{
				SocketState state = (SocketState)asyncResult.AsyncState;
				_client.EndConnect(asyncResult);
				string msg = "";
				_stringCallbackSet.emitEvent("_connected", ref msg);
				_client.BeginReceive(_segmentBuffer, 0, _segmentBuffer.Length, SocketFlags.None, _recieveHandler, state);
			}
			catch (Exception)
			{
				string msg2 = "";
				_stringCallbackSet.emitEvent("_connectError", ref msg2);
				recreateSocket();
			}
			_tcpOperationMutex.ReleaseMutex();
		}

		public override void SendJ(string eventName, JSONNode message)
		{
			if (!canConnect)
			{
				string s = message.ToString();
				byte[] bytes = Encoding.UTF8.GetBytes("J" + eventName);
				byte[] bytes2 = Encoding.UTF8.GetBytes(s);
				int num = 4 + bytes.Length + bytes2.Length;
				byte[] bytes3 = BitConverter.GetBytes((uint)num);
				byte[] array = new byte[num];
				Buffer.BlockCopy(bytes3, 0, array, 0, bytes3.Length);
				Buffer.BlockCopy(bytes, 0, array, bytes3.Length, bytes.Length);
				Buffer.BlockCopy(bytes2, 0, array, bytes3.Length + bytes.Length, bytes2.Length);
				try
				{
					_client.Send(array);
				}
				catch (Exception)
				{
					string msg = "";
					_stringCallbackSet.emitEvent("_disconnect", ref msg);
					recreateSocket();
				}
			}
		}

		public override void SendB(string eventName, byte[] message)
		{
			if (!canConnect)
			{
				byte[] bytes = Encoding.UTF8.GetBytes("B" + eventName);
				int num = 4 + bytes.Length + message.Length;
				byte[] bytes2 = BitConverter.GetBytes((uint)num);
				byte[] array = new byte[num];
				Buffer.BlockCopy(bytes2, 0, array, 0, bytes2.Length);
				Buffer.BlockCopy(bytes, 0, array, bytes2.Length, bytes.Length);
				Buffer.BlockCopy(message, 0, array, bytes2.Length + bytes.Length, message.Length);
				try
				{
					_client.Send(array);
				}
				catch (Exception)
				{
					string msg = "";
					_stringCallbackSet.emitEvent("_disconnect", ref msg);
					recreateSocket();
				}
			}
		}

		public override void SendU(string eventName, string message)
		{
			if (!canConnect)
			{
				byte[] bytes = Encoding.UTF8.GetBytes("U" + eventName);
				byte[] bytes2 = Encoding.UTF8.GetBytes(message);
				int num = 4 + bytes.Length + bytes2.Length;
				byte[] bytes3 = BitConverter.GetBytes((uint)num);
				byte[] array = new byte[num];
				Buffer.BlockCopy(bytes3, 0, array, 0, bytes3.Length);
				Buffer.BlockCopy(bytes, 0, array, bytes3.Length, bytes.Length);
				Buffer.BlockCopy(bytes2, 0, array, bytes3.Length + bytes.Length, bytes2.Length);
				try
				{
					_client.Send(array);
				}
				catch (Exception)
				{
					string msg = "";
					_stringCallbackSet.emitEvent("_disconnect", ref msg);
					recreateSocket();
				}
			}
		}

		public override void OnJ(string eventName, TypedCallback<JSONNode> cb, bool once = false)
		{
			_jsonCallbackSet.addEventCallback(eventName, cb, once);
		}

		public override void OnB(string eventName, TypedCallback<byte[]> cb, bool once = false)
		{
			_bufferCallbackSet.addEventCallback(eventName, cb, once);
		}

		public override void OnU(string eventName, TypedCallback<string> cb, bool once = false)
		{
			_stringCallbackSet.addEventCallback(eventName, cb, once);
		}

		private void _recieveHandler(IAsyncResult asyncResult)
		{
			_tcpOperationMutex.WaitOne();
			SocketState socketState = (SocketState)asyncResult.AsyncState;
			socketState.counter++;
			int counter = socketState.counter;
			int num = _client.EndReceive(asyncResult);
			if (num > 0)
			{
				if (_mergedLen + num > _maxBufferLen)
				{
					_tcpOperationMutex.ReleaseMutex();
					return;
				}
				Buffer.BlockCopy(_segmentBuffer, 0, _mergedBuffer, _mergedLen, num);
				_mergedLen += num;
				int num2 = 0;
				int num3 = 0;
				while (true)
				{
					num3++;
					if (_mergedLen < 4)
					{
						break;
					}
					num2 = Convert.ToInt32(BitConverter.ToUInt32(_mergedBuffer, 0));
					if (num2 > _mergedLen)
					{
						break;
					}
					_translateMessage(num2);
					if (num2 < _mergedLen)
					{
						Buffer.BlockCopy(_mergedBuffer, num2, _mergedBuffer, 0, _mergedLen - num2);
						_mergedLen -= num2;
						continue;
					}
					_mergedLen = 0;
					break;
				}
				_client.BeginReceive(_segmentBuffer, 0, _segmentBuffer.Length, SocketFlags.None, _recieveHandler, socketState);
			}
			else
			{
				string msg = "";
				_stringCallbackSet.emitEvent("_disconnect", ref msg);
				recreateSocket();
			}
			_tcpOperationMutex.ReleaseMutex();
		}

		public override void Translate(string type, string eventName, string data)
		{
		}

		public override void ForceDisconnect()
		{
			try
			{
				_client.DisconnectAsync(new SocketAsyncEventArgs());
			}
			catch (Exception)
			{
			}
			recreateSocket();
		}

		public override void Update()
		{
			try
			{
				_jsonCallbackSet.emitFromQueue();
				_stringCallbackSet.emitFromQueue();
				_bufferCallbackSet.emitFromQueue();
			}
			catch (Exception arg)
			{
				UnityEngine.Debug.Log("TRANSLATION ERROR! " + arg);
			}
		}

		private void _translateMessage(int messageLen)
		{
			Buffer.BlockCopy(_mergedBuffer, 4, _typeBuffer, 0, 1);
			Buffer.BlockCopy(_mergedBuffer, 5, _eventNameBuffer, 0, 2);
			string @string = Encoding.Default.GetString(_typeBuffer);
			string string2 = Encoding.Default.GetString(_eventNameBuffer);
			if (@string == "J")
			{
				JSONNode msg = JSON.Parse(Encoding.Default.GetString(_mergedBuffer, 7, messageLen - 7));
				_jsonCallbackSet.delayedEmitEvent(string2, ref msg);
			}
			else if (@string == "U")
			{
				string msg2 = Encoding.UTF8.GetString(_mergedBuffer, 7, messageLen - 7);
				_stringCallbackSet.delayedEmitEvent(string2, ref msg2);
			}
			else if (@string == "B")
			{
				byte[] msg3 = new byte[messageLen - 7];
				Buffer.BlockCopy(_mergedBuffer, 7, msg3, 0, messageLen - 7);
				_bufferCallbackSet.delayedEmitEvent(string2, ref msg3);
			}
		}

		public override void Destroy()
		{
			try
			{
				_client.DisconnectAsync(new SocketAsyncEventArgs());
			}
			catch (Exception)
			{
			}
			_jsonCallbackSet.cbs.Clear();
			_bufferCallbackSet.cbs.Clear();
			_stringCallbackSet.cbs.Clear();
		}
	}
}
