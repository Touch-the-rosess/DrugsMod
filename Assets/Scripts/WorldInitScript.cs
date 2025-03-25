using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.DrugsMod;

public class WorldInitScript : MonoBehaviour
{
	private Obvyazka obvyazka;

	public GameObject mainRenderer;

	public Text FPSText;

	public Image pad;

	public Canvas canvas;

	public GameObject UpdateWindow;

	public Text UpdateText;

	public Button UpdateButton;

	public static WorldInitScript THIS;

	public float lastSavingMapTime;

	public float lastLoadingMapTime;

	public static bool ignoreConfig;

	public static bool inited;

	private static bool isExceptionHandlingSetup;

	private int VERSION = 3403;

	private bool updatingStarted;

	private WebClient wb = new WebClient();

	private int downloadedPercent;

	private bool updateProgress;

	private bool launchUpdater;

	private Mutex UpdatingMutex = new Mutex();

	private string downloadedFilename;

	private static bool saving;

	private static bool saved;

	private void Start()
	{
		THIS = this;
		pad.gameObject.SetActive(value: true);
		obvyazka = base.gameObject.GetComponent<Obvyazka>();
		obvyazka.OnU("cf", this.OnWorldConfig);
		string[] commandLineArgs = Environment.GetCommandLineArgs();
		for (int i = 0; i < commandLineArgs.Length; i++)
		{
			if (commandLineArgs[i] == "-ignoreConfig")
			{
				ignoreConfig = true;
			}
		}
	}

	public static void SetupExceptionHandling()
	{
		if (!isExceptionHandlingSetup)
		{
			isExceptionHandlingSetup = true;
			Application.logMessageReceived += HandleException;
		}
	}

	private static void HandleException(string condition, string stackTrace, LogType type)
	{
		if (type == LogType.Exception)
		{
			UnityEngine.Debug.Log(condition + "\n" + stackTrace);
		}
	}

	private void wb_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
	{
		UpdatingMutex.WaitOne();
		downloadedPercent = e.ProgressPercentage;
		updateProgress = true;
		UpdatingMutex.ReleaseMutex();
	}

	private void wb_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
	{
		UpdatingMutex.WaitOne();
		launchUpdater = true;
		UpdatingMutex.ReleaseMutex();
	}

	private void OnWorldConfig(ref string msg)
	{
		WorldSize w = JsonUtility.FromJson<WorldSize>(msg);
		if (w.name == null)
		{
			w.name = "barsoom";
		}
		UnityEngine.Debug.Log(w.width + "x" + w.height + " - in " + w.name);
		ConnectionManager.THIS.connectionText.text = "";
    UnityEngine.Debug.Log(VERSION);
		if (!inited)
		{
      UnityEngine.Debug.Log("WorldInitScript.OnWorldConfig() started the initiation.");
			//string text = obvyazka.vk_access_token2;
			//if (text == "")
			//{
			//	text = SystemInfo.deviceUniqueIdentifier;
			//}
			ServerTime.THIS.SendTypicalMessage(-1, "Rndm", 0, 0, "hash=" + DMGlobalVariables.currentLoggedRobot.hwid);
			if (!ConnectionManager.THIS.DEBUG)
			{
				SetupExceptionHandling();
			}
			SoundManager.THIS.PlayMusic();
			CellModel.Init();
			BzScript.Init();
			SkillButtonScript.InitColors();
			ClanSpriteScript.Init();
			inited = true;
			MapModel map = new MapModel(w.width, w.height, w.name);
			InventoryItem.InitSprites();
			ProgAction.InitSprites();
			PackSpriteScript.InitSprites();
			ObjectMapModel objects = new ObjectMapModel(w.width, w.height);
			mainRenderer.GetComponent<TerrainRendererScript>().SetMaps(map, objects);
			ClientController.map = map;
			FPSText.gameObject.SetActive(value: true);
			obvyazka.GetComponent<ServerController>().Init();
			ServerTime.THIS.SendTypicalMessage(-1, "Miss", 0, 0, "0");
			ServerTime.THIS.SendTypicalMessage(-1, "Chin", 0, 0, "_");
		}
		else
		{
			PopupManager.THIS.CloseWindow();
			mainRenderer.GetComponent<RobotRenderer>().RemoveAll();
			GUIManager.THIS.CloseInventoryItem();
			ServerTime.THIS.SendTypicalMessage(-1, "Miss", 0, 0, "1");
			ServerTime.THIS.SendTypicalMessage(-1, "Chin", 0, 0, "1:" + ChatManager.THIS.getCurrentChat() + ":" + ChatManager.THIS.getLasts());
		}
		pad.gameObject.SetActive(value: false);
	}

	private void Update()
	{
		if (updatingStarted)
		{
			UpdatingMutex.WaitOne();
			if (updateProgress)
			{
				updateProgress = false;
				UpdateText.text = "СКАЧИВАЕМ ОБНОВЛЕНИЕ - " + downloadedPercent + "%";
			}
			if (launchUpdater)
			{
				launchUpdater = false;
				UpdateText.text = "ОБНОВЛЯЕМ!                      ";
				wb.Dispose();
				saved = true;
				saving = true;
				Process.Start(downloadedFilename, "/SP- /SILENT /NOICONS");
				UpdatingMutex.ReleaseMutex();
				Application.Quit();
				return;
			}
			UpdatingMutex.ReleaseMutex();
		}
		if (saving || updatingStarted)
		{
			return;
		}
		if (Time.unscaledTime > lastLoadingMapTime + 1f)
		{
			if (ClientController.map != null)
			{
				ClientController.map.LoadBlocks();
			}
			lastLoadingMapTime = Time.unscaledTime;
		}
		if (Time.unscaledTime > lastSavingMapTime + 60f)
		{
			if (ClientController.map != null)
			{
				ClientController.map.SaveMapV2();
			}
			lastSavingMapTime = Time.unscaledTime;
		}
	}

	private void SaveOnQuit()
	{
		if (ClientController.map != null)
		{
      if(inited)
			  ClientController.map.SaveMapV2();
			ClientController.map.Destroy();
		}
		THIS.Invoke("QuitAfterSaving", 0.5f);
	}

	private static bool WantsToQuit()
	{
    DMGlobalVariables.currentLoggedRobot.isLoggedIn = false;
    DMRegistryFunctionality.OverrideRobot(
      DMGlobalVariables.currentLoggedRobot.name,
      DMGlobalVariables.currentLoggedRobot.hwid,
      DMGlobalVariables.currentLoggedRobot.uniq,
      DMGlobalVariables.currentLoggedRobot.hash,
      DMGlobalVariables.currentLoggedRobot.id,
      DMGlobalVariables.currentLoggedRobot.isLoggedIn
    );
		if (!saved && inited)
		{
			string text = "СОХРАНЕНИЕ КАРТЫ.\nДОЖДИТЕСЬ ЗАВЕРШЕНИЯ ПРИЛОЖЕНИЯ";
			ConnectionManager.THIS.connectionText.text = text;
			THIS.Invoke("SaveOnQuit", 0.2f);
			saving = true;
			return false;
		}
		if (saving && !saved)
		{
			return false;
		}
    THIS.Invoke("QuitAfterSaving", 0.5f);
		return true;
	}

	[RuntimeInitializeOnLoadMethod]
	private static void RunOnStart()
	{
		Application.wantsToQuit += WantsToQuit;
	}

	private void QuitAfterSaving()
	{
		saved = true;
		Application.Quit();
	}
}
