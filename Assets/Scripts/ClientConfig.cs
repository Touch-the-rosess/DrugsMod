using System;
using System.Collections.Generic;
using UnityEngine;

public class ClientConfig
{
    public static void Toggle(int button)
	{
		if (!ClientConfig.ToggleStates.ContainsKey(button))
		{
			ClientConfig.ToggleStates.Add(button, -1);
		}
		Dictionary<int, int> toggleStates = ClientConfig.ToggleStates;
		int num = toggleStates[button];
		toggleStates[button] = num + 1;
		if (ClientConfig.ToggleStates[button] >= ClientConfig.toggleLists[button].Count)
		{
			ClientConfig.ToggleStates[button] = 0;
		}
		UnityEngine.Debug.Log(string.Concat(new object[]
		{
			"toggle ",
			button,
			" > ",
			ClientConfig.ToggleStates[button],
			"/",
			ClientConfig.toggleLists[button].Count,
			"  ?",
			ClientConfig.toggleList1.Count
		}));
		if (ClientConfig.toggleLists[button].Count > 0)
		{
			List<string> list = ClientConfig.toggleLists[button][ClientConfig.ToggleStates[button]];
			for (int i = 0; i < list.Count; i += 3)
			{
				string a = list[i];
				string name = list[i + 1];
				string text = list[i + 2];
				if (!(a == "O"))
				{
					if (!(a == "="))
					{
						if (a == "+")
						{
							ClientConfig.ParseBoolean(name, text == "+");
						}
					}
					else
					{
						ClientConfig.ParseEquals(name, text);
					}
				}
				else
				{
					ClientConfig.ParseOperator(name);
				}
			}
		}
	}

    public static void EndConfig()
    {
        ClientConfig.TOGGLE_UPDATING = false;
    }

    public static void SetDefaults()
    {
        GUIManager.THIS.mapButton.gameObject.SetActive(true);
        GUIManager.THIS.AccountPanel.SetActive(true);
        MiniSkillManager.THIS.gameObject.SetActive(true);
        MapViewer.ResetCustomTable();
        ClientConfig.CHAT_SHOW_ID = true;
        ClientConfig.CHAT_SHOW_TIME = true;
        ClientConfig.CHAT_SHOW_NICK = true;
        ClientConfig.SHOW_MY_NICK = false;
        ClientConfig.OLD_PROGRAM_FORMAT = false;
        ClientConfig.noDistortion = false;
        ClientConfig.toggleList1.Clear();
        ClientConfig.toggleList2.Clear();
        ClientConfig.toggleList3.Clear();
        ClientConfig.toggleList4.Clear();
        ClientConfig.toggleList5.Clear();
        ClientConfig.toggleList6.Clear();
        ClientConfig.toggleList7.Clear();
        ClientConfig.toggleList8.Clear();
        ClientConfig.toggleList9.Clear();
        ClientConfig.toggleList0.Clear();
        ClientConfig.TOGGLE_UPDATING = false;
        ClientConfig.mouseDefMaxLen = int.MaxValue;
        ClientConfig.mouseDefMaxStack = int.MaxValue;
        ClientConfig.mouseDefR = 2000;
        ClientConfig.mouseR = ClientConfig.mouseDefR;
        ClientConfig.mouseMaxLen = ClientConfig.mouseDefMaxLen;
        ClientConfig.mouseMaxStack = ClientConfig.mouseDefMaxStack;
        ClientConfig.mouseMapMaxLen = int.MaxValue;
        ClientConfig.mouseMapMaxStack = int.MaxValue;
        ClientConfig.mouseMapR = 2000;
        ClientConfig.mouseNoDig = false;
        ClientConfig.gunRadius = true;
        ClientConfig.MOVE_UP_KEY = KeyCode.W;
        ClientConfig.MOVE_DOWN_KEY = KeyCode.S;
        ClientConfig.MOVE_LEFT_KEY = KeyCode.A;
        ClientConfig.MOVE_RIGHT_KEY = KeyCode.D;
        ClientConfig.DIGG_KEY = KeyCode.Z;
        ClientConfig.WARBLOCK_KEY = KeyCode.Y;
        ClientConfig.BLOCK_KEY = KeyCode.F;
        ClientConfig.ROAD_KEY = KeyCode.H;
        ClientConfig.QUADRO_KEY = KeyCode.J;
        ClientConfig.HEAL_KEY = KeyCode.V;
        ClientConfig.GEO_KEY = KeyCode.G;
        ClientConfig.AGR_KEY = KeyCode.L;
        ClientConfig.AUTODIG_KEY = KeyCode.E;
        ClientConfig.MAP_KEY = KeyCode.M;
        ClientConfig.LOCALCHAT_KEY = KeyCode.T;
        ClientConfig.PROG_KEY = KeyCode.R;
        ClientConfig.INV_KEY = KeyCode.I;
        ClientConfig.SOUND_BASKET = true;
        ClientConfig.SOUND_SIGNAL = true;
        ClientConfig.SOUND_BOMB = true;
        ClientConfig.SOUND_BOMBTICK = true;
        ClientConfig.SOUND_DEATH = true;
        ClientConfig.SOUND_DESTROY = true;
        ClientConfig.SOUND_EMI = true;
        ClientConfig.SOUND_GEOLOGY = true;
        ClientConfig.SOUND_HEAL = true;
        ClientConfig.SOUND_HURT = true;
        ClientConfig.SOUND_MINING = true;
        ClientConfig.SOUND_DIZZ = true;
        ClientConfig.SOUND_TP_IN = true;
        ClientConfig.SOUND_TP_OUT = true;
        ClientConfig.SOUND_VOLC = true;
        ClientConfig.SOUND_C190 = true;
        ClientController.THIS.boomPool.max_size = 30;
        ClientController.THIS.gunShotPool.max_size = 50;
        ClientController.THIS.crysPlusPool.max_size = 20;
        ClientController.THIS.bzPool.max_size = 20;
    }

    public static void ParseBoolean(string name, bool value)
    {
        if (TOGGLE_UPDATING)
        {
            currentToggleList.Add("+");
            currentToggleList.Add(name);
            currentToggleList.Add(value ? "+" : "-");
            UnityEngine.Debug.Log("currentToggleList.Count=" + currentToggleList.Count);
            return;
        }
        UnityEngine.Debug.Log(" ParseBoolean " + name + " " + value.ToString());
        switch (name.ToLower())
        {
            case "chatid":
                CHAT_SHOW_ID = value;
                ChatManager.THIS.UpdateChatStyle();
                break;
            case "chattime":
                CHAT_SHOW_TIME = value;
                ChatManager.THIS.UpdateChatStyle();
                break;
            case "chatnick":
                CHAT_SHOW_NICK = value;
                ChatManager.THIS.UpdateChatStyle();
                break;
            case "sfxbasket":
                SOUND_BASKET = value;
                break;
            case "sfxsignal":
                SOUND_SIGNAL = value;
                break;
            case "sfxbomb":
                SOUND_BOMB = value;
                break;
            case "sfxtick":
                SOUND_BOMBTICK = value;
                break;
            case "sfxdeath":
                SOUND_DEATH = value;
                break;
            case "sfxdestroy":
                SOUND_DESTROY = value;
                break;
            case "sfxemi":
                SOUND_EMI = value;
                break;
            case "sfxgeo":
                SOUND_GEOLOGY = value;
                break;
            case "sfxheal":
                SOUND_HEAL = value;
                break;
            case "sfxhurt":
                SOUND_HURT = value;
                break;
            case "sfxmine":
                SOUND_MINING = value;
                break;
            case "sfxdizz":
                SOUND_DIZZ = value;
                break;
            case "sfxtpin":
                SOUND_TP_IN = value;
                break;
            case "sfxtpout":
                SOUND_TP_OUT = value;
                break;
            case "sfxvolc":
                SOUND_VOLC = value;
                break;
            case "sfxc190":
                SOUND_C190 = value;
                break;
            case "map":
                GUIManager.THIS.mapButton.gameObject.SetActive(value);
                break;
            case "acc":
                GUIManager.THIS.AccountPanel.SetActive(value);
                break;
            case "showmynick":
                SHOW_MY_NICK = value;
                break;
            case "mousenodig":
            case "mousenodigg":
                mouseNoDig = value;
                break;
            case "radius":
            case "gunr":
            case "gunradius":
                gunRadius = value;
                break;
            case "skills":
                MiniSkillManager.THIS.gameObject.SetActive(value);
                break;
            case "plain":
                noDistortion = value;
                break;
            case "oldprogramformat":
                OLD_PROGRAM_FORMAT = value;
                break;
        }
    }
        
    public static KeyCode TranslateCode(string input)
    {
        switch (input.ToLower())
        {
            case "f1":
                return KeyCode.F1;
            case "f2":
                return KeyCode.F2;
            case "f3":
                return KeyCode.F3;
            case "f4":
                return KeyCode.F4;
            case "f5":
                return KeyCode.F5;
            case "f6":
                return KeyCode.F6;
            case "f7":
                return KeyCode.F7;
            case "f8":
                return KeyCode.F8;
            case "f9":
                return KeyCode.F9;
            case "f10":
                return KeyCode.F10;
            case "f11":
                return KeyCode.F11;
            case "f12":
                return KeyCode.F12;
            case "q":
                return KeyCode.Q;
            case "w":
                return KeyCode.W;
            case "e":
                return KeyCode.E;
            case "r":
                return KeyCode.R;
            case "t":
                return KeyCode.T;
            case "y":
                return KeyCode.Y;
            case "u":
                return KeyCode.U;
            case "i":
                return KeyCode.I;
            case "o":
                return KeyCode.O;
            case "p":
                return KeyCode.P;
            case "a":
                return KeyCode.A;
            case "s":
                return KeyCode.S;
            case "d":
                return KeyCode.D;
            case "f":
                return KeyCode.F;
            case "g":
                return KeyCode.G;
            case "h":
                return KeyCode.H;
            case "j":
                return KeyCode.J;
            case "k":
                return KeyCode.K;
            case "l":
                return KeyCode.L;
            case "z":
                return KeyCode.Z;
            case "x":
                return KeyCode.X;
            case "c":
                return KeyCode.C;
            case "v":
                return KeyCode.V;
            case "b":
                return KeyCode.B;
            case "n":
                return KeyCode.N;
            case "m":
                return KeyCode.M;
            case "}":
                return KeyCode.RightCurlyBracket;
            case "{":
                return KeyCode.LeftCurlyBracket;
            case "<":
                return KeyCode.Less;
            case ">":
                return KeyCode.Greater;
            case "-":
                return KeyCode.Minus;
            case "+":
                return KeyCode.Plus;
            default:
                return KeyCode.None;
        }
        /*string text = input.ToLower();
        uint num = _003CPrivateImplementationDetails_003E.ComputeStringHash(text);
        if (num <= 3909890315u)
        {
            if (num <= 772578730u)
            {
                if (num <= 388133425u)
                {
                    if (num <= 220357235u)
                    {
                        if (num != 203579616u)
                        {
                            if (num == 220357235u)
                            {
                                if (text == "f8")
                                {
                                    return KeyCode.F8;
                                }
                            }
                        }
                        else if (text == "f9")
                        {
                            return KeyCode.F9;
                        }
                    }
                    else if (num != 337800568u)
                    {
                        if (num != 371355806u)
                        {
                            if (num == 388133425u)
                            {
                                if (text == "f2")
                                {
                                    return KeyCode.F2;
                                }
                            }
                        }
                        else if (text == "f3")
                        {
                            return KeyCode.F3;
                        }
                    }
                    else if (text == "f1")
                    {
                        return KeyCode.F1;
                    }
                }
                else if (num <= 438466282u)
                {
                    if (num != 404911044u)
                    {
                        if (num != 421688663u)
                        {
                            if (num == 438466282u)
                            {
                                if (text == "f7")
                                {
                                    return KeyCode.F7;
                                }
                            }
                        }
                        else if (text == "f4")
                        {
                            return KeyCode.F4;
                        }
                    }
                    else if (text == "f5")
                    {
                        return KeyCode.F5;
                    }
                }
                else if (num != 455243901u)
                {
                    if (num != 671913016u)
                    {
                        if (num == 772578730u)
                        {
                            if (text == "+")
                            {
                                return KeyCode.Plus;
                            }
                        }
                    }
                    else if (text == "-")
                    {
                        return KeyCode.Minus;
                    }
                }
                else if (text == "f6")
                {
                    return KeyCode.F6;
                }
            }
            else if (num <= 3792446982u)
            {
                if (num <= 990687777u)
                {
                    if (num != 957132539u)
                    {
                        if (num == 990687777u)
                        {
                            if (text == ">")
                            {
                                return KeyCode.Greater;
                            }
                        }
                    }
                    else if (text == "<")
                    {
                        return KeyCode.Less;
                    }
                }
                else if (num != 3758891744u)
                {
                    if (num != 3775669363u)
                    {
                        if (num == 3792446982u)
                        {
                            if (text == "g")
                            {
                                return KeyCode.G;
                            }
                        }
                    }
                    else if (text == "d")
                    {
                        return KeyCode.D;
                    }
                }
                else if (text == "e")
                {
                    return KeyCode.E;
                }
            }
            else if (num <= 3859557458u)
            {
                if (num != 3809224601u)
                {
                    if (num != 3826002220u)
                    {
                        if (num == 3859557458u)
                        {
                            if (text == "c")
                            {
                                return KeyCode.C;
                            }
                        }
                    }
                    else if (text == "a")
                    {
                        return KeyCode.A;
                    }
                }
                else if (text == "f")
                {
                    return KeyCode.F;
                }
            }
            else if (num != 3876335077u)
            {
                if (num != 3893112696u)
                {
                    if (num == 3909890315u)
                    {
                        if (text == "l")
                        {
                            return KeyCode.L;
                        }
                    }
                }
                else if (text == "m")
                {
                    return KeyCode.M;
                }
            }
            else if (text == "b")
            {
                return KeyCode.B;
            }
        }
        else if (num <= 4094444124u)
        {
            if (num <= 3993778410u)
            {
                if (num <= 3943445553u)
                {
                    if (num != 3926667934u)
                    {
                        if (num == 3943445553u)
                        {
                            if (text == "n")
                            {
                                return KeyCode.N;
                            }
                        }
                    }
                    else if (text == "o")
                    {
                        return KeyCode.O;
                    }
                }
                else if (num != 3960223172u)
                {
                    if (num != 3977000791u)
                    {
                        if (num == 3993778410u)
                        {
                            if (text == "k")
                            {
                                return KeyCode.K;
                            }
                        }
                    }
                    else if (text == "h")
                    {
                        return KeyCode.H;
                    }
                }
                else if (text == "i")
                {
                    return KeyCode.I;
                }
            }
            else if (num <= 4044111267u)
            {
                if (num != 4010556029u)
                {
                    if (num != 4027333648u)
                    {
                        if (num == 4044111267u)
                        {
                            if (text == "t")
                            {
                                return KeyCode.T;
                            }
                        }
                    }
                    else if (text == "u")
                    {
                        return KeyCode.U;
                    }
                }
                else if (text == "j")
                {
                    return KeyCode.J;
                }
            }
            else if (num != 4060888886u)
            {
                if (num != 4077666505u)
                {
                    if (num == 4094444124u)
                    {
                        if (text == "q")
                        {
                            return KeyCode.Q;
                        }
                    }
                }
                else if (text == "v")
                {
                    return KeyCode.V;
                }
            }
            else if (text == "w")
            {
                return KeyCode.W;
            }
        }
        else if (num <= 4197582936u)
        {
            if (num <= 4127999362u)
            {
                if (num != 4111221743u)
                {
                    if (num == 4127999362u)
                    {
                        if (text == "s")
                        {
                            return KeyCode.S;
                        }
                    }
                }
                else if (text == "p")
                {
                    return KeyCode.P;
                }
            }
            else if (num != 4144776981u)
            {
                if (num != 4161554600u)
                {
                    if (num == 4197582936u)
                    {
                        if (text == "f10")
                        {
                            return KeyCode.F10;
                        }
                    }
                }
                else if (text == "}")
                {
                    return KeyCode.RightCurlyBracket;
                }
            }
            else if (text == "r")
            {
                return KeyCode.R;
            }
        }
        else if (num <= 4231138174u)
        {
            if (num != 4214360555u)
            {
                if (num != 4228665076u)
                {
                    if (num == 4231138174u)
                    {
                        if (text == "f12")
                        {
                            return KeyCode.F12;
                        }
                    }
                }
                else if (text == "y")
                {
                    return KeyCode.Y;
                }
            }
            else if (text == "f11")
            {
                return KeyCode.F11;
            }
        }
        else if (num != 4245442695u)
        {
            if (num != 4262220314u)
            {
                if (num == 4278997933u)
                {
                    if (text == "z")
                    {
                        return KeyCode.Z;
                    }
                }
            }
            else if (text == "{")
            {
                return KeyCode.LeftCurlyBracket;
            }
        }
        else if (text == "x")
        {
            return KeyCode.X;
        }
        return KeyCode.None;*/
    }

    public static int fromHex(string c)
    {
        switch (c)
        {
            case "0":
                return 0;
            case "1":
                return 1;
            case "2":
                return 2;
            case "3":
                return 3;
            case "4":
                return 4;
            case "5":
                return 5;
            case "6":
                return 6;
            case "7":
                return 7;
            case "8":
                return 8;
            case "9":
                return 9;
            case "a":
                return 10;
            case "b":
                return 11;
            case "c":
                return 12;
            case "d":
                return 13;
            case "e":
                return 14;
            case "f":
                return 15;
            default:
                return 0;
        }
        /*uint num = _003CPrivateImplementationDetails_003E.ComputeStringHash(c);
        if (num <= 923577301u)
        {
            if (num <= 856466825u)
            {
                if (num <= 822911587u)
                {
                    if (num != 806133968u)
                    {
                        if (num == 822911587u)
                        {
                            if (c == "4")
                            {
                                return 4;
                            }
                        }
                    }
                    else if (c == "5")
                    {
                        return 5;
                    }
                }
                else if (num != 839689206u)
                {
                    if (num == 856466825u)
                    {
                        if (c == "6")
                        {
                            return 6;
                        }
                    }
                }
                else if (c == "7")
                {
                    return 7;
                }
            }
            else if (num <= 890022063u)
            {
                if (num != 873244444u)
                {
                    if (num == 890022063u)
                    {
                        if (c == "0")
                        {
                            return 0;
                        }
                    }
                }
                else if (c == "1")
                {
                    return 1;
                }
            }
            else if (num != 906799682u)
            {
                if (num == 923577301u)
                {
                    if (c == "2")
                    {
                        return 2;
                    }
                }
            }
            else if (c == "3")
            {
                return 3;
            }
        }
        else if (num <= 3775669363u)
        {
            if (num <= 1024243015u)
            {
                if (num != 1007465396u)
                {
                    if (num == 1024243015u)
                    {
                        if (c == "8")
                        {
                            return 8;
                        }
                    }
                }
                else if (c == "9")
                {
                    return 9;
                }
            }
            else if (num != 3758891744u)
            {
                if (num == 3775669363u)
                {
                    if (c == "d")
                    {
                        return 13;
                    }
                }
            }
            else if (c == "e")
            {
                return 14;
            }
        }
        else if (num <= 3826002220u)
        {
            if (num != 3809224601u)
            {
                if (num == 3826002220u)
                {
                    if (c == "a")
                    {
                        return 10;
                    }
                }
            }
            else if (c == "f")
            {
                return 15;
            }
        }
        else if (num != 3859557458u)
        {
            if (num == 3876335077u)
            {
                if (c == "b")
                {
                    return 11;
                }
            }
        }
        else if (c == "c")
        {
            return 12;
        }
        return 0;*/
    }

    public static void ParseOperator(string name)
    {
        name = name.ToUpper();
        if ((name.Length < 6 || name.Substring(0, 6) != "TOGGLE") && ClientConfig.TOGGLE_UPDATING)
        {
            ClientConfig.currentToggleList.Add("O");
            ClientConfig.currentToggleList.Add(name);
            ClientConfig.currentToggleList.Add("");
            return;
        }
        /*uint num = _003CPrivateImplementationDetails_003E.ComputeStringHash(name);
        if (num <= 3008976316u)
        {
            if (num <= 930781901u)
            {
                if (num <= 880449044u)
                {
                    if (num != 846893806u)
                    {
                        if (num != 880449044u)
                        {
                            return;
                        }
                        if (!(name == "MAPMODE2"))
                        {
                            return;
                        }
                        MapViewer.THIS.modeDropdown.value = 2;
                        return;
                    }
                    else if (!(name == "MAPMODE4"))
                    {
                        return;
                    }
                }
                else if (num != 897226663u)
                {
                    if (num != 914004282u)
                    {
                        if (num != 930781901u)
                        {
                            return;
                        }
                        if (!(name == "MAPMODE1"))
                        {
                            return;
                        }
                        MapViewer.THIS.modeDropdown.value = 1;
                        return;
                    }
                    else
                    {
                        if (!(name == "MAPMODE0"))
                        {
                            return;
                        }
                        MapViewer.THIS.modeDropdown.value = 0;
                        return;
                    }
                }
                else
                {
                    if (!(name == "MAPMODE3"))
                    {
                        return;
                    }
                    MapViewer.THIS.modeDropdown.value = 3;
                    return;
                }
            }
            else
            {
                if (num > 2121063338u)
                {
                    if (num != 2406282861u)
                    {
                        if (num != 2531527371u)
                        {
                            if (num != 3008976316u)
                            {
                                return;
                            }
                            if (!(name == "CMAP"))
                            {
                                return;
                            }
                            MapViewer.ResetCustomTable();
                            MapViewer.THIS.modeDropdown.value = 4;
                            if (!MapViewer.THIS.gameObject.activeSelf)
                            {
                                MapViewer.THIS.Show();
                                return;
                            }
                            return;
                        }
                        else if (!(name == "CMAPRESET"))
                        {
                            return;
                        }
                    }
                    else if (!(name == "CMR"))
                    {
                        return;
                    }
                    MapViewer.ResetCustomTable();
                    return;
                }
                if (num != 1294831065u)
                {
                    if (num != 1732332450u)
                    {
                        if (num != 2121063338u)
                        {
                            return;
                        }
                        if (!(name == "CMC"))
                        {
                            return;
                        }
                    }
                    else
                    {
                        if (!(name == "SHOWMAP"))
                        {
                            return;
                        }
                        if (!MapViewer.THIS.gameObject.activeSelf)
                        {
                            MapViewer.THIS.Show();
                            return;
                        }
                        return;
                    }
                }
                else
                {
                    if (!(name == "HIDEMAP"))
                    {
                        return;
                    }
                    if (MapViewer.THIS.gameObject.activeSelf)
                    {
                        MapViewer.THIS.OnExit();
                        return;
                    }
                    return;
                }
            }
        }
        else if (num <= 3543319471u)
        {
            if (num <= 3492986614u)
            {
                if (num != 3459431376u)
                {
                    if (num != 3476208995u)
                    {
                        if (num != 3492986614u)
                        {
                            return;
                        }
                        if (!(name == "TOGGLE7"))
                        {
                            return;
                        }
                        ClientConfig.TOGGLE_UPDATING = true;
                        List<string> item = new List<string>();
                        ClientConfig.toggleList7.Add(item);
                        ClientConfig.currentToggleList = item;
                        return;
                    }
                    else
                    {
                        if (!(name == "TOGGLE4"))
                        {
                            return;
                        }
                        ClientConfig.TOGGLE_UPDATING = true;
                        List<string> item = new List<string>();
                        ClientConfig.toggleList4.Add(item);
                        ClientConfig.currentToggleList = item;
                        return;
                    }
                }
                else
                {
                    if (!(name == "TOGGLE5"))
                    {
                        return;
                    }
                    ClientConfig.TOGGLE_UPDATING = true;
                    List<string> item = new List<string>();
                    ClientConfig.toggleList5.Add(item);
                    ClientConfig.currentToggleList = item;
                    return;
                }
            }
            else if (num != 3509764233u)
            {
                if (num != 3526541852u)
                {
                    if (num != 3543319471u)
                    {
                        return;
                    }
                    if (!(name == "TOGGLE0"))
                    {
                        return;
                    }
                    ClientConfig.TOGGLE_UPDATING = true;
                    List<string> item = new List<string>();
                    ClientConfig.toggleList0.Add(item);
                    ClientConfig.currentToggleList = item;
                    return;
                }
                else
                {
                    if (!(name == "TOGGLE1"))
                    {
                        return;
                    }
                    ClientConfig.TOGGLE_UPDATING = true;
                    List<string> item = new List<string>();
                    ClientConfig.toggleList1.Add(item);
                    ClientConfig.currentToggleList = item;
                    return;
                }
            }
            else
            {
                if (!(name == "TOGGLE6"))
                {
                    return;
                }
                ClientConfig.TOGGLE_UPDATING = true;
                List<string> item = new List<string>();
                ClientConfig.toggleList6.Add(item);
                ClientConfig.currentToggleList = item;
                return;
            }
        }
        else if (num <= 3660762804u)
        {
            if (num != 3560097090u)
            {
                if (num != 3576874709u)
                {
                    if (num != 3660762804u)
                    {
                        return;
                    }
                    if (!(name == "TOGGLE9"))
                    {
                        return;
                    }
                    ClientConfig.TOGGLE_UPDATING = true;
                    List<string> item = new List<string>();
                    ClientConfig.toggleList9.Add(item);
                    ClientConfig.currentToggleList = item;
                    return;
                }
                else
                {
                    if (!(name == "TOGGLE2"))
                    {
                        return;
                    }
                    ClientConfig.TOGGLE_UPDATING = true;
                    List<string> item = new List<string>();
                    ClientConfig.toggleList2.Add(item);
                    ClientConfig.currentToggleList = item;
                    return;
                }
            }
            else
            {
                if (!(name == "TOGGLE3"))
                {
                    return;
                }
                ClientConfig.TOGGLE_UPDATING = true;
                List<string> item = new List<string>();
                ClientConfig.toggleList3.Add(item);
                ClientConfig.currentToggleList = item;
                return;
            }
        }
        else if (num != 3677540423u)
        {
            if (num != 3955401994u)
            {
                if (num != 4015534957u)
                {
                    return;
                }
                if (!(name == "CMAPCHOOSE"))
                {
                    return;
                }
            }
            else
            {
                if (!(name == "TOGGLEEND"))
                {
                    return;
                }
                ClientConfig.TOGGLE_UPDATING = false;
                return;
            }
        }
        else
        {
            if (!(name == "TOGGLE8"))
            {
                return;
            }
            ClientConfig.TOGGLE_UPDATING = true;
            List<string> item = new List<string>();
            ClientConfig.toggleList8.Add(item);
            ClientConfig.currentToggleList = item;
            return;
        }
        MapViewer.THIS.modeDropdown.value = 4;*/
        switch (name)
        {
            case "SHOWMAP":
                if (!MapViewer.THIS.gameObject.activeSelf)
                {
                    MapViewer.THIS.Show();
                }
                break;
            case "HIDEMAP":
                if (MapViewer.THIS.gameObject.activeSelf)
                {
                    MapViewer.THIS.OnExit();
                }
                break;
            case "CMAP":
                MapViewer.ResetCustomTable();
                MapViewer.THIS.modeDropdown.value = 4;
                if (!MapViewer.THIS.gameObject.activeSelf)
                {
                    MapViewer.THIS.Show();
                }
                break;
            case "CMR":
            case "CMAPRESET":
                MapViewer.ResetCustomTable();
                break;
            case "CMC":
            case "CMAPCHOOSE":
            case "MAPMODE4":
                MapViewer.THIS.modeDropdown.value = 4;
                break;
            case "MAPMODE0":
                MapViewer.THIS.modeDropdown.value = 0;
                break;
            case "MAPMODE1":
                MapViewer.THIS.modeDropdown.value = 1;
                break;
            case "MAPMODE2":
                MapViewer.THIS.modeDropdown.value = 2;
                break;
            case "MAPMODE3":
                MapViewer.THIS.modeDropdown.value = 3;
                break;
            case "TOGGLE1":
                {
                    TOGGLE_UPDATING = true;
                    List<string> item = new List<string>();
                    toggleList1.Add(item);
                    currentToggleList = item;
                    break;
                }
            case "TOGGLE2":
                {
                    TOGGLE_UPDATING = true;
                    List<string> item = new List<string>();
                    toggleList2.Add(item);
                    currentToggleList = item;
                    break;
                }
            case "TOGGLE3":
                {
                    TOGGLE_UPDATING = true;
                    List<string> item = new List<string>();
                    toggleList3.Add(item);
                    currentToggleList = item;
                    break;
                }
            case "TOGGLE4":
                {
                    TOGGLE_UPDATING = true;
                    List<string> item = new List<string>();
                    toggleList4.Add(item);
                    currentToggleList = item;
                    break;
                }
            case "TOGGLE5":
                {
                    TOGGLE_UPDATING = true;
                    List<string> item = new List<string>();
                    toggleList5.Add(item);
                    currentToggleList = item;
                    break;
                }
            case "TOGGLE6":
                {
                    TOGGLE_UPDATING = true;
                    List<string> item = new List<string>();
                    toggleList6.Add(item);
                    currentToggleList = item;
                    break;
                }
            case "TOGGLE7":
                {
                    TOGGLE_UPDATING = true;
                    List<string> item = new List<string>();
                    toggleList7.Add(item);
                    currentToggleList = item;
                    break;
                }
            case "TOGGLE8":
                {
                    TOGGLE_UPDATING = true;
                    List<string> item = new List<string>();
                    toggleList8.Add(item);
                    currentToggleList = item;
                    break;
                }
            case "TOGGLE9":
                {
                    TOGGLE_UPDATING = true;
                    List<string> item = new List<string>();
                    toggleList9.Add(item);
                    currentToggleList = item;
                    break;
                }
            case "TOGGLE0":
                {
                    TOGGLE_UPDATING = true;
                    List<string> item = new List<string>();
                    toggleList0.Add(item);
                    currentToggleList = item;
                    break;
                }
            case "TOGGLEEND":
                TOGGLE_UPDATING = false;
                break;
        }
    }

    public static void ParseEquals(string name, string value)
    {
        if (ClientConfig.TOGGLE_UPDATING)
        {
            ClientConfig.currentToggleList.Add("=");
            ClientConfig.currentToggleList.Add(name);
            ClientConfig.currentToggleList.Add(value);
            return;
        }
        short result = 0;
        float result2 = 0f;
        bool flag = short.TryParse(value, out result);
        bool flag2 = float.TryParse(value, out result2);
        name = name.ToLower();
        value = value.ToLower();
        if (name.Substring(0, 4) == "cmap" && short.TryParse(name.Substring(4), out result) && result > 0 && result < 255 && value.Length >= 3)
        {
            int num3 = ClientConfig.fromHex(value.Substring(0, 1));
            int num4 = ClientConfig.fromHex(value.Substring(1, 1));
            int num5 = ClientConfig.fromHex(value.Substring(2, 1));
            if (value.Length >= 5 && value.Substring(3, 1) == "!")
            {
                int num6 = ClientConfig.fromHex(value.Substring(4, 1));
                MapViewer.customBlinkRateTable[(int)result] = 2.5f * Mathf.Pow(1.3f, (float)num6);
            }
            MapViewer.customTable[(int)result] = new Color((float)num3 / 16f, (float)num4 / 16f, (float)num5 / 16f);
        }
        /*string text = name.ToLower();
        uint num7 = _003CPrivateImplementationDetails_003E.ComputeStringHash(text);
        if (num7 > 1778508743u)
        {
            if (num7 <= 2903810408u)
            {
                if (num7 <= 2344590436u)
                {
                    if (num7 <= 1969521593u)
                    {
                        if (num7 != 1827869724u)
                        {
                            if (num7 != 1969521593u)
                            {
                                return;
                            }
                            if (!(text == "mousemaxlen"))
                            {
                                return;
                            }
                            if (flag && num > 100)
                            {
                                ClientConfig.mouseDefMaxLen = (int)num;
                                ClientConfig.mouseMaxLen = ClientConfig.mouseDefMaxLen;
                                return;
                            }
                            return;
                        }
                        else
                        {
                            if (!(text == "key_local"))
                            {
                                return;
                            }
                            ClientConfig.LOCALCHAT_KEY = ClientConfig.TranslateCode(value);
                            return;
                        }
                    }
                    else if (num7 != 2104807237u)
                    {
                        if (num7 != 2157399624u)
                        {
                            if (num7 != 2344590436u)
                            {
                                return;
                            }
                            if (!(text == "key_debug"))
                            {
                                return;
                            }
                        }
                        else
                        {
                            if (!(text == "mousemapr"))
                            {
                                return;
                            }
                            if (flag && num > 10 && num < 30000)
                            {
                                ClientConfig.mouseMapR = (int)num;
                                return;
                            }
                            return;
                        }
                    }
                    else
                    {
                        if (!(text == "key_map"))
                        {
                            return;
                        }
                        ClientConfig.MAP_KEY = ClientConfig.TranslateCode(value);
                        return;
                    }
                }
                else if (num7 <= 2469845962u)
                {
                    if (num7 != 2467194956u)
                    {
                        if (num7 != 2469845962u)
                        {
                            return;
                        }
                        if (!(text == "gunanimations"))
                        {
                            return;
                        }
                        if (flag && num >= 0 && num < 4096)
                        {
                            ClientConfig.animationNumGunShot = (int)num;
                            ClientController.THIS.gunShotPool.max_size = ClientConfig.animationNumGunShot;
                            return;
                        }
                        return;
                    }
                    else
                    {
                        if (!(text == "key_up"))
                        {
                            return;
                        }
                        ClientConfig.MOVE_UP_KEY = ClientConfig.TranslateCode(value);
                        return;
                    }
                }
                else if (num7 != 2616480018u)
                {
                    if (num7 != 2715731931u)
                    {
                        if (num7 != 2903810408u)
                        {
                            return;
                        }
                        if (!(text == "key_wb"))
                        {
                            return;
                        }
                        ClientConfig.WARBLOCK_KEY = ClientConfig.TranslateCode(value);
                        return;
                    }
                    else
                    {
                        if (!(text == "guizoom"))
                        {
                            return;
                        }
                        if (flag2 && num2 > 0.01f && num2 < 5f)
                        {
                            ServerController.THIS.MainCanvasScaler.scaleFactor = num2;
                            return;
                        }
                        return;
                    }
                }
                else
                {
                    if (!(text == "key_autodig"))
                    {
                        return;
                    }
                    ClientConfig.AUTODIG_KEY = ClientConfig.TranslateCode(value);
                    return;
                }
            }
            else if (num7 <= 3762097329u)
            {
                if (num7 <= 3502087988u)
                {
                    if (num7 != 3460624886u)
                    {
                        if (num7 != 3502087988u)
                        {
                            return;
                        }
                        if (!(text == "key_inv"))
                        {
                            return;
                        }
                        ClientConfig.INV_KEY = ClientConfig.TranslateCode(value);
                        return;
                    }
                    else
                    {
                        if (!(text == "key_geo"))
                        {
                            return;
                        }
                        ClientConfig.GEO_KEY = ClientConfig.TranslateCode(value);
                        return;
                    }
                }
                else if (num7 != 3683658673u)
                {
                    if (num7 != 3750945330u)
                    {
                        if (num7 != 3762097329u)
                        {
                            return;
                        }
                        if (!(text == "key_prog"))
                        {
                            return;
                        }
                        ClientConfig.PROG_KEY = ClientConfig.TranslateCode(value);
                        return;
                    }
                    else
                    {
                        if (!(text == "zoom"))
                        {
                            return;
                        }
                        if (flag2 && num2 >= 0.2f && num2 < 5f)
                        {
                            TerrainRendererScript.unitSize = 16f * num2;
                            TerrainRendererScript.needUpdate = true;
                            ClientController.THIS.terrainRenderer.RecreateMeshes();
                            return;
                        }
                        return;
                    }
                }
                else
                {
                    if (!(text == "key_right"))
                    {
                        return;
                    }
                    ClientConfig.MOVE_RIGHT_KEY = ClientConfig.TranslateCode(value);
                    return;
                }
            }
            else if (num7 <= 3938375620u)
            {
                if (num7 != 3895047942u)
                {
                    if (num7 != 3938375620u)
                    {
                        return;
                    }
                    if (!(text == "key_left"))
                    {
                        return;
                    }
                    ClientConfig.MOVE_LEFT_KEY = ClientConfig.TranslateCode(value);
                    return;
                }
                else if (!(text == "key_autorem"))
                {
                    return;
                }
            }
            else if (num7 != 4070664958u)
            {
                if (num7 != 4230274009u)
                {
                    if (num7 != 4294627599u)
                    {
                        return;
                    }
                    if (!(text == "key_heal"))
                    {
                        return;
                    }
                    ClientConfig.HEAL_KEY = ClientConfig.TranslateCode(value);
                    return;
                }
                else
                {
                    if (!(text == "key_down"))
                    {
                        return;
                    }
                    ClientConfig.MOVE_DOWN_KEY = ClientConfig.TranslateCode(value);
                    return;
                }
            }
            else
            {
                if (!(text == "key_block"))
                {
                    return;
                }
                ClientConfig.BLOCK_KEY = ClientConfig.TranslateCode(value);
                return;
            }
            ClientConfig.AUTOREM_KEY = ClientConfig.TranslateCode(value);
            return;
        }
        if (num7 <= 1454666881u)
        {
            if (num7 <= 690217924u)
            {
                if (num7 <= 138405506u)
                {
                    if (num7 != 43805890u)
                    {
                        if (num7 != 138405506u)
                        {
                            return;
                        }
                        if (!(text == "mouser"))
                        {
                            return;
                        }
                        if (flag && num > 10 && num < 30000)
                        {
                            ClientConfig.mouseDefR = (int)num;
                            ClientConfig.mouseR = ClientConfig.mouseDefR;
                            return;
                        }
                    }
                    else
                    {
                        if (!(text == "mousemaxstack"))
                        {
                            return;
                        }
                        if (flag && num > 100)
                        {
                            ClientConfig.mouseDefMaxStack = (int)num;
                            ClientConfig.mouseMaxStack = ClientConfig.mouseDefMaxStack;
                            return;
                        }
                    }
                }
                else if (num7 != 485371085u)
                {
                    if (num7 != 627248831u)
                    {
                        if (num7 != 690217924u)
                        {
                            return;
                        }
                        if (!(text == "mousemapmaxstack"))
                        {
                            return;
                        }
                        if (flag && num > 100)
                        {
                            ClientConfig.mouseMapMaxStack = (int)num;
                            return;
                        }
                    }
                    else
                    {
                        if (!(text == "crysanimations"))
                        {
                            return;
                        }
                        if (flag && num >= 0 && num < 4096)
                        {
                            ClientConfig.animationNumCrysPlus = (int)num;
                            ClientController.THIS.crysPlusPool.max_size = ClientConfig.animationNumCrysPlus;
                            return;
                        }
                    }
                }
                else
                {
                    if (!(text == "key_quadro"))
                    {
                        return;
                    }
                    ClientConfig.QUADRO_KEY = ClientConfig.TranslateCode(value);
                    return;
                }
            }
            else if (num7 <= 1216626464u)
            {
                if (num7 != 1038161709u)
                {
                    if (num7 != 1216626464u)
                    {
                        return;
                    }
                    if (!(text == "diganimations"))
                    {
                        return;
                    }
                    if (flag && num >= 0 && num < 4096)
                    {
                        ClientConfig.animationNumBz = (int)num;
                        ClientController.THIS.bzPool.max_size = ClientConfig.animationNumBz;
                        return;
                    }
                }
                else
                {
                    if (!(text == "key_agr"))
                    {
                        return;
                    }
                    ClientConfig.AGR_KEY = ClientConfig.TranslateCode(value);
                    return;
                }
            }
            else if (num7 != 1412371120u)
            {
                if (num7 != 1429148739u)
                {
                    if (num7 != 1454666881u)
                    {
                        return;
                    }
                    if (!(text == "boomanimations"))
                    {
                        return;
                    }
                    if (flag && num >= 0 && num < 4096)
                    {
                        ClientConfig.animationNumBoom = (int)num;
                        ClientController.THIS.boomPool.max_size = ClientConfig.animationNumBoom;
                        return;
                    }
                }
                else
                {
                    if (!(text == "key_toggle8"))
                    {
                        return;
                    }
                    ClientConfig.TOGGLE8_KEY = ClientConfig.TranslateCode(value);
                    return;
                }
            }
            else
            {
                if (!(text == "key_toggle9"))
                {
                    return;
                }
                ClientConfig.TOGGLE9_KEY = ClientConfig.TranslateCode(value);
                return;
            }
        }
        else if (num7 <= 1596924929u)
        {
            if (num7 <= 1546592072u)
            {
                if (num7 != 1495194099u)
                {
                    if (num7 != 1546592072u)
                    {
                        return;
                    }
                    if (!(text == "key_toggle1"))
                    {
                        return;
                    }
                    ClientConfig.TOGGLE1_KEY = ClientConfig.TranslateCode(value);
                    return;
                }
                else
                {
                    if (!(text == "mousemapmaxlen"))
                    {
                        return;
                    }
                    if (flag && num > 100)
                    {
                        ClientConfig.mouseMapMaxLen = (int)num;
                        return;
                    }
                }
            }
            else if (num7 != 1563369691u)
            {
                if (num7 != 1580147310u)
                {
                    if (num7 != 1596924929u)
                    {
                        return;
                    }
                    if (!(text == "key_toggle2"))
                    {
                        return;
                    }
                    ClientConfig.TOGGLE2_KEY = ClientConfig.TranslateCode(value);
                    return;
                }
                else
                {
                    if (!(text == "key_toggle3"))
                    {
                        return;
                    }
                    ClientConfig.TOGGLE3_KEY = ClientConfig.TranslateCode(value);
                    return;
                }
            }
            else
            {
                if (!(text == "key_toggle0"))
                {
                    return;
                }
                ClientConfig.TOGGLE0_KEY = ClientConfig.TranslateCode(value);
                return;
            }
        }
        else if (num7 <= 1630480167u)
        {
            if (num7 != 1613702548u)
            {
                if (num7 != 1630480167u)
                {
                    return;
                }
                if (!(text == "key_toggle4"))
                {
                    return;
                }
                ClientConfig.TOGGLE4_KEY = ClientConfig.TranslateCode(value);
                return;
            }
            else
            {
                if (!(text == "key_toggle5"))
                {
                    return;
                }
                ClientConfig.TOGGLE5_KEY = ClientConfig.TranslateCode(value);
                return;
            }
        }
        else if (num7 != 1647257786u)
        {
            if (num7 != 1664035405u)
            {
                if (num7 != 1778508743u)
                {
                    return;
                }
                if (!(text == "key_dig"))
                {
                    return;
                }
                ClientConfig.DIGG_KEY = ClientConfig.TranslateCode(value);
                return;
            }
            else
            {
                if (!(text == "key_toggle6"))
                {
                    return;
                }
                ClientConfig.TOGGLE6_KEY = ClientConfig.TranslateCode(value);
                return;
            }
        }
        else
        {
            if (!(text == "key_toggle7"))
            {
                return;
            }
            ClientConfig.TOGGLE7_KEY = ClientConfig.TranslateCode(value);
            return;
        }*/
        switch (name.ToLower())
        {
            case "key_up":
                MOVE_UP_KEY = TranslateCode(value);
                break;
            case "key_down":
                MOVE_DOWN_KEY = TranslateCode(value);
                break;
            case "key_left":
                MOVE_LEFT_KEY = TranslateCode(value);
                break;
            case "key_right":
                MOVE_RIGHT_KEY = TranslateCode(value);
                break;
            case "key_dig":
                DIGG_KEY = TranslateCode(value);
                break;
            case "key_heal":
                HEAL_KEY = TranslateCode(value);
                break;
            case "key_geo":
                GEO_KEY = TranslateCode(value);
                break;
            case "key_block":
                BLOCK_KEY = TranslateCode(value);
                break;
            case "key_quadro":
                QUADRO_KEY = TranslateCode(value);
                break;
            case "key_wb":
                WARBLOCK_KEY = TranslateCode(value);
                break;
            case "key_agr":
                AGR_KEY = TranslateCode(value);
                break;
            case "key_autodig":
                AUTODIG_KEY = TranslateCode(value);
                break;
            case "key_map":
                MAP_KEY = TranslateCode(value);
                break;
            case "key_local":
                LOCALCHAT_KEY = TranslateCode(value);
                break;
            case "key_inv":
                INV_KEY = TranslateCode(value);
                break;
            case "key_prog":
                PROG_KEY = TranslateCode(value);
                break;
            case "key_toggle1":
                TOGGLE1_KEY = TranslateCode(value);
                break;
            case "key_toggle2":
                TOGGLE2_KEY = TranslateCode(value);
                break;
            case "key_toggle3":
                TOGGLE3_KEY = TranslateCode(value);
                break;
            case "key_toggle4":
                TOGGLE4_KEY = TranslateCode(value);
                break;
            case "key_toggle5":
                TOGGLE5_KEY = TranslateCode(value);
                break;
            case "key_toggle6":
                TOGGLE6_KEY = TranslateCode(value);
                break;
            case "key_toggle7":
                TOGGLE7_KEY = TranslateCode(value);
                break;
            case "key_toggle8":
                TOGGLE8_KEY = TranslateCode(value);
                break;
            case "key_toggle9":
                TOGGLE9_KEY = TranslateCode(value);
                break;
            case "key_toggle0":
                TOGGLE0_KEY = TranslateCode(value);
                break;
            case "mousemapr":
                if (flag && result > 10 && result < 30000)
                {
                    mouseMapR = result;
                }
                break;
            case "mousemapmaxlen":
                if (flag && result > 100)
                {
                    mouseMapMaxLen = result;
                }
                break;
            case "mousemapmaxstack":
                if (flag && result > 100)
                {
                    mouseMapMaxStack = result;
                }
                break;
            case "mouser":
                if (flag && result > 10 && result < 30000)
                {
                    mouseDefR = result;
                    mouseR = mouseDefR;
                }
                break;
            case "mousemaxlen":
                if (flag && result > 100)
                {
                    mouseDefMaxLen = result;
                    mouseMaxLen = mouseDefMaxLen;
                }
                break;
            case "mousemaxstack":
                if (flag && result > 100)
                {
                    mouseDefMaxStack = result;
                    mouseMaxStack = mouseDefMaxStack;
                }
                break;
            case "zoom":
                if (flag2 && result2 >= 0.2f && result2 < 5f)
                {
                    TerrainRendererScript.unitSize = 16f * result2;
                    TerrainRendererScript.needUpdate = true;
                    ClientController.THIS.terrainRenderer.RecreateMeshes();
                }
                break;
            case "guizoom":
                if (flag2 && result2 > 0.01f && result2 < 5f)
                {
                    ServerController.THIS.MainCanvasScaler.scaleFactor = result2;
                }
                break;
            case "gunanimations":
                if (flag && result >= 0 && result < 4096)
                {
                    animationNumGunShot = result;
                    ClientController.THIS.gunShotPool.max_size = animationNumGunShot;
                }
                break;
            case "key_autorem":
                break;
            case "crysanimations":
                if (flag && result >= 0 && result < 4096)
                {
                    animationNumCrysPlus = result;
                    ClientController.THIS.crysPlusPool.max_size = animationNumCrysPlus;
                }
                break;
            case "diganimations":
                if (flag && result >= 0 && result < 4096)
                {
                    animationNumBz = result;
                    ClientController.THIS.bzPool.max_size = animationNumBz;
                }
                break;
            case "boomanimations":
                if (flag && result >= 0 && result < 4096)
                {
                    animationNumBoom = (int)result;
                    ClientController.THIS.boomPool.max_size = animationNumBoom;
                }
                break;
        } 
    }

    
	public static int mouseMaxLen = int.MaxValue;

	public static int mouseMaxStack = int.MaxValue;

	public static int mouseR = int.MaxValue;

	public static int mouseDefMaxLen = int.MaxValue;

	public static int mouseDefMaxStack = int.MaxValue;

	public static int mouseDefR = int.MaxValue;

	public static bool mouseNoDig = false;

	public static bool gunRadius = true;

	public static bool noDistortion = false;

	public static int mouseMapMaxLen = int.MaxValue;

	public static int mouseMapMaxStack = 20000;

	public static int mouseMapR = 4000;

	public static int animationNumBoom = 30;

	public static int animationNumGunShot = 50;

	public static int animationNumCrysPlus = 20;

	public static int animationNumBz = 20;

	public static KeyCode MOVE_UP_KEY = KeyCode.W;

	public static KeyCode MOVE_DOWN_KEY = KeyCode.S;

	public static KeyCode MOVE_LEFT_KEY = KeyCode.A;

	public static KeyCode MOVE_RIGHT_KEY = KeyCode.D;

	public static KeyCode DIGG_KEY = KeyCode.Z;

	public static KeyCode WARBLOCK_KEY = KeyCode.Y;

	public static KeyCode BLOCK_KEY = KeyCode.F;

	public static KeyCode ROAD_KEY = KeyCode.H;

	public static KeyCode QUADRO_KEY = KeyCode.J;

	public static KeyCode HEAL_KEY = KeyCode.V;

	public static KeyCode GEO_KEY = KeyCode.G;

	public static KeyCode AUTOREM_KEY = KeyCode.B;

	public static KeyCode AGR_KEY = KeyCode.L;

	public static KeyCode AUTODIG_KEY = KeyCode.E;

	public static KeyCode MAP_KEY = KeyCode.M;

	public static KeyCode LOCALCHAT_KEY = KeyCode.T;

	public static KeyCode PROG_KEY = KeyCode.R;

	public static KeyCode INV_KEY = KeyCode.I;

	public static KeyCode TOGGLE1_KEY = KeyCode.None;

	public static KeyCode TOGGLE2_KEY = KeyCode.None;

	public static KeyCode TOGGLE3_KEY = KeyCode.None;

	public static KeyCode TOGGLE4_KEY = KeyCode.None;

	public static KeyCode TOGGLE5_KEY = KeyCode.None;

	public static KeyCode TOGGLE6_KEY = KeyCode.None;

	public static KeyCode TOGGLE7_KEY = KeyCode.None;

	public static KeyCode TOGGLE8_KEY = KeyCode.None;

	public static KeyCode TOGGLE9_KEY = KeyCode.None;

	public static KeyCode TOGGLE0_KEY = KeyCode.None;

	public static bool SOUND_BASKET = true;

	public static bool SOUND_SIGNAL = true;

	public static bool SOUND_BOMB = true;

	public static bool SOUND_BOMBTICK = true;

	public static bool SOUND_DEATH = true;

	public static bool SOUND_DESTROY = true;

	public static bool SOUND_EMI = true;

	public static bool SOUND_GEOLOGY = true;

	public static bool SOUND_HEAL = true;

	public static bool SOUND_HURT = true;

	public static bool SOUND_MINING = true;

	public static bool SOUND_DIZZ = true;

	public static bool SOUND_TP_IN = true;

	public static bool SOUND_TP_OUT = true;

	public static bool SOUND_VOLC = true;

	public static bool SOUND_C190 = true;

	public static bool TOGGLE_UPDATING = false;

	public static bool OLD_PROGRAM_FORMAT = false;

	public static bool SHOW_MY_NICK = false;

	public static List<List<string>> toggleList1 = new List<List<string>>();

	public static List<List<string>> toggleList2 = new List<List<string>>();

	public static List<List<string>> toggleList3 = new List<List<string>>();

	public static List<List<string>> toggleList4 = new List<List<string>>();

	public static List<List<string>> toggleList5 = new List<List<string>>();

	public static List<List<string>> toggleList6 = new List<List<string>>();

	public static List<List<string>> toggleList7 = new List<List<string>>();

	public static List<List<string>> toggleList8 = new List<List<string>>();

	public static List<List<string>> toggleList9 = new List<List<string>>();

	public static List<List<string>> toggleList0 = new List<List<string>>();

	public static List<string> currentToggleList;

	public static Dictionary<int, int> ToggleStates = new Dictionary<int, int>();

	public static List<List<string>>[] toggleLists = new List<List<string>>[]
	{
		ClientConfig.toggleList0,
		ClientConfig.toggleList1,
		ClientConfig.toggleList2,
		ClientConfig.toggleList3,
		ClientConfig.toggleList4,
		ClientConfig.toggleList5,
		ClientConfig.toggleList6,
		ClientConfig.toggleList7,
		ClientConfig.toggleList8,
		ClientConfig.toggleList9
	};

	public static bool CHAT_SHOW_ID = true;

	public static bool CHAT_SHOW_TIME = true;

	public static bool CHAT_SHOW_NICK = true;
}
