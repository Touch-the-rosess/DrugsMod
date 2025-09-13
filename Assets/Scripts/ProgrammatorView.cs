using SevenZip.Compression.LZMA;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ProgrammatorView : MonoBehaviour
{
    public void UpdateIconsWithoutSaving()
    {
        int current = this.pageSelector.current;
        for (int i = 0; i < ProgrammatorView.ROWS; i++)
        {
            for (int j = 0; j < ProgrammatorView.COLS; j++)
            {
                ProgAction ProgAction2 = ProgrammatorView.actions[i * ProgrammatorView.COLS + j];
                int _id = ProgrammatorView.codes[current * ProgrammatorView.COLS * ProgrammatorView.ROWS + i * ProgrammatorView.COLS + j];
                string @string = ProgrammatorView.code_labels[current * ProgrammatorView.COLS * ProgrammatorView.ROWS + i * ProgrammatorView.COLS + j];
                int num = ProgrammatorView.nums[current * ProgrammatorView.COLS * ProgrammatorView.ROWS + i * ProgrammatorView.COLS + j];
                ProgAction2.setString(@string);
                ProgAction2.setNum(num);
                ProgAction2.ChangeTo(_id);
            }
        }
        this.prevPage = current;
    }

    public void UpdateIcons()
    {
        int current = this.pageSelector.current;
        for (int i = 0; i < ProgrammatorView.ROWS; i++)
        {
            for (int j = 0; j < ProgrammatorView.COLS; j++)
            {
                ProgAction ProgAction = ProgrammatorView.actions[i * ProgrammatorView.COLS + j];
                ProgrammatorView.codes[this.prevPage * ProgrammatorView.COLS * ProgrammatorView.ROWS + i * ProgrammatorView.COLS + j] = ProgAction.id;
                ProgrammatorView.code_labels[this.prevPage * ProgrammatorView.COLS * ProgrammatorView.ROWS + i * ProgrammatorView.COLS + j] = ProgAction.getString();
                ProgrammatorView.nums[this.prevPage * ProgrammatorView.COLS * ProgrammatorView.ROWS + i * ProgrammatorView.COLS + j] = ProgAction.getNum();
                int _id = ProgrammatorView.codes[current * ProgrammatorView.COLS * ProgrammatorView.ROWS + i * ProgrammatorView.COLS + j];
                int num = ProgrammatorView.nums[current * ProgrammatorView.COLS * ProgrammatorView.ROWS + i * ProgrammatorView.COLS + j];
                string @string = ProgrammatorView.code_labels[current * ProgrammatorView.COLS * ProgrammatorView.ROWS + i * ProgrammatorView.COLS + j];
                ProgAction.setString(@string);
                ProgAction.setNum(num);
                ProgAction.ChangeTo(_id);
            }
        }
        this.prevPage = current;
    }

    public void ClearSource()
    {
        for (int i = 0; i < ProgrammatorView.PAGES; i++)
        {
            for (int j = 0; j < ProgrammatorView.ROWS; j++)
            {
                for (int k = 0; k < ProgrammatorView.COLS; k++)
                {
                    ProgrammatorView.codes[i * ProgrammatorView.COLS * ProgrammatorView.ROWS + j * ProgrammatorView.COLS + k] = 0;
                    ProgrammatorView.nums[i * ProgrammatorView.COLS * ProgrammatorView.ROWS + j * ProgrammatorView.COLS + k] = 0;
                    ProgrammatorView.code_labels[i * ProgrammatorView.COLS * ProgrammatorView.ROWS + j * ProgrammatorView.COLS + k] = "0";
                }
            }
        }
    }

    private void Start()
    {
        ProgrammatorView.THIS = this;
        string[] array = new string[ProgrammatorView.PAGES];
        for (int i = 0; i < ProgrammatorView.PAGES; i++)
        {
            if (i < 10)
            {
                array[i] = "0" + i;
            }
            else
            {
                array[i] = string.Concat(i);
            }
        }
        this.pageSelector.SetStrings(array);
        this.ClearSource();
        for (int j = 0; j < ProgrammatorView.ROWS; j++)
        {
            for (int k = 0; k < ProgrammatorView.COLS; k++)
            {
                GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.iconPrefab);
                gameObject.transform.SetParent(this.iconContainer.transform);
                Vector3 localPosition = new Vector3(16f + 32f * (float)k, -16f - 32f * (float)j, 0f);
                gameObject.GetComponent<RectTransform>().localPosition = localPosition;
                gameObject.GetComponent<ProgAction>().ChangeTo(ProgrammatorView.codes[j * ProgrammatorView.COLS + k]);
                ProgrammatorView.actions[j * ProgrammatorView.COLS + k] = gameObject.GetComponent<ProgAction>();
            }
        }
        this.pageSelector.ChangeEvent.AddListener(new UnityAction(this.OnPageChange));
        this.UpdateIcons();
    }

    private void OnPageChange()
    {
        this.UpdateIcons();
    }

    private bool MakePosition()
    {
        Vector2 vector = default(Vector2);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(base.gameObject.GetComponent<RectTransform>(), UnityEngine.Input.mousePosition, null, out vector);
        int num = Mathf.FloorToInt(vector.x / 32f);
        int num2 = Mathf.FloorToInt(-vector.y / 32f);
        if (num < 0 || num > ProgrammatorView.COLS - 1)
        {
            return false;
        }
        if (num2 < 0 || num2 > ProgrammatorView.ROWS - 1)
        {
            return false;
        }
        this.position = num2 * ProgrammatorView.COLS + num;
        return true;
    }

    private void ShiftCode(int dx, int dy)
    {
        if (this.MakePosition())
        {
            int num = this.position;
            int num2 = 0;
            int num3 = Mathf.FloorToInt((float)(this.position / (ProgrammatorView.ROWS * ProgrammatorView.COLS)));
            int num4 = Mathf.FloorToInt((float)((this.position - num3 * ProgrammatorView.ROWS * ProgrammatorView.COLS) / ProgrammatorView.COLS));
            int num5 = Mathf.FloorToInt((float)(this.position % ProgrammatorView.COLS));
            bool flag = false;
            for (int i = 0; i < 1000; i++)
            {
                if (ProgrammatorView.actions[num].id == 0)
                {
                    flag = true;
                    break;
                }
                num2++;
                num = num + dx + ProgrammatorView.COLS * dy;
                int num6 = Mathf.FloorToInt((float)(num / (ProgrammatorView.ROWS * ProgrammatorView.COLS)));
                int num7 = Mathf.FloorToInt((float)((num - num6 * ProgrammatorView.ROWS * ProgrammatorView.COLS) / ProgrammatorView.COLS));
                int num8 = Mathf.FloorToInt((float)(num % ProgrammatorView.COLS));
                if (num < 0 || num6 != num3 || (dx == 0 && num8 != num5) || (dy == 0 && num7 != num4))
                {
                    break;
                }
            }
            if (flag)
            {
                for (int j = num2; j > 0; j--)
                {
                    int num9 = this.position + (j - 1) * dx + (j - 1) * ProgrammatorView.COLS * dy;
                    int num10 = this.position + j * dx + j * ProgrammatorView.COLS * dy;
                    ProgAction ProgAction = ProgrammatorView.actions[num10];
                    ProgAction ProgAction2 = ProgrammatorView.actions[num9];
                    ProgAction.setString(ProgAction2.getString());
                    ProgAction.setNum(ProgAction2.getNum());
                    ProgAction.ChangeTo(ProgAction2.id);
                }
                if (num2 > 0)
                {
                    ProgrammatorView.actions[this.position].ChangeTo(0);
                    ProgrammatorView.unsaved = true;
                    this.titleTF.text = ProgrammatorView.title + " [*]";
                }
            }
        }
    }

    private void MakeCycle(int[] cycle, int usePrev = -1)
    {
        if (this.MakePosition())
        {
            this.CyclicChange(cycle, usePrev);
        }
    }

    private void CyclicChange(int[] cycle, int usePrev)
    {
        int num = -1;
        for (int i = 0; i < cycle.Length; i++)
        {
            if (cycle[i] == ProgrammatorView.actions[this.position].id)
            {
                num = i;
            }
        }
        if (num == -1)
        {
            num = usePrev;
        }
        int num2 = cycle[(num + 1) % cycle.Length];
        if (ProgrammatorView.actions[this.position].id != num2)
        {
            ProgrammatorView.unsaved = true;
            this.titleTF.text = ProgrammatorView.title + " [*]";
        }
        ProgrammatorView.actions[this.position].ChangeTo(num2);
    }

    public int BufShift(int code)
    {
        switch(code){
          case   0: case 1:   case 40:  return 0;
          case 160: case 24:  case 140: case 139: case 25: case 26: case 137: return 3;
          case 120: case 119: case 123: return 7;
          case 181: case 182: return 4;
          default: return 1;
        }
        return 1;
    }

    

    public void LoadFromString(string source)
    {
        //Debug.Log(source);
        this.ClearSource();
        if (source[0] == 'X')
        {
            byte[] array = SevenZipHelper.Decompress(Convert.FromBase64String(source));
            Debug.Log("da");
            this.ClearSource();
            int num = BitConverter.ToInt32(array, 0);
            for (int i = 0; i < num; i++)
            {
                ProgrammatorView.codes[i] = (int)Convert.ToInt16(array[i + 4]);
            }
            string[] array2 = Encoding.UTF8.GetString(array, num + 4, array.Length - num - 4).Split(new char[]
            {
                ':'
            });
            for (int j = 0; j < array2.Length; j++)
            {
                if (array2[j].Contains("@"))
                {
                    string[] array3 = array2[j].Split(new char[]
                    {
                        '@'
                    });
                    ProgrammatorView.code_labels[j] = array3[0];
                    ProgrammatorView.nums[j] = int.Parse(array3[1]);
                }
                else
                {
                    ProgrammatorView.code_labels[j] = array2[j];
                    ProgrammatorView.nums[j] = 0;
                }
            }
            //UpdateIconsWithoutSaving();
        }
        else if (source[0] == '$')
        {
            source = source.Replace("$", "");
            source = source.Replace("\r", "");
            source = source.Replace("_", "   ");
            source = source.Replace(".0.", "\n\n\n\n\n\n\n\n\n");
            source = source.Replace(".9.", "\n\n\n\n\n\n\n\n");
            source = source.Replace(".8.", "\n\n\n\n\n\n\n");
            source = source.Replace(".7.", "\n\n\n\n\n\n");
            source = source.Replace(".6.", "\n\n\n\n\n");
            source = source.Replace(".5.", "\n\n\n\n");
            source = source.Replace("...", "\n\n\n");
            source = source.Replace("..", "\n\n");
            source = source.Replace(".", "\n");
            this.str200 = source;
            this.int1 = 0;
            this.int2 = 0;
            this.int3 = ProgrammatorView.COLS;
            this.int4 = ProgrammatorView.COLS * ProgrammatorView.ROWS;
            while (this.int1 < this.str200.Length)
            {
                string text = this.NewProgramm2();
                
                int num2 = -1;

                switch (text)
                {
                    case "CRAFT;":
                        {
                            num2 = 172;
                            break;
                        }
                    case "[DW]":
                        {
                            num2 = 32;
                            break;
                        }
                    case "=hp50":
                        {
                            num2 = 149;
                            break;
                        }
                    case "^A":
                        {
                            num2 = 5;
                            break;
                        }
                    case "->":
                        {
                            num2 = 26;
                            ProgrammatorView.code_labels[this.int2] = this.NewProgramm('>');
                            break;
                        }
                    case "\n":
                        {
                            this.int2 += this.int3;
                            this.int4 -= this.int3;
                            this.int3 = ProgrammatorView.COLS;
                            break;
                        }
                    case "BEEP;":
                        {
                            num2 = 23;
                            break;
                        }
                    case "HEAL;":
                        {
                            num2 = 143;
                            break;
                        }
                    case "B1;":
                        {
                            num2 = 162;
                            break;
                        }
                    case "^F":
                        {
                            num2 = 14;
                            break;
                        }
                    case "^D":
                        {
                            num2 = 7;
                            break;
                        }
                    case "#R":
                        {
                            num2 = 166;
                            break;
                        }
                    case " ":
                        {
                            num2 = 0;
                            break;
                        }
                    case "#S":
                        {
                            num2 = 2;
                            break;
                        }
                    case "CW;":
                        {
                            num2 = 16;
                            break;
                        }
                    case "^S":
                        {
                            num2 = 6;
                            break;
                        }
                    case "^W":
                        {
                            num2 = 4;
                            break;
                        }
                    case ",":
                        {
                            num2 = 1;
                            break;
                        }
                    case "C190;":
                        {
                            num2 = 169;
                            break;
                        }
                    case "(":
                        {
                            string text2 = this.NewProgramm(')');
                            string[] array4;
                            if (text2.Contains("="))
                            {
                                array4 = text2.Split(new char[]
                                {
                                    '='
                                });
                                num2 = 123;
                                
                            }
                            else if (text2.Contains("<"))
                            {
                                array4 = text2.Split(new char[]
                                {
                                    '<'
                                });
                                num2 = 120;
                                
                            }
                            else
                            {
                                if (!text2.Contains(">"))
                                {
                                    return;
                                }
                                array4 = text2.Split(new char[]
                                {
                                    '>'
                                });
                                num2 = 119;
                                
                            }
                            ProgrammatorView.code_labels[this.int2] = array4[0];
                            int num4 = 0;
                            if (!int.TryParse(array4[1], out num4))
                            {
                                num4 = 0;
                            }
                            ProgrammatorView.nums[this.int2] = num4;
                            Debug.Log(array4[0]);
                            break;
                        }
                    case "AUT+":
                        {
                            num2 = 158;
                            break;
                        }
                    case "id":
                        {
                            num2 = 178;
                            break;
                        }
                    case "AUT-":
                        {
                            num2 = 159;
                            break;
                        }
                    case ">":
                        {
                            num2 = 24;
                            ProgrammatorView.code_labels[this.int2] = this.NewProgramm('|');
                            break;
                        }
                    case "?":
                        {
                            num2 = 139;
                            ProgrammatorView.code_labels[this.int2] = this.NewProgramm('<');
                            break;
                        }
                    case "ia":
                        {
                            num2 = 176;
                            break;
                        }
                    case "AGR+":
                        {
                            num2 = 160;
                            break;
                        }
                    case "=k":
                        {
                            num2 = 50;
                            break;
                        }
                    case "AGR-":
                        {
                            num2 = 161;
                            break;
                        }
                    case "#E":
                        {
                            num2 = 3;
                            break;
                        }
                    case "=o":
                        {
                            num2 = 76;
                            break;
                        }
                    case "=n":
                        {
                            num2 = 43;
                            break;
                        }
                    case "FLIP;":
                        {
                            num2 = 144;
                            break;
                        }
                    case "=c":
                        {
                            num2 = 46;
                            break;
                        }
                    case "=b":
                        {
                            num2 = 48;
                            break;
                        }
                    case "=a":
                        {
                            num2 = 47;
                            break;
                        }
                    case "=e":
                        {
                            num2 = 44;
                            break;
                        }
                    case "=d":
                        {
                            num2 = 51;
                            break;
                        }
                    case "iw":
                        {
                            num2 = 175;
                            break;
                        }
                    case "=f":
                        {
                            num2 = 45;
                            break;
                        }
                    case "=y":
                        {
                            num2 = 60;
                            break;
                        }
                    case "=x":
                        {
                            num2 = 74;
                            break;
                        }
                    case "=g":
                        {
                            num2 = 77;
                            break;
                        }
                    case "<=|":
                        {
                            num2 = 138;
                            break;
                        }
                    case "is":
                        {
                            num2 = 177;
                            break;
                        }
                    case "GEO;":
                        {
                            num2 = 167;
                            break;
                        }
                    case "<-|":
                        {
                            num2 = 28;
                            break;
                        }
                    case "<|":
                        {
                            num2 = 27;
                            break;
                        }
                    case "=r":
                        {
                            num2 = 59;
                            break;
                        }
                    case "=q":
                        {
                            num2 = 57;
                            break;
                        }
                    case "=s":
                        {
                            num2 = 49;
                            break;
                        }
                    case "!{":
                        {
                            num2 = 181;
                            ProgrammatorView.code_labels[this.int2] = this.NewProgramm('}');
                            break;
                        }
                    case "RAND;":
                        {
                            num2 = 22;
                            break;
                        }
                    case "=K":
                        {
                            num2 = 52;
                            break;
                        }
                    case "=B":
                        {
                            num2 = 53;
                            break;
                        }
                    case "=A":
                        {
                            num2 = 54;
                            break;
                        }
                    case "UP;":
                        {
                            num2 = 171;
                            break;
                        }
                    case "[r]":
                        {
                            num2 = 156;
                            break;
                        }
                    case "=R":
                        {
                            num2 = 58;
                            break;
                        }
                    case "=G":
                        {
                            num2 = 146;
                            break;
                        }
                    case "[S]":
                        {
                            num2 = 37;
                            break;
                        }
                    case "[f]":
                        {
                            num2 = 136;
                            break;
                        }
                    case "[a]":
                        {
                            num2 = 132;
                            break;
                        }
                    case "[D]":
                        {
                            num2 = 35;
                            break;
                        }
                    case "[W]":
                        {
                            num2 = 31;
                            break;
                        }
                    case "=hp-":
                        {
                            num2 = 148;
                            break;
                        }
                    case "OR":
                        {
                            num2 = 38;
                            break;
                        }
                    case "AND":
                        {
                            num2 = 39;
                            break;
                        }
                    case "FILL;":
                        {
                            num2 = 147;
                            break;
                        }
                    case "B3;":
                        {
                            num2 = 164;
                            break;
                        }
                    case "!?":
                        {
                            num2 = 140;
                            ProgrammatorView.code_labels[this.int2] = this.NewProgramm('<');
                            break;
                        }
                    case "=>":
                        {
                            num2 = 137;
                            ProgrammatorView.code_labels[this.int2] = this.NewProgramm('>');
                            break;
                        }
                    case "B2;":
                        {
                            num2 = 163;
                            break;
                        }
                    case ":>":
                        {
                            num2 = 25;
                            ProgrammatorView.code_labels[this.int2] = this.NewProgramm('>');
                            break;
                        }
                    case "DIGG;":
                        {
                            num2 = 141;
                            break;
                        }
                    case "VB;":
                        {
                            num2 = 165;
                            break;
                        }
                    case "~\n":
                        {
                            this.int2 += this.int4;
                            this.int3 = ProgrammatorView.COLS;
                            this.int4 = ProgrammatorView.COLS * ProgrammatorView.ROWS;
                            UnityEngine.Debug.Log("next page!!!");
                            break;
                        }
                    case "RESTART;":
                        {
                            num2 = 200;
                            break;
                        }
                    case "CCW;":
                        {
                            num2 = 15;
                            break;
                        }
                    case "[A]":
                        {
                            num2 = 33;
                            break;
                        }
                    case "BUILD;":
                        {
                            num2 = 142;
                            break;
                        }
                    case "[w]":
                        {
                            num2 = 131;
                            break;
                        }
                    case "[s]":
                        {
                            num2 = 133;
                            break;
                        }
                    case "[F]":
                        {
                            num2 = 135;
                            break;
                        }
                    case "[d]":
                        {
                            num2 = 134;
                            break;
                        }
                    case "[l]":
                        {
                            num2 = 157;
                            break;
                        }
                    case "NANO;":
                        {
                            num2 = 173;
                            break;
                        }
                    case "REM;":
                        {
                            num2 = 174;
                            break;
                        }
                    case "POLY;":
                        {
                            num2 = 170;
                            break;
                        }
                    case "[AS]":
                        {
                            num2 = 36;
                            break;
                        }
                    case "ZZ;":
                        {
                            num2 = 168;
                            break;
                        }
                    case "[SD]":
                        {
                            num2 = 30;
                            break;
                        }
                    case "[WA]":
                        {
                            num2 = 29;
                            break;
                        }
                    case "d":
                        {
                            num2 = 12;
                            break;
                        }
                    case "MINE;":
                        {
                            num2 = 145;
                            break;
                        }
                    case "Hand-":
                        {
                            num2 = 180;
                            break;
                        }
                    case "g":
                        {
                            num2 = 18;
                            break;
                        }
                    case "Hand+":
                        {
                            num2 = 179;
                            break;
                        }
                    case "b":
                        {
                            num2 = 17;
                            break;
                        }
                    case "a":
                        {
                            num2 = 10;
                            break;
                        }
                    case "h":
                        {
                            num2 = 20;
                            break;
                        }
                    case "l":
                        {
                            num2 = 13;
                            break;
                        }
                    case "s":
                        {
                            num2 = 11;
                            break;
                        }
                    case "q":
                        {
                            num2 = 21;
                            break;
                        }
                    case "w":
                        {
                            num2 = 9;
                            break;
                        }
                    case "|":
                        {
                            num2 = 40;
                            string text3 = this.NewProgramm(':');
                            ProgrammatorView.code_labels[this.int2] = text3;
                            break;
                        }
                    case "r":
                        {
                            num2 = 19;
                            break;
                        }
                    case "z":
                        {
                            num2 = 8;
                            break;
                        }
                    case "{":
                        {
                            num2 = 182;
                            ProgrammatorView.code_labels[this.int2] = this.NewProgramm('}');
                            break;
                        }
                }
                if (num2 != -1)
                {
                    ProgrammatorView.codes[this.int2] = num2;
                    this.int2++;
                    this.int3--;
                    this.int4--;
                    //break;
                }
                if (this.int2 > ProgrammatorView.COLS * ProgrammatorView.ROWS * ProgrammatorView.PAGES)
                {
                    break;
                }
            }
        }
        else
        {
            source = source.Replace("_", "   ");
            source = source.Replace(".0.", "\n\n\n\n\n\n\n\n\n");
            source = source.Replace(".9.", "\n\n\n\n\n\n\n\n");
            source = source.Replace(".8.", "\n\n\n\n\n\n\n");
            source = source.Replace(".7.", "\n\n\n\n\n\n");
            source = source.Replace(".6.", "\n\n\n\n\n");
            source = source.Replace(".5.", "\n\n\n\n");
            source = source.Replace("...", "\n\n\n");
            source = source.Replace("..", "\n\n");
            source = source.Replace(".", "\n");
            this.str200 = source;
            this.int1 = 0;
            this.int2 = 0;
            this.int3 = ProgrammatorView.COLS;
            this.int4 = ProgrammatorView.COLS * ProgrammatorView.ROWS;
            while (this.int1 < this.str200.Length)
            {
                string text4 = this.NewProgramm1();
                int num5 = -1;
                switch (text4)
                {
                    case "CRAFT;":
                        {
                            num5 = 172;
                            break;
                        }
                    case "[DW]":
                        {
                            num5 = 32;
                            break;
                        }
                    case "=hp50":
                        {
                            num5 = 149;
                            break;
                        }
                    case "^A":
                        {
                            num5 = 5;
                            break;
                        }
                    case "->":
                        {
                            num5 = 26;
                            ProgrammatorView.code_labels[this.int2] = this.NewProgramm('>');
                            break;
                        }
                    case "\n":
                        {
                            this.int2 += this.int3;
                            this.int4 -= this.int3;
                            this.int3 = ProgrammatorView.COLS;
                            break;
                        }
                    case "BEEP;":
                        {
                            num5 = 23;
                            break;
                        }
                    case "HEAL;":
                        {
                            num5 = 143;
                            break;
                        }
                    case "B1;":
                        {
                            num5 = 162;
                            break;
                        }
                    case "^F":
                        {
                            num5 = 14;
                            break;
                        }
                    case "^D":
                        {
                            num5 = 7;
                            break;
                        }
                    case "#R":
                        {
                            num5 = 166;
                            break;
                        }
                    case " ":
                        {
                            num5 = 0;
                            break;
                        }
                    case "#S":
                        {
                            num5 = 2;
                            break;
                        }
                    case "CW;":
                        {
                            num5 = 16;
                            break;
                        }
                    case "^S":
                        {
                            num5 = 6;
                            break;
                        }
                    case "^W":
                        {
                            num5 = 4;
                            break;
                        }
                    case ",":
                        {
                            num5 = 1;
                            break;
                        }
                    case "C190;":
                        {
                            num5 = 169;
                            break;
                        }
                    case "(":
                        {
                            string text5 = this.NewProgramm(')');
                            string[] array5;
                            if (text5.Contains("="))
                            {
                                array5 = text5.Split(new char[]
                                {
                                            '='
                                });
                                num5 = 123;
                            }
                            else if (text5.Contains("<"))
                            {
                                array5 = text5.Split(new char[]
                                {
                                            '<'
                                });
                                num5 = 120;
                            }
                            else
                            {
                                if (!text5.Contains(">"))
                                {
                                    return;
                                }
                                array5 = text5.Split(new char[]
                                {
                                            '>'
                                });
                                num5 = 119;
                            }
                            ProgrammatorView.code_labels[this.int2] = array5[0];
                            int num6 = 0;
                            if (!int.TryParse(array5[1], out num6))
                            {
                                num6 = 0;
                            }
                            ProgrammatorView.nums[this.int2] = num6;
                            break;
                        }
                    case "AUT+":
                        {
                            num5 = 158;
                            break;
                        }
                    case "id":
                        {
                            num5 = 178;
                            break;
                        }
                    case "AUT-":
                        {
                            num5 = 159;
                            break;
                        }
                    case ">":
                        {
                            num5 = 24;
                            ProgrammatorView.code_labels[this.int2] = this.NewProgramm('|');
                            break;
                        }
                    case "?":
                        {
                            num5 = 139;
                            ProgrammatorView.code_labels[this.int2] = this.NewProgramm('<');
                            break;
                        }
                    case "ia":
                        {
                            num5 = 176;
                            break;
                        }
                    case "AGR+":
                        {
                            num5 = 160;
                            break;
                        }
                    case "=k":
                        {
                            num5 = 50;
                            break;
                        }
                    case "AGR-":
                        {
                            num5 = 161;
                            break;
                        }
                    case "#E":
                        {
                            num5 = 3;
                            break;
                        }
                    case "=o":
                        {
                            num5 = 76;
                            break;
                        }
                    case "=n":
                        {
                            num5 = 43;
                            break;
                        }
                    case "FLIP;":
                        {
                            num5 = 144;
                            break;
                        }
                    case "=c":
                        {
                            num5 = 46;
                            break;
                        }
                    case "=b":
                        {
                            num5 = 48;
                            break;
                        }
                    case "=a":
                        {
                            num5 = 47;
                            break;
                        }
                    case "=e":
                        {
                            num5 = 44;
                            break;
                        }
                    case "=d":
                        {
                            num5 = 51;
                            break;
                        }
                    case "iw":
                        {
                            num5 = 175;
                            break;
                        }
                    case "=f":
                        {
                            num5 = 45;
                            break;
                        }
                    case "=y":
                        {
                            num5 = 60;
                            break;
                        }
                    case "=x":
                        {
                            num5 = 74;
                            break;
                        }
                    case "=g":
                        {
                            num5 = 77;
                            break;
                        }
                    case "<=|":
                        {
                            num5 = 138;
                            break;
                        }
                    case "is":
                        {
                            num5 = 177;
                            break;
                        }
                    case "GEO;":
                        {
                            num5 = 167;
                            break;
                        }
                    case "<-|":
                        {
                            num5 = 28;
                            break;
                        }
                    case "<|":
                        {
                            num5 = 27;
                            break;
                        }
                    case "=r":
                        {
                            num5 = 59;
                            break;
                        }
                    case "=q":
                        {
                            num5 = 57;
                            break;
                        }
                    case "=s":
                        {
                            num5 = 49;
                            break;
                        }
                    case "!{":
                        {
                            num5 = 181;
                            ProgrammatorView.code_labels[this.int2] = this.NewProgramm('}');
                            break;
                        }
                    case "RAND;":
                        {
                            num5 = 22;
                            break;
                        }
                    case "=K":
                        {
                            num5 = 52;
                            break;
                        }
                    case "=B":
                        {
                            num5 = 53;
                            break;
                        }
                    case "=A":
                        {
                            num5 = 54;
                            break;
                        }
                    case "UP;":
                        {
                            num5 = 171;
                            break;
                        }
                    case "[r]":
                        {
                            num5 = 156;
                            break;
                        }
                    case "=R":
                        {
                            num5 = 58;
                            break;
                        }
                    case "=G":
                        {
                            num5 = 146;
                            break;
                        }
                    case "[S]":
                        {
                            num5 = 37;
                            break;
                        }
                    case "[f]":
                        {
                            num5 = 136;
                            break;
                        }
                    case "[a]":
                        {
                            num5 = 132;
                            break;
                        }
                    case "[D]":
                        {
                            num5 = 35;
                            break;
                        }
                    case "[W]":
                        {
                            num5 = 31;
                            break;
                        }
                    case "=hp-":
                        {
                            num5 = 148;
                            break;
                        }
                    case "OR":
                        {
                            num5 = 38;
                            break;
                        }
                    case "AND":
                        {
                            num5 = 39;
                            break;
                        }
                    case "FILL;":
                        {
                            num5 = 147;
                            break;
                        }
                    case "B3;":
                        {
                            num5 = 164;
                            break;
                        }
                    case "!?":
                        {
                            num5 = 140;
                            ProgrammatorView.code_labels[this.int2] = this.NewProgramm('<');
                            break;
                        }
                    case "=>":
                        {
                            num5 = 137;
                            ProgrammatorView.code_labels[this.int2] = this.NewProgramm('>');
                            break;
                        }
                    case "B2;":
                        {
                            num5 = 163;
                            break;
                        }
                    case ":>":
                        {
                            num5 = 25;
                            ProgrammatorView.code_labels[this.int2] = this.NewProgramm('>');
                            break;
                        }
                    case "DIGG;":
                        {
                            num5 = 141;
                            break;
                        }
                    case "VB;":
                        {
                            num5 = 165;
                            break;
                        }
                    case "~\n":
                        {
                            this.int2 += this.int4;
                            this.int3 = ProgrammatorView.COLS;
                            this.int4 = ProgrammatorView.COLS * ProgrammatorView.ROWS;
                            UnityEngine.Debug.Log("next page!!!");
                            break;
                        }
                    case "RESTART;":
                        {
                            num5 = 200;
                            break;
                        }
                    case "CCW;":
                        {
                            num5 = 15;
                            break;
                        }
                    case "[A]":
                        {
                            num5 = 33;
                            break;
                        }
                    case "BUILD;":
                        {
                            num5 = 142;
                            break;
                        }
                    case "[w]":
                        {
                            num5 = 131;
                            break;
                        }
                    case "[s]":
                        {
                            num5 = 133;
                            break;
                        }
                    case "[F]":
                        {
                            num5 = 135;
                            break;
                        }
                    case "[d]":
                        {
                            num5 = 134;
                            break;
                        }
                    case "[l]":
                        {
                            num5 = 157;
                            break;
                        }
                    case "NANO;":
                        {
                            num5 = 173;
                            break;
                        }
                    case "REM;":
                        {
                            num5 = 174;
                            break;
                        }
                    case "POLY;":
                        {
                            num5 = 170;
                            break;
                        }
                    case "[AS]":
                        {
                            num5 = 36;
                            break;
                        }
                    case "ZZ;":
                        {
                            num5 = 168;
                            break;
                        }
                    case "[SD]":
                        {
                            num5 = 30;
                            break;
                        }
                    case "[WA]":
                        {
                            num5 = 29;
                            break;
                        }
                    case "d":
                        {
                            num5 = 12;
                            break;
                        }
                    case "MINE;":
                        {
                            num5 = 145;
                            break;
                        }
                    case "Hand-":
                        {
                            num5 = 180;
                            break;
                        }
                    case "g":
                        {
                            num5 = 18;
                            break;
                        }
                    case "Hand+":
                        {
                            num5 = 179;
                            break;
                        }
                    case "b":
                        {
                            num5 = 17;
                            break;
                        }
                    case "a":
                        {
                            num5 = 10;
                            break;
                        }
                    case "h":
                        {
                            num5 = 20;
                            break;
                        }
                    case "l":
                        {
                            num5 = 13;
                            break;
                        }
                    case "s":
                        {
                            num5 = 11;
                            break;
                        }
                    case "q":
                        {
                            num5 = 21;
                            break;
                        }
                    case "w":
                        {
                            num5 = 9;
                            break;
                        }
                    case "|":
                        {
                            num5 = 40;
                            string text6 = this.NewProgramm(':');
                            ProgrammatorView.code_labels[this.int2] = text6;
                            break;
                        }
                    case "r":
                        {
                            num5 = 19;
                            break;
                        }
                    case "z":
                        {
                            num5 = 8;
                            break;
                        }
                    case "{":
                        {
                            num5 = 182;
                            ProgrammatorView.code_labels[this.int2] = this.NewProgramm('}');
                            break;
                        }
                        
                }
                if (num5 != -1)
                {
                    ProgrammatorView.codes[this.int2] = num5;
                    this.int2++;
                    this.int3--;
                    this.int4--;
                }
                if (this.int2 > ProgrammatorView.COLS * ProgrammatorView.ROWS * ProgrammatorView.PAGES)
                {
                    break;
                }
            }
        }

        for (int k = 0; k < 10; k++)
            Debug.Log(nums[k]);
        this.UpdateIconsWithoutSaving();
    }
            

    public string SaveToString()
    {
        this.UpdateIcons();
        if (!ClientConfig.OLD_PROGRAM_FORMAT)
        {
            return this.SaveToStringNew();
        }
        int num = 0;
        for (int i = 0; i < ProgrammatorView.codes.Length; i++)
        {
            if (ProgrammatorView.codes[i] != 0)
            {
                num = i;
            }
        }
        if (num == 0)
        {
            num = 1;
        }
        int num2 = 0;
        for (int j = 0; j < ProgrammatorView.code_labels.Length; j++)
        {
            if (ProgrammatorView.code_labels[j] != "0" || ProgrammatorView.nums[j] != 0)
            {
                num2 = j;
            }
        }
        if (num2 == 0)
        {
            num2 = 1;
        }
        string[] array = new string[num2 + 1];
        Array.Copy(ProgrammatorView.code_labels, array, num2 + 1);
        for (int k = 0; k < array.Length; k++)
        {
            if (ProgrammatorView.nums[k] != 0)
            {
                string[] array2 = array;
                int num3 = k;
                array2[num3] = array2[num3] + "@" + ProgrammatorView.nums[k];
            }
        }
        string s = string.Join(":", array);
        byte[] array3 = new byte[num + 1];
        for (int l = 0; l < array3.Length; l++)
        {
            array3[l] = Convert.ToByte(ProgrammatorView.codes[l]);
        }
        byte[] bytes = Encoding.UTF8.GetBytes(s);
        byte[] bytes2 = BitConverter.GetBytes(array3.Length);
        byte[] array4 = new byte[bytes2.Length + array3.Length + bytes.Length];
        Buffer.BlockCopy(bytes2, 0, array4, 0, bytes2.Length);
        Buffer.BlockCopy(array3, 0, array4, bytes2.Length, array3.Length);
        Buffer.BlockCopy(bytes, 0, array4, bytes2.Length + array3.Length, bytes.Length);
        return Convert.ToBase64String(SevenZipHelper.Compress(array4));
    }

    public void SendAndStartProgram()
    {
        byte[] array = this.CompileProgram();
        string s = this.SaveToString();
        byte[] bytes = Encoding.UTF8.GetBytes(s);
        byte[] bytes2 = BitConverter.GetBytes(array.Length);
        byte[] bytes3 = BitConverter.GetBytes(ProgrammatorView.programId);
        byte[] array2 = new byte[bytes2.Length + bytes3.Length + array.Length + bytes.Length];
        Buffer.BlockCopy(bytes2, 0, array2, 0, bytes2.Length);
        Buffer.BlockCopy(bytes3, 0, array2, bytes2.Length, bytes3.Length);
        Buffer.BlockCopy(array, 0, array2, bytes2.Length + bytes3.Length, array.Length);
        Buffer.BlockCopy(bytes, 0, array2, bytes2.Length + array.Length + bytes3.Length, bytes.Length);
        ServerTime.THIS.SendTypicalMessage(ClientController.THIS.TimeOfMove(), "PROG", 0, 0, array2);
    }

    public byte[] CompileProgram()
    {
        this.UpdateIcons();
        this.labels = new Dictionary<string, int>();
        int num = 0;
        for (int i = 0; i < ProgrammatorView.PAGES; i++)
        {
            for (int j = 0; j < ProgrammatorView.ROWS; j++)
            {
                bool flag = true;
                bool flag2 = false;
                for (int k = 0; k < ProgrammatorView.COLS; k++)
                {
                    if (!flag2)
                    {
                        int num2 = ProgrammatorView.codes[i * ProgrammatorView.ROWS * ProgrammatorView.COLS + j * ProgrammatorView.COLS + k];
                        if (num2 != 0)
                        {
                            flag = false;
                        }
                        if (num2 == 1)
                        {
                            flag2 = true;
                        }
                        else if (num2 == 40)
                        {
                            this.labels[ProgrammatorView.code_labels[i * ProgrammatorView.ROWS * ProgrammatorView.COLS + j * ProgrammatorView.COLS + k]] = num;
                        }
                        num += this.BufShift(num2);
                    }
                }
                if (!flag && !flag2)
                {
                    num++;
                }
            }
        }
        List<int> list = new List<int>();
        for (int l = 0; l < ProgrammatorView.PAGES; l++)
        {
            for (int m = 0; m < ProgrammatorView.ROWS; m++)
            {
                bool flag3 = true;
                bool flag4 = false;
                for (int n = 0; n < ProgrammatorView.COLS; n++)
                {
                    if (!flag4)
                    {
                        int num3 = ProgrammatorView.codes[l * ProgrammatorView.ROWS * ProgrammatorView.COLS + m * ProgrammatorView.COLS + n];
                        if (num3 != 0)
                        {
                            if (num3 == 1)
                            {
                                flag4 = true;
                            }
                            else if (num3 == 40)
                            {
                                flag3 = false;
                            }
                            else if (num3 == 24 || num3 == 140 || num3 == 166 || num3 == 139 || num3 == 25 || num3 == 26 || num3 == 137)
                            {
                                list.Add(num3);
                                string key = ProgrammatorView.code_labels[l * ProgrammatorView.ROWS * ProgrammatorView.COLS + m * ProgrammatorView.COLS + n];
                                int num4 = 0;
                                if (this.labels.ContainsKey(key))
                                {
                                    num4 = this.labels[key];
                                }
                                int item = num4 / 256;
                                int item2 = num4 % 256;
                                list.Add(item);
                                list.Add(item2);
                                flag3 = false;
                            }
                            else if (num3 == 123 || num3 == 119 || num3 == 120)
                            {
                                list.Add(num3);
                                string text2;
                                string text = text2 = ProgrammatorView.code_labels[l * ProgrammatorView.ROWS * ProgrammatorView.COLS + m * ProgrammatorView.COLS + n];
                                if (text.Length == 0)
                                {
                                    text2 += "   ";
                                }
                                if (text.Length == 1)
                                {
                                    text2 += "  ";
                                }
                                if (text.Length == 2)
                                {
                                    text2 += " ";
                                }
                                char[] array = text2.ToCharArray();
                                list.Add((int)array[0]);
                                list.Add((int)array[1]);
                                list.Add((int)array[2]);
                                int num5 = ProgrammatorView.nums[l * ProgrammatorView.ROWS * ProgrammatorView.COLS + m * ProgrammatorView.COLS + n];
                                if (num5 < 0)
                                {
                                    num5 += 16777216;
                                }
                                list.Add(num5 / 65536);
                                list.Add(num5 / 256 % 256);
                                list.Add(num5 % 256);
                                flag3 = false;
                            }
                            else if (num3 == 181 || num3 == 182)
                            {
                                list.Add(num3);
                                string text4;
                                string text3 = text4 = ProgrammatorView.code_labels[l * ProgrammatorView.ROWS * ProgrammatorView.COLS + m * ProgrammatorView.COLS + n];
                                if (text3.Length == 0)
                                {
                                    text4 += "   ";
                                }
                                if (text3.Length == 1)
                                {
                                    text4 += "  ";
                                }
                                if (text3.Length == 2)
                                {
                                    text4 += " ";
                                }
                                char[] array2 = text4.ToCharArray();
                                list.Add((int)array2[0]);
                                list.Add((int)array2[1]);
                                list.Add((int)array2[2]);
                                flag3 = false;
                            }
                            else
                            {
                                list.Add(num3);
                                flag3 = false;
                            }
                        }
                    }
                }
                if (!flag3 && !flag4)
                {
                    list.Add(200);
                }
            }
        }
        int[] array3 = list.ToArray();
        byte[] array4 = new byte[array3.Length];
        for (int num6 = 0; num6 < array3.Length; num6++)
        {
            array4[num6] = (byte)array3[num6];
        }
        return array4;
    }

    public void Show()
    {
        GUIManager.THIS.m_EventSystem.SetSelectedGameObject(null);
        ProgrammatorView.opened = true;
        this.UpdateIcons();
    }

    private void Update()
    {
        if (ProgrammatorView.active)
        {
            if (GUIManager.THIS.m_EventSystem.currentSelectedGameObject == null)
            {
                if ((UnityEngine.Input.GetKey(KeyCode.LeftCommand) || UnityEngine.Input.GetKey(KeyCode.RightCommand) || UnityEngine.Input.GetKey(KeyCode.LeftControl) || UnityEngine.Input.GetKey(KeyCode.RightControl)) && UnityEngine.Input.GetKeyDown(KeyCode.C))
                {
                    GUIUtility.systemCopyBuffer = this.SaveToString();
                    return;
                }
                if ((UnityEngine.Input.GetKey(KeyCode.LeftCommand) || UnityEngine.Input.GetKey(KeyCode.RightCommand) || UnityEngine.Input.GetKey(KeyCode.LeftControl) || UnityEngine.Input.GetKey(KeyCode.RightControl)) && UnityEngine.Input.GetKeyDown(KeyCode.V))
                {
                    AYSWindowManager.THIS.Show("ЗАГРУЗКА СТРОКИ ИЗ ПРОГРАММЫ", "Вы собираетесь загрузить программу из буффера обмена.\nТекущая программа будет потеряна.\nУверены?", delegate
                    {
                        LoadFromString(GUIUtility.systemCopyBuffer);
                    });
                    return;
                }
                if (UnityEngine.Input.GetKeyDown(KeyCode.C))
                {
                    if (UnityEngine.Input.GetKey(KeyCode.LeftShift) || UnityEngine.Input.GetKey(KeyCode.RightShift))
                    {
                        this.MakeCycle(this.shc_button, -1);
                    }
                    else
                    {
                        this.MakeCycle(this.c_button, -1);
                    }
                }
                if (UnityEngine.Input.GetKeyDown(KeyCode.W))
                {
                    if (UnityEngine.Input.GetKey(KeyCode.LeftShift) || UnityEngine.Input.GetKey(KeyCode.RightShift))
                    {
                        this.MakeCycle(this.shw_button, 5);
                    }
                    else
                    {
                        this.MakeCycle(this.w_button, -1);
                    }
                }
                if (UnityEngine.Input.GetKeyDown(KeyCode.A))
                {
                    if (UnityEngine.Input.GetKey(KeyCode.LeftShift) || UnityEngine.Input.GetKey(KeyCode.RightShift))
                    {
                        this.MakeCycle(this.sha_button, 5);
                    }
                    else
                    {
                        this.MakeCycle(this.a_button, -1);
                    }
                }
                if (UnityEngine.Input.GetKeyDown(KeyCode.S))
                {
                    if (UnityEngine.Input.GetKey(KeyCode.LeftShift) || UnityEngine.Input.GetKey(KeyCode.RightShift))
                    {
                        this.MakeCycle(this.shs_button, 5);
                    }
                    else
                    {
                        this.MakeCycle(this.s_button, -1);
                    }
                }
                if (UnityEngine.Input.GetKeyDown(KeyCode.D))
                {
                    if (UnityEngine.Input.GetKey(KeyCode.LeftShift) || UnityEngine.Input.GetKey(KeyCode.RightShift))
                    {
                        this.MakeCycle(this.shd_button, 5);
                    }
                    else
                    {
                        this.MakeCycle(this.d_button, -1);
                    }
                }
                if (UnityEngine.Input.GetKeyDown(KeyCode.F))
                {
                    if (UnityEngine.Input.GetKey(KeyCode.LeftShift) || UnityEngine.Input.GetKey(KeyCode.RightShift))
                    {
                        this.MakeCycle(this.shf_button, -1);
                    }
                    else
                    {
                        this.MakeCycle(this.f_button, -1);
                    }
                }
                if (UnityEngine.Input.GetKeyDown(KeyCode.X))
                {
                    if (UnityEngine.Input.GetKey(KeyCode.LeftShift) || UnityEngine.Input.GetKey(KeyCode.RightShift))
                    {
                        this.MakeCycle(this.shx_button, -1);
                    }
                    else
                    {
                        this.MakeCycle(this.x_button, -1);
                    }
                }
                if (UnityEngine.Input.GetKeyDown(KeyCode.V))
                {
                    this.MakeCycle(this.v_button, -1);
                }
                if (UnityEngine.Input.GetKeyDown(KeyCode.R))
                {
                    this.MakeCycle(this.r_button, -1);
                }
                if (UnityEngine.Input.GetKeyDown(KeyCode.E))
                {
                    this.MakeCycle(this.e_button, -1);
                }
                if (UnityEngine.Input.GetKeyDown(KeyCode.Z))
                {
                    if (UnityEngine.Input.GetKey(KeyCode.LeftShift) || UnityEngine.Input.GetKey(KeyCode.RightShift))
                    {
                        this.MakeCycle(this.shz_button, -1);
                    }
                    else
                    {
                        this.MakeCycle(this.z_button, -1);
                    }
                }
                if (UnityEngine.Input.GetKeyDown(KeyCode.T))
                {
                    this.MakeCycle(this.t_button, -1);
                }
                if (UnityEngine.Input.GetKeyDown(KeyCode.L))
                {
                    this.MakeCycle(this.l_button, -1);
                }
                if (UnityEngine.Input.GetKeyDown(KeyCode.G))
                {
                    this.MakeCycle(this.g_button, -1);
                }
                if (UnityEngine.Input.GetKeyDown(KeyCode.Q))
                {
                    this.MakeCycle(this.q_button, -1);
                }
                if (UnityEngine.Input.GetKeyDown(KeyCode.O))
                {
                    this.MakeCycle(this.o_button, -1);
                }
                if (UnityEngine.Input.GetKeyDown(KeyCode.I))
                {
                    this.MakeCycle(this.i_button, -1);
                }
                if (UnityEngine.Input.GetKeyDown(KeyCode.H))
                {
                    this.MakeCycle(this.h_button, -1);
                }
                if (UnityEngine.Input.GetKeyDown(KeyCode.J))
                {
                    this.MakeCycle(this.j_button, -1);
                }
                if (UnityEngine.Input.GetKeyDown(KeyCode.B))
                {
                    if (UnityEngine.Input.GetKey(KeyCode.LeftShift) || UnityEngine.Input.GetKey(KeyCode.RightShift))
                    {
                        this.MakeCycle(this.shb_button, -1);
                    }
                    else
                    {
                        this.MakeCycle(this.b_button, -1);
                    }
                }
                if (UnityEngine.Input.GetKeyDown(KeyCode.M))
                {
                    this.MakeCycle(this.m_button, -1);
                }
                if (UnityEngine.Input.GetKeyDown(KeyCode.Y))
                {
                    if (UnityEngine.Input.GetKey(KeyCode.LeftCommand))
                    {
                        this.NewProgramm3();
                    }
                    else
                    {
                        this.MakeCycle(this.y_button, -1);
                    }
                }
                if (UnityEngine.Input.GetKeyDown(KeyCode.Delete))
                {
                    this.MakeCycle(this.del_button, -1);
                }
                if (UnityEngine.Input.GetKeyDown(KeyCode.Backspace))
                {
                    this.MakeCycle(this.back_button, -1);
                }
                if (UnityEngine.Input.GetKeyDown(KeyCode.DownArrow) && (UnityEngine.Input.GetKey(KeyCode.LeftShift) || UnityEngine.Input.GetKey(KeyCode.RightShift)))
                {
                    this.ShiftCode(0, 1);
                }
                if (UnityEngine.Input.GetKeyDown(KeyCode.LeftArrow) && (UnityEngine.Input.GetKey(KeyCode.LeftShift) || UnityEngine.Input.GetKey(KeyCode.RightShift)))
                {
                    this.ShiftCode(-1, 0);
                }
                if (UnityEngine.Input.GetKeyDown(KeyCode.RightArrow) && (UnityEngine.Input.GetKey(KeyCode.LeftShift) || UnityEngine.Input.GetKey(KeyCode.RightShift)))
                {
                    this.ShiftCode(1, 0);
                }
                if (UnityEngine.Input.GetKeyDown(KeyCode.UpArrow) && (UnityEngine.Input.GetKey(KeyCode.LeftShift) || UnityEngine.Input.GetKey(KeyCode.RightShift)))
                {
                    this.ShiftCode(0, -1);
                    return;
                }
            }
            else if (GUIManager.THIS.m_EventSystem.currentSelectedGameObject.name == "ProgInput")
            {
                if (UnityEngine.Input.GetKeyDown(KeyCode.Return) || UnityEngine.Input.GetKeyDown(KeyCode.KeypadEnter))
                {
                    GUIManager.THIS.m_EventSystem.SetSelectedGameObject(null);
                    return;
                }
            }
            else if (GUIManager.THIS.m_EventSystem.currentSelectedGameObject.name == "RenameButton" || GUIManager.THIS.m_EventSystem.currentSelectedGameObject.name == "CopyButton" || GUIManager.THIS.m_EventSystem.currentSelectedGameObject.name == "ClearButton" || GUIManager.THIS.m_EventSystem.currentSelectedGameObject.name == "ToMenuButton" || GUIManager.THIS.m_EventSystem.currentSelectedGameObject.name == "LessButton" || GUIManager.THIS.m_EventSystem.currentSelectedGameObject.name == "MoreButton")
            {
                GUIManager.THIS.m_EventSystem.SetSelectedGameObject(null);
            }
        }
    }

    public ProgrammatorView()
    {
        int[] array = new int[2];
        array[0] = 1;
        this.back_button = array;
        this.t_button = new int[]
        {
            141,
            142,
            143,
            145
        };
        this.v_button = new int[]
        {
            123,
            120,
            119
        };
        this.g_button = new int[]
        {
            24,
            25,
            26,
            137
        };
        this.h_button = new int[]
        {
            148,
            149
        };
        this.j_button = new int[]
        {
            146,
            147
        };
        this.q_button = new int[]
        {
            27,
            28,
            138
        };
        this.position = -1;
        this.str200 = "";
        this.strmas1 = new string[]
        {
            " ",
            "\n",
            "~\n",
            ",",
            "#S",
            "#E",
            "W",
            "A",
            "S",
            "D",
            "z",
            "w",
            "a",
            "s",
            "d",
            "l",
            "F",
            "CCW;",
            "CW;",
            "b",
            "g",
            "r",
            "h",
            "q",
            "RAND;",
            "BEEP;",
            "B1;",
            "B2;",
            "B3;",
            "VB;",
            "GEO;",
            "ZZ;",
            "C190;",
            "POLY;",
            "UP;",
            "CRAFT;",
            "NANO;",
            "REM;",
            "iw",
            "ia",
            "is",
            "id",
            "|",
            ">",
            ":>",
            "->",
            "=>",
            "<|",
            "<-|",
            "<=|",
            "[W]",
            "[WA]",
            "[A]",
            "[AS]",
            "[S]",
            "[SD]",
            "[D]",
            "[DW]",
            "[w]",
            "[a]",
            "[s]",
            "[d]",
            "[F]",
            "[f]",
            "=n",
            "=e",
            "=f",
            "=c",
            "=a",
            "=b",
            "=s",
            "=k",
            "=d",
            "=K",
            "=B",
            "=A",
            "=q",
            "=R",
            "=r",
            "=y",
            "=g",
            "=o",
            "=x",
            "OR",
            "AND",
            "AUT+",
            "AUT-",
            "AGR+",
            "AGR-",
            "(",
            "?",
            "!?",
            "DIGG;",
            "BUILD;",
            "HEAL;",
            "MINE;",
            "[r]",
            "[l]",
            "=G",
            "FILL;",
            "=hp-",
            "=hp50",
            "FLIP;",
            "#R",
            "RESTART;",
            "Hand+",
            "Hand-",
            "!{",
            "{"
        };
        this.strmas2 = new string[]
        {
            " ",
            "\n",
            "~\n",
            ",",
            "#S",
            "#E",
            "^W",
            "^A",
            "^S",
            "^D",
            "z",
            "w",
            "a",
            "s",
            "d",
            "l",
            "^F",
            "CCW;",
            "CW;",
            "b",
            "g",
            "r",
            "h",
            "q",
            "RAND;",
            "BEEP;",
            "B1;",
            "B2;",
            "B3;",
            "VB;",
            "GEO;",
            "ZZ;",
            "C190;",
            "POLY;",
            "UP;",
            "CRAFT;",
            "NANO;",
            "REM;",
            "iw",
            "ia",
            "is",
            "id",
            "|",
            ">",
            ":>",
            "->",
            "=>",
            "<|",
            "<-|",
            "<=|",
            "[W]",
            "[WA]",
            "[A]",
            "[AS]",
            "[S]",
            "[SD]",
            "[D]",
            "[DW]",
            "[w]",
            "[a]",
            "[s]",
            "[d]",
            "[F]",
            "[f]",
            "=n",
            "=e",
            "=f",
            "=c",
            "=a",
            "=b",
            "=s",
            "=k",
            "=d",
            "=K",
            "=B",
            "=A",
            "=q",
            "=R",
            "=r",
            "=y",
            "=g",
            "=o",
            "=x",
            "OR",
            "AND",
            "AUT+",
            "AUT-",
            "AGR+",
            "AGR-",
            "(",
            "?",
            "!?",
            "DIGG;",
            "BUILD;",
            "HEAL;",
            "MINE;",
            "[r]",
            "[l]",
            "=G",
            "FILL;",
            "=hp-",
            "=hp50",
            "FLIP;",
            "#R",
            "RESTART;",
            "Hand+",
            "Hand-",
            "!{",
            "{"
        };
        this.str201 = "XQAAgACaAQAAAAAAAAA+gDAFAhG0ciqkm4PJHRcPgIruSXXpLO0McQ2EaD0CLDkX1Bwh+LW9OM3QB1T+3qVdAUpFp581o584qDxt6eJa5ZmL9IccwWgVwreNz8sedV9O4qdfTvr9Fq21T0SXWLC0hOZTGUlNudYz1VsnqRCp4i7gf/AWcIHs2KgLmwS8IEgJkaNgAA==";
        this.str202 = "XQAAgADOAQAAAAAAAABKADAFCPBEHWF/wkqwvU4Eb7ohVxyHTQCnB1tuOQ/mId9a0zVcyCiQh32E9c7xYZu2Pl8fEh/P3fjzX7/iO1MmqYc/qeocZ8w1lXWa7RaZcFhX7GuXWTLBsXzjlA0KeZpby+P8vwIXwiaA4LU3j9kw1D7zQzt/VRLMs5eYauhxvlEpC/VRXtxodxe8aLB9QD25fpbplLo/R0aUG1SxETQdFrKdpAzY2gA=";
        this.str203 = "XQAAgADPLQAAAAAAAAAAA221b5AWlMuE8z/Xt7w74lgUrnx+dRhKi4sEAcSMKXqeXUPStyWpBdDNlMOEQCIkQWrLhbzOttujqROPHMDvK+I5gscHJvyAUWhNYRkC0+wB8elhb/lKccCjj+uzMXVRLRHVY80PwQEZ65yDOYlCQBaPwcfbnJsTzuveQiBOy+EGsxJKX/cNTlXoKsaUgABWRCeUr9kyB1cWIsSWIZkbQxgrbIVRFPdPOp03bh/dh54copSiGmct01ZOlbNbMHIzNBC5aSo/qdeGuorVulJx2emBzmSdZ3mtBMtTCnM3DL87FrRZRvloJN8+cCIxLtbVLfCVt6c63NPBIVzCaCitfIvr3b6JNaGOCjzuqKnpBFOLNV2fTK7P2cQY9HAiysW3YJubG/Aav6zajPcfYyj3Vh+xj6IaynIi0+3rPTH/kyCEa+luX7ZXFY2WKfcUiYGeZxc7UFWGrxNerLJQhIFCdFDZHmQsZgu1LtYQnFCcyiUNBWMfuVCL+x687Cf0Q+aCnU2ZY+/9Jf8qmcaqBztpRN5q3v80rD/5nOrbEw0gXhwYUj23BFn6frybu4llJfJ8X/WkDRFR/GLhrX4CiGZ8E9KjFbYV4zqtnkOrS4pB9BN17cumWLTLIJYHm9nAc0s1RthJWL//MQYQy3lF5+YW7wj0a/j9Pk27JC6jarObe+qNUoYrVQYnlDtHC4hnBDOeu39QTDyPUXWgxNuR0YaC05Orb1ybW4PnF5GRiu1/injOo88bsqiOg27NGk580QQ8z2/0sG2kkceuKBjFGFjMwaJ98N8zAHP+ibMj9KDFBzw8B4VleITAvstFp9fnfnunOg3EJ+28CB9Z0JhyOeW4l0YiVIBqhisfwMgKt8WDBdZG+blxKXcpDaJENz/1Xx6P6AtlTQvmTAwHKZhwvCc7vOuojUnypMuAYVs+faX15ZripHnT2M8c+ynsBD3p8oIhgAKVlIzQihael+JTu1u0DnWsuyKib5l3nc4EA0XRizR6MYYeelvcLeVWdPo2KDHrfb+USorE47+2f/3D/B7ZEUOrYYqeOjSdrFRMqTsvPPDbffp+wpZJC+dS+8nFge17F7zRiXj8fTsbUTFyBLDt7bt3sIBD6nen7UYGTW//hiy5AkycPeYoE6phXD3u37TBJ0OY0Ci7eOIDTST3ftBMC5zjzptFe0Wt8hYAyfsIuuCHJeeBSsLv1eRmDKkKvvAGLNnnlTCzsMH8RtWWtZSgLwKDAUSWXvMyXdDnquSko2aJNbpEYKpXlpIZg6HSVx4YOLU6bwNdxoiCh6j+OnOTtgSY3RWQJw6oQadjKIvCM8RCGBXmZymS8KngjVPhr0+NPFXu98MS2sqKTINYhaezLtvjbeJSUHR//VxgvlRFtqDntq1vbei/nyCOGjWuCRGABllv3kuYX3+wSLrGJV6YObTkwxX+jKlmYs3EB2VUJBrmZ5pUcQum0mcHPduJBtqkqE9ArkdFkyb4A7GDuVj2oFf4uxjGcWFkvUIhL8dtk1kTf86G3q48EnzH8rRHDXscoTiKxcAhmdnOAY2Kp5lQmkpHSgJn12a/eIkMAQViI/cLemwb6Hgn7wL1miyeT+vqmkERJ3sX9dZZ+k5dk0KmB0301LC2dZieZyn0UAP4XtcsRWzjU9AxrJak+Ql8xK5P9TypwzEyTlkL22g/ncUDb5IN+daeSael90SCVn3dSQ1jGUhjxd3lMMSfz7j52XnZboNEPb/zktlAJ8LyjaBONvtwqaZMzFoF8mS9p0vPmeS6hXH/7WavrFRad6ZV2kiW/D0oGL5w24SxL4a+xxpJjN2Tu591mzXy31PgXjhOVt4911/KLriXKTxWeeCCuv8IortQO0rZIR/q+7MJ9blqr0r0WCLzilDjD9b6XaTzAxJ0mstha7s3BNGMEvOj/qY8xryN4eE2MHqd0toCOPVhrDaCo6txzudt0rAeIlfZksgnecCaUdEZWfSL5b43RrKsHhSeoxzFZcIqu6yTPzLf6mrRBTjrdDrYlWDz3uz487RifwUQbs5MZYJ9uxsk97poBiYxtHZYWnHyEnt0A030uCavacgH86JVLFr15kNlD2b+uATl5QeNTi161ah4hkcmSjYRC4f9iPpTk2pHKaRv9DvvDp3ovI+i/IJpdFR2aXXLJnlHJpHAlhKaWQ1otkEEjmnSOcNlbfsgjDdvQte3daGwKx5LgmBwgxt2rJz0E1VE5btbKjeAypXOyUmHo3yTFeWgfbAp5MDWO7eVF+ed6ebYVLDPqzwx1Mp/eoAsVEVDtycOkvJLL2AklZ6KaqUQSatbBwdJjZP9dnXbq1s2cNuYQhj+ZVkRL0f/hg0xgyKQFHqdZownpfk1U/oXEtzvGcbUcPXXn0/l0P4YHGinDR+XwMalq9lEqa+wcYzrCJUD6ljk/eNSc4jvrti4pdliAoWoSRqJtHbekeAAISEoP6iMPy4HXDJd+vsdy2G5SzopOIWP5puknmlLoA4kqo1XP7dpn2K8UAL/X1uEweOqHW0BWY6AqsSaI0N7EN27HKl0isEI5voDY71hor98zUlyYHmP7wPAJNQI6WvNqziBxh5Wg/EyQ5jdj/TdkqoPOwPpAM1WyIqVgYR63MlNKTbXFhsb4/Qj3HzjF19T/jzttgXpIiO/LV2Q+Rf+SEMqJ7IGvJ+XrrvYvIVdhW7JMjYYZ54hVkC82E0EdrB7jcKFN0O1dxFMWQO+CMuzEcwd5+iY2jXVvj3onrYJwaVoD7+JdvRYBZooRA2n2bpZOg5ycYxHUeicO00XaK2glWOFf8WQTAgv09xmZr0gSS+XAFnWVbA+I512Z9+dZ9mTgN+681jq3PIhIweb1Ri1cwjqvHphxPLSQG31gXjjb/iDeVaHV5JBZNX2RPTDxzli16HUfh3cfmhhJkX9PSVscXbwuiO4t6Gq6YnqJaF5tTYiRN105N7Y4RivzlZgai0eLblmOoCGmBT89BPMkAIEdxCb0XSnT6k4eDXAWGTeUBtbTKtsz7CGdbt3zF2CMNlmBQAxOCovUiSKrGCF3MiF+QLcvcsrv2PmSrJM2wugXAmH0znCeUn9KKCL8Y9KdgpgdE6hJkHY9lIi8qgzkKgML9r/zCvnkWrBRN6Oh1RmS/sf0gMbhnZv4v4Z20kyXCNImeY6sL99OEmvD6ZNRaV7ACkc/Az5avCUNYhqxIaAbGKpIivgU+bbMfE/Ckl7RSmSMVO11GBUAwUI2bb2fRFFanIh1p6pi+afBWyuLZ43Onryh3MvR4rajE1fTNJjRz/eO2KdWezYTjeTN34Vy8TeQkLJK0D6he/6f+rx8EdvmNb2GltIuOiRGFVfknWw4KwFfpp3+XQ2sVxLzcaWePOodYAjZoseCa0Axyk3Wl7STgO6pchvulLjG418u1ZGiMGvIu0eXQKhjsqnvAnTufDab/dBXM5c1KAaWNC6Jy6HW3itGTXj2uF4WF2U83079IPyQ95Adt4gYHoO7agqd5jPq51GvZmGR78XvECWaUCNqru6jJeQtKKDvxIdQd/Fl7WG1RIdSj1D8ylMZA/+7eVy4uh2rtayQaVN574DtDE+";

    }

    private string NewProgramm(char ch2)
    {
        int num = 0;
        while (num < 13 && this.int1 + num < this.str200.Length)
        {
            if (this.str200[this.int1 + num] == ch2)
            {
                string result = this.str200.Substring(this.int1, num);
                this.int1 += num + 1;
                return result;
            }
            num++;
        }
        this.int1++;
        return "";
    }

    private string NewProgramm1()
    {
        string text = this.str200.Substring(this.int1);
        for (int i = 0; i < this.strmas1.Length; i++)
        {
            if (text.StartsWith(this.strmas1[i]))
            {
                this.int1 += this.strmas1[i].Length;
                return this.strmas1[i];
            }
        }
        this.int1++;
        return "";
    }

    private string NewProgramm2()
    {
        string text = this.str200.Substring(this.int1);
        for (int i = 0; i < this.strmas2.Length; i++)
        {
            if (text.StartsWith(this.strmas2[i]))
            {
                this.int1 += this.strmas2[i].Length;
                return this.strmas2[i];
            }
        }
        this.int1++;
        //Debug.Log(text);
        return "";
    }

    private void NewProgramm3()
    {
    }

    public string SaveToStringNew()
    {
        string text = "";
        for (int i = 0; i < ProgrammatorView.codes.Length; i++)
        {
            this.int2 = i;
            int num = i % ProgrammatorView.COLS;
            int num2 = Mathf.FloorToInt((float)(i / ProgrammatorView.COLS)) % ProgrammatorView.ROWS;
            Mathf.FloorToInt((float)(i / (ProgrammatorView.COLS * ProgrammatorView.ROWS)));
            switch (ProgrammatorView.codes[i])
            {
                case 0:
                    text += " ";
                    break;
                case 1:
                    text += ",";
                    break;
                case 2:
                    text += "#S";
                    break;
                case 3:
                    text += "#E";
                    break;
                case 4:
                    text += "^W";
                    break;
                case 5:
                    text += "^A";
                    break;
                case 6:
                    text += "^S";
                    break;
                case 7:
                    text += "^D";
                    break;
                case 8:
                    text += "z";
                    break;
                case 9:
                    text += "w";
                    break;
                case 10:
                    text += "a";
                    break;
                case 11:
                    text += "s";
                    break;
                case 12:
                    text += "d";
                    break;
                case 13:
                    text += "l";
                    break;
                case 14:
                    text += "^F";
                    break;
                case 15:
                    text += "CCW;";
                    break;
                case 16:
                    text += "CW;";
                    break;
                case 17:
                    text += "b";
                    break;
                case 18:
                    text += "g";
                    break;
                case 19:
                    text += "r";
                    break;
                case 20:
                    text += "h";
                    break;
                case 21:
                    text += "q";
                    break;
                case 22:
                    text += "RAND;";
                    break;
                case 23:
                    text += "BEEP;";
                    break;
                case 24:
                    text += ">";
                    text += ProgrammatorView.code_labels[this.int2];
                    text += "|";
                    break;
                case 25:
                    text += ":>";
                    text += ProgrammatorView.code_labels[this.int2];
                    text += ">";
                    break;
                case 26:
                    text += "->";
                    text += ProgrammatorView.code_labels[this.int2];
                    text += ">";
                    break;
                case 27:
                    text += "<|";
                    break;
                case 28:
                    text += "<-|";
                    break;
                case 29:
                    text += "[WA]";
                    break;
                case 30:
                    text += "[SD]";
                    break;
                case 31:
                    text += "[W]";
                    break;
                case 32:
                    text += "[DW]";
                    break;
                case 33:
                    text += "[A]";
                    break;
                case 35:
                    text += "[D]";
                    break;
                case 36:
                    text += "[AS]";
                    break;
                case 37:
                    text += "[S]";
                    break;
                case 38:
                    text += "OR";
                    break;
                case 39:
                    text += "AND";
                    break;
                case 40:
                    text += "|";
                    text += ProgrammatorView.code_labels[this.int2];
                    text += ":";
                    break;
                case 43:
                    text += "=n";
                    break;
                case 44:
                    text += "=e";
                    break;
                case 45:
                    text += "=f";
                    break;
                case 46:
                    text += "=c";
                    break;
                case 47:
                    text += "=a";
                    break;
                case 48:
                    text += "=b";
                    break;
                case 49:
                    text += "=s";
                    break;
                case 50:
                    text += "=k";
                    break;
                case 51:
                    text += "=d";
                    break;
                case 52:
                    text += "=K";
                    break;
                case 53:
                    text += "=B";
                    break;
                case 54:
                    text += "=A";
                    break;
                case 57:
                    text += "=q";
                    break;
                case 58:
                    text += "=R";
                    break;
                case 59:
                    text += "=r";
                    break;
                case 60:
                    text += "=y";
                    break;
                case 74:
                    text += "=x";
                    break;
                case 76:
                    text += "=o";
                    break;
                case 77:
                    text += "=g";
                    break;
                case 119:
                    text += "(";
                    text += ProgrammatorView.code_labels[this.int2];
                    text += ">";
                    text += ProgrammatorView.nums[i];
                    text += ")";
                    break;
                case 120:
                    text += "(";
                    text += ProgrammatorView.code_labels[this.int2];
                    text += "<";
                    text += ProgrammatorView.nums[i];
                    text += ")";
                    break;
                case 123:
                    text += "(";
                    text += ProgrammatorView.code_labels[this.int2];
                    text += "=";
                    text += ProgrammatorView.nums[i];
                    text += ")";
                    break;
                case 131:
                    text += "[w]";
                    break;
                case 132:
                    text += "[a]";
                    break;
                case 133:
                    text += "[s]";
                    break;
                case 134:
                    text += "[d]";
                    break;
                case 135:
                    text += "[F]";
                    break;
                case 136:
                    text += "[f]";
                    break;
                case 137:
                    text += "=>";
                    text += ProgrammatorView.code_labels[this.int2];
                    text += ">";
                    break;
                case 138:
                    text += "<=|";
                    break;
                case 139:
                    text += "?";
                    text += ProgrammatorView.code_labels[this.int2];
                    text += "<";
                    break;
                case 140:
                    text += "!?";
                    text += ProgrammatorView.code_labels[this.int2];
                    text += "<";
                    break;
                case 141:
                    text += "DIGG;";
                    break;
                case 142:
                    text += "BUILD;";
                    break;
                case 143:
                    text += "HEAL;";
                    break;
                case 144:
                    text += "FLIP;";
                    break;
                case 145:
                    text += "MINE;";
                    break;
                case 146:
                    text += "=G";
                    break;
                case 147:
                    text += "FILL;";
                    break;
                case 148:
                    text += "=hp-";
                    break;
                case 149:
                    text += "=hp50";
                    break;
                case 156:
                    text += "[r]";
                    break;
                case 157:
                    text += "[l]";
                    break;
                case 158:
                    text += "AUT+";
                    break;
                case 159:
                    text += "AUT-";
                    break;
                case 160:
                    text += "AGR+";
                    break;
                case 161:
                    text += "AGR-";
                    break;
                case 162:
                    text += "B1;";
                    break;
                case 163:
                    text += "B2;";
                    break;
                case 164:
                    text += "B3;";
                    break;
                case 165:
                    text += "VB;";
                    break;
                case 166:
                    text += "#R";
                    text += ProgrammatorView.code_labels[this.int2];
                    text += "<";
                    break;
                case 167:
                    text += "GEO;";
                    break;
                case 168:
                    text += "ZZ;";
                    break;
                case 169:
                    text += "C190;";
                    break;
                case 170:
                    text += "POLY;";
                    break;
                case 171:
                    text += "UP;";
                    break;
                case 172:
                    text += "CRAFT;";
                    break;
                case 173:
                    text += "NANO;";
                    break;
                case 174:
                    text += "REM;";
                    break;
                case 175:
                    text += "iw";
                    break;
                case 176:
                    text += "ia";
                    break;
                case 177:
                    text += "is";
                    break;
                case 178:
                    text += "id";
                    break;
                case 179:
                    text += "Hand+";
                    break;
                case 180:
                    text += "Hand-";
                    break;
                case 181:
                    text += "!{";
                    text += ProgrammatorView.code_labels[this.int2];
                    text += "}";
                    break;
                case 182:
                    text += "{";
                    text += ProgrammatorView.code_labels[this.int2];
                    text += "}";
                    break;
                case 200:
                    text += "RESTART;";
                    break;
            }
            if (num == ProgrammatorView.COLS - 1)
            {
                text += "\n";
            }
            if (num == ProgrammatorView.COLS - 1 && num2 == ProgrammatorView.ROWS - 1)
            {
                text += "~";
            }
        }
        text += "$";
        for (int j = 0; j < ProgrammatorView.COLS; j++)
        {
            text = text.Replace(" \n", "\n");
        }
        for (int k = 0; k < ProgrammatorView.ROWS; k++)
        {
            text = text.Replace("\n~", "~");
        }
        for (int l = 0; l < ProgrammatorView.PAGES; l++)
        {
            text = text.Replace("~$", "$");
        }
        text = text.Replace("\n\n\n\n\n\n\n\n\n\n\n", "\n.0.\n");
        text = text.Replace("\n\n\n\n\n\n\n\n\n\n", "\n.9.\n");
        text = text.Replace("\n\n\n\n\n\n\n\n\n", "\n.8.\n");
        text = text.Replace("\n\n\n\n\n\n\n\n", "\n.7.\n");
        text = text.Replace("\n\n\n\n\n\n\n", "\n.6.\n");
        text = text.Replace("\n\n\n\n\n\n", "\n.5.\n");
        text = text.Replace("\n\n\n\n\n", "\n...\n");
        text = text.Replace("\n\n\n\n", "\n..\n");
        text = text.Replace("\n\n\n", "\n.\n");
        text = text.Replace("~", "~\n");
        text = text.Replace("   ", "_");
        text = text.Replace("$", "");
        return "$" + text;
    }

    public GameObject iconPrefab;

	public GameObject iconContainer;

	public StringSelectorScript pageSelector;

	public Text titleTF;

	public static bool opened = false;

	public static bool unsaved = false;

	public const int EMPTY = 0;

	public const int BACK = 1;

	public const int START = 2;

	public const int END = 3;

	public const int MOVE_W = 4;

	public const int MOVE_A = 5;

	public const int MOVE_S = 6;

	public const int MOVE_D = 7;

	public const int DIGG = 8;

	public const int LOOK_W = 9;

	public const int LOOK_A = 10;

	public const int LOOK_S = 11;

	public const int LOOK_D = 12;

	public const int LAST = 13;

	public const int MOVE_F = 14;

	public const int ROTATE_CCW = 15;

	public const int ROTATE_CW = 16;

	public const int ACTION_BUILD = 17;

	public const int ACTION_GEO = 18;

	public const int ACTION_ROAD = 19;

	public const int ACTION_HEAL = 20;

	public const int ACTION_QUADRO = 21;

	public const int ACTION_RANDOM = 22;

	public const int ACTION_BIBIKA = 23;

	public const int ACTION_B1 = 162;

	public const int ACTION_B3 = 163;

	public const int ACTION_B2 = 164;

	public const int ACTION_WB = 165;

	public const int ACTION_GEOPACK = 167;

	public const int ACTION_ZM = 168;

	public const int ACTION_C190 = 169;

	public const int ACTION_POLY = 170;

	public const int ACTION_UP = 171;

	public const int ACTION_CRAFT = 172;

	public const int ACTION_NANO = 173;

	public const int ACTION_REMBOT = 174;

	public const int INVDIR_W = 175;

	public const int INVDIR_A = 176;

	public const int INVDIR_S = 177;

	public const int INVDIR_D = 178;

	public const int LABEL = 40;

	public const int GOTO = 24;

	public const int GOSUB = 25;

	public const int GOSUB1 = 26;

	public const int GOSUBF = 137;

	public const int RETURN = 27;

	public const int RETURN1 = 28;

	public const int RETURNF = 138;

	public const int CELL_W = 31;

	public const int CELL_WA = 29;

	public const int CELL_A = 33;

	public const int CELL_AS = 36;

	public const int CELL_S = 37;

	public const int CELL_SD = 30;

	public const int CELL_D = 35;

	public const int CELL_DW = 32;

	public const int CELL_WW = 131;

	public const int CELL_AA = 132;

	public const int CELL_SS = 133;

	public const int CELL_DD = 134;

	public const int CELL_F = 135;

	public const int CELL_FF = 136;

	public const int CC_NOTEMPTY = 43;

	public const int CC_EMPTY = 44;

	public const int CC_GRAVITY = 45;

	public const int CC_CRYSTALL = 46;

	public const int CC_ALIVE = 47;

	public const int CC_BOLDER = 48;

	public const int CC_SAND = 49;

	public const int CC_ROCK = 50;

	public const int CC_DEAD = 51;

	public const int CCC_REDROCK = 52;

	public const int CCC_BLACKROCK = 53;

	public const int CC_ACID = 54;

	public const int CCC_QUADRO = 57;

	public const int CCC_ROAD = 58;

	public const int CCC_REDBLOCK = 59;

	public const int CCC_YELLOWBLOCK = 60;

	public const int CCC_GREENBLOCK = 77;

	public const int CCC_OPOR = 76;

	public const int CCC_BOX = 74;

	public const int BOOLMODE_OR = 38;

	public const int BOOLMODE_AND = 39;

	public const int MODE_AUTODIGG_ON = 158;

	public const int MODE_AUTODIGG_OFF = 159;

	public const int MODE_AGR_ON = 160;

	public const int MODE_AGR_OFF = 161;

	public const int VAR_LESS = 120;

	public const int VAR_MORE = 119;

	public const int VAR_EQUAL = 123;

	public const int IF_NOT_GOTO = 139;

	public const int IF_GOTO = 140;

	public const int STD_DIGG = 141;

	public const int STD_BUILD = 142;

	public const int STD_HEAL = 143;

	public const int STD_MINE = 145;

	public const int CELL_RIGHT_HAND = 156;

	public const int CELL_LEFT_HAND = 157;

	public const int CC_GUN = 146;

	public const int FILL_GUN = 147;

	public const int CB_HP = 148;

	public const int CB_HP50 = 149;

	public const int PROG_FLIP = 144;

	public const int ON_RESP = 166;

	public const int RESTART = 200;

	public const int HANDMODE_ON = 179;

	public const int HANDMODE_OFF = 180;

	public const int DEBUG_BREAK = 181;

	public const int DEBUG_SET = 182;

	private int[] c_button = new int[]
	{
		43,
		44,
		45,
		46,
		47,
		48,
		49,
		50,
		51,
		54
	};

	private int[] shc_button = new int[]
	{
		53,
		52,
		77,
		60,
		59,
		76,
		57,
		58,
		74
	};

	private int[] w_button = new int[]
	{
		4,
		9,
		175
	};

	private int[] a_button = new int[]
	{
		5,
		10,
		176
	};

	private int[] s_button = new int[]
	{
		6,
		11,
		177
	};

	private int[] d_button = new int[]
	{
		7,
		12,
		178
	};

	private int[] b_button = new int[]
	{
		23,
		181,
		182
	};

	private int[] shb_button = new int[]
	{
		179,
		180
	};

	private int[] m_button = new int[]
	{
		158,
		159,
		160,
		161
	};

	private int[] shw_button = new int[]
	{
		31,
		33,
		29,
		31,
		35,
		32,
		31,
		131
	};

	private int[] sha_button = new int[]
	{
		33,
		31,
		29,
		33,
		37,
		36,
		33,
		132
	};

	private int[] shs_button = new int[]
	{
		37,
		33,
		36,
		37,
		35,
		30,
		37,
		133
	};

	private int[] shd_button = new int[]
	{
		35,
		37,
		30,
		35,
		31,
		32,
		35,
		134
	};

	private int[] z_button = new int[]
	{
		8,
		17,
		18,
		19,
		20,
		21
	};

	private int[] shz_button = new int[]
	{
		165,
		162,
		164,
		163
	};

	private int[] x_button = new int[]
	{
		167,
		168,
		170,
		169
	};

	private int[] shx_button = new int[]
	{
		172,
		171,
		173,
		174
	};

	private int[] f_button = new int[]
	{
		14
	};

	private int[] shf_button = new int[]
	{
		135,
		136,
		156,
		157
	};

	private int[] e_button = new int[]
	{
		2,
		3,
		166
	};

	private int[] r_button = new int[]
	{
		15,
		16,
		22
	};

	private int[] l_button = new int[]
	{
		40
	};

	private int[] y_button = new int[]
	{
		144
	};

	private int[] o_button = new int[]
	{
		38,
		39
	};

	private int[] i_button = new int[]
	{
		139,
		140
	};

	private int[] del_button = new int[1];

	private int[] back_button;

	private int[] t_button;

	private int[] v_button;

	private int[] g_button;

	private int[] h_button;

	private int[] j_button;

	private int[] q_button;

	public static ProgrammatorView THIS;

	private static int ROWS = 12;

	private static int COLS = 16;

	private static int PAGES = 16;

	public static int[] codes = new int[ProgrammatorView.PAGES * ProgrammatorView.ROWS * ProgrammatorView.COLS];

	public static int[] nums = new int[ProgrammatorView.PAGES * ProgrammatorView.ROWS * ProgrammatorView.COLS];

	public static string[] code_labels = new string[ProgrammatorView.PAGES * ProgrammatorView.ROWS * ProgrammatorView.COLS];

	public static ProgAction[] actions = new ProgAction[ProgrammatorView.ROWS * ProgrammatorView.COLS];

	public static bool active = false;

	public static int programId = 0;

	public static string title = "";

	private int prevPage;

	private int position;

	private Dictionary<string, int> labels;

	private string str200;

	private int int1;

	private int int2;

	private int int3;

	private int int4;

	private string[] strmas1;

	private string[] strmas2;

	private string str201;

	private string str202;

	private string str203;
}

