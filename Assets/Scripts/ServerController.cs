using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
public class ServerController : MonoBehaviour
{
    private void Start()
    {
      
    }

    public void Init()
    {
        
        ServerController.THIS = this;
        this.obvyazka = base.gameObject.GetComponent<Obvyazka>();
        this.robotRenderer = this.mainRenderer.GetComponent<RobotRenderer>();
        this.terrainRenderer = this.mainRenderer.GetComponent<TerrainRendererScript>();
        this.clientController = this.clientControllerObject.GetComponent<ClientController>();
        this._typeBuffer = new byte[1];
        this.obvyazka.OnB(this.str345, new TypedCallback<byte[]>(this.HubTranslator), false);
        this.obvyazka.OnU(this.str300, new TypedCallback<string>(this.BotInfoHandler), false);
        this.obvyazka.OnU(this.str301, new TypedCallback<string>(this.TPHandler), false);
        this.obvyazka.OnU(this.str302, new TypedCallback<string>(this.SmoothTPHandler), false);
        this.obvyazka.OnU(this.str303, new TypedCallback<string>(this.SpeedHandler), false);
        this.obvyazka.OnU(this.str304, new TypedCallback<string>(this.LiveHandler), false);
        this.obvyazka.OnU(this.str305, new TypedCallback<string>(this.SkillHandler), false);
        this.obvyazka.OnU(this.str306, new TypedCallback<string>(this.BasketHandler), false);
        this.obvyazka.OnU(this.str307, new TypedCallback<string>(this.NickListHandler), false);
        this.obvyazka.OnU(this.str308, new TypedCallback<string>(this.OnlineHandler), false);
        this.obvyazka.OnU(this.str309, new TypedCallback<string>(this.LevelHandler), false);
        this.obvyazka.OnU(this.str310, new TypedCallback<string>(this.PopupHandler), false);
        this.obvyazka.OnU(this.str311, new TypedCallback<string>(this.PopupCloseHandler), false);
        this.obvyazka.OnU(this.str312, new TypedCallback<string>(this.OpenURLHandler), false);
        this.obvyazka.OnU(this.str313, new TypedCallback<string>(this.ClanShowHandler), false);
        this.obvyazka.OnU(this.str314, new TypedCallback<string>(this.ClanHideHandler), false);
        this.obvyazka.OnU(this.str315, new TypedCallback<string>(this.PurchaseHandler), false);
        this.obvyazka.OnU(this.str316, new TypedCallback<string>(this.MoneyHandler), false);
        this.obvyazka.OnU(this.str317, new TypedCallback<string>(this.ModulesHandler), false);
        this.obvyazka.OnU(this.str318, new TypedCallback<string>(this.ProgrammatorHandler), false);
        this.obvyazka.OnU(this.str319, new TypedCallback<string>(this.ProgrammatorOpenHandler), false);
        this.obvyazka.OnU(this.str320, new TypedCallback<string>(this.ProgrammatorUpdateHandler), false);
        this.obvyazka.OnU(this.str321, new TypedCallback<string>(this.OKHandler), false);
        this.obvyazka.OnU(this.str322, new TypedCallback<string>(this.InventoryHandler), false);
        this.obvyazka.OnU(this.str323, new TypedCallback<string>(this.BadCellsHandler), false);
        this.obvyazka.OnU(this.str324, new TypedCallback<string>(this.AgrHandler), false);
        this.obvyazka.OnU(this.str325, new TypedCallback<string>(this.AutoRemHandler), false);
        this.obvyazka.OnU(this.str326, new TypedCallback<string>(this.AutoDiggHandler), false);
        this.obvyazka.OnU(this.str327, new TypedCallback<string>(this.HandModeHandler), false);
        this.obvyazka.OnU(this.str328, new TypedCallback<string>(this.PanelHandler), false);
        this.obvyazka.OnU(this.str329, new TypedCallback<string>(this.GeoHandler), false);
        this.obvyazka.OnU(this.str330, new TypedCallback<string>(this.SuHandler), false);
        this.obvyazka.OnU(this.str331, new TypedCallback<string>(ChatManager.THIS.mnHandler), false);
        this.obvyazka.OnU(this.str332, new TypedCallback<string>(ChatManager.THIS.mlHandler), false);
        this.obvyazka.OnU(this.str333, new TypedCallback<string>(ChatManager.THIS.moHandler), false);
        this.obvyazka.OnU(this.str334, new TypedCallback<string>(ChatManager.THIS.muHandler), false);
        this.obvyazka.OnU(this.str335, new TypedCallback<string>(ChatManager.THIS.mcHandler), false);
        this.obvyazka.OnU(this.str336, new TypedCallback<string>(this.UMPHandler), false);
        this.obvyazka.OnU(this.str337, new TypedCallback<string>(this.MNHandler), false);
        this.obvyazka.OnU(this.str338, new TypedCallback<string>(this.MPHandler), false);
        this.obvyazka.OnU(this.str339, new TypedCallback<string>(this.SettingsHandler), false);
        this.obvyazka.OnU(this.str340, new TypedCallback<string>(this.ClientConfigHandler), false);
        this.obvyazka.OnU(this.str341, new TypedCallback<string>(this.BibikaHandler), false);
        this.obvyazka.OnU(this.str342, new TypedCallback<string>(this.respHandler), false);
        this.obvyazka.OnU(this.str343, new TypedCallback<string>(this.GoHandler), false);
        this.obvyazka.OnU(this.str344, new TypedCallback<string>(this.DailyRewardNotificationHandler), false);
    }


    private void DailyRewardNotificationHandler(ref string msg)
    {
        GUIManager.THIS.DailyRewardToggle(msg == "1");
    }

    private void GoHandler(ref string msg)
    {
        string[] array = msg.Split(new char[]
        {
            ':'
        });
        TutorialNavigation.THIS.SetNaviArrow(int.Parse(array[0]), int.Parse(array[1]));
    }

    private void respHandler(ref string msg)
    {
        ClientController.THIS.stopAutoMove();
    }

    private void BibikaHandler(ref string msg)
    {
        if (ClientConfig.SOUND_SIGNAL)
        {
            SoundManager.THIS.PlayBibika();
        }
    }

    private void ClientConfigHandler(ref string msg)
    {
        if (WorldInitScript.ignoreConfig)
        {
            return;
        }
        ClientConfig.SetDefaults();
        msg = msg.Replace('\t', ' ');
        msg = msg.Replace("    ", " ");
        msg = msg.Replace("   ", " ");
        msg = msg.Replace("  ", " ");
        msg = msg.Replace('\r', '\n');
        msg = msg.Replace(':', ';');
        List<string> list = new List<string>();
        string[] array = msg.Split(new char[]
        {
            '\n'
        });
        for (int i = 0; i < array.Length; i++)
        {
            int num = array[i].IndexOf("//");
            if (num >= 0)
            {
                array[i] = array[i].Substring(0, num);
            }
            string[] array2 = array[i].Split(new char[]
            {
                ';'
            });
            for (int j = 0; j < array2.Length; j++)
            {
                array2[j] = array2[j].Trim();
                array2[j] = array2[j].Replace("  ", " ");
                list.Add(array2[j]);
            }
        }
        foreach (string text in list)
        {
            if (text.Length != 0)
            {
                if (text.EndsWith("+") || text.EndsWith("-"))
                {
                    ClientConfig.ParseBoolean(text.Substring(0, text.Length - 1), text.EndsWith("+"));
                }
                else if (text.Contains("="))
                {
                    string[] array3 = text.Replace(" ", "").Split(new char[]
                    {
                        '='
                    });
                    ClientConfig.ParseEquals(array3[0], array3[1]);
                }
                else
                {
                    ClientConfig.ParseOperator(text.Replace(" ", ""));
                }
            }
        }
        ClientConfig.EndConfig();
    }

    private void SettingsHandler(ref string msg)
    {
        string[] array = msg.Split(new char[]
        {
            '#'
        });
        for (int i = 1; i < array.Length; i += 2)
        {
            string a = array[i];
            int num = 0;
            string text = array[i + 1];
            try
            {
                num = int.Parse(array[i + 1]);
            }
            catch (Exception)
            {
            }
            if (a == "snd")
            {
                if (num == 1)
                {
                    SoundManager.SoundOn = true;
                }
                else
                {
                    SoundManager.SoundOn = false;
                }
                GUIManager.THIS.SetSound();
            }
            if (a == "mus")
            {
                if (num == 1)
                {
                    SoundManager.MusicOn = true;
                }
                else
                {
                    SoundManager.MusicOn = false;
                }
                GUIManager.THIS.SetMusic();
            }
            if (a == "mof")
            {
                if (num == 0)
                {
                    ClientController.ownSounds = true;
                }
                else
                {
                    ClientController.ownSounds = false;
                }
            }
            if (a == "pot")
            {
                if (num == 0)
                {
                    this.terrainRenderer.ChangeQualityFor(0);
                }
                else
                {
                    this.terrainRenderer.ChangeQualityFor(2);
                }
            }
            if (a == "frc")
            {
                if (num == 0)
                {
                    TerrainRendererScript.alwaysUpdate = false;
                }
                else
                {
                    TerrainRendererScript.alwaysUpdate = true;
                }
            }
            if (a == "ctrl")
            {
                if (num == 0)
                {
                    ClientController.CtrlToggle = false;
                }
                else
                {
                    ClientController.CtrlToggle = true;
                }
            }
            if (a == "tsca")
            {
                if (num == 1)
                {
                    TerrainRendererScript.unitSize = 24f;
                    TerrainRendererScript.needUpdate = true;
                    this.terrainRenderer.RecreateMeshes();
                }
                else
                {
                    TerrainRendererScript.unitSize = 16f;
                    TerrainRendererScript.needUpdate = true;
                    this.terrainRenderer.RecreateMeshes();
                }
            }
            if (a == "isca")
            {
                if (num == 1)
                {
                    this.MainCanvasScaler.scaleFactor = 1.4f;
                }
                else
                {
                    this.MainCanvasScaler.scaleFactor = 1f;
                }
            }
            if (a == "mous")
            {
                if (num == 0)
                {
                    ClientController.MouseControl = false;
                }
                else
                {
                    ClientController.MouseControl = true;
                }
            }
        }
    }

    private void MPHandler(ref string msg)
    {
        string[] array = msg.Split(new char[]
        {
            ':'
        });
        int exp = (int)short.Parse(array[0]);
        int max = (int)short.Parse(array[1]);
        MissionPad.THIS.UpdateMissionProgress(exp, max);
    }

    private void MNHandler(ref string msg)
    {
        string[] array = msg.Split(new char[]
        {
            '#'
        });
        string text = array[0];
        int dx = (int)short.Parse(array[1]);
        int dy = (int)short.Parse(array[2]);
        int anchorType = (int)short.Parse(array[3]);
        string hideReason = array[4];
        TutorialNavigation.THIS.SetNavi(text, dx, dy, anchorType, hideReason);
    }

    public void OpenURLHandler(ref string msg)
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            Application.ExternalEval("window.open(\"" + msg + "\",\"_blank\")");
            return;
        }
        Application.OpenURL(msg);
    }

    private void PanelHandler(ref string msg)
    {
        string[] array = msg.Split(new char[]
        {
            '#'
        });
        string text = array[0];
        string text2 = array[1];
        string s = array[2];
        if (text2 == "")
        {
            if (text == "CLEAR")
            {
                GUIManager.THIS.statePanel.RemoveAll();
                return;
            }
            GUIManager.THIS.statePanel.RemoveLine(text);
            return;
        }
        else
        {
            int num = (int)short.Parse(s);
            string[] text3 = text2.Split(new char[]
            {
                '~'
            });
            if (num == 0)
            {
                GUIManager.THIS.statePanel.AddLine(text, text3, true, new Color(0.5f, 0.25f, 0.25f));
                return;
            }
            if (num == 1)
            {
                GUIManager.THIS.statePanel.AddLine(text, text3, false, new Color(0.15f, 0.4f, 0.15f));
                return;
            }
            if (num == 2)
            {
                GUIManager.THIS.statePanel.AddLine(text, text3, false, new Color(0.15f, 0.15f, 0.6f));
            }
            return;
        }
    }

    private void SuHandler(ref string msg)
    {
        GUIManager.THIS.banHammer.gameObject.SetActive(msg == "1");
    }

    private void AgrHandler(ref string msg)
    {
        GUIManager.THIS.agrShow.SetActive(msg == "1");
    }

    private void AutoDiggHandler(ref string msg)
    {
        ClientController.autoDigg = (msg == "1");
        ClientController.THIS.ShowAutoDigg();
    }

    private void ClanHideHandler(ref string msg)
    {
        GUIManager.THIS.HideClanIcon();
    }

    private void ClanShowHandler(ref string msg)
    {
        GUIManager.THIS.ShowClanIcon(int.Parse(msg));
    }

    private void BadCellsHandler(ref string msg)
    {
        string[] array = msg.Split(new char[]
        {
            ':'
        });
        for (int i = 0; i < array.Length; i += 3)
        {
            ClientController.THIS.AddFX(int.Parse(array[i]), int.Parse(array[i + 1]), 0);
        }
    }

    private void InventoryHandler(ref string msg)
    {
        string[] array = msg.Split(new char[]
        {
            ':'
        });
        string a = array[0];
        if (a == "show")
        {
            InventoryPanel.THIS.ShowInventory(array[3], int.Parse(array[2]), int.Parse(array[1]));
            return;
        }
        if (a == "full")
        {
            InventoryPanel.THIS.ShowFullGrid(array[2], int.Parse(array[1]));
            return;
        }
        if (!(a == "choose"))
        {
            if (a == "close")
            {
                GUIManager.THIS.CloseInventoryItem();
            }
            return;
        }
        string hint = array[1];
        int d = int.Parse(array[2]);
        int dx = int.Parse(array[3]);
        int dy = int.Parse(array[4]);
        int num = int.Parse(array[5]);
        int h = int.Parse(array[6]);
        string mapStr = array[7];
        GUIManager.THIS.ChooseInventoryItem(0, 0, hint);
        if (num > 0)
        {
            GUIManager.THIS.ShowInventoryGrid(d, dx, dy, num, h, mapStr);
            return;
        }
        OverlayRenderer.THIS.HideGrid();
    }

    private void SpeedHandler(ref string msg)
    {
        string[] array = msg.Split(new char[]
        {
            ':'
        });
        int num = int.Parse(array[0]);
        int num2 = int.Parse(array[1]);
        int num3 = int.Parse(array[2]);
        ClientController.THIS.XY_PAUSE = (float)num * 0.001f;
        ClientController.THIS.ROAD_PAUSE = (float)num2 * 0.001f;
        ClientController.THIS.DEPTH = (float)num3;
    }

    private void OKHandler(ref string msg)
    {
        int num = msg.IndexOf('#');
        if (num != -1)
        {
            OKMessage msg2 = default(OKMessage);
            msg2.title = msg.Substring(0, num);
            msg2.message = msg.Substring(num + 1);
            OKWindowManager.THIS.AddMessage(msg2);
        }
    }

    private void ProgrammatorOpenHandler(ref string msg)
    {
        ProgrammWrapper ProgrammWrapper = JsonUtility.FromJson<ProgrammWrapper>(msg);
        //Debug.Log(msg);
        GUIManager.THIS.OpenProgramm(ProgrammWrapper.id, ProgrammWrapper.title, ProgrammWrapper.source);
    }

    private void ProgrammatorUpdateHandler(ref string msg)
    {
        ProgrammWrapper ProgrammWrapper = JsonUtility.FromJson<ProgrammWrapper>(msg);
        GUIManager.THIS.UpdateProgramm(ProgrammWrapper.id, ProgrammWrapper.title, ProgrammWrapper.source);
    }

    private void ProgrammatorHandler(ref string msg)
    {
        
        if (msg == "1")
        {
            GUIManager.THIS.ChangeProgTo(true); //Debug.Log(msg);
            this.robotRenderer.isProgrammator = true;
            this.clientController.isProgrammator = true;
            ProgPanel.playing = true;
            this.clientController.stopAutoMove();
            return;
        }
        GUIManager.THIS.ChangeProgTo(false);
        this.robotRenderer.isProgrammator = false;
        this.clientController.isProgrammator = false;
        this.clientController.TimeSync();
        ProgPanel.playing = false;
    }

    private void PurchaseHandler(ref string msg)
    {
    }

    private void MoneyHandler(ref string msg)
    {
        MoneyWrapper MoneyWrapper = JsonUtility.FromJson<MoneyWrapper>(msg);
        GUIManager.THIS.SetMoney(MoneyWrapper.money, MoneyWrapper.creds);
    }

    private void LevelHandler(ref string msg)
    {
        UnityEngine.Debug.Log(msg);
        GUIManager.THIS.SetLevel(int.Parse(msg));
    }

    private void ModulesHandler(ref string msg)
    {
        JsonUtility.FromJson<ModsWrapper>(msg);
    }

    private void PopupHandler(ref string msg)
    {
        if (ConnectionManager.THIS.DEBUG)
        {
            this.unsafePopupHandler(ref msg);
            return;
        }
        try
        {
            this.unsafePopupHandler(ref msg);
        }
        catch (Exception)
        {
        }
    }

    private void unsafePopupHandler(ref string msg)
    {
        UnityEngine.Debug.Log("PopupHandler " + msg);
        string a = msg.Substring(0, msg.IndexOf(':'));
        if (a == "horb")
        {
            HORBConfig cfg = JsonUtility.FromJson<HORBConfig>(msg.Substring(msg.IndexOf(':') + 1));
            PopupManager.THIS.ShowHORB(cfg); //UnityEngine.Debug.Log("TEST!");
            return;

        }
        if (a == "up")
        {
            UPConfig UPConfig = default(UPConfig);
            SkillsWrapper SkillsWrapper = JsonUtility.FromJson<SkillsWrapper>(msg.Substring(msg.IndexOf(':') + 1));
            UPConfig.admin = SkillsWrapper.admin;
            UPConfig.tabs = SkillsWrapper.tabs;
            UPConfig.skills = new SkillConfig[SkillsWrapper.s];
            for (int i = 0; i < UPConfig.skills.Length; i++)
            {
                UPConfig.skills[i].type = -1;
            }
            string[] array = SkillsWrapper.k.Split(new char[]
            {
                '#'
            });
            for (int j = 0; j < array.Length - 1; j++)
            {
                if (!(array[j] == ""))
                {
                    string[] array2 = array[j].Split(new char[]
                    {
                        ':'
                    });
                    int num = int.Parse(array2[2]);
                    if (num < SkillsWrapper.s)
                    {
                        UPConfig.skills[num].type = SkillButtonScript.skillShorts[array2[0]];
                        UPConfig.skills[num].level = int.Parse(array2[1]);
                        UPConfig.skills[num].isUp = (array2[3] != "0");
                        UPConfig.skills[num].isLocked = (array2[3] == "2");
                    }
                    //for (int k = 0; k < array2.Length; k++)
                      //  UnityEngine.Debug.Log(array2[k]);

                }
            }
            UPConfig.title = SkillsWrapper.title;
            UPConfig.text = SkillsWrapper.txt;
            UPConfig.button = SkillsWrapper.b;
            UPConfig.buttonAction = SkillsWrapper.ba;
            
            if (SkillButtonScript.skillShorts.ContainsKey(SkillsWrapper.si))
            {
                UPConfig.skillIcon = SkillButtonScript.skillShorts[SkillsWrapper.si];
                //UnityEngine.Debug.Log("ПРОШЛО!");
            }
            else
            {
                UPConfig.skillIcon = -1;
                
            }
            UPConfig.toInstall = null;
            if (SkillsWrapper.i.Length > 0)
            {
                UPConfig.toInstall = SkillsWrapper.i.Split(new char[]
                {
                    ':'
                });
            }
            UPConfig.slot = SkillsWrapper.sl;
            UPConfig.canDelete = (SkillsWrapper.del > 0);
            UPConfig.lockState = ((SkillsWrapper.del == 2) ? 1 : 0);
            PopupManager.THIS.ShowUP(UPConfig);
            //return;
        }
    }

    private void PopupCloseHandler(ref string msg)
    {
        this.clientController.stopAutoMove();
        PopupManager.THIS.CloseWindow();
    }

    private void OnlineHandler(ref string msg)
    {
        string[] array = msg.Split(new char[]
        {
            ':'
        });
        GUIManager.THIS.SetOnline(array[0], array[1]);
    }

    private void BasketHandler(ref string msg)
    {
        if (ClientController.ownSounds && ClientConfig.SOUND_BASKET)
        {
            SoundManager.THIS.PlaySound(0, 1f);
        }
        string[] array = msg.Split(new char[]
        {
            ':'
        });
        GUIManager.THIS.SetBasket(long.Parse(array[0]), long.Parse(array[1]), long.Parse(array[2]), long.Parse(array[3]), long.Parse(array[4]), long.Parse(array[5]), long.Parse(array[6]));
    }

    private void NickListHandler(ref string msg)
    {
        string[] array = msg.Split(new char[]
        {
            ','
        });
        for (int i = 0; i < array.Length; i++)
        {
            string[] array2 = array[i].Split(new char[]
            {
                ':'
            });
            this.robotRenderer.AddNick(int.Parse(array2[0]), array2[1]);
        }
    }

    private void SkillHandler(ref string msg)
    {
        string[] array = msg.Split(new char[]
        {
            '#'
        });
        for (int i = 0; i < array.Length - 1; i++)
        {
            string[] array2 = array[i].Split(new char[]
            {
                ':'
            });
            string code = array2[0];
            int progress = int.Parse(array2[1]);
            MiniSkillManager.THIS.AddIcon(progress, code);
        }
    }

    private void GeoHandler(ref string msg)
    {
        GUIManager.THIS.GeoTF.text = " " + msg + " ";
        if (msg == " ")
        {
            GUIManager.THIS.GeoTF.gameObject.SetActive(false);
            return;
        }
        GUIManager.THIS.GeoTF.gameObject.SetActive(true);
    }

    private void LiveHandler(ref string msg)
    {
        string[] array = msg.Split(new char[]
        {
            ':'
        });
        this.clientController.Tremor();
        GUIManager.THIS.SetHP(int.Parse(array[0]), int.Parse(array[1]));
    }

    private void SmoothTPHandler(ref string msg)
    {
        string[] array = msg.Split(new char[]
        {
            ':'
        });
        this.clientController.SmoothTPMyBot(int.Parse(array[0]), int.Parse(array[1]));
    }

    private void TPHandler(ref string msg)
    {
        string[] array = msg.Split(new char[]
        {
            ':'
        });
        this.clientController.TPMyBot(int.Parse(array[0]), int.Parse(array[1]));
    }

    private void BotInfoHandler(ref string msg)
    {
        BotInfo BotInfo = JsonUtility.FromJson<BotInfo>(msg);
        this.robotRenderer.Init();
        this.clientController.InitMyBot(BotInfo.x, BotInfo.y, BotInfo.id, BotInfo.name);
    }

    private void HubTranslator(ref byte[] buffer)
    {
        int i = 0;
        int num = 0;
        while (i < buffer.Length)
        {
            num++;
            char c = Convert.ToChar(buffer[i]);
            i++;
            if (c <= 'O')
            {
                switch (c)
                {
                    case 'B':
                        {
                            int num2 = Convert.ToInt32(BitConverter.ToUInt16(buffer, i));
                            i += 2;
                            int[] array = new int[num2];
                            for (int j = 0; j < num2; j++)
                            {
                                array[j] = Convert.ToInt32(BitConverter.ToUInt16(buffer, i));
                                i += 2;
                            }
                            if (RobotRenderer.inited)
                            {
                                this.robotRenderer.CheckAliveBots(num2, array);
                                continue;
                            }
                            continue;
                        }
                    case 'C':
                        {
                            int num3 = Convert.ToInt32(BitConverter.ToUInt16(buffer, i));
                            int num4 = Convert.ToInt32(BitConverter.ToUInt16(buffer, i + 2));
                            int num5 = Convert.ToInt32(BitConverter.ToUInt16(buffer, i + 4));
                            int num6 = Convert.ToInt32(BitConverter.ToUInt16(buffer, i + 6));
                            i += 8;
                            string @string = Encoding.UTF8.GetString(buffer, i, num6);
                            i += num6;
                            LocalChatMessages.THIS.AddLocalMessage(num3, num4, num5, @string);
                            continue;
                        }
                    case 'D':
                        {
                            int fx = Convert.ToInt32(buffer[i]);
                            int dir = Convert.ToInt32(buffer[i + 1]);
                            int col = Convert.ToInt32(buffer[i + 2]);
                            int num4 = Convert.ToInt32(BitConverter.ToUInt16(buffer, i + 3));
                            int num5 = Convert.ToInt32(BitConverter.ToUInt16(buffer, i + 5));
                            int num3 = Convert.ToInt32(BitConverter.ToUInt16(buffer, i + 7));
                            i += 9;
                            if (ClientController.inited)
                            {
                                this.clientController.AddDirectedFX(num3, num4, num5, fx, dir, col);
                                continue;
                            }
                            continue;
                        }
                    case 'E':
                        break;
                    case 'F':
                        {
                            int fx = Convert.ToInt32(buffer[i]);
                            int num4 = Convert.ToInt32(BitConverter.ToUInt16(buffer, i + 1));
                            int num5 = Convert.ToInt32(BitConverter.ToUInt16(buffer, i + 3));
                            i += 5;
                            if (ClientController.inited)
                            {
                                this.clientController.AddFX(num4, num5, fx);
                                continue;
                            }
                            continue;
                        }
                    default:
                        switch (c)
                        {
                            case 'L':
                                {
                                    int num3 = Convert.ToInt32(BitConverter.ToUInt16(buffer, i));
                                    i += 2;
                                    if (RobotRenderer.inited)
                                    {
                                        this.robotRenderer.RemoveBot(num3);
                                        continue;
                                    }
                                    continue;
                                }
                            case 'M':
                                {
                                    int num7 = Convert.ToInt32(buffer[i]);
                                    int num8 = Convert.ToInt32(buffer[i + 1]);
                                    int num4 = Convert.ToInt32(BitConverter.ToUInt16(buffer, i + 2));
                                    int num5 = Convert.ToInt32(BitConverter.ToUInt16(buffer, i + 4));
                                    i += 6;
                                    for (int k = 0; k < num8; k++)
                                    {
                                        for (int l = 0; l < num7; l++)
                                        {
                                            byte cell = buffer[i];
                                            i++;
                                            if (TerrainRendererScript.inited)
                                            {
                                                TerrainRendererScript.map.SetCell(num4 + l, num5 + k, cell);
                                            }
                                        }
                                    }
                                    TerrainRendererScript.needUpdate = true;
                                    continue;
                                }
                            case 'O':
                                {
                                    int num9 = Convert.ToInt32(BitConverter.ToInt32(buffer, i));
                                    int num2 = Convert.ToInt32(BitConverter.ToUInt16(buffer, i + 4));
                                    i += 6;
                                    PackRenderer.THIS.RemoveObjectInBlock(num9);
                                    for (int m = 0; m < num2; m++)
                                    {
                                        char type = Convert.ToChar(buffer[i]);
                                        int num4 = Convert.ToInt32(BitConverter.ToUInt16(buffer, i + 1));
                                        int num5 = Convert.ToInt32(BitConverter.ToUInt16(buffer, i + 3));
                                        int num10 = Convert.ToInt32(BitConverter.ToUInt16(buffer, i + 5));
                                        int off = Convert.ToInt32(buffer[i + 7]);
                                        i += 8;
                                        ObjectModel ObjectModel = new ObjectModel();
                                        ObjectModel.x = num4;
                                        ObjectModel.y = num5;
                                        ObjectModel.cid = num10;
                                        ObjectModel.off = off;
                                        ObjectModel.type = type;
                                        PackRenderer.THIS.AddObject(ObjectModel, num9);
                                    }
                                    continue;
                                }
                        }
                        break;
                }
            }
            else if (c != 'S')
            {
                if (c != 'X')
                {
                    if (c == 'Z')
                    {
                        int num2 = Convert.ToInt32(buffer[i]);
                        int col = Convert.ToInt32(buffer[i + 1]);
                        int num4 = Convert.ToInt32(BitConverter.ToUInt16(buffer, i + 2));
                        int num5 = Convert.ToInt32(BitConverter.ToUInt16(buffer, i + 4));
                        i += 6;
                        int[] array2 = new int[num2];
                        for (int n = 0; n < num2; n++)
                        {
                            array2[n] = Convert.ToInt32(BitConverter.ToUInt16(buffer, i));
                            i += 2;
                            if (ClientController.inited)
                            {
                                this.clientController.AddDirectedFX(array2[n], num4, num5, -1, 0, col);
                            }
                        }
                        continue;
                    }
                }
                else
                {
                    int dir = Convert.ToInt32(buffer[i]);
                    int skin = Convert.ToInt32(buffer[i + 1]);
                    int tail = Convert.ToInt32(buffer[i + 2]);
                    int num3 = Convert.ToInt32(BitConverter.ToUInt16(buffer, i + 3));
                    int num4 = Convert.ToInt32(BitConverter.ToUInt16(buffer, i + 5));
                    int num5 = Convert.ToInt32(BitConverter.ToUInt16(buffer, i + 7));
                    int num10 = Convert.ToInt32(BitConverter.ToUInt16(buffer, i + 9));
                    i += 11;
                    if (RobotRenderer.inited)
                    {
                        this.robotRenderer.XYBot(num3, num4, num5, dir, num10, skin, tail);
                        continue;
                    }
                    continue;
                }
            }
            else
            {
                int num3 = Convert.ToInt32(BitConverter.ToUInt16(buffer, i));
                int num9 = Convert.ToInt32(BitConverter.ToInt32(buffer, i + 2));
                i += 6;
                if (RobotRenderer.inited)
                {
                    this.robotRenderer.RemoveBotFromBlock(num3, num9);
                    continue;
                }
                continue;
            }
            throw new Exception("Corrupted HB type - " + c.ToString());
        }
    }

    private void AutoRemHandler(ref string msg)
    {
        GUIManager.THIS.autoRemShow.SetActive(msg != "0");
        GUIManager.THIS.autoRemShow.GetComponentInChildren<Text>().text = "[B] " + msg;
    }

    private void HandModeHandler(ref string msg)
    {
        UnityEngine.Debug.Log("BH >" + msg);
        ProgPanel.handMode = (msg == "1");
    }

    private void UMPHandler(ref string msg)
    {
        string[] array = msg.Split(new char[]
        {
            '#'
        });
        string url = array[0];
        int imgx = (int)short.Parse(array[1]);
        int imgy = (int)short.Parse(array[2]);
        int progress = (int)short.Parse(array[3]);
        string text = array[4];
        MissionPad.THIS.UpdateMissionPanel(url, imgx, imgy, progress, text);
    }

    private void Update()
    {
    }

    private Obvyazka obvyazka;

	public GameObject mainRenderer;

	public GameObject clientControllerObject;

	private ClientController clientController;

	private RobotRenderer robotRenderer;

	private TerrainRendererScript terrainRenderer;

	private byte[] _typeBuffer;

	public CanvasScaler MainCanvasScaler;

	public static ServerController THIS;

	public static string onlineString;

  private string str300 = "BI";
  private string str301 = "@T";
  private string str302 = "@t";
  private string str303 = "sp";
  private string str304 = "@L";
  private string str305 = "@S";
  private string str306 = "@B";
  private string str307 = "NL";
  private string str308 = "ON";
  private string str309 = "LV";
  private string str310 = "GU";
  private string str311 = "Gu";
  private string str312 = "GR";
  private string str313 = "cS";
  private string str314 = "cH";
  private string str315 = "$$";
  private string str316 = "P$";
  private string str317 = "PM";
  private string str318 = "@P";
  private string str319 = "#P";
  private string str320 = "#p";
  private string str321 = "OK";
  private string str322 = "IN";
  private string str323 = "BC";
  private string str324 = "BA";
  private string str325 = "BR";
  private string str326 = "BD";
  private string str327 = "BH";
  private string str328 = "SP";
  private string str329 = "GE";
  private string str330 = "SU";
  private string str331 = "mN";
  private string str332 = "mL";
  private string str333 = "mO";
  private string str334 = "mU";
  private string str335 = "mC";
  private string str336 = "MM";
  private string str337 = "MN";
  private string str338 = "MP";
  private string str339 = "#S";
  private string str340 = "#F";
  private string str341 = "BB";
  private string str342 = "@R";
  private string str343 = "GO";
  private string str344 = "DR";
  private string str345 = "HB";
}

