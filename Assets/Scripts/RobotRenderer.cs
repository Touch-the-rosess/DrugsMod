using System;
using System.Collections.Generic;
using Assets.Scripts.DrugsMod;
using UnityEngine;
using UnityEngine.UI;

public class RobotRenderer : MonoBehaviour
{
    private void Start()
    {
        RobotRenderer.THIS = this;
        this.terrainRenderer = base.gameObject.GetComponent<TerrainRendererScript>();
        this.clientController = this.clientControllerObject.GetComponent<ClientController>();
    }

    public void Init()
    {
        if (!RobotRenderer.inited)
        {
            RobotRenderer.inited = true;
        }
    }

    public void XYBot(int id, int x, int y, int dir, int cid, int skin, int tail)
    {
        if (this.bots.ContainsKey(id))
        {
            if (id != this.clientController.myBotId || tail == 1)
            {
                this.bots[id].GetComponent<RobotScript>().SetXY((float)x, (float)y);
                this.bots[id].GetComponent<RobotScript>().SetRotation(dir);
                if (id == this.clientController.myBotId)
                {
                    this.clientController.dir = dir;
                    ClientController.StaticDirection = dir;
                }
            }
            else
            {
                this.clientController.myBotLastSyncX = x;
                this.clientController.myBotLastSyncY = y;
            }
        }
        else
        {
            this.AddNewBot(x, y, id);
            this.bots[id].GetComponent<RobotScript>().SetRotation(dir);
        }
        this.bots[id].GetComponent<RobotScript>().lastPingTime = Time.unscaledTime;
        this.bots[id].GetComponent<RobotScript>().SetSkin(skin);
        this.bots[id].GetComponent<RobotScript>().SetClan(cid);
        this.bots[id].GetComponent<RobotScript>().deathPingTime = -1f;
        this.tails[id] = tail;
    }

    public void RemoveBotFromBlock(int id, int block)
    {
        if (this.bots.ContainsKey(id) && id != this.clientController.myBotId)
        {
            RobotScript component = this.bots[id].GetComponent<RobotScript>();
            if ((component.gx >> 5) + (component.gy >> 5) * MapModel._blocksW == block)
            {
                component.deathPingTime = Time.unscaledTime;
            }
        }
    }

    public void RemoveAll()
    {
        foreach (KeyValuePair<int, GameObject> keyValuePair in this.bots)
        {
            UnityEngine.Object.Destroy(keyValuePair.Value);
        }
        this.bots.Clear();
    }

    public void RemoveBot(int id)
    {
        if (this.bots.ContainsKey(id) && id != this.clientController.myBotId)
        {
            UnityEngine.Object.Destroy(this.bots[id]);
            this.bots.Remove(id);
            UnityEngine.Object.Destroy(this.nickTFs[id].gameObject);
            this.tails.Remove(id);
            this.nickTFs.Remove(id);
            if (this.nicks.ContainsKey(id))
            {
                this.nicks.Remove(id);
            }
        }
    }

    public RobotScript AddNewBot(int x, int y, int id)
    {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.robotPrefab);
        gameObject.transform.SetParent(this.RenderWrapper.transform, false);
        RobotScript component = gameObject.GetComponent<RobotScript>();
        component.SetXY((float)x, (float)y);
        component.SyncXY();
        component.id = id;
        if (this.bots.ContainsKey(id))
        {
            this.RemoveBot(id);
            this.bots.Add(id, gameObject);
        }
        this.bots.Add(id, gameObject);
        Text text = UnityEngine.Object.Instantiate<Text>(this.nickPrefab);
        text.text = "";
        text.transform.SetParent(this.canvas.transform, false);
        if (this.nickTFs.ContainsKey(id))
        {
            UnityEngine.Object.Destroy(this.nickTFs[id].gameObject);
            this.nickTFs.Remove(id);
        }
        this.nickTFs[id] = text;
        this.tails[id] = 0;
        return component;
    }

    public void RemoveAllBots()
    {
        List<int> list = new List<int>();
        foreach (KeyValuePair<int, GameObject> keyValuePair in this.bots)
        {
            list.Add(keyValuePair.Key);
        }
        foreach (int id in list)
        {
            this.RemoveBot(id);
        }
    }

    public void AddNick(int id, string nick)
    {
        this.nicks[id] = nick;
    }

    public void AddNewBotForMe(int x, int y, int id, string name, out RobotScript rs)
    {
        this.nicks[id] = name;
        rs = this.AddNewBot(x, y, id);
    }

    public void CheckAliveBots(int num, int[] bids)
    {
        if (num != 0)
        {
            HashSet<int> hashSet = new HashSet<int>();
            foreach (KeyValuePair<int, GameObject> keyValuePair in this.bots)
            {
                hashSet.Add(keyValuePair.Key);
            }
            for (int i = 0; i < num; i++)
            {
                hashSet.Remove(bids[i]);
            }
            foreach (int id in hashSet)
            {
                this.RemoveBot(id);
            }
        }
    }

    public void RobotsGarbageCollector()
    {
        List<int> list = new List<int>();
        foreach (KeyValuePair<int, GameObject> keyValuePair in this.bots)
        {
            float deathPingTime = keyValuePair.Value.GetComponent<RobotScript>().deathPingTime;
            if ((Vector2.Distance(keyValuePair.Value.transform.position, new Vector2((float)this.clientController.view_x, (float)(-(float)this.clientController.view_y))) > 205f || keyValuePair.Value.GetComponent<RobotScript>().lastPingTime < Time.unscaledTime - 6f || (deathPingTime > 0f && deathPingTime < Time.unscaledTime - 0.5f)) && keyValuePair.Key != this.clientController.myBotId)
            {
                list.Add(keyValuePair.Key);
            }
        }
        foreach (int id in list)
        {
            this.RemoveBot(id);
        }
    }

    private void Update()
    {
        if (!RobotRenderer.inited)
        {
            return;
        }
        if (Time.unscaledTime > this.lastRobotGCTime + 4f)
        {
            this.RobotsGarbageCollector();
            this.lastRobotGCTime = Time.unscaledTime;
        }
        foreach (KeyValuePair<int, GameObject> keyValuePair in this.bots)
        {
            Vector3 a = Camera.main.WorldToScreenPoint(keyValuePair.Value.transform.position);
            this.nickTFs[keyValuePair.Key].transform.position = a + new Vector3(89f, 2f, 0f);
            if (this.nicks.ContainsKey(keyValuePair.Key))
            {
                this.nickTFs[keyValuePair.Key].text = this.nicks[keyValuePair.Key];
                if (!ClientConfig.SHOW_MY_NICK && keyValuePair.Key == ClientController.THIS.myBotId)
                {
                    this.nickTFs[keyValuePair.Key].text = "";
                }
                if (this.tails.ContainsKey(keyValuePair.Key))
                {
                    if (this.tails[keyValuePair.Key] == 1)
                    {
                        this.nickTFs[keyValuePair.Key].color = Color.yellow;
                    }
                    else if (this.tails[keyValuePair.Key] == 2)
                    {
                        this.nickTFs[keyValuePair.Key].color = Color.green;
                    }
                    else
                    {
                        this.nickTFs[keyValuePair.Key].color = Color.white;
                    }
                }
            }
            else
            {
                this.bidsToKnow.Add(keyValuePair.Key);
            }
        }
        if (this.clientController.myBotId != -1)
        {
            if (Time.unscaledTime > this.lastMyRobotNamingTime + 2f)
            {
                
                if(DMGlobalVariables.IsSigningInNewRobot){
                  UnityEngine.Debug.Log("RobotRenderer.Update() is this function running constantly?");
                  DMGlobalVariables.currentLoggedRobot.name = this.nicks[this.clientController.myBotId];
                  DMRegistryFunctionality.AddRobot(DMGlobalVariables.currentLoggedRobot.name,
                                                   DMGlobalVariables.currentLoggedRobot.hwid,
                                                   DMGlobalVariables.currentLoggedRobot.uniq,
                                                   DMGlobalVariables.currentLoggedRobot.hash,
                                                   DMGlobalVariables.currentLoggedRobot.id,
                                                   DMGlobalVariables.currentLoggedRobot.isLoggedIn);
                  DMGlobalVariables.IsSigningInNewRobot = false;
                }
                this.nickTF.text = this.nicks[this.clientController.myBotId];
                this.lastMyRobotNamingTime = Time.unscaledTime;
            }
            GUIManager.THIS.SetCoord(this.clientController.myBot.gx, this.clientController.myBot.gy);
        }
        if (Time.unscaledTime > this.lastRobotNamingTime + 1f)
        {
            string text = "";
            foreach (int num in this.bidsToKnow)
            {
                if (text != "")
                {
                    text += ",";
                }
                text += num;
            }
            this.bidsToKnow.Clear();
            if (text != "")
            {
                ServerTime.THIS.SendTypicalMessage(ClientController.THIS.TimeOfMove(), "Whoi", 0, 0, text);
            }
            this.lastRobotNamingTime = Time.unscaledTime;
        }
    }

    
	public GameObject robotPrefab;

	public GameObject clientControllerObject;

	private ClientController clientController;

	public GameObject canvas;

	public Text nickPrefab;

	public Text nickTF;

	private Dictionary<int, int> tails = new Dictionary<int, int>();

	private Dictionary<int, string> nicks = new Dictionary<int, string>();

	private Dictionary<int, Text> nickTFs = new Dictionary<int, Text>();

	public GameObject RenderWrapper;

	public Dictionary<int, GameObject> bots = new Dictionary<int, GameObject>();

	private TerrainRendererScript terrainRenderer;

	public static RobotRenderer THIS;

	public static bool inited;

	public bool isProgrammator;

	private float lastRobotGCTime;

	private float lastRobotNamingTime;

	private float lastMyRobotNamingTime;

	public HashSet<int> bidsToKnow = new HashSet<int>();
}

