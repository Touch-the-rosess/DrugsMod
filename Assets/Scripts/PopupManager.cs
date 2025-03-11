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
                    gameObject.transform.SetParent(this.tabsRow.transform, worldPositionStays: false);
                    gameObject.GetComponentInChildren<Text>().text = cfg.tabs[i];
                }
                else
                {
                    GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.closedTabPrefab);
                    gameObject2.transform.SetParent(this.tabsRow.transform, worldPositionStays: false);
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
        ShowHORBLocalData localData = new ShowHORBLocalData();
        localData.popupManager = this;
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
        localData.InvButton = "choose";
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
                                localData.InvButton = text2;
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
                                                    this.SimpleButtonListener(actiontp);
                                                });
                                                j++;
                                                break;
                                            }
                                        case "b":
                                            {
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
                                                    this.SimpleButtonListener(action);
                                                });
                                                j++;
                                                break;
                                            }
                                        case "B":
                                            {
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
                                                    this.SimpleButtonListener(action2);
                                                });
                                                j++;
                                                break;
                                            }
                                        case "T":
                                            {
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
                                    k = text4.Length;
                                    break;
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
                            ServerTime.THIS.SendTypicalMessage(ClientController.THIS.TimeOfMove(), localData.popupManager.str102, 0, 0, string.Concat(new string[]
                            {
                                localData.popupManager.str101,
                                localData.InvButton,
                                localData.popupManager.str111,
                                type,
                                localData.popupManager.str110
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
                            ServerTime.THIS.SendTypicalMessage(ClientController.THIS.TimeOfMove(), localData.popupManager.str102, 0, 0, string.Concat(new object[]
                            {
                                localData.popupManager.str101,
                                localData.InvButton,
                                localData.popupManager.str111,
                                type,
                                localData.popupManager.str110
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
                        localData.popupManager.CommonButtonListener("th", actionLabel);
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
                    localData.popupManager.SimpleButtonListener(action);
                });
            }
        }
        else if (cfg.richList != null && cfg.richList.Length != 0)
        {
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
                RichListHandler richHandler = new RichListHandler();
                richHandler.localData = localData;
                string text12 = cfg.richList[5 * num21];
                string text13 = cfg.richList[5 * num21 + 1];
                string text14 = cfg.richList[5 * num21 + 2];
                richHandler.resp = cfg.richList[5 * num21 + 3];
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
                            string[] array12 = richHandler.resp.Split(new char[]
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
                                    richHandler.localData.popupManager.SimpleButtonListener(buttonResp);
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
                                localData.popupManager.SimpleButtonListener(richHandler.resp);
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
                                richHandler.localData.popupManager.SimpleButtonListener(fillparts[3]);
                            });
                            componentsInChildren3[1].onClick.AddListener(delegate ()
                            {
                                richHandler.localData.popupManager.SimpleButtonListener(fillparts[4]);
                            });
                            componentsInChildren3[2].onClick.AddListener(delegate ()
                            {
                                richHandler.localData.popupManager.SimpleButtonListener(fillparts[5]);
                            });
                            this.richListObjects[num21] = gameObject20;
                            break;
                        }
                }
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
                        localData.popupManager.CommonButtonListener("l", num);
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
                            localData.popupManager.SimpleButtonListener(action);
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
    private string str101 = "{\"b\":\"";
    private string str102 = "GUI_";
    private string str103 = "exit";
    private string str104 = "back";
    private string str105 = "ADMN";
    private string str106 = "</color>";
    private string str107 = "%C%";
    private string str108 = "</size>";
    private string str109 = "%S%";
    private string str110 = "\"}";
    private static string statstr1 = "#";
    private string str111 = ":";
    private string str112 = "";

    private char ch1 = ':';

    private HORBConfig horbcfg;

    private UPConfig upcfg;

    private string mode = "horb";

    public static int[] slotPositions = new int[]
    {
        20, 110, 21, 153, 38, 75, 38, 191, 74, 94,
        74, 169, 59, 132, 100, 132, 51, 22, 93, 36,
        51, 241, 93, 226, 281, 50, 256, 121, 312, 23,
        307, 87, 377, 27, 376, 76, 374, 137, 375, 176,
        165, 46, 145, 131, 185, 142, 332, 153, 203, 24,
        248, 76, 341, 57, 343, 103, 138, 87, 248, 17,
        297, 133, 214, 115, 175, 98, 205, 68
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
    private sealed class ShowHORBLocalData
    {
        public PopupManager popupManager;
        public string InvButton;
    }

    [CompilerGenerated]
    private sealed class RichListHandler
    {
        public ShowHORBLocalData localData;
        public string resp;
    }
}