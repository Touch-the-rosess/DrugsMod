using MyUI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PopupManager : MonoBehaviour
{
    private void Start()
    {
        PopupManager.THIS = this;
        this.stringFunction();
        this.GUIWindow.SetActive(false);
        for (int i = 0; i < this.buttons.Length; i++)
        {
            string buttonName = this.str112;
            int buttonNum = i;
            EventTrigger eventTrigger = this.buttons[i].gameObject.AddComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerDown
            };
            entry.callback.AddListener(delegate (BaseEventData e)
            {
                this.CommonButtonListener(buttonName, buttonNum);
            });
            eventTrigger.triggers.Add(entry);
        }
        this.backButton.onClick.AddListener(new UnityAction(this.onBack));
        this.exitButton.onClick.AddListener(new UnityAction(this.onExit));
        this.upButton.onClick.AddListener(new UnityAction(this.OnUpButton));
        this.deleteButton.onClick.AddListener(new UnityAction(this.OnDeleteButton));
        this.lockButton.onClick.AddListener(new UnityAction(this.onLock));
        this.upExitButton.onClick.AddListener(new UnityAction(this.OnUpExitButton));
        this.adminButton.onClick.AddListener(new UnityAction(this.OnAdmin));
    }

    private void onBack()
	{
		this.CommonButtonListener(this.str104, 0);
	}

    private void onExit()
    {
        this.CommonButtonListener(this.str103, 0);
    }

    private void onLock()
    {
        ServerTime.THIS.SendTypicalMessage(ClientController.THIS.TimeOfMove(), this.str102, 0, 0, string.Concat(new object[]
        {
            this.str101,
            "lock:",
            this.upcfg.slot,
            this.str110
        }));
    }

    private void OnAdmin()
    {
        this.fixScrollSave();
        ServerTime.THIS.SendTypicalMessage(ClientController.THIS.TimeOfMove(), this.str105, 0, 0, "_");
        Debug.Log(str105);
    }

    private string escapeString(string inStr)
    {
        inStr = inStr.Replace(this.str106, this.str107);
        inStr = inStr.Replace(this.str108, this.str109);
        inStr = inStr.Replace("\\", "\\\\");
        inStr = inStr.Replace("\n", "\\n");
        return inStr.Replace("\"", "\\\"");
    }

    private string MacroReplacer(string inStr)
    {
        string text = this.escapeString(this.inputText.text);
        if (inStr.Contains("%I%") && text.StartsWith("&"))
        {
            text = text.Substring(1);
        }
        inStr = inStr.Replace("%I%", text);
        inStr = inStr.Replace("%M%", this.crystallSection.GetComponent<CrystallSection>().GetValuesInString());
        if (inStr.IndexOf("%B%") != -1)
        {
            string text2 = "";
            Toggle[] componentsInChildren = this.paint.GetComponentsInChildren<Toggle>();
            for (int i = 0; i < componentsInChildren.Length; i++)
            {
                if (componentsInChildren[i].isOn)
                {
                    text2 += "1";
                }
                else
                {
                    text2 += "0";
                }
            }
            inStr = inStr.Replace("%B%", text2);
        }
        if (inStr.IndexOf("%R%") != -1)
        {
            string text3 = "";
            if (this.horbcfg.richList != null)
            {
                for (int j = 0; j < this.horbcfg.richList.Length / 5; j++)
                {
                    string text4 = this.horbcfg.richList[5 * j];
                    string a = this.horbcfg.richList[5 * j + 1];
                    string text5 = this.horbcfg.richList[5 * j + 2];
                    string str = this.horbcfg.richList[5 * j + 3];
                    string text6 = this.horbcfg.richList[5 * j + 4];
                    if (!(a == "bool"))
                    {
                        if (!(a == "drop"))
                        {
                            if (!(a == "uint"))
                            {
                                if (a == "text")
                                {
                                    text3 += str;
                                    text3 += this.str111;
                                    text3 += "0";
                                }
                            }
                            else
                            {
                                text3 += str;
                                text3 += this.str111;
                                text3 += this.richListObjects[j].GetComponentInChildren<MyInputField>().text;
                            }
                        }
                        else
                        {
                            text3 += str;
                            text3 += this.str111;
                            string[] array = text5.Split(new char[]
                            {
                                '#'
                            });
                            int value = this.richListObjects[j].GetComponentInChildren<Dropdown>().value;
                            string[] array2 = array[value].Split(new char[]
                            {
                                this.ch1
                            });
                            text3 += array2[0];
                        }
                    }
                    else
                    {
                        text3 += str;
                        text3 += this.str111;
                        text3 += (this.richListObjects[j].GetComponentInChildren<Toggle>().isOn ? "1" : "0");
                    }
                    text3 += PopupManager.statstr1;
                }
                inStr = inStr.Replace("%R%", text3);
            }
        }
        return inStr;
    }

    private void SimpleButtonListener(string action)
    {
        ServerTime.THIS.SendTypicalMessage(ClientController.THIS.TimeOfMove(), this.str102, 0, 0, this.str101 + action + this.str110);
        this.fixScrollSave();
    }

    private void CommonButtonListener(string buttonType, int num)
    {
        ClientController.CanGoto = false;
        this.fixScrollSave();
        if (buttonType == this.str112)
        {
            string text = this.MacroReplacer(this.horbcfg.buttons[2 * num + 1]);
            if (text.Length > 0 && text.Substring(0, 1) == "@")
            {
                text = text.Substring(1);
                if (!this.buttons[num].interactable)
                {
                    return;
                }
                this.buttons[num].interactable = false;
            }
            if (text.Length > 0 && text.Substring(0, 1) == "&")
            {
                Application.OpenURL(text.Substring(1));
                return;
            }
            ServerTime.THIS.SendTypicalMessage(ClientController.THIS.TimeOfMove(), this.str102, 0, 0, this.str101 + text + this.str110);
            return;
        }
        else
        {
            if (buttonType == this.str103)
            {
                ServerTime.THIS.SendTypicalMessage(ClientController.THIS.TimeOfMove(), this.str102, 0, 0, this.str101 + this.str103 + this.str110);
                return;
            }
            if (buttonType == this.str104)
            {
                ServerTime.THIS.SendTypicalMessage(ClientController.THIS.TimeOfMove(), this.str102, 0, 0, this.str101 + this.MacroReplacer(this.backButtonAction) + this.str110);
                return;
            }
            if (buttonType == "l")
            {
                ServerTime.THIS.SendTypicalMessage(ClientController.THIS.TimeOfMove(), this.str102, 0, 0, this.str101 + this.MacroReplacer(this.horbcfg.list[3 * num + 2]) + this.str110);
                return;
            }
            if (buttonType == "th")
            {
                ServerTime.THIS.SendTypicalMessage(ClientController.THIS.TimeOfMove(), this.str102, 0, 0, this.str101 + this.MacroReplacer(this.horbcfg.tabs[num]) + this.str110);
                return;
            }
            if (buttonType == "tu")
            {
                ServerTime.THIS.SendTypicalMessage(ClientController.THIS.TimeOfMove(), this.str102, 0, 0, this.str101 + this.MacroReplacer(this.upcfg.tabs[num]) + this.str110);
            }
            return;
        }
    }

    public void CloseWindow()
    {
        this.GUIWindow.SetActive(false);
        GUIManager.THIS.ClearFocus();
    }

    public void ShowUP(UPConfig cfg)
    {
        this.GUIWindow.SetActive(true);
        this.mode = "up";
        this.upcfg = cfg;
        this.disableKeyboard = false;
        this.blinkingObject = null;
        this.canvasGUI.SetActive(false);
        this.cardView.SetActive(false);
        this.upView.SetActive(true);
        this.backButton.gameObject.SetActive(false);
        this.scrollView.SetActive(false);
        this.insideTF.gameObject.SetActive(false);
        this.inputText.gameObject.SetActive(false);
        this.buttonRow.gameObject.SetActive(false);
        this.crystallSection.gameObject.SetActive(false);
        this.inventory.gameObject.SetActive(false);
        this.richContent.gameObject.SetActive(false);
        Vector2 sizeDelta = this.scrollView.GetComponent<RectTransform>().sizeDelta;
        sizeDelta.y = 216f;
        this.scrollView.GetComponent<RectTransform>().sizeDelta = sizeDelta;
        this.adminButton.gameObject.SetActive(cfg.admin);
        ClientController.THIS.stopAutoMove();
        if (cfg.tabs != null && cfg.tabs.Length != 0)
        {
            this.tabs.gameObject.SetActive(true);
            foreach (object obj in this.tabsRow.transform)
            {
                UnityEngine.Object.Destroy(((Transform)obj).gameObject);
            }
            for (int i = 0; i < cfg.tabs.Length; i += 2)
            {
                if (cfg.tabs[i + 1] == "")
                {
                    GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.openedTabPrefab);
                    gameObject.transform.SetParent(this.tabsRow.transform, worldPositionStays:false);
                    gameObject.GetComponentInChildren<Text>().text = cfg.tabs[i];
                }
                else
                {
                    GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.closedTabPrefab);
                    gameObject2.transform.SetParent(this.tabsRow.transform, worldPositionStays:false);
                    gameObject2.GetComponentInChildren<Text>().text = cfg.tabs[i];
                    int actionLabel = i + 1;
                    gameObject2.GetComponent<Button>().onClick.AddListener(delegate ()
                    {
                        this.CommonButtonListener("tu", actionLabel);
                    });
                }
            }
        }
        else
        {
            this.tabs.gameObject.SetActive(false);
        }
        this.deleteButton.gameObject.SetActive(cfg.canDelete);
        this.lockButton.gameObject.SetActive(cfg.canDelete);
        this.lockButton.gameObject.GetComponentInChildren<Image>().sprite = ((cfg.lockState == 1) ? this.lockButtonLocked : this.lockButtonUnlocked);
        foreach (object obj2 in this.upRoboView.transform)
        {
            UnityEngine.Object.Destroy(((Transform)obj2).gameObject);
        }
        int x = -80;
        int counter = 0;
        int y = -30;
        for (int j = 0; j <= cfg.skills.Length - 1; j++)
        {
            GameObject gameObject = UnityEngine.Object.Instantiate(skillButtonPrefab);
            gameObject.transform.SetParent(upRoboView.transform, worldPositionStays: false);
            if (counter == 20)
            {
                y -= 30;
                x = -80;
                counter = 0;
            }
            gameObject.transform.localPosition = new Vector3(x, y);
            x += 30;
            counter++;
            gameObject.GetComponent<SkillButtonScript>().SetIcon(cfg.skills[j].type, cfg.skills[j].isUp, cfg.skills[j].level, cfg.skills[j].isLocked);
            int num4 = j;
            gameObject.GetComponent<Button>().onClick.AddListener(delegate
            {
                OnSkill(num4);
            });
            if (cfg.slot == j)
            {
                gameObject.GetComponent<SkillButtonScript>().blinking = true;
            }
        }


        if (cfg.toInstall == null)
        {
            this.upSkillView.gameObject.SetActive(true);
            this.upInstallView.gameObject.SetActive(false);
            if (cfg.button != "")
            {
                this.upButton.gameObject.SetActive(true);
                this.upButton.GetComponentInChildren<Text>().text = cfg.button;
            }
            else
            {
                this.upButton.gameObject.SetActive(false);
            }
            if (cfg.skillIcon == -1)
            {
                this.upImage.gameObject.SetActive(false);
            }
            else
            {
                this.upImage.gameObject.SetActive(true);
                this.upImage.sprite = SkillButtonScript.sprites[cfg.skillIcon];
            }
        }
        else
        {
            this.upSkillView.gameObject.SetActive(false);
            this.upInstallView.gameObject.SetActive(true);
            foreach (object obj3 in this.upInstallContent.transform)
            {
                UnityEngine.Object.Destroy(((Transform)obj3).gameObject);
            }
            for (int k = 0; k < cfg.toInstall.Length; k++)
            {
                Button button = UnityEngine.Object.Instantiate<Button>(this.upInstallPrefab);
                button.gameObject.transform.SetParent(this.upInstallContent.transform, false);
                string shortCode = cfg.toInstall[k];
                if (cfg.toInstall[k].StartsWith("_"))
                {
                    shortCode = cfg.toInstall[k].Substring(1);
                    button.GetComponent<Image>().sprite = SkillButtonScript.sprites[SkillButtonScript.skillShorts[shortCode]];
                    button.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.5f);
                }
                else
                {
                    button.GetComponent<Image>().sprite = SkillButtonScript.sprites[SkillButtonScript.skillShorts[shortCode]];
                }
                button.onClick.AddListener(delegate ()
                {
                    this.OnInstall(shortCode, cfg.slot);
                });
            }
        }
        this.titleTF.text = cfg.title;
        this.upText.text = cfg.text;
        this.UpdateLayout();
    }

    private void UpdateFixScroll()
    {
        if (this.fixScroll)
        {
            float num = this.scrollView.GetComponent<ScrollRect>().content.GetComponent<RectTransform>().sizeDelta.y - this.scrollView.GetComponent<RectTransform>().sizeDelta.y;
            if (num < 0f)
            {
                num = 1f;
            }
            float num2 = 1f - this.fixScrolls[this.fixScrollTag] / num;
            if (num2 > 1f)
            {
                num2 = 1f;
            }
            if (num2 < 0f)
            {
                num2 = 0f;
            }
            this.scrollView.GetComponent<ScrollRect>().verticalNormalizedPosition = num2;
        }
    }

    private void fixScrollSave()
    {
        if (this.fixScroll)
        {
            float num = this.scrollView.GetComponent<ScrollRect>().content.GetComponent<RectTransform>().sizeDelta.y - this.scrollView.GetComponent<RectTransform>().sizeDelta.y;
            if (num < 0f)
            {
                num = 1f;
            }
            this.fixScrolls[this.fixScrollTag] = num * (1f - this.scrollView.GetComponent<ScrollRect>().verticalNormalizedPosition);
        }
    }

    private void OnInstall(string code, int slot)
    {
        ServerTime.THIS.SendTypicalMessage(ClientController.THIS.TimeOfMove(), this.str102, 0, 0, string.Concat(new object[]
        {
            this.str101,
            "install",
            this.str111,
            code,
            PopupManager.statstr1,
            slot,
            this.str110
        }));
    }

    private void OnUpExitButton()
    {
        ServerTime.THIS.SendTypicalMessage(ClientController.THIS.TimeOfMove(), this.str102, 0, 0, this.str101 + this.str103 + ":0\"}");
    }

    private void OnDeleteButton()
    {
        ServerTime.THIS.SendTypicalMessage(ClientController.THIS.TimeOfMove(), this.str102, 0, 0, string.Concat(new object[]
        {
            this.str101,
            "delete:",
            this.upcfg.slot,
            this.str110
        }));
    }

    private void OnUpButton()
    {
        ServerTime.THIS.SendTypicalMessage(ClientController.THIS.TimeOfMove(), this.str102, 0, 0, this.str101 + this.upcfg.buttonAction + this.str110);
    }

    private void OnSkill(int slotNum)
    {
        ServerTime.THIS.SendTypicalMessage(ClientController.THIS.TimeOfMove(), this.str102, 0, 0, string.Concat(new object[]
        {
            this.str101,
            "skill",
            this.str111,
            slotNum,
            this.str110
        }));
    }

    public void ShowHORB(HORBConfig cfg)
    {
        PopupManager.їљљљјњљјљњњљњјјњњњљјљњњ їљљљјњљјљњњљњјјњњњљјљњњ = new PopupManager.їљљљјњљјљњњљњјјњњњљјљњњ();
        їљљљјњљјљњњљњјјњњњљјљњњ._003C_003E4__this = this;
        this.GUIWindow.SetActive(true);
        this.mode = "horb";
        this.horbcfg = cfg;
        this.disableKeyboard = false;
        this.blinkingObject = null;
        this.bigInput = false;
        this.canvasGUI.SetActive(false);
        this.upView.SetActive(false);
        this.adminButton.gameObject.SetActive(cfg.admin);
        this.richContent.gameObject.SetActive(false);
        ClientController.THIS.stopAutoMove();
        this.fixScroll = false;
        this.inventory.GetComponent<RectTransform>().sizeDelta = new Vector2(498f, 55f);
        this.inventory.GetComponent<GridLayoutGroup>().cellSize = new Vector2(55f, 55f);
        this.canvasGUI.GetComponent<RectTransform>().sizeDelta = new Vector2(600f, 245f);
        float num29 = 0f;
        float y = 216f;
        this.backButtonAction = this.str104;
        bool flag = false;
        їљљљјњљјљњњљњјјњњњљјљњњ.InvButton = "choose";
        Vector2 vector;
        if (cfg.css != null)
        {
            string[] array = cfg.css.Split(new char[]
            {
                ';'
            });
            for (int i = 0; i < array.Length; i++)
            {
                string[] array2 = array[i].Split(new char[]
                {
                    '='
                });
                string text = array2[0];
                string text2 = (array2.Length > 1) ? array2[1] : "";
                string a = "";
                string a2 = "";
                if (text.IndexOf('-') != -1)
                {
                    string[] array3 = text.Split(new char[]
                    {
                        '-'
                    });
                    text = "param";
                    a = array3[0];
                    a2 = array3[1];
                }
                GameObject gameObject = null;
                if (!(a == "inv"))
                {
                    if (a == "canv")
                    {
                        gameObject = this.canvasGUI;
                    }
                }
                else
                {
                    gameObject = this.inventory;
                }
                if (gameObject != null)
                {
                    if (!(a2 == "ch"))
                    {
                        if (!(a2 == "w"))
                        {
                            if (a2 == "h")
                            {
                                vector = gameObject.GetComponent<RectTransform>().sizeDelta;
                                vector.y = float.Parse(text2);
                                gameObject.GetComponent<RectTransform>().sizeDelta = vector;
                            }
                        }
                        else
                        {
                            vector = gameObject.GetComponent<RectTransform>().sizeDelta;
                            vector.x = float.Parse(text2);
                            gameObject.GetComponent<RectTransform>().sizeDelta = vector;
                        }
                    }
                    else
                    {
                        vector = gameObject.GetComponent<GridLayoutGroup>().cellSize;
                        vector.y = float.Parse(text2);
                        gameObject.GetComponent<GridLayoutGroup>().cellSize = vector;
                    }
                }
                if (!(text == "fixScroll"))
                {
                    if (!(text == "space"))
                    {
                        if (!(text == "scrollh"))
                        {
                            if (!(text == "invButton"))
                            {
                                if (!(text == "keysOff"))
                                {
                                    if (text == "biginput")
                                    {
                                        this.bigInput = true;
                                    }
                                }
                                else
                                {
                                    this.disableKeyboard = true;
                                }
                            }
                            else
                            {
                                їљљљјњљјљњњљњјјњњњљјљњњ.InvButton = text2;
                            }
                        }
                        else
                        {
                            y = float.Parse(text2);
                        }
                    }
                    else
                    {
                        num29 = float.Parse(text2);
                    }
                }
                else
                {
                    this.fixScroll = true;
                    this.fixScrollTag = text2;
                    if (!this.fixScrolls.ContainsKey(this.fixScrollTag))
                    {
                        this.fixScrolls[this.fixScrollTag] = 1f;
                    }
                }
            }
        }
        int num2 = 0;
        int num3 = 0;
        if (cfg.canvas != null && cfg.canvas.Length != 0)
        {
            this.canvasGUI.SetActive(true);
            foreach (object obj in this.canvasGUI.transform)
            {
                UnityEngine.Object.Destroy(((Transform)obj).gameObject);
            }
            for (int j = 0; j < cfg.canvas.Length; j++)
            {
                string text3 = cfg.canvas[j];
                text3 = text3.Replace(this.str107, this.str106);
                text3 = text3.Replace(this.str109, this.str108);
                string text4 = text3;
                string text5 = "";
                if (text3.IndexOf(PopupManager.statstr1) != -1)
                {
                    text4 = text3.Substring(0, text3.IndexOf(PopupManager.statstr1));
                    text5 = text3.Substring(text3.IndexOf(PopupManager.statstr1) + 1);
                }

               
                int num4 = 0;
                int num5 = 1;
                int num6 = 0;
                int num7 = 0;
                int num8 = 1;
                int num9 = 1;
                int num10 = 1;
                bool flag2 = false;
                int num11 = 255;
                int num12 = 0;
                for (int k = 0; k < text4.Length; k++)
                {
                    
                    char c = text4[k];
                    //Debug.Log(c);
                    if (c <= 'Y')
                    {
                        if (c <= 'L')
                        {
                            switch (c)
                            {
                                case '-':
                                    num5 = -1;
                                    break;
                                case '.':
                                case '/':
                                case ':':
                                case ';':
                                case '<':
                                case '>':
                                case '?':
                                case '@':
                                    break;
                                case '0':
                                    num4 *= 10;
                                    num4 = num4;
                                    break;
                                case '1':
                                    num4 *= 10;
                                    num4++;
                                    break;
                                case '2':
                                    num4 *= 10;
                                    num4 += 2;
                                    break;
                                case '3':
                                    num4 *= 10;
                                    num4 += 3;
                                    break;
                                case '4':
                                    num4 *= 10;
                                    num4 += 4;
                                    break;
                                case '5':
                                    num4 *= 10;
                                    num4 += 5;
                                    break;
                                case '6':
                                    num4 *= 10;
                                    num4 += 6;
                                    break;
                                case '7':
                                    num4 *= 10;
                                    num4 += 7;
                                    break;
                                case '8':
                                    num4 *= 10;
                                    num4 += 8;
                                    break;
                                case '9':
                                    num4 *= 10;
                                    num4 += 9;
                                    break;
                                case '=':
                                    switch (text4.Substring(k + 1))
                                    {
                                        case "t":
                                        {
                                           // Debug.Log(text4.Substring(k + 1));
                                                GameObject gameObject7 = UnityEngine.Object.Instantiate<GameObject>(this.canvasTPButtonPrefab);
                                                gameObject7.transform.SetParent(this.canvasGUI.transform, false);
                                                if (num12 != 0)
                                                {
                                                    gameObject7.GetComponent<RectTransform>().pivot = new Vector2(0.5f - (float)num12 * 0.5f, 1f);
                                                }
                                                gameObject7.transform.localPosition = new Vector3((float)(num2 + num6), (float)(num3 + num7));
                                                string actiontp = cfg.canvas[j + 1];
                                                gameObject7.GetComponent<Button>().onClick.AddListener(delegate ()
                                                {
                                                    //SimpleButtonListener(actiontp);
                                                    їљљљјњљјљњњљњјјњњњљјљњњ._003C_003E4__this.SimpleButtonListener(actiontp);
                                                });
                                                j++;
                                                break;
                                            }
                                        case "b":
                                            {
                                               // Debug.Log(text4.Substring(k + 1));
                                                GameObject gameObject8 = UnityEngine.Object.Instantiate<GameObject>(this.canvasMicroButtonPrefab);
                                                gameObject8.transform.SetParent(this.canvasGUI.transform, false);
                                                if (num12 != 0)
                                                {
                                                    gameObject8.GetComponent<RectTransform>().pivot = new Vector2(0.5f - (float)num12 * 0.5f, 1f);
                                                }
                                                gameObject8.transform.localPosition = new Vector3((float)(num2 + num6), (float)(num3 + num7));
                                                gameObject8.GetComponentInChildren<Text>().text = text5;
                                                string action = cfg.canvas[j + 1];
                                                gameObject8.GetComponent<Button>().onClick.AddListener(delegate ()
                                                {
                                                    //SimpleButtonListener(actiontp);
                                                    їљљљјњљјљњњљњјјњњњљјљњњ._003C_003E4__this.SimpleButtonListener(action);
                                                });
                                                j++;
                                                break;
                                            }
                                        case "B":
                                            {
                                                //Debug.Log(text4.Substring(k + 1));
                                                GameObject gameObject4 = UnityEngine.Object.Instantiate<GameObject>(this.canvasButtonPrefab);
                                                gameObject4.transform.SetParent(this.canvasGUI.transform, false);
                                                if (num12 != 0)
                                                {
                                                    gameObject4.GetComponent<RectTransform>().pivot = new Vector2(0.5f - (float)num12 * 0.5f, 1f);
                                                }
                                                gameObject4.transform.localPosition = new Vector3((float)(num2 + num6), (float)(num3 + num7));
                                                gameObject4.GetComponentInChildren<Text>().text = text5;
                                                string action2 = cfg.canvas[j + 1];
                                                gameObject4.GetComponent<Button>().onClick.AddListener(delegate ()
                                                {
                                                    //SimpleButtonListener(actiontp);
                                                    їљљљјњљјљњњљњјјњњњљјљњњ._003C_003E4__this.SimpleButtonListener(action2);
                                                });
                                                j++;
                                                break;
                                            }
                                        case "T":
                                            {
                                                //Debug.Log(text4.Substring(k + 1));
                                                GameObject gameObject6 = UnityEngine.Object.Instantiate<GameObject>(this.canvasTextFieldPrefab);
                                                if (num12 != 0)
                                                {
                                                    gameObject6.GetComponent<RectTransform>().pivot = new Vector2(0.5f - (float)num12 * 0.5f, 1f);
                                                }
                                                gameObject6.transform.SetParent(this.canvasGUI.transform, false);
                                                gameObject6.transform.localPosition = new Vector3((float)(num2 + num6), (float)(num3 + num7));
                                                gameObject6.GetComponent<Text>().text = text5;
                                                if (flag2)
                                                {
                                                    this.blinkingObject = gameObject6;
                                                }
                                                break;
                                            }
                                        case "I":
                                            {
                                               // Debug.Log(text4.Substring(k + 1));
                                                GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.canvasWebImagePrefab);
                                                gameObject2.transform.SetParent(this.canvasGUI.transform, false);
                                                if (num12 != 0)
                                                {
                                                    gameObject2.GetComponent<RectTransform>().pivot = new Vector2(0.5f - (float)num12 * 0.5f, 1f);
                                                }
                                                gameObject2.transform.localPosition = new Vector3((float)(num2 + num6), (float)(num3 + num7));
                                                gameObject2.GetComponent<WebImage>().SetSizeAndUrl(num8, num9, text5);
                                                if (flag2)
                                                {
                                                    this.blinkingObject = gameObject2;
                                                }
                                                break;
                                            }
                                        case "R":
                                            {
                                                //Debug.Log(text4.Substring(k + 1));
                                                GameObject gameObject5 = UnityEngine.Object.Instantiate<GameObject>(this.canvasRectPrefab);
                                                gameObject5.transform.SetParent(this.canvasGUI.transform, false);
                                                if (num12 != 0)
                                                {
                                                    gameObject5.GetComponent<RectTransform>().pivot = new Vector2(0.5f - (float)num12 * 0.5f, 1f);
                                                }
                                                gameObject5.transform.localPosition = new Vector3((float)(num2 + num6), (float)(num3 + num7));
                                                gameObject5.GetComponent<RectTransform>().sizeDelta = new Vector2((float)num8, (float)num9);
                                                gameObject5.GetComponent<Image>().color = PopupManager.hexToColor(text5, (byte)num11);
                                                if (flag2)
                                                {
                                                    this.blinkingObject = gameObject5;
                                                }
                                                break;
                                            }
                                        case "L":
                                            {
                                                //Debug.Log(text4.Substring(k + 1));
                                                GameObject gameObject3 = UnityEngine.Object.Instantiate<GameObject>(this.canvasLinePrefab);
                                                gameObject3.transform.SetParent(this.canvasGUI.transform, false);
                                                if (num12 != 0)
                                                {
                                                    gameObject3.GetComponent<RectTransform>().pivot = new Vector2(0.5f - (float)num12 * 0.5f, 1f);
                                                }
                                                gameObject3.transform.localPosition = new Vector3((float)(num2 + (num6 + num8) / 2), (float)(num3 + (num7 + num9) / 2));
                                                int num14 = Mathf.FloorToInt(Mathf.Sqrt((float)((num6 - num8) * (num6 - num8) + (num7 - num9) * (num7 - num9))));
                                                gameObject3.GetComponent<RectTransform>().sizeDelta = new Vector2((float)num10, (float)num14);
                                                Quaternion rotation = default(Quaternion);
                                                rotation.eulerAngles = new Vector3(0f, 0f, 90f + 180f * Mathf.Atan2((float)(num7 - num9), (float)(num6 - num8)) / 3.14159274f);
                                                gameObject3.transform.rotation = rotation;
                                                gameObject3.GetComponent<Image>().color = PopupManager.hexToColor(text5, (byte)num11);
                                                if (flag2)
                                                {
                                                    this.blinkingObject = gameObject3;
                                                }
                                                break;
                                            }
                                    
                                    }
                                    //Debug.Log(text4.Substring(k + 1));
                                    k = text4.Length;
                                    break;


                                //{
                                //    string text6 = text4.Substring(k + 1);
                                //    uint num13 = _003CPrivateImplementationDetails_003E.ComputeStringHash(text6);
                                //    if (num13 <= 3423339364u)
                                //    {
                                //        if (num13 != 3339451269u)
                                //        {
                                //            if (num13 != 3373006507u)
                                //            {
                                //                if (num13 == 3423339364u)
                                //                {
                                //                    if (text6 == "I")
                                //                    {
                                //                        GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.canvasWebImagePrefab);
                                //                        gameObject2.transform.SetParent(this.canvasGUI.transform, false);
                                //                        if (num12 != 0)
                                //                        {
                                //                            gameObject2.GetComponent<RectTransform>().pivot = new Vector2(0.5f - (float)num12 * 0.5f, 1f);
                                //                        }
                                //                        gameObject2.transform.localPosition = new Vector3((float)(num2 + num6), (float)(num3 + num7));
                                //                        gameObject2.GetComponent<WebImage>().SetSizeAndUrl(num8, num9, text5);
                                //                        if (flag2)
                                //                        {
                                //                            this.blinkingObject = gameObject2;
                                //                        }
                                //                    }
                                //                }
                                //            }
                                //            else if (text6 == "L")
                                //            {
                                //                GameObject gameObject3 = UnityEngine.Object.Instantiate<GameObject>(this.canvasLinePrefab);
                                //                gameObject3.transform.SetParent(this.canvasGUI.transform, false);
                                //                if (num12 != 0)
                                //                {
                                //                    gameObject3.GetComponent<RectTransform>().pivot = new Vector2(0.5f - (float)num12 * 0.5f, 1f);
                                //                }
                                //                gameObject3.transform.localPosition = new Vector3((float)(num2 + (num6 + num8) / 2), (float)(num3 + (num7 + num9) / 2));
                                //                int num14 = Mathf.FloorToInt(Mathf.Sqrt((float)((num6 - num8) * (num6 - num8) + (num7 - num9) * (num7 - num9))));
                                //                gameObject3.GetComponent<RectTransform>().sizeDelta = new Vector2((float)num10, (float)num14);
                                //                Quaternion rotation = default(Quaternion);
                                //                rotation.eulerAngles = new Vector3(0f, 0f, 90f + 180f * Mathf.Atan2((float)(num7 - num9), (float)(num6 - num8)) / 3.14159274f);
                                //                gameObject3.transform.rotation = rotation;
                                //                gameObject3.GetComponent<Image>().color = PopupManager.hexToColor(text5, (byte)num11);
                                //                if (flag2)
                                //                {
                                //                    this.blinkingObject = gameObject3;
                                //                }
                                //            }
                                //        }
                                //        else if (text6 == "B")
                                //        {
                                //            GameObject gameObject4 = UnityEngine.Object.Instantiate<GameObject>(this.canvasButtonPrefab);
                                //            gameObject4.transform.SetParent(this.canvasGUI.transform, false);
                                //            if (num12 != 0)
                                //            {
                                //                gameObject4.GetComponent<RectTransform>().pivot = new Vector2(0.5f - (float)num12 * 0.5f, 1f);
                                //            }
                                //            gameObject4.transform.localPosition = new Vector3((float)(num2 + num6), (float)(num3 + num7));
                                //            gameObject4.GetComponentInChildren<Text>().text = text5;
                                //            string action2 = cfg.canvas[j + 1];
                                //            gameObject4.GetComponent<Button>().onClick.AddListener(delegate ()
                                //            {
                                //                їљљљјњљјљњњљњјјњњњљјљњњ._003C_003E4__this.SimpleButtonListener(action2);
                                //            });
                                //            j++;
                                //        }
                                //    }
                                //    else if (num13 <= 3607893173u)
                                //    {
                                //        if (num13 != 3507227459u)
                                //        {
                                //            if (num13 == 3607893173u)
                                //            {
                                //                if (text6 == "R")
                                //                {
                                //                    GameObject gameObject5 = UnityEngine.Object.Instantiate<GameObject>(this.canvasRectPrefab);
                                //                    gameObject5.transform.SetParent(this.canvasGUI.transform, false);
                                //                    if (num12 != 0)
                                //                    {
                                //                        gameObject5.GetComponent<RectTransform>().pivot = new Vector2(0.5f - (float)num12 * 0.5f, 1f);
                                //                    }
                                //                    gameObject5.transform.localPosition = new Vector3((float)(num2 + num6), (float)(num3 + num7));
                                //                    gameObject5.GetComponent<RectTransform>().sizeDelta = new Vector2((float)num8, (float)num9);
                                //                    gameObject5.GetComponent<Image>().color = PopupManager.hexToColor(text5, (byte)num11);
                                //                    if (flag2)
                                //                    {
                                //                        this.blinkingObject = gameObject5;
                                //                    }
                                //                }
                                //            }
                                //        }
                                //        else if (text6 == "T")
                                //        {
                                //            GameObject gameObject6 = UnityEngine.Object.Instantiate<GameObject>(this.canvasTextFieldPrefab);
                                //            if (num12 != 0)
                                //            {
                                //                gameObject6.GetComponent<RectTransform>().pivot = new Vector2(0.5f - (float)num12 * 0.5f, 1f);
                                //            }
                                //            gameObject6.transform.SetParent(this.canvasGUI.transform, false);
                                //            gameObject6.transform.localPosition = new Vector3((float)(num2 + num6), (float)(num3 + num7));
                                //            gameObject6.GetComponent<Text>().text = text5;
                                //            if (flag2)
                                //            {
                                //                this.blinkingObject = gameObject6;
                                //            }
                                //        }
                                //    }
                                //    else if (num13 != 3876335077u)
                                //    {
                                //        if (num13 == 4044111267u)
                                //        {
                                //            if (text6 == "t")
                                //            {
                                //                GameObject gameObject7 = UnityEngine.Object.Instantiate<GameObject>(this.canvasTPButtonPrefab);
                                //                gameObject7.transform.SetParent(this.canvasGUI.transform, false);
                                //                if (num12 != 0)
                                //                {
                                //                    gameObject7.GetComponent<RectTransform>().pivot = new Vector2(0.5f - (float)num12 * 0.5f, 1f);
                                //                }
                                //                gameObject7.transform.localPosition = new Vector3((float)(num2 + num6), (float)(num3 + num7));
                                //                string actiontp = cfg.canvas[j + 1];
                                //                gameObject7.GetComponent<Button>().onClick.AddListener(delegate ()
                                //                {
                                //                    їљљљјњљјљњњљњјјњњњљјљњњ._003C_003E4__this.SimpleButtonListener(actiontp);
                                //                });
                                //                j++;
                                //            }
                                //        }
                                //    }
                                //    else if (text6 == "b")
                                //    {
                                //        GameObject gameObject8 = UnityEngine.Object.Instantiate<GameObject>(this.canvasMicroButtonPrefab);
                                //        gameObject8.transform.SetParent(this.canvasGUI.transform, false);
                                //        if (num12 != 0)
                                //        {
                                //            gameObject8.GetComponent<RectTransform>().pivot = new Vector2(0.5f - (float)num12 * 0.5f, 1f);
                                //        }
                                //        gameObject8.transform.localPosition = new Vector3((float)(num2 + num6), (float)(num3 + num7));
                                //        gameObject8.GetComponentInChildren<Text>().text = text5;
                                //        string action = cfg.canvas[j + 1];
                                //        gameObject8.GetComponent<Button>().onClick.AddListener(delegate ()
                                //        {
                                //            їљљљјњљјљњњљњјјњњњљјљњњ._003C_003E4__this.SimpleButtonListener(action);
                                //        });
                                //        j++;
                                //    }
                                //    k = text4.Length;
                                //    break;
                                //}


                                case 'A':
                                    num11 = num5 * num4;
                                    num4 = 0;
                                    num5 = 1;
                                    break;
                                case 'B':
                                    flag2 = true;
                                    num4 = 0;
                                    num5 = 1;
                                    break;
                                default:
                                    if (c == 'L')
                                    {
                                        num10 = num5 * num4;
                                        num4 = 0;
                                        num5 = 1;
                                    }
                                    break;
                            }
                        }
                        else if (c != 'X')
                        {
                            if (c == 'Y')
                            {
                                num7 = num5 * num4;
                                num4 = 0;
                                num5 = 1;
                            }
                        }
                        else
                        {
                            num6 = num5 * num4;
                            num4 = 0;
                            num5 = 1;
                        }
                    }
                    else if (c <= 'l')
                    {
                        if (c != 'h')
                        {
                            if (c == 'l')
                            {
                                num12 = 1;
                                num4 = 0;
                                num5 = 1;
                            }
                        }
                        else
                        {
                            num9 = num5 * num4;
                            num4 = 0;
                            num5 = 1;
                        }
                    }
                    else if (c != 'r')
                    {
                        switch (c)
                        {
                            case 'w':
                                num8 = num5 * num4;
                                num4 = 0;
                                num5 = 1;
                                break;
                            case 'x':
                                num2 += num5 * num4;
                                num4 = 0;
                                num5 = 1;
                                break;
                            case 'y':
                                num3 += num5 * num4;
                                num4 = 0;
                                num5 = 1;
                                break;
                        }
                    }
                    else
                    {
                        num12 = -1;
                        num4 = 0;
                        num5 = 1;
                    }
                }
            }
        }
        vector = this.scrollView.GetComponent<RectTransform>().sizeDelta;
        vector.y = y;
        this.scrollView.GetComponent<RectTransform>().sizeDelta = vector;
        if (cfg.inv != null)
        {
            this.inventory.SetActive(true);
            flag = true;
            foreach (object obj2 in this.inventory.transform)
            {
                UnityEngine.Object.Destroy(((Transform)obj2).gameObject);
            }
            string[] array4 = cfg.inv.Split(new char[]
            {
                this.ch1
            });
            if (array4.Length > 1)
            {
                for (int l = 0; l < array4.Length; l += 2)
                {
                    GameObject gameObject9 = UnityEngine.Object.Instantiate<GameObject>(this.inventoryItemPrefab);
                    gameObject9.transform.SetParent(this.inventory.transform, false);
                    int num = 0;
                    bool frame = false;
                    string upstr = "";
                    string downstr = "";
                    if (array4[l + 1].Substring(0, 1) == "f")
                    {
                        upstr = "@";
                    }
                    else if (array4[l + 1].IndexOf(';') != -1)
                    {
                        string[] array5 = array4[l + 1].Split(new char[]
                        {
                            ';'
                        });
                        upstr = array5[0];
                        downstr = array5[1];
                    }
                    else
                    {
                        num = int.Parse(array4[l + 1]);
                    }
                    if (array4[l].Substring(0, 1) == "s")
                    {
                        int num15 = SkillButtonScript.skillShorts[array4[l].Substring(1)];
                        gameObject9.GetComponent<InventoryItem>().Setup(2000 + num15, num, frame, upstr, downstr);
                        string type = array4[l].Substring(1);
                        gameObject9.GetComponent<Button>().onClick.AddListener(delegate ()
                        {
                            ServerTime.THIS.SendTypicalMessage(ClientController.THIS.TimeOfMove(), їљљљјњљјљњњљњјјњњњљјљњњ._003C_003E4__this.str102, 0, 0, string.Concat(new string[]
                            {
                                їљљљјњљјљњњљњјјњњњљјљњњ._003C_003E4__this.str101,
                                їљљљјњљјљњњљњјјњњњљјљњњ.InvButton,
                                їљљљјњљјљњњљњјјњњњљјљњњ._003C_003E4__this.str111,
                                type,
                                їљљљјњљјљњњљњјјњњњљјљњњ._003C_003E4__this.str110
                            }));
                        });
                    }
                    else
                    {
                        int.Parse(array4[l]);
                        gameObject9.GetComponent<InventoryItem>().Setup(int.Parse(array4[l]), num, frame, upstr, downstr);
                        int type = int.Parse(array4[l]);
                        gameObject9.GetComponent<Button>().onClick.AddListener(delegate ()
                        {
                            ServerTime.THIS.SendTypicalMessage(ClientController.THIS.TimeOfMove(), їљљљјњљјљњњљњјјњњњљјљњњ._003C_003E4__this.str102, 0, 0, string.Concat(new object[]
                            {
                                їљљљјњљјљњњљњјјњњњљјљњњ._003C_003E4__this.str101,
                                їљљљјњљјљњњљњјјњњњљјљњњ.InvButton,
                                їљљљјњљјљњњљњјјњњњљјљњњ._003C_003E4__this.str111,
                                type,
                                їљљљјњљјљњњљњјјњњњљјљњњ._003C_003E4__this.str110
                            }));
                        });
                    }
                }
            }
            int num16 = array4.Length / 2;
            int num17 = num16 - 9 * (num16 / 9);
            num17 = 0;
            for (int m = 0; m < num17; m++)
            {
                GameObject gameObject10 = UnityEngine.Object.Instantiate<GameObject>(this.inventoryItemPrefab);
                gameObject10.transform.SetParent(this.inventory.transform, false);
                gameObject10.GetComponent<InventoryItem>().Setup(-1, 0, false, "", "");
            }
        }
        else
        {
            this.inventory.SetActive(false);
        }
        if (cfg.card != null && cfg.card != "")
        {
            this.cardView.SetActive(true);
            string text7 = cfg.card.Substring(0, cfg.card.IndexOf(this.ch1));
            string a3 = text7.Substring(0, 1);
            string text8 = text7.Substring(1);
            string text9 = cfg.card.Substring(cfg.card.IndexOf(this.ch1) + 1);
            if (text9 != "")
            {
                this.pad340.SetActive(false);
            }
            else
            {
                this.pad340.SetActive(true);
            }
            this.cardView.GetComponentInChildren<Text>().text = text9;
            this.cardView.GetComponentInChildren<WebImage>().off = true;
            if (a3 == "s")
            {
                Sprite sprite = SkillButtonScript.sprites[SkillButtonScript.skillShorts[text8]];
                this.cardView.GetComponentInChildren<Image>().sprite = sprite;
                this.cardView.GetComponentInChildren<Image>().SetNativeSize();
            }
            else if (a3 == "i")
            {
                Sprite sprite2 = InventoryItem.sprites[(int)short.Parse(text8)];
                this.cardView.GetComponentInChildren<Image>().sprite = sprite2;
                this.cardView.GetComponentInChildren<Image>().SetNativeSize();
            }
            else if (a3 == "c")
            {
                Sprite sprite3 = ClanSpriteScript.sprites[(int)(short.Parse(text8) - 1)];
                this.cardView.GetComponentInChildren<Image>().sprite = sprite3;
                this.cardView.GetComponentInChildren<Image>().SetNativeSize();
                this.cardView.GetComponentInChildren<Image>().rectTransform.sizeDelta = 4f * this.cardView.GetComponentInChildren<Image>().rectTransform.sizeDelta;
            }
            else if (a3 == "w")
            {
                this.cardView.GetComponentInChildren<WebImage>().off = false;
                string[] array6 = text8.Replace("%", this.str111).Split(new char[]
                {
                    '#'
                });
                this.cardView.GetComponentInChildren<WebImage>().SetSizeAndUrl(int.Parse(array6[0]), int.Parse(array6[1]), array6[2]);
            }
        }
        else
        {
            this.cardView.SetActive(false);
        }
        if (cfg.paint)
        {
            this.paint.SetActive(true);
            flag = true;
        }
        else
        {
            this.paint.SetActive(false);
        }
        if (cfg.tabs != null && cfg.tabs.Length != 0)
        {
            this.tabs.gameObject.SetActive(true);
            foreach (object obj3 in this.tabsRow.transform)
            {
                UnityEngine.Object.Destroy(((Transform)obj3).gameObject);
            }
            for (int n = 0; n < cfg.tabs.Length; n += 2)
            {
                if (cfg.tabs[n + 1] == "")
                {
                    GameObject gameObject11 = UnityEngine.Object.Instantiate<GameObject>(this.openedTabPrefab);
                    gameObject11.transform.SetParent(this.tabsRow.transform, false);
                    gameObject11.GetComponentInChildren<Text>().text = cfg.tabs[n];
                }
                else
                {
                    GameObject gameObject12 = UnityEngine.Object.Instantiate<GameObject>(this.closedTabPrefab);
                    gameObject12.transform.SetParent(this.tabsRow.transform, false);
                    gameObject12.GetComponentInChildren<Text>().text = cfg.tabs[n];
                    int actionLabel = n + 1;
                    gameObject12.GetComponent<Button>().onClick.AddListener(delegate ()
                    {
                        їљљљјњљјљњњљњјјњњњљјљњњ._003C_003E4__this.CommonButtonListener("th", actionLabel);
                    });
                }
            }
        }
        else
        {
            this.tabs.gameObject.SetActive(false);
        }
        if (cfg.crys_lines != null && cfg.crys_lines.Length == 6)
        {
            flag = true;
            if (cfg.crys_buy)
            {
                CrystalScroller.BUY_LOGIC = true;
            }
            else
            {
                CrystalScroller.BUY_LOGIC = false;
            }
            this.crystallSection.gameObject.SetActive(true);
            if (cfg.crys_left != null && cfg.crys_left != "")
            {
                this.crystallSection.GetComponent<CrystallSection>().leftDesc.gameObject.SetActive(true);
                this.crystallSection.GetComponent<CrystallSection>().leftDesc.text = cfg.crys_left;
            }
            else
            {
                this.crystallSection.SetActive(false);
            }
            if (cfg.crys_right != null && cfg.crys_right != "")
            {
                this.crystallSection.GetComponent<CrystallSection>().rightDesc.gameObject.SetActive(true);
                this.crystallSection.GetComponent<CrystallSection>().rightDesc.text = cfg.crys_right;
            }
            else
            {
                this.crystallSection.SetActive(false);
            }
            for (int num18 = 0; num18 < cfg.crys_lines.Length; num18++)
            {
                string[] array7 = cfg.crys_lines[num18].Split(new char[]
                {
                    this.ch1
                });
                long leftMin = long.Parse(array7[0]);
                long rightMin = long.Parse(array7[1]);
                long d = long.Parse(array7[2]);
                long value = long.Parse(array7[3]);
                string descText = array7[4];
                CrystalScroller component = this.crystallSection.GetComponent<CrystallSection>().lines[num18].GetComponent<CrystalScroller>();
                component.leftMin = leftMin;
                component.rightMin = rightMin;
                component.d = d;
                component.value = value;
                component.descText = descText;
                component.UpdateFromModel();
            }
        }
        else
        {
            this.crystallSection.gameObject.SetActive(false);
        }
        if (cfg.input_place != null)
        {
            flag = true;
            if (cfg.input_len > 0)
            {
                this.inputText.characterLimit = cfg.input_len;
            }
            else
            {
                this.inputText.characterLimit = 35;
            }
            this.inputText.gameObject.SetActive(true);
            if (this.bigInput)
            {
                this.inputText.characterLimit = 1000;
                this.inputText.lineType = MyInputField.LineType.MultiLineNewline;
                this.inputText.placeholder.GetComponent<Text>().text = "";
                this.inputText.text = cfg.input_place;
                this.inputText.GetComponent<RectTransform>().sizeDelta = new Vector2(596f, 264f);
            }
            else
            {
                this.inputText.placeholder.GetComponent<Text>().text = cfg.input_place;
                this.inputText.text = "";
                this.inputText.lineType = MyInputField.LineType.SingleLine;
                this.inputText.GetComponent<RectTransform>().sizeDelta = new Vector2(596f, 23f);
            }
            if (cfg.input_console)
            {
                GUIManager.THIS.ClearFocus();
                GUIManager.THIS.m_EventSystem.SetSelectedGameObject(this.inputText.gameObject, null);
            }
        }
        else
        {
            this.inputText.gameObject.SetActive(false);
        }
        if (cfg.back)
        {
            this.backButton.gameObject.SetActive(true);
            this.backButtonAction = this.str104;
        }
        else
        {
            this.backButton.gameObject.SetActive(false);
        }
        if (cfg.clanlist != null && cfg.clanlist.Length != 0)
        {
            flag = true;
            this.listContent.GetComponent<VerticalLayoutGroup>().spacing = 26f;
            this.scrollView.SetActive(true);
            foreach (object obj4 in this.listContent.transform)
            {
                UnityEngine.Object.Destroy(((Transform)obj4).gameObject);
            }
            for (int num19 = 0; num19 < cfg.clanlist.Length / 4; num19++)
            {
                int num20 = int.Parse(cfg.clanlist[4 * num19]);
                string text10 = cfg.clanlist[4 * num19 + 1];
                string text11 = cfg.clanlist[4 * num19 + 2];
                string action = cfg.clanlist[4 * num19 + 3];
                GameObject gameObject13 = UnityEngine.Object.Instantiate<GameObject>(this.clanLinePrefab);
                gameObject13.transform.SetParent(this.listContent.transform, false);
                if (num20 > 0)
                {
                    gameObject13.GetComponentsInChildren<Image>()[0].sprite = ClanSpriteScript.sprites[num20 - 1];
                }
                else
                {
                    gameObject13.GetComponentsInChildren<Image>()[0].gameObject.SetActive(false);
                }
                gameObject13.GetComponentsInChildren<Text>()[0].text = text10;
                gameObject13.GetComponentsInChildren<Text>()[1].text = text11;
                gameObject13.GetComponentInChildren<Button>().onClick.AddListener(delegate ()
                {
                    їљљљјњљјљњњљњјјњњњљјљњњ._003C_003E4__this.SimpleButtonListener(action);
                });
            }
        }
        else if (cfg.richList != null && cfg.richList.Length != 0)
        {
            //Debug.Log("da");
            flag = true;
            this.listContent.GetComponent<VerticalLayoutGroup>().spacing = 44f;
            GameObject gameObject14 = this.listContent;
            if (cfg.rich_no_scroll)
            {
                this.scrollView.SetActive(false);
                gameObject14 = this.richContent;
                this.richContent.SetActive(true);
                this.listContent.GetComponent<VerticalLayoutGroup>().spacing = 35f;
            }
            else
            {
                this.scrollView.SetActive(true);
            }
            this.richListObjects = new GameObject[cfg.richList.Length / 5];
            foreach (object obj5 in gameObject14.transform)
            {
                UnityEngine.Object.Destroy(((Transform)obj5).gameObject);
            }
            for (int num21 = 0; num21 < cfg.richList.Length / 5; num21++)
            {
                PopupManager.љњїјїњјїјїљїїњїјљїїљїјї љњїјїњјїјїљїїњїјљїїљїјї = new PopupManager.љњїјїњјїјїљїїњїјљїїљїјї();
                љњїјїњјїјїљїїњїјљїїљїјї.CS_0024_003C_003E8__locals6 = їљљљјњљјљњњљњјјњњњљјљњњ;
                string text12 = cfg.richList[5 * num21];
                string text13 = cfg.richList[5 * num21 + 1];
                string text14 = cfg.richList[5 * num21 + 2];
                љњїјїњјїјїљїїњїјљїїљїјї.resp = cfg.richList[5 * num21 + 3];
                string text15 = cfg.richList[5 * num21 + 4];
                Debug.Log(text13);
                switch (text13)
                {
                    case "3card":
                        {
                            string[] array10 = text12.Split(new char[]
                        {
                            '&'
                        });
                            string[] array11 = text14.Split(new char[]
                            {
                            '&'
                            });
                            string[] array12 = љњїјїњјїјїљїїњїјљїїљїјї.resp.Split(new char[]
                            {
                            '&'
                            });
                            string[] array13 = text15.Split(new char[]
                            {
                            '&'
                            });
                            GameObject gameObject17 = UnityEngine.Object.Instantiate<GameObject>(this.cardLinePrefab);
                            gameObject17.transform.SetParent(gameObject14.transform, false);
                            for (int num23 = 0; num23 < array10.Length; num23++)
                            {
                                GameObject gameObject18 = UnityEngine.Object.Instantiate<GameObject>(this.cardPrefab);
                                gameObject18.transform.SetParent(gameObject17.transform, false);
                                string[] array14 = array13[num23].Split(new char[]
                                {
                                '%'
                                });
                                gameObject18.GetComponent<WebImage>().SetSizeAndUrl(int.Parse(array14[1]), int.Parse(array14[2]), array14[0]);
                                Text[] componentsInChildren = gameObject18.GetComponentsInChildren<Text>();
                                componentsInChildren[0].text = array10[num23];
                                componentsInChildren[1].text = array11[num23];
                                if (array11[num23] == "" || array11[num23] == " ")
                                {
                                    Image[] componentsInChildren2 = gameObject18.GetComponentsInChildren<Image>();
                                    componentsInChildren2[1].color = new Color(0f, 0f, 0f, 0.7f);
                                    componentsInChildren2[2].color = new Color(0f, 0f, 0f, 0f);
                                }
                                string buttonResp = array12[num23];
                                gameObject18.GetComponent<Button>().onClick.AddListener(delegate ()
                                {
                                    љњїјїњјїјїљїїњїјљїїљїјї.CS_0024_003C_003E8__locals6._003C_003E4__this.SimpleButtonListener(buttonResp);
                                });
                            }
                            this.richListObjects[num21] = gameObject17;
                            break;
                        }
                    case "bool":
                        {
                            GameObject gameObject22 = UnityEngine.Object.Instantiate<GameObject>(this.toggleLinePrefab);
                            gameObject22.transform.SetParent(gameObject14.transform, false);
                            gameObject22.GetComponentInChildren<Text>().text = text12;
                            gameObject22.GetComponentInChildren<Toggle>().isOn = (int.Parse(text15) != 0);
                            this.richListObjects[num21] = gameObject22;
                            break;
                        }
                    case "drop":
                        {
                            GameObject gameObject15 = UnityEngine.Object.Instantiate<GameObject>(this.dropdownLinePrefab);
                            gameObject15.transform.SetParent(gameObject14.transform, false);
                            gameObject15.GetComponentsInChildren<Text>()[0].text = text12;
                            string[] array8 = text14.Split(new char[]
                            {
                                        '#'
                            });
                            gameObject15.GetComponentInChildren<Dropdown>().options.Clear();
                            for (int num22 = 0; num22 < array8.Length - 1; num22++)
                            {
                                string[] array9 = array8[num22].Split(new char[]
                                {
                                            this.ch1
                                });
                                gameObject15.GetComponentInChildren<Dropdown>().options.Add(new Dropdown.OptionData(array9[1]));
                            }
                            gameObject15.GetComponentInChildren<Dropdown>().value = int.Parse(text15);
                            this.richListObjects[num21] = gameObject15;
                            break;
                        }
                    case "uint":
                        {
                            GameObject gameObject21 = UnityEngine.Object.Instantiate<GameObject>(this.uintLinePrefab);
                            gameObject21.transform.SetParent(gameObject14.transform, false);
                            gameObject21.GetComponentsInChildren<Text>()[0].text = text12;
                            gameObject21.GetComponentsInChildren<MyInputField>()[0].text = text15;
                            this.richListObjects[num21] = gameObject21;
                            break;
                        }
                    case "text":
                        {
                            GameObject gameObject19 = UnityEngine.Object.Instantiate<GameObject>(this.textLinePrefab);
                            gameObject19.transform.SetParent(gameObject14.transform, false);
                            gameObject19.GetComponentInChildren<Text>().text = text12;
                            this.richListObjects[num21] = gameObject19;
                            break;
                        }

                    case "button":
                    {
                        GameObject gameObject16 = UnityEngine.Object.Instantiate(buttonLinePrefab);
                        gameObject16.transform.SetParent(gameObject14.transform, worldPositionStays: false);
                        gameObject16.GetComponentsInChildren<Text>()[1].text = text12;
                        gameObject16.GetComponentsInChildren<Text>()[0].text = text14;
                        gameObject16.GetComponentInChildren<Button>().onClick.AddListener(delegate
                        {
                            їљљљјњљјљњњљњјјњњњљјљњњ._003C_003E4__this.SimpleButtonListener(љњїјїњјїјїљїїњїјљїїљїјї.resp);
                            //SimpleButtonListener
                        });
                        if (text14 == "")
                        {
                            gameObject16.GetComponentInChildren<Button>().gameObject.SetActive(value: false);
                        }
                        richListObjects[num21] = gameObject16;
                        break;
                    }

                    case "fill":
                        {
                            GameObject gameObject20 = UnityEngine.Object.Instantiate<GameObject>(this.fillLinePrefab);
                            gameObject20.transform.SetParent(gameObject14.transform, false);
                            string[] fillparts = text14.Split(new char[]
                            {
                            '#'
                            });
                            gameObject20.GetComponent<FuelLineScript>().Setup(int.Parse(fillparts[0]), text12, fillparts[1], int.Parse(fillparts[2]), fillparts[3] != "", fillparts[4] != "", fillparts[5] != "");
                            Button[] componentsInChildren3 = gameObject20.GetComponentsInChildren<Button>();
                            componentsInChildren3[0].onClick.AddListener(delegate ()
                            {
                                љњїјїњјїјїљїїњїјљїїљїјї.CS_0024_003C_003E8__locals6._003C_003E4__this.SimpleButtonListener(fillparts[3]);
                            });
                            componentsInChildren3[1].onClick.AddListener(delegate ()
                            {
                                љњїјїњјїјїљїїњїјљїїљїјї.CS_0024_003C_003E8__locals6._003C_003E4__this.SimpleButtonListener(fillparts[4]);
                            });
                            componentsInChildren3[2].onClick.AddListener(delegate ()
                            {
                                љњїјїњјїјїљїїњїјљїїљїјї.CS_0024_003C_003E8__locals6._003C_003E4__this.SimpleButtonListener(fillparts[5]);
                            });
                            this.richListObjects[num21] = gameObject20;
                            break;
                        }
                }
                //uint num13 = _003CPrivateImplementationDetails_003E.ComputeStringHash(text13);
                //if (num13 <= 2846199180u)
                //{
                //    if (num13 != 477871960u)
                //    {
                //        if (num13 != 1135768689u)
                //        {
                //            if (num13 == 2846199180u)
                //            {
                //                if (text13 == "drop")
                //                {
                //                    GameObject gameObject15 = UnityEngine.Object.Instantiate<GameObject>(this.dropdownLinePrefab);
                //                    gameObject15.transform.SetParent(gameObject14.transform, false);
                //                    gameObject15.GetComponentsInChildren<Text>()[0].text = text12;
                //                    string[] array8 = text14.Split(new char[]
                //                    {
                //                        '#'
                //                    });
                //                    gameObject15.GetComponentInChildren<Dropdown>().options.Clear();
                //                    for (int num22 = 0; num22 < array8.Length - 1; num22++)
                //                    {
                //                        string[] array9 = array8[num22].Split(new char[]
                //                        {
                //                            this.ch1
                //                        });
                //                        gameObject15.GetComponentInChildren<Dropdown>().options.Add(new Dropdown.OptionData(array9[1]));
                //                    }
                //                    gameObject15.GetComponentInChildren<Dropdown>().value = int.Parse(text15);
                //                    this.richListObjects[num21] = gameObject15;
                //                }
                //            }
                //        }
                //        else if (text13 == "button")
                //        {
                //            GameObject gameObject16 = UnityEngine.Object.Instantiate<GameObject>(this.buttonLinePrefab);
                //            gameObject16.transform.SetParent(gameObject14.transform, false);
                //            gameObject16.GetComponentsInChildren<Text>()[1].text = text12;
                //            gameObject16.GetComponentsInChildren<Text>()[0].text = text14;
                //            gameObject16.GetComponentInChildren<Button>().onClick.AddListener(delegate ()
                //            {
                //                љњїјїњјїјїљїїњїјљїїљїјї.CS_0024_003C_003E8__locals6._003C_003E4__this.SimpleButtonListener(љњїјїњјїјїљїїњїјљїїљїјї.resp);
                //            });
                //            if (text14 == "")
                //            {
                //                gameObject16.GetComponentInChildren<Button>().gameObject.SetActive(false);
                //            }
                //            this.richListObjects[num21] = gameObject16;
                //        }
                //    }
                //    else if (text13 == "3card")
                //    {
                //        string[] array10 = text12.Split(new char[]
                //        {
                //            '&'
                //        });
                //        string[] array11 = text14.Split(new char[]
                //        {
                //            '&'
                //        });
                //        string[] array12 = љњїјїњјїјїљїїњїјљїїљїјї.resp.Split(new char[]
                //        {
                //            '&'
                //        });
                //        string[] array13 = text15.Split(new char[]
                //        {
                //            '&'
                //        });
                //        GameObject gameObject17 = UnityEngine.Object.Instantiate<GameObject>(this.cardLinePrefab);
                //        gameObject17.transform.SetParent(gameObject14.transform, false);
                //        for (int num23 = 0; num23 < array10.Length; num23++)
                //        {
                //            GameObject gameObject18 = UnityEngine.Object.Instantiate<GameObject>(this.cardPrefab);
                //            gameObject18.transform.SetParent(gameObject17.transform, false);
                //            string[] array14 = array13[num23].Split(new char[]
                //            {
                //                '%'
                //            });
                //            gameObject18.GetComponent<WebImage>().SetSizeAndUrl(int.Parse(array14[1]), int.Parse(array14[2]), array14[0]);
                //            Text[] componentsInChildren = gameObject18.GetComponentsInChildren<Text>();
                //            componentsInChildren[0].text = array10[num23];
                //            componentsInChildren[1].text = array11[num23];
                //            if (array11[num23] == "" || array11[num23] == " ")
                //            {
                //                Image[] componentsInChildren2 = gameObject18.GetComponentsInChildren<Image>();
                //                componentsInChildren2[1].color = new Color(0f, 0f, 0f, 0.7f);
                //                componentsInChildren2[2].color = new Color(0f, 0f, 0f, 0f);
                //            }
                //            string buttonResp = array12[num23];
                //            gameObject18.GetComponent<Button>().onClick.AddListener(delegate ()
                //            {
                //                љњїјїњјїјїљїїњїјљїїљїјї.CS_0024_003C_003E8__locals6._003C_003E4__this.SimpleButtonListener(buttonResp);
                //            });
                //        }
                //        this.richListObjects[num21] = gameObject17;
                //    }
                //}
                //else if (num13 <= 3185987134u)
                //{
                //    if (num13 != 2984927816u)
                //    {
                //        if (num13 == 3185987134u)
                //        {
                //            if (text13 == "text")
                //            {
                //                GameObject gameObject19 = UnityEngine.Object.Instantiate<GameObject>(this.textLinePrefab);
                //                gameObject19.transform.SetParent(gameObject14.transform, false);
                //                gameObject19.GetComponentInChildren<Text>().text = text12;
                //                this.richListObjects[num21] = gameObject19;
                //            }
                //        }
                //    }
                //    else if (text13 == "fill")
                //    {
                //        GameObject gameObject20 = UnityEngine.Object.Instantiate<GameObject>(this.fillLinePrefab);
                //        gameObject20.transform.SetParent(gameObject14.transform, false);
                //        string[] fillparts = text14.Split(new char[]
                //        {
                //            '#'
                //        });
                //        gameObject20.GetComponent<FuelLineScript>().Setup(int.Parse(fillparts[0]), text12, fillparts[1], int.Parse(fillparts[2]), fillparts[3] != "", fillparts[4] != "", fillparts[5] != "");
                //        Button[] componentsInChildren3 = gameObject20.GetComponentsInChildren<Button>();
                //        componentsInChildren3[0].onClick.AddListener(delegate ()
                //        {
                //            љњїјїњјїјїљїїњїјљїїљїјї.CS_0024_003C_003E8__locals6._003C_003E4__this.SimpleButtonListener(fillparts[3]);
                //        });
                //        componentsInChildren3[1].onClick.AddListener(delegate ()
                //        {
                //            љњїјїњјїјїљїїњїјљїїљїјї.CS_0024_003C_003E8__locals6._003C_003E4__this.SimpleButtonListener(fillparts[4]);
                //        });
                //        componentsInChildren3[2].onClick.AddListener(delegate ()
                //        {
                //            љњїјїњјїјїљїїњїјљїїљїјї.CS_0024_003C_003E8__locals6._003C_003E4__this.SimpleButtonListener(fillparts[5]);
                //        });
                //        this.richListObjects[num21] = gameObject20;
                //    }
                //}
                //else if (num13 != 3365180733u)
                //{
                //    if (num13 == 3415750305u)
                //    {
                //        if (text13 == "uint")
                //        {
                //            GameObject gameObject21 = UnityEngine.Object.Instantiate<GameObject>(this.uintLinePrefab);
                //            gameObject21.transform.SetParent(gameObject14.transform, false);
                //            gameObject21.GetComponentsInChildren<Text>()[0].text = text12;
                //            gameObject21.GetComponentsInChildren<MyInputField>()[0].text = text15;
                //            this.richListObjects[num21] = gameObject21;
                //        }
                //    }
                //}
                //else if (text13 == "bool")
                //{
                //    GameObject gameObject22 = UnityEngine.Object.Instantiate<GameObject>(this.toggleLinePrefab);
                //    gameObject22.transform.SetParent(gameObject14.transform, false);
                //    gameObject22.GetComponentInChildren<Text>().text = text12;
                //    gameObject22.GetComponentInChildren<Toggle>().isOn = (int.Parse(text15) != 0);
                //    this.richListObjects[num21] = gameObject22;
                //}
            }
        }
        else if (cfg.list == null || cfg.list.Length == 0)
        {
            this.scrollView.SetActive(false);
        }
        else
        {
            flag = true;
            this.scrollView.SetActive(true);
            this.listContent.GetComponent<VerticalLayoutGroup>().spacing = 44f;
            foreach (object obj6 in this.listContent.transform)
            {
                UnityEngine.Object.Destroy(((Transform)obj6).gameObject);
            }
            for (int num24 = 0; num24 < cfg.list.Length / 3; num24++)
            {
                GameObject gameObject23 = UnityEngine.Object.Instantiate<GameObject>(this.buttonLinePrefab);
                gameObject23.transform.SetParent(this.listContent.transform, false);
                gameObject23.GetComponentsInChildren<Text>()[0].text = cfg.list[3 * num24 + 1];
                gameObject23.GetComponentsInChildren<Text>()[1].text = cfg.list[3 * num24];
                int num = num24;
                if (cfg.list[3 * num24 + 1] == "")
                {
                    gameObject23.GetComponentInChildren<Button>().gameObject.SetActive(false);
                }
                else
                {
                    gameObject23.GetComponentInChildren<Button>().onClick.AddListener(delegate ()
                    {
                        їљљљјњљјљњњљњјјњњњљјљњњ._003C_003E4__this.CommonButtonListener("l", num);
                    });
                }
            }
        }
        if (cfg.title != "")
        {
            this.titleTF.gameObject.SetActive(true);
            this.titleTF.text = cfg.title;
        }
        else
        {
            this.titleTF.gameObject.SetActive(false);
        }
        if (cfg.text != null && cfg.text != "")
        {
            if (cfg.text.Length > 2 && cfg.text.Substring(0, 2) == "%%")
            {
                Vector2 sizeDelta = this.scrollView.GetComponent<RectTransform>().sizeDelta;
                sizeDelta.y = 400f;
                this.scrollView.GetComponent<RectTransform>().sizeDelta = sizeDelta;
                GameObject gameObject24 = this.listContent;
                foreach (object obj7 in gameObject24.transform)
                {
                    UnityEngine.Object.Destroy(((Transform)obj7).gameObject);
                }
                this.listContent.GetComponent<VerticalLayoutGroup>().spacing = 5f;
                this.scrollView.SetActive(true);
                foreach (string text16 in cfg.text.Substring(2).Split(new char[]
                {
                    '§'
                }))
                {
                    if (text16.StartsWith("="))
                    {
                        string[] array16 = text16.Substring(1).Split(new char[]
                        {
                            '#'
                        });
                        GameObject gameObject25 = UnityEngine.Object.Instantiate<GameObject>(this.centeredImagePrefab);
                        gameObject25.GetComponentInChildren<WebImage>().SetSizeAndUrl(int.Parse(array16[0]), int.Parse(array16[1]), array16[2]);
                        gameObject25.GetComponentInChildren<LayoutElement>().minHeight = (float)gameObject25.GetComponentInChildren<WebImage>().GetHeight();
                        gameObject25.transform.SetParent(gameObject24.transform, false);
                    }
                    else if (text16.StartsWith(">"))
                    {
                        string[] array17 = text16.Substring(1).Split(new char[]
                        {
                            '|'
                        });
                        GameObject gameObject26 = UnityEngine.Object.Instantiate<GameObject>(this.clanLinePrefab);
                        gameObject26.transform.SetParent(this.listContent.transform, false);
                        gameObject26.GetComponentsInChildren<Image>()[0].gameObject.SetActive(false);
                        gameObject26.GetComponentsInChildren<Text>()[0].text = array17[0];
                        gameObject26.GetComponentsInChildren<Text>()[1].text = array17[1];
                        gameObject26.GetComponentsInChildren<Text>()[0].color = new Color(0.8f, 0.8f, 0.5f);
                        string action = array17[2];
                        gameObject26.GetComponentInChildren<Button>().onClick.AddListener(delegate ()
                        {
                            їљљљјњљјљњњљњјјњњњљјљњњ._003C_003E4__this.SimpleButtonListener(action);
                        });
                    }
                    else
                    {
                        GameObject gameObject27 = UnityEngine.Object.Instantiate<GameObject>(this.multitextPrefab);
                        gameObject27.transform.SetParent(gameObject24.transform, false);
                        gameObject27.GetComponent<Text>().text = text16;
                    }
                }
            }
            else if (cfg.text.Length > 1 && cfg.text.Substring(0, 1) == "@")
            {
                this.insideTF.gameObject.SetActive(true);
                RectOffset padding = this.insideContainer.padding;
                padding.left = 20;
                padding.right = 20;
                this.insideContainer.padding = padding;
                this.insideTF.alignment = TextAnchor.UpperLeft;
                this.insideTF.text = cfg.text.Substring(2);
            }
            else
            {
                this.insideTF.gameObject.SetActive(true);
                RectOffset padding2 = this.insideContainer.padding;
                padding2.left = 0;
                padding2.right = 0;
                this.insideContainer.padding = padding2;
                this.insideTF.alignment = TextAnchor.MiddleCenter;
                this.insideTF.text = cfg.text;
            }
        }
        else
        {
            this.insideTF.gameObject.SetActive(false);
        }
        if (cfg.buttons != null)
        {
            this.buttonRow.gameObject.SetActive(true);
            for (int num26 = 0; num26 < this.buttons.Length; num26++)
            {
                this.buttons[num26].gameObject.SetActive(false);
            }
            int num27 = 0;
            for (int num28 = 0; num28 < cfg.buttons.Length / 2; num28++)
            {
                string text17 = cfg.buttons[2 * num28];
                string text18 = cfg.buttons[2 * num28 + 1];
                if (text18.Substring(0, 1) == "<")
                {
                    this.backButton.gameObject.SetActive(true);
                    this.backButtonAction = text18.Substring(1);
                }
                else if (text18 != this.str103)
                {
                    this.buttons[num28].gameObject.SetActive(true);
                    this.buttons[num28].interactable = true;
                    num27++;
                }
                this.buttons[num28].GetComponentInChildren<Text>().text = text17;
            }
            if (num27 == 0)
            {
                this.buttonRow.gameObject.SetActive(false);
            }
            else
            {
                flag = true;
            }
        }
        else
        {
            this.buttonRow.gameObject.SetActive(false);
        }
        if (cfg.text != "" && !flag && !this.insideTF.text.EndsWith("\n"))
        {
            this.insideTF.text = this.insideTF.text + "\n";
        }
        if (cfg.text == "##")
        {
            this.insideTF.gameObject.SetActive(false);
        }
        if (num29 != 0f)
        {
            this.listContent.GetComponent<VerticalLayoutGroup>().spacing = 26f;
        }
        if (this.fixScroll)
        {
            this.UpdateFixScroll();
            base.Invoke("UpdateFixScroll", 0.1f);
        }
        else
        {
            this.scrollView.GetComponent<ScrollRect>().verticalNormalizedPosition = 1f;
        }
        this.UpdateLayout();
    }

    private void UpdateLayout()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(this.buttonRow.GetComponent<RectTransform>());
        LayoutRebuilder.ForceRebuildLayoutImmediate(this.tabsRow.GetComponent<RectTransform>());
        LayoutRebuilder.ForceRebuildLayoutImmediate(this.GUIWindow.GetComponent<RectTransform>());
    }

    public static string colorToHex(Color32 color)
    {
        return color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2");
    }

    public static Color hexToColor(string hex, byte alpha = 255)
    {
        hex = hex.Replace("0x", "");
        hex = hex.Replace(PopupManager.statstr1, "");
        byte a = alpha;
        byte r = byte.Parse(hex.Substring(0, 2), NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), NumberStyles.HexNumber);
        if (hex.Length == 8)
        {
            a = byte.Parse(hex.Substring(6, 2), NumberStyles.HexNumber);
        }
        return new Color32(r, g, b, a);
    }

    private void Update()
    {
        if (this.GUIWindow.activeSelf)
        {
            if (this.inputText.gameObject.activeSelf && this.scrollView.activeSelf && this.inputText.text != this.lastInputString)
            {
                this.lastInputString = this.inputText.text;
                string text = this.lastInputString.ToLower();
                UnityEngine.Debug.Log("search -> " + text);
                HashSet<Transform> hashSet = new HashSet<Transform>();
                foreach (object obj in this.listContent.transform)
                {
                    Transform transform = (Transform)obj;
                    if (!(transform.parent != this.listContent.transform))
                    {
                        transform.gameObject.SetActive(false);
                        Text[] componentsInChildren = transform.GetComponentsInChildren<Text>();
                        for (int i = 0; i < componentsInChildren.Length; i++)
                        {
                            if (this.lastInputString == "")
                            {
                                hashSet.Add(transform);
                            }
                            else if (componentsInChildren[i].text.ToLower().Contains(text))
                            {
                                hashSet.Add(transform);
                            }
                        }
                    }
                }
                foreach (Transform transform2 in hashSet)
                {
                    transform2.gameObject.SetActive(true);
                }
            }
            if (this.blinkingObject != null)
            {
                this.blinkingObject.SetActive(Mathf.FloorToInt(5f * Time.time) % 2 == 0);
            }
            if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
            {
                if (this.mode == "up")
                {
                    this.OnUpExitButton();
                }
                else if (this.mode == "horb" && this.horbcfg.buttons != null && this.horbcfg.buttons.Length != 0)
                {
                    this.CommonButtonListener(this.str112, this.horbcfg.buttons.Length / 2 - 1);
                }
            }
            if ((UnityEngine.Input.GetKeyDown(KeyCode.Return) || UnityEngine.Input.GetKeyDown(KeyCode.KeypadEnter)) && (!(GUIManager.THIS.m_EventSystem.currentSelectedGameObject != null) || (!(GUIManager.THIS.m_EventSystem.currentSelectedGameObject.name == "ChatField") && !(GUIManager.THIS.m_EventSystem.currentSelectedGameObject.name == "LocalChat"))) && !this.disableKeyboard && !this.bigInput)
            {
                if (this.mode == "up")
                {
                    this.OnUpExitButton();
                }
                else if (this.mode == "horb" && this.horbcfg.buttons != null && this.horbcfg.buttons.Length != 0)
                {
                    this.CommonButtonListener(this.str112, 0);
                }
            }
            if (UnityEngine.Input.GetKeyDown(KeyCode.R) && UnityEngine.Input.GetKey(KeyCode.LeftCommand) && this.canvasGUI.activeSelf)
            {
                string text2 = "";
                foreach (object obj2 in this.canvasGUI.transform)
                {
                    Transform transform3 = (Transform)obj2;
                    int num = (int)transform3.localPosition.x;
                    int num2 = (int)transform3.localPosition.y;
                    if (transform3.gameObject.tag == this.str112)
                    {
                        text2 = string.Concat(new object[]
                        {
                            text2,
                            "\"",
                            num,
                            "X",
                            num2,
                            "Y=b#",
                            this.escapeString(transform3.GetComponentInChildren<Text>().text),
                            "\",\"action\","
                        });
                    }
                    if (transform3.gameObject.tag == this.str112)
                    {
                        text2 = string.Concat(new object[]
                        {
                            text2,
                            "\"",
                            num,
                            "X",
                            num2,
                            "Y=B#",
                            this.escapeString(transform3.GetComponentInChildren<Text>().text),
                            "\",\"action\","
                        });
                    }
                    if (transform3.gameObject.tag == "T")
                    {
                        text2 = string.Concat(new object[]
                        {
                            text2,
                            "\"",
                            num,
                            "X",
                            num2,
                            "Y=T#",
                            this.escapeString(transform3.GetComponent<Text>().text),
                            "\","
                        });
                    }
                    if (transform3.gameObject.tag == "I")
                    {
                        int w = transform3.GetComponent<WebImage>().w;
                        int h = transform3.GetComponent<WebImage>().h;
                        string url = transform3.GetComponent<WebImage>().url;
                        text2 = string.Concat(new object[]
                        {
                            text2,
                            "\"",
                            w,
                            "w",
                            h,
                            "h",
                            num,
                            "X",
                            num2,
                            "Y=I#",
                            url,
                            "\","
                        });
                    }
                    if (transform3.gameObject.tag == "R")
                    {
                        int num3 = (int)transform3.GetComponent<RectTransform>().sizeDelta.x;
                        int num4 = (int)transform3.GetComponent<RectTransform>().sizeDelta.y;
                        string text3 = PopupManager.colorToHex(transform3.GetComponent<Image>().color);
                        text2 = string.Concat(new object[]
                        {
                            text2,
                            "\"",
                            num3,
                            "w",
                            num4,
                            "h",
                            num,
                            "X",
                            num2,
                            "Y=R#",
                            text3,
                            "\","
                        });
                    }
                }
                UnityEngine.Debug.Log(text2);
            }
        }
    }

    private void stringFunction()
    {
        this.str101 = UnknownClass2.smethod_16(new byte[]
        {
            140,
            156,
            77,
            70,
            13,
            126,
            213,
            110,
            215,
            152,
            81,
            92,
            60,
            61,
            220,
            94,
            78,
            233,
            241,
            47,
            126,
            14,
            9,
            159,
            47,
            170,
            88,
            31,
            16,
            68,
            254,
            60,
            3,
            22,
            57,
            112,
            200,
            56,
            223,
            94,
            208,
            54,
            61,
            188,
            231,
            157,
            249,
            146,
            141,
            130,
            229,
            99,
            88,
            64,
            197,
            37,
            99,
            217,
            132,
            185,
            237,
            90,
            112,
            30,
            19,
            64,
            57,
            208,
            223,
            23,
            213,
            107,
            185,
            208,
            8,
            78,
            181,
            164,
            251,
            230,
            211,
            88,
            29,
            171,
            121,
            93,
            134,
            67,
            205,
            51,
            114,
            25,
            5,
            241,
            203,
            247,
            233,
            166,
            235,
            180,
            73,
            246,
            64,
            75,
            175,
            129,
            181,
            119,
            127,
            87,
            185,
            16,
            139,
            119,
            18,
            230,
            199,
            146,
            137,
            127,
            82,
            213,
            30,
            84,
            165,
            236,
            107,
            167
        }, false);
        this.str102 = UnknownClass2.smethod_16(new byte[]
        {
            52,
            39,
            235,
            11,
            51,
            140,
            35,
            144,
            197,
            137,
            153,
            0,
            21,
            165,
            215,
            223,
            166,
            6,
            89,
            144,
            248,
            64,
            22,
            226,
            157,
            241,
            138,
            248,
            171,
            219,
            125,
            128,
            86,
            121,
            204,
            253,
            145,
            146,
            91,
            13,
            129,
            86,
            24,
            228,
            39,
            105,
            24,
            52,
            108,
            230,
            131,
            95,
            193,
            68,
            77,
            70,
            106,
            108,
            252,
            169,
            16,
            181,
            175,
            14,
            203,
            131,
            159,
            13,
            7,
            180,
            9,
            242,
            149,
            232,
            49,
            50,
            68,
            98,
            112,
            54,
            246,
            218,
            51,
            164,
            210,
            165,
            9,
            54,
            177,
            55,
            222,
            180,
            118,
            28,
            207,
            24,
            252,
            137,
            167,
            222,
            215,
            58,
            1,
            56,
            144,
            37,
            156,
            119,
            146,
            222,
            57,
            218,
            253,
            169,
            126,
            43,
            191,
            209,
            18,
            158,
            199,
            104,
            11,
            33,
            141,
            215,
            226,
            134
        }, false);
        this.str103 = UnknownClass2.smethod_16(new byte[]
        {
            35,
            156,
            166,
            141,
            189,
            110,
            214,
            185,
            5,
            26,
            194,
            242,
            68,
            115,
            176,
            176,
            241,
            49,
            102,
            173,
            252,
            227,
            56,
            1,
            105,
            146,
            37,
            215,
            97,
            36,
            168,
            181,
            208,
            215,
            232,
            167,
            9,
            66,
            42,
            208,
            222,
            36,
            57,
            176,
            133,
            byte.MaxValue,
            144,
            80,
            67,
            168,
            238,
            244,
            206,
            14,
            41,
            119,
            198,
            67,
            151,
            160,
            123,
            190,
            14,
            196,
            105,
            137,
            119,
            182,
            141,
            249,
            112,
            120,
            33,
            183,
            168,
            64,
            249,
            68,
            223,
            225,
            86,
            7,
            250,
            251,
            90,
            23,
            66,
            210,
            0,
            124,
            171,
            131,
            62,
            37,
            147,
            105,
            199,
            23,
            24,
            163,
            142,
            133,
            72,
            19,
            203,
            140,
            100,
            54,
            86,
            249,
            228,
            131,
            204,
            183,
            63,
            130,
            12,
            28,
            71,
            214,
            33,
            57,
            66,
            244,
            232,
            226,
            94,
            200
        }, false);
        this.str104 = UnknownClass2.smethod_16(new byte[]
        {
            122,
            147,
            245,
            167,
            16,
            208,
            192,
            95,
            93,
            131,
            117,
            121,
            243,
            81,
            6,
            41,
            56,
            40,
            181,
            66,
            132,
            96,
            223,
            227,
            133,
            172,
            186,
            245,
            82,
            105,
            217,
            58,
            63,
            23,
            86,
            222,
            170,
            236,
            181,
            238,
            119,
            102,
            124,
            238,
            173,
            93,
            109,
            1,
            59,
            7,
            140,
            92,
            117,
            45,
            11,
            200,
            187,
            92,
            232,
            107,
            164,
            106,
            189,
            33,
            172,
            236,
            212,
            79,
            39,
            65,
            129,
            238,
            180,
            69,
            51,
            173,
            44,
            68,
            198,
            66,
            58,
            223,
            65,
            15,
            224,
            byte.MaxValue,
            145,
            64,
            58,
            147,
            22,
            9,
            225,
            75,
            21,
            64,
            129,
            254,
            173,
            103,
            135,
            32,
            225,
            61,
            245,
            23,
            153,
            115,
            94,
            171,
            62,
            153,
            101,
            224,
            179,
            254,
            65,
            26,
            248,
            206,
            221,
            0,
            229,
            28,
            33,
            118,
            36,
            174
        }, false);
        this.str105 = UnknownClass2.smethod_16(new byte[]
        {
            68,
            0,
            153,
            62,
            216,
            113,
            136,
            183,
            127,
            116,
            46,
            147,
            150,
            5,
            183,
            155,
            246,
            37,
            30,
            180,
            114,
            67,
            219,
            231,
            248,
            175,
            225,
            198,
            57,
            163,
            38,
            230,
            211,
            151,
            104,
            33,
            126,
            98,
            92,
            213,
            76,
            233,
            177,
            27,
            99,
            101,
            160,
            129,
            99,
            73,
            44,
            250,
            156,
            167,
            70,
            167,
            138,
            243,
            184,
            211,
            49,
            85,
            76,
            31,
            117,
            207,
            102,
            214,
            70,
            133,
            109,
            236,
            19,
            84,
            74,
            65,
            64,
            222,
            163,
            230,
            80,
            220,
            186,
            248,
            98,
            46,
            249,
            112,
            105,
            10,
            223,
            83,
            128,
            36,
            231,
            8,
            153,
            148,
            125,
            161,
            32,
            39,
            158,
            79,
            3,
            39,
            223,
            3,
            30,
            147,
            117,
            55,
            97,
            9,
            208,
            86,
            120,
            172,
            71,
            160,
            40,
            114,
            160,
            byte.MaxValue,
            byte.MaxValue,
            207,
            193,
            60
        }, false);
        this.str106 = UnknownClass2.smethod_16(new byte[]
        {
            0,
            191,
            16,
            10,
            30,
            153,
            6,
            214,
            237,
            182,
            184,
            172,
            218,
            180,
            105,
            86,
            254,
            184,
            35,
            167,
            byte.MaxValue,
            8,
            11,
            225,
            126,
            27,
            158,
            139,
            101,
            175,
            198,
            250,
            121,
            3,
            107,
            0,
            134,
            216,
            17,
            235,
            95,
            99,
            85,
            80,
            21,
            207,
            37,
            213,
            79,
            191,
            190,
            94,
            184,
            244,
            133,
            236,
            215,
            43,
            152,
            45,
            238,
            198,
            222,
            59,
            228,
            62,
            189,
            47,
            204,
            86,
            4,
            48,
            146,
            104,
            107,
            51,
            198,
            23,
            205,
            122,
            190,
            145,
            136,
            203,
            88,
            159,
            1,
            124,
            99,
            73,
            185,
            41,
            75,
            213,
            7,
            191,
            135,
            197,
            115,
            250,
            78,
            222,
            199,
            238,
            16,
            245,
            19,
            165,
            222,
            3,
            144,
            208,
            175,
            29,
            39,
            110,
            151,
            217,
            180,
            67,
            157,
            23,
            237,
            159,
            1,
            51,
            191,
            177
        }, false);
        this.str107 = UnknownClass2.smethod_16(new byte[]
        {
            134,
            80,
            228,
            58,
            179,
            60,
            31,
            124,
            184,
            252,
            69,
            163,
            36,
            215,
            118,
            211,
            202,
            99,
            199,
            250,
            194,
            118,
            141,
            239,
            77,
            6,
            6,
            225,
            94,
            91,
            28,
            194,
            220,
            232,
            96,
            237,
            32,
            161,
            190,
            144,
            202,
            151,
            135,
            104,
            199,
            114,
            184,
            238,
            242,
            226,
            243,
            199,
            69,
            115,
            252,
            12,
            139,
            130,
            48,
            181,
            128,
            186,
            7,
            191,
            68,
            163,
            71,
            16,
            199,
            28,
            109,
            208,
            145,
            162,
            111,
            249,
            2,
            25,
            144,
            86,
            219,
            63,
            70,
            244,
            53,
            171,
            41,
            83,
            209,
            100,
            217,
            204,
            160,
            192,
            59,
            1,
            229,
            191,
            176,
            3,
            162,
            202,
            105,
            200,
            42,
            118,
            248,
            102,
            115,
            29,
            193,
            238,
            184,
            112,
            101,
            145,
            119,
            10,
            69,
            36,
            141,
            208,
            245,
            86,
            241,
            42,
            41,
            245
        }, false);
        this.str108 = UnknownClass2.smethod_16(new byte[]
        {
            112,
            10,
            254,
            47,
            75,
            19,
            192,
            103,
            247,
            87,
            147,
            158,
            85,
            156,
            100,
            133,
            161,
            181,
            72,
            193,
            126,
            246,
            80,
            57,
            170,
            106,
            25,
            236,
            252,
            124,
            3,
            187,
            251,
            204,
            208,
            105,
            162,
            151,
            1,
            172,
            77,
            7,
            13,
            106,
            249,
            164,
            194,
            174,
            249,
            196,
            30,
            132,
            32,
            111,
            235,
            32,
            158,
            254,
            107,
            9,
            104,
            141,
            152,
            136,
            211,
            167,
            97,
            151,
            31,
            109,
            251,
            146,
            16,
            33,
            27,
            135,
            236,
            34,
            198,
            77,
            8,
            149,
            32,
            53,
            167,
            141,
            194,
            117,
            252,
            169,
            39,
            56,
            36,
            90,
            219,
            134,
            172,
            17,
            56,
            132,
            0,
            160,
            246,
            231,
            215,
            32,
            133,
            23,
            90,
            142,
            66,
            234,
            56,
            28,
            79,
            182,
            187,
            93,
            141,
            195,
            140,
            180,
            30,
            18,
            124,
            64,
            240,
            46
        }, false);
        this.str109 = UnknownClass2.smethod_16(new byte[]
        {
            22,
            194,
            163,
            162,
            251,
            73,
            186,
            150,
            65,
            20,
            251,
            32,
            8,
            151,
            246,
            109,
            104,
            247,
            216,
            118,
            137,
            144,
            79,
            54,
            123,
            247,
            161,
            119,
            116,
            186,
            89,
            78,
            23,
            180,
            234,
            220,
            130,
            105,
            172,
            252,
            166,
            105,
            253,
            32,
            99,
            9,
            222,
            253,
            30,
            7,
            169,
            80,
            182,
            10,
            221,
            42,
            72,
            141,
            48,
            99,
            68,
            208,
            24,
            170,
            140,
            8,
            95,
            96,
            171,
            62,
            198,
            31,
            118,
            97,
            53,
            165,
            228,
            136,
            216,
            106,
            146,
            215,
            139,
            242,
            54,
            98,
            188,
            208,
            61,
            193,
            8,
            80,
            26,
            154,
            86,
            16,
            74,
            4,
            248,
            130,
            72,
            213,
            135,
            111,
            112,
            byte.MaxValue,
            45,
            133,
            103,
            147,
            183,
            41,
            99,
            233,
            103,
            177,
            187,
            73,
            208,
            202,
            17,
            197,
            5,
            147,
            70,
            22,
            34,
            34
        }, false);
        this.str110 = UnknownClass2.smethod_16(new byte[]
        {
            38,
            234,
            71,
            4,
            69,
            35,
            169,
            181,
            184,
            227,
            221,
            119,
            160,
            224,
            39,
            37,
            211,
            124,
            215,
            156,
            162,
            251,
            141,
            236,
            201,
            213,
            232,
            254,
            223,
            87,
            44,
            158,
            246,
            231,
            82,
            254,
            25,
            87,
            75,
            74,
            87,
            162,
            66,
            88,
            173,
            236,
            14,
            184,
            170,
            211,
            165,
            129,
            86,
            122,
            232,
            185,
            18,
            224,
            96,
            187,
            201,
            224,
            192,
            175,
            129,
            232,
            0,
            byte.MaxValue,
            84,
            43,
            221,
            188,
            209,
            143,
            194,
            158,
            136,
            234,
            19,
            82,
            93,
            93,
            202,
            87,
            34,
            235,
            177,
            241,
            152,
            55,
            177,
            165,
            148,
            196,
            67,
            33,
            160,
            214,
            171,
            228,
            87,
            16,
            186,
            50,
            61,
            72,
            65,
            138,
            73,
            60,
            193,
            238,
            184,
            228,
            181,
            40,
            55,
            104,
            186,
            174,
            13,
            241,
            100,
            98,
            98,
            byte.MaxValue,
            103,
            18
        }, false);
        PopupManager.statstr1 = UnknownClass2.smethod_16(new byte[]
        {
            54,
            160,
            201,
            101,
            218,
            40,
            175,
            69,
            79,
            99,
            147,
            21,
            78,
            67,
            22,
            10,
            252,
            211,
            113,
            14,
            165,
            0,
            22,
            157,
            4,
            122,
            18,
            54,
            239,
            160,
            191,
            85,
            10,
            155,
            18,
            101,
            174,
            227,
            180,
            113,
            120,
            163,
            129,
            241,
            8,
            184,
            218,
            131,
            250,
            42,
            245,
            252,
            173,
            236,
            7,
            173,
            165,
            227,
            64,
            72,
            142,
            215,
            93,
            108,
            106,
            183,
            121,
            252,
            129,
            39,
            144,
            38,
            187,
            141,
            113,
            42,
            237,
            20,
            95,
            195,
            44,
            160,
            96,
            200,
            59,
            171,
            70,
            226,
            43,
            83,
            135,
            29,
            103,
            122,
            186,
            8,
            204,
            6,
            192,
            154,
            59,
            167,
            125,
            67,
            69,
            201,
            113,
            102,
            139,
            192,
            94,
            161,
            15,
            97,
            41,
            210,
            27,
            120,
            77,
            168,
            55,
            97,
            186,
            97,
            69,
            62,
            85,
            66
        }, false);
        this.str111 = UnknownClass2.smethod_16(new byte[]
        {
            138,
            71,
            209,
            179,
            219,
            233,
            byte.MaxValue,
            45,
            54,
            101,
            207,
            80,
            187,
            228,
            88,
            177,
            132,
            209,
            113,
            100,
            195,
            190,
            234,
            192,
            19,
            55,
            252,
            32,
            199,
            111,
            93,
            107,
            158,
            95,
            203,
            72,
            162,
            175,
            155,
            141,
            215,
            20,
            249,
            184,
            4,
            159,
            147,
            196,
            158,
            30,
            165,
            71,
            214,
            152,
            79,
            63,
            140,
            11,
            197,
            128,
            210,
            187,
            64,
            242,
            212,
            84,
            42,
            93,
            240,
            106,
            157,
            217,
            138,
            71,
            15,
            182,
            187,
            76,
            229,
            17,
            40,
            30,
            62,
            175,
            181,
            127,
            122,
            20,
            231,
            70,
            226,
            169,
            245,
            15,
            137,
            124,
            96,
            34,
            118,
            207,
            156,
            118,
            238,
            102,
            108,
            31,
            19,
            228,
            104,
            87,
            75,
            216,
            22,
            53,
            204,
            76,
            133,
            98,
            80,
            93,
            84,
            145,
            135,
            71,
            6,
            16,
            23,
            91
        }, false);
        this.str112 = "";
    }

   	public GameObject GUIWindow;

	public Text titleTF;

	public Text insideTF;

	public VerticalLayoutGroup insideContainer;

	public GameObject openedTabPrefab;

	public GameObject closedTabPrefab;

	public GameObject cardView;

	public GameObject tabs;

	public GameObject tabsRow;

	public GameObject buttonRow;

	public GameObject upView;

	public GameObject upRoboView;

	public GameObject upSkillView;

	public GameObject upInstallView;

	public GameObject upInstallContent;

	public GameObject crystallSection;

	public GameObject canvasGUI;

	public GameObject paint;

	public GameObject inventory;

	public GameObject inventoryItemPrefab;

	public Button upInstallPrefab;

	public Button upButton;

	public Button adminButton;

	public Button deleteButton;

	public Button lockButton;

	public Sprite lockButtonLocked;

	public Sprite lockButtonUnlocked;

	public Button upExitButton;

	public Button exitButton;

	public Image upImage;

	public Text upText;

	public MyInputField inputText;

	public GameObject fillLinePrefab;

	public GameObject buttonLinePrefab;

	public GameObject clanLinePrefab;

	public GameObject toggleLinePrefab;

	public GameObject uintLinePrefab;

	public GameObject dropdownLinePrefab;

	public GameObject textLinePrefab;

	public GameObject cardLinePrefab;

	public GameObject cardPrefab;

	public GameObject canvasButtonPrefab;

	public GameObject canvasMicroButtonPrefab;

	public GameObject canvasTextFieldPrefab;

	public GameObject canvasWebImagePrefab;

	public GameObject canvasRectPrefab;

	public GameObject canvasLinePrefab;

	public GameObject canvasTPButtonPrefab;

	public GameObject multitextPrefab;

	public GameObject centeredImagePrefab;

	public GameObject listContent;

	public GameObject richContent;

	public GameObject scrollView;

	public GameObject skillButtonPrefab;

	public GameObject pad340;

	public Button[] buttons;

	public Button backButton;

	public static PopupManager THIS;

	private bool disableKeyboard;

	private string backButtonAction = "";

	private string str101 = "";

	private string str102 = "";

	private string str103 = "";

	private string str104 = "";

	private string str105 = "";

	private string str106 = "";

	private string str107 = "";

	private string str108 = "";

	private string str109 = "";

	private string str110 = "";

	private static string statstr1 = "";

	private string str111 = "";

	private string str112 = "";

	private char ch1 = ':';

	private HORBConfig horbcfg;

	private UPConfig upcfg;

	private string mode = "horb";

	public static int[] slotPositions = new int[]
	{
		20,
		110,
		21,
		153,
		38,
		75,
		38,
		191,
		74,
		94,
		74,
		169,
		59,
		132,
		100,
		132,
		51,
		22,
		93,
		36,
		51,
		241,
		93,
		226,
		281,
		50,
		256,
		121,
		312,
		23,
		307,
		87,
		377,
		27,
		376,
		76,
		374,
		137,
		375,
		176,
		165,
		46,
		145,
		131,
		185,
		142,
		332,
		153,
		203,
		24,
		248,
		76,
		341,
		57,
		343,
		103,
		138,
		87,
		248,
		17,
		297,
		133,
		214,
		115,
		175,
		98,
		205,
		68
	};

	public GameObject[] richListObjects;

	public GameObject blinkingObject;

	private bool fixScroll;

	private string fixScrollTag = "";

	private bool bigInput;

	private Dictionary<string, float> fixScrolls = new Dictionary<string, float>();

	private string lastInputString = "";
    public string actiontp;

    public string action;

    public string action2;

    [CompilerGenerated]
	private sealed class їїљњљјњљјїљјјјїїљњњїїњї
	{
		internal void _003CStart_003Eb__2(BaseEventData e)
		{
			this._003C_003E4__this.CommonButtonListener(this.buttonName, this.buttonNum);
		}

		public string buttonName;

		public int buttonNum;

		public PopupManager _003C_003E4__this;
	}

	[CompilerGenerated]
	private sealed class їљјљјїњїљјїљїїјїљњјїљњј
	{
		public PopupManager _003C_003E4__this;

		public UPConfig cfg;
	}

	[CompilerGenerated]
	private sealed class їњјњјјјїїјњљїїњїїљїїњљј
	{
		internal void _003CShowUP_003Eb__0()
		{
			this.CS_0024_003C_003E8__locals1._003C_003E4__this.CommonButtonListener("tu", this.actionLabel);
		}

		public int actionLabel;

		public PopupManager.їљјљјїњїљјїљїїјїљњјїљњј CS_0024_003C_003E8__locals1;
	}

	[CompilerGenerated]
	private sealed class јјљјїјљјљјљљљјњјњњїљїјљ
	{
		internal void _003CShowUP_003Eb__1()
		{
			this.CS_0024_003C_003E8__locals2._003C_003E4__this.OnSkill(this.num);
		}

		public int num;

		public PopupManager.їљјљјїњїљјїљїїјїљњјїљњј CS_0024_003C_003E8__locals2;
	}

	[CompilerGenerated]
	private sealed class їњљјљњјїњњјїїїїјљљњјјїї
	{
		internal void _003CShowUP_003Eb__2()
		{
			this.CS_0024_003C_003E8__locals3._003C_003E4__this.OnInstall(this.shortCode, this.CS_0024_003C_003E8__locals3.cfg.slot);
		}

		public string shortCode;

		public PopupManager.їљјљјїњїљјїљїїјїљњјїљњј CS_0024_003C_003E8__locals3;
	}

	[CompilerGenerated]
	private sealed class їљљљјњљјљњњљњјјњњњљјљњњ
	{
		public PopupManager _003C_003E4__this;

		public string InvButton;
	}

	[CompilerGenerated]
	private sealed class љњњњїїїљљїїњїљњјљљјљјїї
	{
		internal void _003CShowHORB_003Eb__0()
		{
			this.CS_0024_003C_003E8__locals1._003C_003E4__this.SimpleButtonListener(this.actiontp);
		}

		internal void _003CShowHORB_003Eb__1()
		{
			this.CS_0024_003C_003E8__locals1._003C_003E4__this.SimpleButtonListener(this.action);
		}

		internal void _003CShowHORB_003Eb__2()
		{
			this.CS_0024_003C_003E8__locals1._003C_003E4__this.SimpleButtonListener(this.action2);
		}

		public string actiontp;

		public string action;

		public string action2;

		public PopupManager.їљљљјњљјљњњљњјјњњњљјљњњ CS_0024_003C_003E8__locals1;
	}

	[CompilerGenerated]
	private sealed class їњљњїїљїїјїњњљљїљїљјїїј
	{
		internal void _003CShowHORB_003Eb__3()
		{
			ServerTime.THIS.SendTypicalMessage(ClientController.THIS.TimeOfMove(), this.CS_0024_003C_003E8__locals2._003C_003E4__this.str102, 0, 0, string.Concat(new string[]
			{
				this.CS_0024_003C_003E8__locals2._003C_003E4__this.str101,
				this.CS_0024_003C_003E8__locals2.InvButton,
				this.CS_0024_003C_003E8__locals2._003C_003E4__this.str111,
				this.type,
				this.CS_0024_003C_003E8__locals2._003C_003E4__this.str110
			}));
		}

		public string type;

		public PopupManager.їљљљјњљјљњњљњјјњњњљјљњњ CS_0024_003C_003E8__locals2;
	}

	[CompilerGenerated]
	private sealed class јїљњјњљїїњњјјњјњљњњњјїї
	{
		internal void _003CShowHORB_003Eb__4()
		{
			ServerTime.THIS.SendTypicalMessage(ClientController.THIS.TimeOfMove(), this.CS_0024_003C_003E8__locals3._003C_003E4__this.str102, 0, 0, string.Concat(new object[]
			{
				this.CS_0024_003C_003E8__locals3._003C_003E4__this.str101,
				this.CS_0024_003C_003E8__locals3.InvButton,
				this.CS_0024_003C_003E8__locals3._003C_003E4__this.str111,
				this.type,
				this.CS_0024_003C_003E8__locals3._003C_003E4__this.str110
			}));
		}

		public int type;

		public PopupManager.їљљљјњљјљњњљњјјњњњљјљњњ CS_0024_003C_003E8__locals3;
	}

	[CompilerGenerated]
	private sealed class јїїљљїњњњјњњїїњјїњњјљњї
	{
		internal void _003CShowHORB_003Eb__5()
		{
			this.CS_0024_003C_003E8__locals4._003C_003E4__this.CommonButtonListener("th", this.actionLabel);
		}

		public int actionLabel;

		public PopupManager.їљљљјњљјљњњљњјјњњњљјљњњ CS_0024_003C_003E8__locals4;
	}

	[CompilerGenerated]
	private sealed class њљљљљїїљїњњїљњјїїјјљјњї
	{
		internal void _003CShowHORB_003Eb__6()
		{
			this.CS_0024_003C_003E8__locals5._003C_003E4__this.SimpleButtonListener(this.action);
		}

		public string action;

		public PopupManager.їљљљјњљјљњњљњјјњњњљјљњњ CS_0024_003C_003E8__locals5;
	}

	[CompilerGenerated]
	private sealed class љњїјїњјїјїљїїњїјљїїљїјї
	{
		internal void _003CShowHORB_003Eb__7()
		{
			this.CS_0024_003C_003E8__locals6._003C_003E4__this.SimpleButtonListener(this.resp);
		}

		public string resp;

		public PopupManager.їљљљјњљјљњњљњјјњњњљјљњњ CS_0024_003C_003E8__locals6;
	}

	[CompilerGenerated]
	private sealed class їњјљљїјљїљљњљїњњњјїљїњј
	{
		internal void _003CShowHORB_003Eb__8()
		{
			this.CS_0024_003C_003E8__locals7.CS_0024_003C_003E8__locals6._003C_003E4__this.SimpleButtonListener(this.fillparts[3]);
		}

		internal void _003CShowHORB_003Eb__9()
		{
			this.CS_0024_003C_003E8__locals7.CS_0024_003C_003E8__locals6._003C_003E4__this.SimpleButtonListener(this.fillparts[4]);
		}

		internal void _003CShowHORB_003Eb__10()
		{
			this.CS_0024_003C_003E8__locals7.CS_0024_003C_003E8__locals6._003C_003E4__this.SimpleButtonListener(this.fillparts[5]);
		}

		public string[] fillparts;

		public PopupManager.љњїјїњјїјїљїїњїјљїїљїјї CS_0024_003C_003E8__locals7;
	}

	[CompilerGenerated]
	private sealed class їїљњњљїїњњњњјјјљјњїїјїї
	{
		internal void _003CShowHORB_003Eb__11()
		{
			this.CS_0024_003C_003E8__locals8.CS_0024_003C_003E8__locals7.CS_0024_003C_003E8__locals6._003C_003E4__this.SimpleButtonListener(this.buttonResp);
		}

		public string buttonResp;

		public PopupManager.їњјљљїјљїљљњљїњњњјїљїњј CS_0024_003C_003E8__locals8;
	}

	[CompilerGenerated]
	private sealed class їњљјљїїљљїњљљљљїњїјњїњј
	{
		internal void _003CShowHORB_003Eb__12()
		{
			this.CS_0024_003C_003E8__locals9._003C_003E4__this.CommonButtonListener("l", this.num);
		}

		public int num;

		public PopupManager.їљљљјњљјљњњљњјјњњњљјљњњ CS_0024_003C_003E8__locals9;
	}

	[CompilerGenerated]
	private sealed class љјњњјјњљјїњјїљљљјјјљјјљ
	{
		internal void _003CShowHORB_003Eb__13()
		{
			this.CS_0024_003C_003E8__locals10._003C_003E4__this.SimpleButtonListener(this.action);
		}

		public string action;

		public PopupManager.їљљљјњљјљњњљњјјњњњљјљњњ CS_0024_003C_003E8__locals10;
	}
}

