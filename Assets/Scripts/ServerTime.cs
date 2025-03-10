using System;
using System.Text;
using UnityEngine;

public class ServerTime : MonoBehaviour
{
    private void Start()
    {
        this.obvyazka = base.gameObject.GetComponent<Obvyazka>();
        this.obvyazka.OnU("PI", new TypedCallback<string>(this.OnPI), false);
        ServerTime.THIS = this;
    }

    public int NowTime()
    {
        return this.clientTimeStart + (int)(Time.unscaledTime * 1000f) - this.clientTimeLocal;
    }

    public bool SendTypicalMessage(int time, string type, int x, int y, string str)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(str);
        return this.SendTypicalMessage(time, type, x, y, bytes);
    }

    public bool SendTypicalMessage(int time, string type, int x, int y, byte[] buffer)
    {
        if (time == -1)
        {
            time = this.lastSendedTime;
        }
        if (!this.ready)
        {
            throw new Exception("SendTypicalMessage - PIP is not ready &!");
        }
        if (type.Length != 4)
        {
            throw new Exception("SendTypicalMessage - bad type styring not 4 chars");
        }
        if (time < this.lastSendedTime)
        {
            time = this.lastSendedTime;
        }
        this.lastSendedTime = time;
        int num = 0;
        if (buffer != null)
        {
            num = buffer.Length;
        }
        byte[] array = new byte[16 + num];
        Array bytes = Encoding.UTF8.GetBytes(type);
        byte[] bytes2 = BitConverter.GetBytes((uint)time);
        byte[] bytes3 = BitConverter.GetBytes((uint)x);
        byte[] bytes4 = BitConverter.GetBytes((uint)y);
        Buffer.BlockCopy(bytes, 0, array, 0, 4);
        Buffer.BlockCopy(bytes2, 0, array, 4, 4);
        Buffer.BlockCopy(bytes3, 0, array, 8, 4);
        Buffer.BlockCopy(bytes4, 0, array, 12, 4);
        if (buffer == null)
        {
            this.obvyazka.SendB("TY", array);
        }
        else
        {
            Buffer.BlockCopy(buffer, 0, array, 16, num);
            this.obvyazka.SendB("TY", array);
        }
        return true;
    }

    private void OnPI(ref string msg)
    {
        string[] array = msg.Split(new char[]
        {
            ':'
        });
        if (this.clientTimeStart == -1)
        {
            this.clientTimeStart = int.Parse(array[1]);
            this.clientTimeLocal = (int)(Time.unscaledTime * 1000f);
            this.lastSendedTime = this.clientTimeStart;
            ClientController.serverTimeOfLastFrame = this.clientTimeStart;
            ClientController.clientTimeOfLastFrame = (int)(Time.unscaledTime * 1000f);
            this.ready = true;
        }
        this.lastPITime = int.Parse(array[1]);
        this.pingStr = array[2];
        ClientController.pongResponse = int.Parse(array[0]);
    }

    private void Update()
    {
        if (this.lastPITime < this.NowTime() - 40500)
        {
            FPSCountScript.PING_MESSAGE = " OFFLINE";
        }
        else if (this.lastPITime < this.NowTime() - 1500)
        {
            FPSCountScript.PING_MESSAGE = " FREEZE " + ((float)(this.NowTime() - this.lastPITime) / 1000f).ToString("0.0") + " sec";
        }
        else
        {
            FPSCountScript.PING_MESSAGE = " PING " + this.pingStr + "ms";
        }
        if (this.clientTimeStart != -1)
        {
            double num = (double)(this.NowTime() - this.lastPITime);
            return;
        }
    }

    private Obvyazka obvyazka;

	public int clientTimeStart = -1;

	private int clientTimeLocal = -1;

	public static ServerTime THIS;

	public int lastSendedTime;

	public int lastPITime;

	private string pingStr;

	public bool ready;
}
