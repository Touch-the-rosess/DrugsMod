using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using Assets.Scripts.DrugsMod;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000005 RID: 5
public class AuthManager : MonoBehaviour
{
    // Token: 0x0600009D RID: 157 RVA: 0x0000BA74 File Offset: 0x00009C74
    private void Start()
    {
        this.connectionManager = base.gameObject.GetComponent<ConnectionManager>();
        this.obvyazka = base.gameObject.GetComponent<Obvyazka>();
        this.connectionManager.onConnect.AddListener(new UnityAction(this.OnConnected));
        this.obvyazka.OnU("AE", new TypedCallback<string>(this.OnAE), true);
        this.obvyazka.OnU("AU", new TypedCallback<string>(this.OnAU), false);
        this.ListenAH();
        this.vkPanel.gameObject.SetActive(false);
    }

    private void OnNV(ref string msg)
    {
        if (int.Parse(msg) > 28)
        {
            ConnectionManager.phrase = "\n\nЭТА ВЕРСИЯ НЕ ПОДДЕРЖИВАЕТСЯ.\nСКАЧАЙТЕ НОВЫЙ КЛИЕНТ ИЛИ ОБНОВИТЕ СТРАНИЦУ";
            this.connectionManager.dontReconnect = true;
            this.connectionManager.ForceDisconnect();
        }
    }

    private void OnConnected()
    {
    }

    // Token: 0x0600009E RID: 158 RVA: 0x0000BB10 File Offset: 0x00009D10
    private void ListenAH()
	{
		this.obvyazka.OnU("AH", new TypedCallback<string>(this.OnAH), true);
	}

    private void OnAE(ref string msg)
    {
        ConnectionManager.THIS.connectionText.text = "ПОДКЛЮЧЕНИЕ ОТМЕНЕНО:\n\n" + msg;
    }
    // Token: 0x060000A0 RID: 160 RVA: 0x0000BB2F File Offset: 0x00009D2F

    private void OnAU(ref string msg)
    {
        this._uniq = msg;
        DMGlobalVariables.currentLoggedRobot.uniq = this._uniq;
        this._haveUniq = true;
        if (AuthManager.debugGid != "")
        {
            this.obvyazka.SendU("AU", this._uniq + "_DEBUG_" + AuthManager.debugGid);
            return;
        }
        if ((ConnectionManager.METHOD == "SITE" || WorldInitScript.inited) 
              && DMGlobalVariables.currentLoggedRobot.id != null // PlayerPrefs.HasKey("user_id") 
              && DMGlobalVariables.currentLoggedRobot.hash != null //PlayerPrefs.HasKey("user_hash")
              )
        {
            this._userId = DMGlobalVariables.currentLoggedRobot.id;//PlayerPrefs.GetString("user_id");
            this._userHash = DMGlobalVariables.currentLoggedRobot.hash;//PlayerPrefs.GetString("user_hash");
            this.obvyazka.SendU("AU", string.Concat(new string[]
            {
                this._uniq,
                "_",
                this._userId,
                "_",
                this.CalculateMD5Hash(this._userHash + this._uniq)
            }));
            return;
        }
        if (this.obvyazka.vk_access_token != "")
        {
            this.obvyazka.SendU("AU", this._uniq + "_VK_" + this.obvyazka.vk_access_token);
            this.obvyazka.OnU("AV", new TypedCallback<string>(this.ShowVKButton), true);
            return;
        }
        this.obvyazka.SendU("AU", this._uniq + "_NO_AUTH");
        string text = "";
        this.ShowVKButton(ref text);
    }

    private void ShowVKButton(ref string msg)
    {
        EventTrigger eventTrigger = this.vkButton.gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerDown
        };
        entry.callback.AddListener(new UnityAction<BaseEventData>(this.VKShow));
        eventTrigger.triggers.Add(entry);
        this.vkPanel.gameObject.SetActive(true);
    }

    private void VKShow(BaseEventData e)
    {
        this.OnVKButtonClick();
    }

    private void OnVKButtonClick()
    {
        if (this._haveUniq)
        {
            Application.OpenURL("https://oauth.vk.com/authorize?client_id=6622327&display=page&redirect_uri=http://mines35.myachin.com/vk_" + this._uniq + "&scope=email&response_type=code&v=5.80");
        }
    }

    // Token: 0x060000A1 RID: 161 RVA: 0x0000BB34 File Offset: 0x00009D34
    private void OnAH(ref string msg)
	{
		if (msg == "BAD")
		{
      if(DMGlobalVariables.currentLoggedRobot != null){
        DMRegistryFunctionality.RemoveRobot(DMGlobalVariables.currentLoggedRobot.name);
      }
			//PlayerPrefs.DeleteKey("user_id");
			//PlayerPrefs.DeleteKey("user_hash");
			//this.vkPanel.gameObject.SetActive(true);
			base.Invoke("ListenAH", 0.01f);
			return;
		}
		string[] array = msg.Split(new char[]
		{
			'_'
		});
		this._userId = array[0];
		this._userHash = array[1];
    DMGlobalVariables.currentLoggedRobot.id = this._userId;
    DMGlobalVariables.currentLoggedRobot.hash = this._userHash;

		//PlayerPrefs.SetString("user_id", this._userId);
		//PlayerPrefs.SetString("user_hash", this._userHash);
		this.obvyazka.SendU("AU", string.Concat(new string[]
		{
			this._uniq,
			"_",
			this._userId,
			"_",
			this.CalculateMD5Hash(this._userHash + this._uniq)
		}));
		this.vkPanel.gameObject.SetActive(false);
	}

    private void Update()
    {
    }
    // Token: 0x060000A2 RID: 162 RVA: 0x0000BC34 File Offset: 0x00009E34

    private string CalculateMD5Hash(string input)
    {
        HashAlgorithm hashAlgorithm = MD5.Create();
        byte[] bytes = Encoding.ASCII.GetBytes(input);
        byte[] array = hashAlgorithm.ComputeHash(bytes);
        StringBuilder stringBuilder = new StringBuilder();
        for (int i = 0; i < array.Length; i++)
        {
            stringBuilder.Append(array[i].ToString("x2"));
        }
        return stringBuilder.ToString();
    }

    // Token: 0x060000B4 RID: 180 RVA: 0x0000C194 File Offset: 0x0000A394
	

	// Token: 0x060000B5 RID: 181 RVA: 0x0000C1EC File Offset: 0x0000A3EC
	

	// Token: 0x04000008 RID: 8
	public GameObject vkPanel;

	// Token: 0x04000009 RID: 9
	public Button vkButton;

	// Token: 0x0400000A RID: 10
	public static string debugGid = "";

	// Token: 0x0400000B RID: 11
	private ConnectionManager connectionManager;

	// Token: 0x0400000C RID: 12
	private Obvyazka obvyazka;

	// Token: 0x0400000D RID: 13
	private bool _haveUniq;

	// Token: 0x0400000E RID: 14
	private string _uniq;

	// Token: 0x0400000F RID: 15
	private string _userHash;

	// Token: 0x04000010 RID: 16
	private string _userId;
}

