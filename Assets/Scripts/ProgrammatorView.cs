using SevenZip.Compression.LZMA;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Assets.Scripts.GameClasses;

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
                string num = ProgrammatorView.nums[current * ProgrammatorView.COLS * ProgrammatorView.ROWS + i * ProgrammatorView.COLS + j];
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
                string num = ProgrammatorView.nums[current * ProgrammatorView.COLS * ProgrammatorView.ROWS + i * ProgrammatorView.COLS + j];
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
                    ProgrammatorView.nums[i * ProgrammatorView.COLS * ProgrammatorView.ROWS + j * ProgrammatorView.COLS + k] = "0";
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
      Vector2 localMousePos;
      bool success = RectTransformUtility.ScreenPointToLocalPointInRectangle( GetComponent<RectTransform>(), Input.mousePosition, null,out localMousePos);

      if (!success)
        return false;

      int col = Mathf.FloorToInt(localMousePos.x / 32f);
      int row = Mathf.FloorToInt(-localMousePos.y / 32f);

      if (col < 0 || col >= COLS)
        return false;

      if (row < 0 || row >= ROWS)
        return false;

      position = row * COLS + col;

      return true;
    }

    private void ShiftCode(int dx, int dy)
    {
      if (!MakePosition())
        return;

      int startIndex = position;                  // Flat index of the clicked cell on the current page (0..ROWS*COLS-1)
      int stepsToEmpty = 0;                       // How many non-empty cells we pass before hitting an empty one
      int currentPage = Mathf.FloorToInt((float)(startIndex / (ROWS * COLS)));
      int currentRow  = Mathf.FloorToInt((float)((startIndex - currentPage * ROWS * COLS) / COLS));
      int currentCol  = startIndex % COLS;

      int targetIndex = startIndex;
      bool foundEmpty = false;

      // Search along the line (horizontal if dy==0, vertical if dx==0) until we hit an empty cell or boundary
      for (int i = 0; i < 1000; i++)
      {
        if (actions[targetIndex].id == 0)
        {
          foundEmpty = true;
          break;
        }

        stepsToEmpty++;
        targetIndex += dx + COLS * dy;

        // Safety / boundary checks
        if (targetIndex < 0)
          break;

        int targetPage = Mathf.FloorToInt((float)(targetIndex / (ROWS * COLS)));
        int targetRow  = Mathf.FloorToInt((float)((targetIndex - targetPage * ROWS * COLS) / COLS));
        int targetCol  = targetIndex % COLS;

        // Must stay on the same page
        if (targetPage != currentPage)
          break;

        // Horizontal shift (dy==0) must stay in the same row
        if (dy == 0 && targetRow != currentRow)
          break;

        // Vertical shift (dx==0) must stay in the same column
        if (dx == 0 && targetCol != currentCol)
          break;
      }

      // Only shift if we actually found an empty cell in the line
      if (foundEmpty)
      {
        // Move each cell one step toward the empty cell (bubble the empty space backward)
        for (int step = stepsToEmpty; step > 0; step--)
        {
          int sourceIndex = startIndex + (step - 1) * (dx + COLS * dy);
          int destIndex   = startIndex + step * (dx + COLS * dy);

          ProgAction source = actions[sourceIndex];
          ProgAction dest   = actions[destIndex];

          dest.setString(source.getString());
          dest.setNum(source.getNum());
          dest.ChangeTo(source.id);
        }

        // If we moved anything, the original clicked cell becomes empty
        if (stepsToEmpty > 0)
        {
          actions[startIndex].ChangeTo(0);
          unsaved = true;
          titleTF.text = title + " [*]";
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

    private void CyclicChange(int[] cycle, int fallbackIndex = -1)
    {
      int currentIndexInCycle = -1;
      int currentId = actions[position].id;

      for (int i = 0; i < cycle.Length; i++)
      {
        if (cycle[i] == currentId)
        {
          currentIndexInCycle = i;
          break;  // Early exit once found
        }
      }

      if (currentIndexInCycle == -1) {
        currentIndexInCycle = fallbackIndex;
      }

      int nextIndex = (currentIndexInCycle + 1) % cycle.Length;
      int nextId = cycle[nextIndex];

      if (currentId != nextId)
      {
        unsaved = true;
        titleTF.text = title + " [*]";
      }

      actions[position].ChangeTo(nextId);
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
      ClearSource();
      if (source.Length == 0) return;

      char first = source[0];
      if (first == 'X')
      {
        byte[] data = SevenZipHelper.Decompress(Convert.FromBase64String(source));
        int count = BitConverter.ToInt32(data, 0);
        for (int i = 0; i < count; i++)
        {
          codes[i] = (int)Convert.ToInt16(data[i + 4]);
        }
        string labelsStr = Encoding.UTF8.GetString(data, count + 4, data.Length - count - 4);
        string[] labelParts = labelsStr.Split(':');
        for (int j = 0; j < labelParts.Length; j++)
        {
          string part = labelParts[j];
          if (part.Contains("@"))
          {
            string[] sub = part.Split('@');
            code_labels[j] = sub[0];
            nums[j] = sub[1];
          }
          else
          {
            code_labels[j] = part;
            nums[j] = "0";
          }
        }
      }
      else
      {
        bool isNew = first == '$';
        if (isNew) source = source.Replace("$", "");
        source = source.Replace("\r", "").Replace("_", "   ")
          .Replace(".0.", "\n\n\n\n\n\n\n\n\n")
          .Replace(".9.", "\n\n\n\n\n\n\n\n")
          .Replace(".8.", "\n\n\n\n\n\n\n")
          .Replace(".7.", "\n\n\n\n\n\n")
          .Replace(".6.", "\n\n\n\n\n")
          .Replace(".5.", "\n\n\n\n")
          .Replace("...", "\n\n\n")
          .Replace("..", "\n\n")
          .Replace(".", "\n");
        preprocessedProgramText = source;
        currentCharIndex = 0;
        currentCellIndex = 0;
        remainingColsInRow = COLS;
        remainingCellsOnPage = COLS * ROWS;

        Func<string> getToken = isNew ? (Func<string>)NewProgramm2 : NewProgramm1;
        while (currentCharIndex < preprocessedProgramText.Length && currentCellIndex <= COLS * ROWS * PAGES)
        {
          string token = getToken();
          int id = -1;
          switch (token)
          {
            case "CRAFT;": id = (int)CustomProgAction.RefillCraft; break;
            case "[DW]": id = (int)CustomProgAction.CellUpRight; break;
            case "=hp50": id = (int)CustomProgAction.IsHealthLessThanHalf; break;
            case "^A": case "A": id = (int)CustomProgAction.MoveLeft; break;
            case "->": id = (int)CustomProgAction.CallArg; code_labels[currentCellIndex] = NewProgramm('>'); break;
            case "BEEP;": id = (int)CustomProgAction.PlaySound; break;
            case "HEAL;": id = (int)CustomProgAction.STDHeal; break;
            case "B1;": id = (int)CustomProgAction.UseBoom; break;
            case "^F": case "F": id = (int)CustomProgAction.MoveForward; break;
            case "^D": case "D": id = (int)CustomProgAction.MoveRight; break;
            case "#R": id = (int)CustomProgAction.CallWhenDied; break;
            case " ": id = (int)CustomProgAction.None; break;
            case "#S": id = (int)CustomProgAction.SetStart; break;
            case "CW;": id = (int)CustomProgAction.RotateRighthand; break;
            case "^S": case "S": id = (int)CustomProgAction.MoveDown; break;
            case "^W": case "W": id = (int)CustomProgAction.MoveUp; break;
            case ",": id = (int)CustomProgAction.NextLine; break;
            case "C190;": id = (int)CustomProgAction.UseC190; break;
            case "(": 
                          string inner = NewProgramm(')');
                          string[] parts;
                          if (inner.Contains("=")) { parts = inner.Split('='); id = (int)CustomProgAction.VarEqualsNumber; }
                          else if (inner.Contains("<")) { parts = inner.Split('<'); id = (int)CustomProgAction.VarLessThanNumber; }
                          else if (inner.Contains(">")) { parts = inner.Split('>'); id = (int)CustomProgAction.VarGreaterThanNumber; }
                          else break;
                          code_labels[currentCellIndex] = parts[0];
                          nums[currentCellIndex] = int.TryParse(parts[1], out int n) ? n.ToString() : "0";
                          break;
            case "AUT+": id = (int)CustomProgAction.EnableAutoDig; break;
            case "id": id = (int)CustomProgAction.InventoryRight; break;
            case "AUT-": id = (int)CustomProgAction.DisableAutoDig; break;
            case ">": id = (int)CustomProgAction.Goto; code_labels[currentCellIndex] = NewProgramm('|'); break;
            case "?": id = (int)CustomProgAction.YesNoGoto; code_labels[currentCellIndex] = NewProgramm('<'); break;
            case "ia": id = (int)CustomProgAction.InventoryLeft; break;
            case "AGR+": id = (int)CustomProgAction.EnableAggression; break;
            case "=k": id = (int)CustomProgAction.IsBreakable; break;
            case "AGR-": id = (int)CustomProgAction.DisableAggression; break;
            case "#E": id = (int)CustomProgAction.Terminate; break;
            case "=o": id = (int)CustomProgAction.IsStructure; break;
            case "=n": id = (int)CustomProgAction.IsNotEmpty; break;
            case "FLIP;": id = (int)CustomProgAction.Flip; break;
            case "=c": id = (int)CustomProgAction.IsCrystal; break;
            case "=b": id = (int)CustomProgAction.IsFallingLikeBoulder; break;
            case "=a": id = (int)CustomProgAction.IsAliveCrystal; break;
            case "=e": id = (int)CustomProgAction.IsEmpty; break;
            case "=d": id = (int)CustomProgAction.IsUnbreakable; break;
            case "iw": id = (int)CustomProgAction.InventoryUp; break;
            case "=f": id = (int)CustomProgAction.IsFalling; break;
            case "=y": id = (int)CustomProgAction.IsYellowBlock; break;
            case "=x": id = (int)CustomProgAction.IsBox; break;
            case "=g": id = (int)CustomProgAction.IsGreenBlock; break;
            case "<=|": id = (int)CustomProgAction.ReturnState; break;
            case "is": id = (int)CustomProgAction.InventoryDown; break;
            case "GEO;": id = (int)CustomProgAction.UseGeopack; break;
            case "<-|": id = (int)CustomProgAction.ReturnArg; break;
            case "<|": id = (int)CustomProgAction.Return; break;
            case "=r": id = (int)CustomProgAction.IsRedBlock; break;
            case "=q": id = (int)CustomProgAction.IsQuadro; break;
            case "=s": id = (int)CustomProgAction.IsFallingLikeLiquid; break;
            case "!{": id = (int)CustomProgAction.DebugPause; code_labels[currentCellIndex] = NewProgramm('}'); break;
            case "RAND;": id = (int)CustomProgAction.RotateRandom; break;
            case "=K": id = (int)CustomProgAction.IsRedRock; break;
            case "=B": id = (int)CustomProgAction.IsBlackRock; break;
            case "=A": id = (int)CustomProgAction.IsAcid; break;
            case "UP;": id = (int)CustomProgAction.Upgrade; break;
            case "[r]": id = (int)CustomProgAction.CellLefthand; break;
            case "=R": id = (int)CustomProgAction.IsRoad; break;
            case "=G": id = (int)CustomProgAction.IsInsideGun; break;
            case "[S]": id = (int)CustomProgAction.CellDown; break;
            case "[f]": id = (int)CustomProgAction.ShiftForward; break;
            case "[a]": id = (int)CustomProgAction.ShiftLeft; break;
            case "[D]": id = (int)CustomProgAction.CellRight; break;
            case "[W]": id = (int)CustomProgAction.CellUp; break;
            case "=hp-": id = (int)CustomProgAction.IsHealthNotFull; break;
            case "OR": id = (int)CustomProgAction.BooleanOR; break;
            case "AND": id = (int)CustomProgAction.BooleanAND; break;
            case "FILL;": id = (int)CustomProgAction.ChargeGun; break;
            case "B3;": id = (int)CustomProgAction.UseProt; break;
            case "!?": id = (int)CustomProgAction.NoYesGoto; code_labels[currentCellIndex] = NewProgramm('<'); break;
            case "=>": id = (int)CustomProgAction.CallState; code_labels[currentCellIndex] = NewProgramm('>'); break;
            case "B2;": id = (int)CustomProgAction.UseRaz; break;
            case ":>": id = (int)CustomProgAction.Call; code_labels[currentCellIndex] = NewProgramm('>'); break;
            case "DIGG;": id = (int)CustomProgAction.STDDig; break;
            case "VB;": id = (int)CustomProgAction.BuildWar; break;
            case "RESTART;": id = (int)CustomProgAction.UNUSED_200; break;
            case "CCW;": id = (int)CustomProgAction.RotateLefthand; break;
            case "[A]": id = (int)CustomProgAction.CellLeft; break;
            case "BUILD;": id = (int)CustomProgAction.STDBlock; break;
            case "[w]": id = (int)CustomProgAction.ShiftUp; break;
            case "[s]": id = (int)CustomProgAction.ShiftDown; break;
            case "[F]": id = (int)CustomProgAction.CellForward; break;
            case "[d]": id = (int)CustomProgAction.ShiftRight; break;
            case "[l]": id = (int)CustomProgAction.CellRighthand; break;
            case "NANO;": id = (int)CustomProgAction.UseNano; break;
            case "REM;": id = (int)CustomProgAction.UseRem; break;
            case "POLY;": id = (int)CustomProgAction.UsePoly; break;
            case "[AS]": id = (int)CustomProgAction.CellDownLeft; break;
            case "ZZ;": id = (int)CustomProgAction.UseZZ; break;
            case "[SD]": id = (int)CustomProgAction.CellDownRight; break;
            case "[WA]": id = (int)CustomProgAction.CellUpLeft; break;
            case "d": id = (int)CustomProgAction.RotateRight; break;
            case "MINE;": id = (int)CustomProgAction.STDTunnel; break;
            case "Hand-": id = (int)CustomProgAction.DisableHand; break;
            case "g": id = (int)CustomProgAction.UseGeo; break;
            case "Hand+": id = (int)CustomProgAction.EnableHand; break;
            case "b": id = (int)CustomProgAction.BuildBlock; break;
            case "a": id = (int)CustomProgAction.RotateLeft; break;
            case "h": id = (int)CustomProgAction.Heal; break;
            case "l": id = (int)CustomProgAction.RepeatLastAction; break;
            case "s": id = (int)CustomProgAction.RotateDown; break;
            case "q": id = (int)CustomProgAction.BuildQuadro; break;
            case "w": id = (int)CustomProgAction.RotateUp; break;
            case "|": id = (int)CustomProgAction.Label; code_labels[currentCellIndex] = NewProgramm(':'); break;
            case "r": id = (int)CustomProgAction.BuildRoad; break;
            case "z": id = (int)CustomProgAction.Dig; break;
            case "{": id = (int)CustomProgAction.DebugShow; code_labels[currentCellIndex] = NewProgramm('}'); break;
            case "\n":
                      currentCellIndex += remainingColsInRow;
                      remainingCellsOnPage -= remainingColsInRow;
                      remainingColsInRow = COLS;
                      break;
            case "~\n":
                      currentCellIndex += remainingCellsOnPage;
                      remainingColsInRow = COLS;
                      remainingCellsOnPage = COLS * ROWS;
                      break;
          }
          if (id != -1)
          {
            codes[currentCellIndex] = id;
            currentCellIndex++;
            remainingColsInRow--;
            remainingCellsOnPage--;
          }
        }
      }
      UpdateIconsWithoutSaving();
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
            if (ProgrammatorView.code_labels[j] != "0" || ProgrammatorView.nums[j] != "0")
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
            if (ProgrammatorView.nums[k] != "0")
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
      UpdateIcons();
      labels = new Dictionary<string, int>();
      int instrCount = 0;
      for (int page = 0; page < PAGES; page++)
      {
        for (int row = 0; row < ROWS; row++)
        {
          bool rowStarted = false;
          bool skipRest = false;
          for (int col = 0; col < COLS; col++)
          {
            if (skipRest) break;
            int id = codes[page * ROWS * COLS + row * COLS + col];
            if (id != (int)CustomProgAction.None) rowStarted = true;
            if (id == (int)CustomProgAction.NextLine)
            {
              skipRest = true;
            }
            else if (id == (int)CustomProgAction.Label)
            {
              labels[code_labels[page * ROWS * COLS + row * COLS + col]] = instrCount;
            }
            else
            {
              if (rowStarted)
              {
                instrCount++;
              }
            }
          }
        }
      }
      byte[] program = new byte[instrCount * 2 + 4];
      byte[] countBytes = BitConverter.GetBytes(instrCount);
      Buffer.BlockCopy(countBytes, 0, program, 0, 4);
      int index = 4;
      for (int page = 0; page < PAGES; page++)
      {
        for (int row = 0; row < ROWS; row++)
        {
          bool rowStarted = false;
          bool skipRest = false;
          for (int col = 0; col < COLS; col++)
          {
            if (skipRest) break;
            int id = codes[page * ROWS * COLS + row * COLS + col];
            string val = nums[page * ROWS * COLS + row * COLS + col];
            string label = code_labels[page * ROWS * COLS + row * COLS + col];
            if (id != (int)CustomProgAction.None) rowStarted = true;
            if (id == (int)CustomProgAction.NextLine)
            {
              skipRest = true;
            }
            else if (id == (int)CustomProgAction.Label)
            {
              // already set
            }
            else
            {
              if (rowStarted)
              {
                int arg = 0;
                if (id == (int)CustomProgAction.Goto ||
                    id == (int)CustomProgAction.Call ||
                    id == (int)CustomProgAction.CallArg ||
                    id == (int)CustomProgAction.YesNoGoto ||
                    id == (int)CustomProgAction.NoYesGoto ||
                    id == (int)CustomProgAction.CallState ||
                    id == (int)CustomProgAction.CallWhenDied)
                {
                  if (labels.TryGetValue(label, out int pos))
                  {
                    arg = pos;
                  }
                }
                else if (id == (int)CustomProgAction.VarGreaterThanNumber ||
                    id == (int)CustomProgAction.VarLessThanNumber ||
                    id == (int)CustomProgAction.VarEqualsNumber)
                {
                  arg = int.TryParse(val, out int n321) ? n321 : 0;
                }
                program[index] = (byte)id;
                program[index + 1] = (byte)arg;
                index += 2;
              }
            }
          }
        }
      }
      return program;
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

                if (UnityEngine.Input.GetKeyDown(KeyCode.K))
                {
                  this.MakeCycle(this.k_button, -1);
                }
                if (UnityEngine.Input.GetKeyDown(KeyCode.N))
                {
                  if (UnityEngine.Input.GetKey(KeyCode.LeftShift) || UnityEngine.Input.GetKey(KeyCode.RightShift)){
                    this.MakeCycle(this.shn_button, -1);
                  }else{
                    this.MakeCycle(this.n_button, -1);
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
                  if (UnityEngine.Input.GetKey(KeyCode.LeftShift) || UnityEngine.Input.GetKey(KeyCode.RightShift)){
                    this.MakeCycle(this.shv_button, -1);
                  }else{
                    this.MakeCycle(this.v_button, -1);
                  }
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
                if (UnityEngine.Input.GetKeyDown(KeyCode.K))
                {
                  this.MakeCycle(this.k_button, -1);
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
                  if (UnityEngine.Input.GetKey(KeyCode.LeftShift) || UnityEngine.Input.GetKey(KeyCode.RightShift)){
                    this.MakeCycle(this.shi_button, -1);
                  }else{
                    this.MakeCycle(this.i_button, -1);
                  }
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
                  if (UnityEngine.Input.GetKey(KeyCode.LeftShift) || UnityEngine.Input.GetKey(KeyCode.RightShift)){
                    this.MakeCycle(this.shm_button, -1);
                  }else{
                    this.MakeCycle(this.m_button, -1);
                  }
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
        this.position = -1;
        this.preprocessedProgramText = "";
        this.sampleProgram1 = "XQAAgACaAQAAAAAAAAA+gDAFAhG0ciqkm4PJHRcPgIruSXXpLO0McQ2EaD0CLDkX1Bwh+LW9OM3QB1T+3qVdAUpFp581o584qDxt6eJa5ZmL9IccwWgVwreNz8sedV9O4qdfTvr9Fq21T0SXWLC0hOZTGUlNudYz1VsnqRCp4i7gf/AWcIHs2KgLmwS8IEgJkaNgAA==";
        this.sampleProgram2 = "XQAAgADOAQAAAAAAAABKADAFCPBEHWF/wkqwvU4Eb7ohVxyHTQCnB1tuOQ/mId9a0zVcyCiQh32E9c7xYZu2Pl8fEh/P3fjzX7/iO1MmqYc/qeocZ8w1lXWa7RaZcFhX7GuXWTLBsXzjlA0KeZpby+P8vwIXwiaA4LU3j9kw1D7zQzt/VRLMs5eYauhxvlEpC/VRXtxodxe8aLB9QD25fpbplLo/R0aUG1SxETQdFrKdpAzY2gA=";
        this.sampleProgram3 = "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";

    }

    private string NewProgramm(char ch2)
    {
        int num = 0;
        while (num < 13 && this.currentCharIndex + num < this.preprocessedProgramText.Length)
        {
            if (this.preprocessedProgramText[this.currentCharIndex + num] == ch2)
            {
                string result = this.preprocessedProgramText.Substring(this.currentCharIndex, num);
                this.currentCharIndex += num + 1;
                return result;
            }
            num++;
        }
        this.currentCharIndex++;
        return "";
    }

    private string NewProgramm1()
    {
        string text = this.preprocessedProgramText.Substring(this.currentCharIndex);
        for (int i = 0; i < this.strmas1.Length; i++)
        {
            if (text.StartsWith(this.strmas1[i]))
            {
                this.currentCharIndex += this.strmas1[i].Length;
                return this.strmas1[i];
            }
        }
        this.currentCharIndex++;
        return "";
    }

    private string NewProgramm2()
    {
        string text = this.preprocessedProgramText.Substring(this.currentCharIndex);
        for (int i = 0; i < this.strmas2.Length; i++)
        {
            if (text.StartsWith(this.strmas2[i]))
            {
                this.currentCharIndex += this.strmas2[i].Length;
                return this.strmas2[i];
            }
        }
        this.currentCharIndex++;
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
            this.currentCellIndex = i;
            int num = i % ProgrammatorView.COLS;
            int num2 = Mathf.FloorToInt((float)(i / ProgrammatorView.COLS)) % ProgrammatorView.ROWS;
            Mathf.FloorToInt((float)(i / (ProgrammatorView.COLS * ProgrammatorView.ROWS)));
            switch (ProgrammatorView.codes[i])
            {
                case (int)CustomProgAction.None: text += " "; break;
                case (int)CustomProgAction.NextLine: text += ","; break;
                case (int)CustomProgAction.SetStart: text += "#S"; break;
                case (int)CustomProgAction.Terminate: text += "#E"; break;
                case (int)CustomProgAction.MoveUp: text += "^W"; break;
                case (int)CustomProgAction.MoveLeft: text += "^A"; break;
                case (int)CustomProgAction.MoveDown: text += "^S"; break;
                case (int)CustomProgAction.MoveRight: text += "^D"; break;
                case (int)CustomProgAction.Dig: text += "z"; break;
                case (int)CustomProgAction.RotateUp: text += "w"; break;
                case (int)CustomProgAction.RotateLeft: text += "a"; break;
                case (int)CustomProgAction.RotateDown: text += "s"; break;
                case (int)CustomProgAction.RotateRight: text += "d"; break;
                case (int)CustomProgAction.RepeatLastAction: text += "l"; break;
                case (int)CustomProgAction.MoveForward: text += "^F"; break;
                case (int)CustomProgAction.RotateLefthand: text += "CCW;"; break;
                case (int)CustomProgAction.RotateRighthand: text += "CW;"; break;
                case (int)CustomProgAction.BuildBlock: text += "b"; break;
                case (int)CustomProgAction.UseGeo: text += "g"; break;
                case (int)CustomProgAction.BuildRoad: text += "r"; break;
                case (int)CustomProgAction.Heal: text += "h"; break;
                case (int)CustomProgAction.BuildQuadro: text += "q"; break;
                case (int)CustomProgAction.RotateRandom: text += "RAND;"; break;
                case (int)CustomProgAction.PlaySound: text += "BEEP;"; break;
                case (int)CustomProgAction.Goto:
                    text += ">";
                    text += ProgrammatorView.code_labels[this.currentCellIndex];
                    text += "|";
                    break;
                case (int)CustomProgAction.Call:
                    text += ":>";
                    text += ProgrammatorView.code_labels[this.currentCellIndex];
                    text += ">";
                    break;
                case (int)CustomProgAction.CallArg:
                    text += "->";
                    text += ProgrammatorView.code_labels[this.currentCellIndex];
                    text += ">";
                    break;
                case (int)CustomProgAction.Return: text += "<|"; break;
                case (int)CustomProgAction.ReturnArg: text += "<-|"; break;
                case (int)CustomProgAction.CellUpLeft: text += "[WA]"; break;
                case (int)CustomProgAction.CellDownRight: text += "[SD]"; break;
                case (int)CustomProgAction.CellUp: text += "[W]"; break;
                case (int)CustomProgAction.CellUpRight: text += "[DW]"; break;
                case (int)CustomProgAction.CellLeft: text += "[A]"; break;
                case (int)CustomProgAction.CellRight: text += "[D]"; break;
                case (int)CustomProgAction.CellDownLeft: text += "[AS]"; break;
                case (int)CustomProgAction.CellDown: text += "[S]"; break;
                case (int)CustomProgAction.BooleanOR: text += "OR"; break;
                case (int)CustomProgAction.BooleanAND: text += "AND"; break;
                case (int)CustomProgAction.Label:
                    text += "|";
                    text += ProgrammatorView.code_labels[this.currentCellIndex];
                    text += ":";
                    break;
                case (int)CustomProgAction.IsNotEmpty: text += "=n"; break;
                case (int)CustomProgAction.IsEmpty: text += "=e"; break;
                case (int)CustomProgAction.IsFalling: text += "=f"; break;
                case (int)CustomProgAction.IsCrystal: text += "=c"; break;
                case (int)CustomProgAction.IsAliveCrystal: text += "=a"; break;
                case (int)CustomProgAction.IsFallingLikeBoulder: text += "=b"; break;
                case (int)CustomProgAction.IsFallingLikeLiquid: text += "=s"; break;
                case (int)CustomProgAction.IsBreakable: text += "=k"; break;
                case (int)CustomProgAction.IsUnbreakable: text += "=d"; break;
                case (int)CustomProgAction.IsRedRock: text += "=K"; break;
                case (int)CustomProgAction.IsBlackRock: text += "=B"; break;
                case (int)CustomProgAction.IsAcid: text += "=A"; break;
                case (int)CustomProgAction.IsQuadro: text += "=q"; break;
                case (int)CustomProgAction.IsRoad: text += "=R"; break;
                case (int)CustomProgAction.IsRedBlock: text += "=r"; break;
                case (int)CustomProgAction.IsYellowBlock: text += "=y"; break;
                case (int)CustomProgAction.IsBox: text += "=x"; break;
                case (int)CustomProgAction.IsStructure: text += "=o"; break;
                case (int)CustomProgAction.IsGreenBlock: text += "=g"; break;
                case (int)CustomProgAction.VarGreaterThanNumber:
                    text += "(";
                    text += ProgrammatorView.code_labels[this.currentCellIndex];
                    text += ">";
                    text += ProgrammatorView.nums[i];
                    text += ")";
                    break;
                case (int)CustomProgAction.VarLessThanNumber:
                    text += "(";
                    text += ProgrammatorView.code_labels[this.currentCellIndex];
                    text += "<";
                    text += ProgrammatorView.nums[i];
                    text += ")";
                    break;
                case (int)CustomProgAction.VarEqualsNumber:
                    text += "(";
                    text += ProgrammatorView.code_labels[this.currentCellIndex];
                    text += "=";
                    text += ProgrammatorView.nums[i];
                    text += ")";
                    break;
                case (int)CustomProgAction.VarLessThanOrEqualNumber:
                    text += "(";
                    text += ProgrammatorView.code_labels[this.currentCellIndex];
                    text += "<=";
                    text += ProgrammatorView.nums[i];
                    text += ")";
                    break;
                case (int)CustomProgAction.VarGreaterThanOrEqualNumber:
                    text += "(";
                    text += ProgrammatorView.code_labels[this.currentCellIndex];
                    text += ">=";
                    text += ProgrammatorView.nums[i];
                    text += ")";
                    break;
                case (int)CustomProgAction.VarNotEqualsNumber:
                    text += "(";
                    text += ProgrammatorView.code_labels[this.currentCellIndex];
                    text += "<>";
                    text += ProgrammatorView.nums[i];
                    text += ")";
                    break;
                //case (int)CustomProgAction.AddNumberToVar:
                //    text += "(";
                //    text += ProgrammatorView.code_labels[this.currentCellIndex];
                //    text += "+";
                //    text += ProgrammatorView.nums[i];
                //    text += ")";
                //    break;


                case (int)CustomProgAction.ShiftUp: text += "[w]"; break;
                case (int)CustomProgAction.ShiftLeft: text += "[a]"; break;
                case (int)CustomProgAction.ShiftDown: text += "[s]"; break;
                case (int)CustomProgAction.ShiftRight: text += "[d]"; break;
                case (int)CustomProgAction.CellForward: text += "[F]"; break;
                case (int)CustomProgAction.ShiftForward: text += "[f]"; break;
                case (int)CustomProgAction.CallState:
                    text += "=>";
                    text += ProgrammatorView.code_labels[this.currentCellIndex];
                    text += ">";
                    break;
                case (int)CustomProgAction.ReturnState: text += "<=|"; break;
                case (int)CustomProgAction.YesNoGoto:
                    text += "?";
                    text += ProgrammatorView.code_labels[this.currentCellIndex];
                    text += "<";
                    break;
                case (int)CustomProgAction.NoYesGoto:
                    text += "!?";
                    text += ProgrammatorView.code_labels[this.currentCellIndex];
                    text += "<";
                    break;
                case (int)CustomProgAction.STDDig: text += "DIGG;"; break;
                case (int)CustomProgAction.STDBlock: text += "BUILD;"; break;
                case (int)CustomProgAction.STDHeal: text += "HEAL;"; break;
                case (int)CustomProgAction.Flip: text += "FLIP;"; break;
                case (int)CustomProgAction.STDTunnel: text += "MINE;"; break;
                case (int)CustomProgAction.IsInsideGun: text += "=G"; break;
                case (int)CustomProgAction.ChargeGun: text += "FILL;"; break;
                case (int)CustomProgAction.IsHealthNotFull: text += "=hp-"; break;
                case (int)CustomProgAction.IsHealthLessThanHalf: text += "=hp50"; break;
                case (int)CustomProgAction.CellLefthand: text += "[r]"; break;
                case (int)CustomProgAction.CellRighthand: text += "[l]"; break;
                case (int)CustomProgAction.EnableAutoDig: text += "AUT+"; break;
                case (int)CustomProgAction.DisableAutoDig: text += "AUT-"; break;
                case (int)CustomProgAction.EnableAggression: text += "AGR+"; break;
                case (int)CustomProgAction.DisableAggression: text += "AGR-"; break;
                case (int)CustomProgAction.UseBoom: text += "B1;"; break;
                case (int)CustomProgAction.UseRaz: text += "B2;"; break;
                case (int)CustomProgAction.UseProt: text += "B3;"; break;
                case (int)CustomProgAction.BuildWar: text += "VB;"; break;
                case (int)CustomProgAction.CallWhenDied:
                    text += "#R";
                    text += ProgrammatorView.code_labels[this.currentCellIndex];
                    text += "<";
                    break;
                case (int)CustomProgAction.UseGeopack: text += "GEO;"; break;
                case (int)CustomProgAction.UseZZ: text += "ZZ;"; break;
                case (int)CustomProgAction.UseC190: text += "C190;"; break;
                case (int)CustomProgAction.UsePoly: text += "POLY;"; break;
                case (int)CustomProgAction.Upgrade: text += "UP;"; break;
                case (int)CustomProgAction.RefillCraft: text += "CRAFT;"; break;
                case (int)CustomProgAction.UseNano: text += "NANO;"; break;
                case (int)CustomProgAction.UseRem: text += "REM;"; break;
                case (int)CustomProgAction.InventoryUp: text += "iw"; break;
                case (int)CustomProgAction.InventoryLeft: text += "ia"; break;
                case (int)CustomProgAction.InventoryDown: text += "is"; break;
                case (int)CustomProgAction.InventoryRight: text += "id"; break;
                case (int)CustomProgAction.EnableHand: text += "Hand+"; break;
                case (int)CustomProgAction.DisableHand: text += "Hand-"; break;
                case (int)CustomProgAction.DebugPause:
                    text += "!{";
                    text += ProgrammatorView.code_labels[this.currentCellIndex];
                    text += "}";
                    break;
                case (int)CustomProgAction.DebugShow:
                    text += "{";
                    text += ProgrammatorView.code_labels[this.currentCellIndex];
                    text += "}";
                    break;
                case (int)CustomProgAction.UNUSED_200: text += "RESTART;"; break;
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


    public enum CustomProgAction : int
    {                            
      None                       = 0,
      NextLine                   = 1,
      SetStart                   = 2,
      Terminate                  = 3,
      MoveUp                     = 4,
      MoveLeft                   = 5,
      MoveDown                   = 6,
      MoveRight                  = 7,
      Dig                        = 8,
      RotateUp                   = 9,
      RotateLeft                 = 10,
      RotateDown                 = 11,
      RotateRight                = 12,
      RepeatLastAction           = 13,
      MoveForward                = 14,
      RotateLefthand             = 15,
      RotateRighthand            = 16,
      BuildBlock                 = 17,
      UseGeo                     = 18,
      BuildRoad                  = 19,
      Heal                       = 20,
      BuildQuadro                = 21,
      RotateRandom               = 22,
      PlaySound                  = 23,
      Goto                       = 24,
      Call                       = 25,
      CallArg                    = 26,
      Return                     = 27,
      ReturnArg                  = 28,
      CellUpLeft                 = 29,
      CellDownRight              = 30,
      CellUp                     = 31,
      CellUpRight                = 32,
      CellLeft                   = 33,
      Cell                       = 34,
      CellRight                  = 35,
      CellDownLeft               = 36,
      CellDown                   = 37,
      BooleanOR                  = 38,
      BooleanAND                 = 39,
      Label                      = 40,
      YesNoReturn                = 41,
      NoYesReturn                = 42,
      IsNotEmpty                 = 43,
      IsEmpty                    = 44,
      IsFalling                  = 45,
      IsCrystal                  = 46,
      IsAliveCrystal             = 47,
      IsFallingLikeBoulder       = 48,
      IsFallingLikeLiquid        = 49,
      IsBreakable                = 50,
      IsUnbreakable              = 51,
      IsRedRock                  = 52,
      IsBlackRock                = 53,
      IsAcid                     = 54,
      UNKNOWN_CONDITION          = 55,
      IsSand                     = 56,
      IsQuadro                   = 57,
      IsRoad                     = 58,
      IsRedBlock                 = 59,
      IsYellowBlock              = 60,
      UNKNOWN_MINUS_HEALTH       = 61,
      UNKNOWN_LESS_HEALTH        = 62,
      IsAcidRock                 = 63,
      IsBoulder                  = 64,
      IsLava                     = 65,
      IsCyanAlive                = 66,
      IsWhiteAlive               = 67,
      IsRedAlive                 = 68,
      IsVioletAlive              = 69,
      IsBlackAlive               = 70,
      IsBlueAlive                = 71,
      IsRainbowAlive             = 72,
      UNKNOWN_73                 = 73,
      IsBox                      = 74,
      UNKNOWN_75                 = 75,
      IsStructure                = 76,
      IsGreenBlock               = 77,
      IsBasketFull               = 78,
      IsGeoFull                  = 79,
      UNKNOWN_80                 = 80,
      SetStartWhenDied           = 81,
      SetStartWhenHurt           = 82,
      SetStartWhenBotNearby      = 83,
      UNKNOWN_84                 = 84,
      UNKNOWN_85                 = 85,
      ShiftLefthand              = 86,
      ShiftRighthand             = 87,
      ShiftBackwards             = 88,
      BoxAll                     = 89,
      BoxHalf                    = 90,
      BoxWhite                   = 91,
      BoxGreen                   = 92,
      BoxRed                     = 93,
      BoxBlue                    = 94,
      BoxCyan                    = 95,
      BoxViolet                  = 96,
      WriteStateToVar            = 97,
      ReadVarToState             = 98,
      SetNumberToVar             = 99,
      AddNumberToVar             = 100,
      MultNumberToVar            = 101,
      DivNumberToVar             = 102,
      SubNumberToVar             = 103,
      AddStateToVar              = 104,
      MultStateToVar             = 105,
      DivStateToVar              = 106,
      SubStateToVar              = 107,
      AddVarToVar                = 108,
      MultVarToVar               = 109,
      DivVarToVar                = 110,
      SubVarToVar                = 111,
      VarLessThanState           = 112,
      VarGreaterThanState        = 113,
      VarGreaterThanOrEqualsState= 114,
      VarLessThanOrEqualState    = 115,
      VarEqualsState             = 116,
      VarNotEqualsState          = 117,
      UNKNOWN_118                = 118,
      VarGreaterThanNumber       = 119,
      VarLessThanNumber          = 120,
      VarGreaterThanOrEqualNumber= 121,
      VarLessThanOrEqualNumber   = 122,
      VarEqualsNumber            = 123,
      VarNotEqualsNumber         = 124,
      VarRound                   = 125,
      VarCeil                    = 126,
      VarFloor                   = 127,
      Var_UNK_128                = 128,
      Var_UNK_129                = 129,
      Var_UNK_130                = 130,
      ShiftUp                    = 131,
      ShiftLeft                  = 132,
      ShiftDown                  = 133,
      ShiftRight                 = 134,
      CellForward                = 135,
      ShiftForward               = 136,
      CallState                  = 137,
      ReturnState                = 138,
      YesNoGoto                  = 139,
      NoYesGoto                  = 140,
      STDDig                     = 141,
      STDBlock                   = 142,
      STDHeal                    = 143,
      Flip                       = 144,
      STDTunnel                  = 145,
      IsInsideGun                = 146,
      ChargeGun                  = 147,
      IsHealthNotFull            = 148,
      IsHealthLessThanHalf       = 149,
      YesNoNextRow               = 150,
      NoYesNextRow               = 151,
      YesNoGotoStart             = 152,
      NoYesGotoStart             = 153,
      YesNoTerminate             = 154,
      NoYesTerminate             = 155,
      CellLefthand               = 156,
      CellRighthand              = 157,
      EnableAutoDig              = 158,
      DisableAutoDig             = 159,
      EnableAggression           = 160,
      DisableAggression          = 161,
      UseBoom                    = 162,
      UseRaz                     = 163,
      UseProt                    = 164,
      BuildWar                   = 165,
      CallWhenDied               = 166,
      UseGeopack                 = 167,
      UseZZ                      = 168,
      UseC190                    = 169,
      UsePoly                    = 170,
      Upgrade                    = 171,
      RefillCraft                = 172,
      UseNano                    = 173,
      UseRem                     = 174,
      InventoryUp                = 175,
      InventoryLeft              = 176,
      InventoryDown              = 177,
      InventoryRight             = 178,
      EnableHand                 = 179,
      DisableHand                = 180,
      DebugPause                 = 181,
      DebugShow                  = 182,
      UNUSED_183                 = 183,
      UNUSED_184                 = 184,
      UNUSED_185                 = 185,
      UNUSED_186                 = 186,
      UNUSED_187                 = 187,
      UNUSED_188                 = 188,
      UNUSED_189                 = 189,
      UNUSED_190                 = 190,
      UNUSED_191                 = 191,
      UNUSED_192                 = 192,
      UNUSED_193                 = 193,
      UNUSED_194                 = 194,
      UNUSED_195                 = 195,
      UNUSED_196                 = 196,
      UNUSED_197                 = 197,
      UNUSED_198                 = 198,
      UNUSED_199                 = 199,
      UNUSED_200                 = 200,
      UNUSED_201                 = 201,
      UNUSED_202                 = 202,
      UNUSED_203                 = 203,
      UNUSED_204                 = 204,
      UNUSED_205                 = 205,
      UNUSED_206                 = 206,
      UNUSED_207                 = 207,
      UNUSED_208                 = 208,
      UNUSED_209                 = 209,
      UNUSED_210                 = 210,
      UNUSED_211                 = 211,
      UNUSED_212                 = 212,
      UNUSED_213                 = 213,
      UNUSED_214                 = 214,
      UNUSED_215                 = 215,
      UNUSED_216                 = 216,
      UNUSED_217                 = 217,
      UNUSED_218                 = 218,
      UNUSED_219                 = 219,
      UNUSED_220                 = 220,
      UNUSED_221                 = 221,
      UNUSED_222                 = 222,
      UNUSED_223                 = 223,
      UNUSED_224                 = 224,
      UNUSED_225                 = 225,
      UNUSED_226                 = 226,
      UNUSED_227                 = 227,
      UNUSED_228                 = 228,
      UNUSED_229                 = 229,
      UNUSED_230                 = 230,
      UNUSED_231                 = 231,
      UNUSED_232                 = 232,
      UNUSED_233                 = 233,
      UNUSED_234                 = 234,
      UNUSED_235                 = 235,
      UNUSED_236                 = 236,
      UNUSED_237                 = 237,
      UNUSED_238                 = 238,
      UNUSED_239                 = 239,
      UNUSED_240                 = 240,
      UNUSED_241                 = 241,
      UNUSED_242                 = 242,
      UNUSED_243                 = 243,
      UNUSED_244                 = 244,
      UNUSED_245                 = 245,
      UNUSED_246                 = 246,
      UNUSED_247                 = 247,
      UNUSED_248                 = 248,
      UNUSED_249                 = 249,
      UNUSED_250                 = 250,
      UNUSED_251                 = 251,
      UNUSED_252                 = 252,
      UNUSED_253                 = 253,
      UNUSED_254                 = 254,
      UNUSED_255                 = 255,
    }                            

    private int[] k_button = new int[] { // there would seat unknown actions
      //(int)CustomProgAction.Return                     ,
      //(int)CustomProgAction.UNKNOWN_73,
      //(int)CustomProgAction.UNKNOWN_75,
      //(int)CustomProgAction.UNKNOWN_80,
      //(int)CustomProgAction.UNKNOWN_84,
      //(int)CustomProgAction.UNKNOWN_85,
      (int)CustomProgAction.UNKNOWN_CONDITION,
      //(int)CustomProgAction.UNKNOWN_118,

    };
    private int[] c_button = new int[]
    {
      (int)CustomProgAction.IsNotEmpty                 ,
      (int)CustomProgAction.IsEmpty                    ,
      (int)CustomProgAction.IsFalling                  ,
      (int)CustomProgAction.IsCrystal                  ,
      (int)CustomProgAction.IsAliveCrystal             ,
      (int)CustomProgAction.IsFallingLikeBoulder       ,
      (int)CustomProgAction.IsFallingLikeLiquid        ,
      (int)CustomProgAction.IsBreakable                ,
      (int)CustomProgAction.IsUnbreakable              ,
      (int)CustomProgAction.IsAcid                     
    };

    private int[] shc_button = new int[]
    {
      (int)CustomProgAction.IsBlackRock                ,
      (int)CustomProgAction.IsRedRock                  ,
      (int)CustomProgAction.IsGreenBlock               ,
      (int)CustomProgAction.IsYellowBlock              ,
      (int)CustomProgAction.IsRedBlock                 ,
      (int)CustomProgAction.IsStructure                ,
      (int)CustomProgAction.IsQuadro                   ,
      (int)CustomProgAction.IsRoad                     ,
      (int)CustomProgAction.IsBox                      ,
      (int)CustomProgAction.IsCyanAlive,
      (int)CustomProgAction.IsWhiteAlive,
      (int)CustomProgAction.IsRedAlive,
      (int)CustomProgAction.IsVioletAlive,
      (int)CustomProgAction.IsBlackAlive,
      (int)CustomProgAction.IsBlueAlive,
      (int)CustomProgAction.IsRainbowAlive,
      (int)CustomProgAction.IsBasketFull,
      (int)CustomProgAction.IsGeoFull,
      (int)CustomProgAction.IsSand                     ,
      (int)CustomProgAction.IsAcidRock                 ,
      (int)CustomProgAction.IsBoulder                  ,
      (int)CustomProgAction.IsLava,
    };

    private int[] w_button = new int[]
    {
      (int)CustomProgAction.MoveUp                     ,
      (int)CustomProgAction.RotateUp                   ,
      (int)CustomProgAction.InventoryUp                
    };

    private int[] a_button = new int[]
    {
      (int)CustomProgAction.MoveLeft                   ,
      (int)CustomProgAction.RotateLeft                 ,
      (int)CustomProgAction.InventoryLeft              
    };

    private int[] s_button = new int[]
    {
      (int)CustomProgAction.MoveDown                   ,
      (int)CustomProgAction.RotateDown                 ,
      (int)CustomProgAction.InventoryDown              
    };

    private int[] d_button = new int[]
    {
      (int)CustomProgAction.MoveRight                  ,
      (int)CustomProgAction.RotateRight                ,
      (int)CustomProgAction.InventoryRight             
    };


    private int[] m_button = new int[] {
      (int)CustomProgAction.EnableAutoDig              ,
      (int)CustomProgAction.DisableAutoDig             ,
      (int)CustomProgAction.EnableAggression           ,
      (int)CustomProgAction.DisableAggression          
    };
    private int[] shm_button = new int[] {
      (int)CustomProgAction.DebugPause                 ,
      (int)CustomProgAction.DebugShow                  ,
      (int)CustomProgAction.EnableHand                 ,
      (int)CustomProgAction.DisableHand                ,
      (int)CustomProgAction.PlaySound                  ,
    };


    private int[] shw_button = new int[]
    {
      (int)CustomProgAction.CellUp                     ,
      (int)CustomProgAction.CellUpRight                ,
      (int)CustomProgAction.CellUpLeft                 ,
      (int)CustomProgAction.CellUp                     ,
      (int)CustomProgAction.CellRight                  ,
      (int)CustomProgAction.CellUpRight                ,
      (int)CustomProgAction.CellUp                     ,
      (int)CustomProgAction.ShiftUp                    ,
    };

    private int[] sha_button = new int[]
    {
      (int)CustomProgAction.CellLeft                   ,
      (int)CustomProgAction.CellUp                     ,
      (int)CustomProgAction.CellUpLeft                 ,
      (int)CustomProgAction.CellLeft                   ,
      (int)CustomProgAction.CellDown                   ,
      (int)CustomProgAction.CellDownLeft               ,
      (int)CustomProgAction.CellLeft                   ,
      (int)CustomProgAction.ShiftLeft                  ,
      (int)CustomProgAction.ShiftLefthand,
    };

    private int[] shs_button = new int[]
    {
      (int)CustomProgAction.CellDown                   ,
      (int)CustomProgAction.CellLeft                   ,
      (int)CustomProgAction.CellDownLeft               ,
      (int)CustomProgAction.CellDown                   ,
      (int)CustomProgAction.CellRight                  ,
      (int)CustomProgAction.CellDownRight              ,
      (int)CustomProgAction.CellDown                   ,
      (int)CustomProgAction.ShiftDown                  
    };

    private int[] shd_button = new int[]
    {
      (int)CustomProgAction.CellRight                  ,
      (int)CustomProgAction.CellDown                   ,
      (int)CustomProgAction.CellDownRight              ,
      (int)CustomProgAction.CellRight                  ,
      (int)CustomProgAction.CellUp                     ,
      (int)CustomProgAction.CellUpRight                ,
      (int)CustomProgAction.CellRight                  ,
      (int)CustomProgAction.ShiftRight                 ,
      (int)CustomProgAction.ShiftRighthand,
    };

    private int[] z_button = new int[]
    {
      (int)CustomProgAction.Dig                        ,
      (int)CustomProgAction.BuildBlock                 ,
      (int)CustomProgAction.BuildQuadro                ,
      (int)CustomProgAction.BuildWar                   ,
      (int)CustomProgAction.BuildRoad                  ,
      (int)CustomProgAction.UseGeo                     ,
      (int)CustomProgAction.Heal                       ,
    };

    private int[] shz_button = new int[]
    {
      (int)CustomProgAction.BoxAll,
      (int)CustomProgAction.BoxHalf,
      (int)CustomProgAction.BoxWhite,
      (int)CustomProgAction.BoxGreen,
      (int)CustomProgAction.BoxRed,
      (int)CustomProgAction.BoxBlue,
      (int)CustomProgAction.BoxCyan,
      (int)CustomProgAction.BoxViolet
    };

    private int[] x_button = new int[]
    {
      (int)CustomProgAction.UseGeopack                 ,
      (int)CustomProgAction.UseZZ                      ,
      (int)CustomProgAction.UsePoly                    ,
      (int)CustomProgAction.UseC190                    ,
      (int)CustomProgAction.UseBoom                    ,
      (int)CustomProgAction.UseProt                    ,
      (int)CustomProgAction.UseRaz                     ,
    };

    private int[] shx_button = new int[]
    {
      (int)CustomProgAction.RefillCraft                ,
      (int)CustomProgAction.Upgrade                    ,
      (int)CustomProgAction.UseNano                    ,
      (int)CustomProgAction.UseRem                     
    };

    private int[] f_button = new int[]
    {
      (int)CustomProgAction.MoveForward                
    };

    private int[] shf_button = new int[]
    {
      (int)CustomProgAction.CellForward                ,
      (int)CustomProgAction.ShiftForward               ,
      (int)CustomProgAction.CellLefthand               ,
      (int)CustomProgAction.CellRighthand              ,
      (int)CustomProgAction.ShiftForward              ,
      (int)CustomProgAction.ShiftBackwards,
    };

    private int[] e_button = new int[]
    {
      (int)CustomProgAction.SetStart                   ,
      (int)CustomProgAction.Terminate                  ,
      (int)CustomProgAction.CallWhenDied               ,
      (int)CustomProgAction.SetStartWhenDied,
      (int)CustomProgAction.SetStartWhenHurt,
      (int)CustomProgAction.SetStartWhenBotNearby,

    };

    private int[] r_button = new int[]
    {
      (int)CustomProgAction.RotateLefthand             ,
      (int)CustomProgAction.RotateRighthand            ,
      (int)CustomProgAction.RotateRandom               ,
      (int)CustomProgAction.RepeatLastAction,
    };

    private int[] l_button = new int[]
    {
      (int)CustomProgAction.Label                      
    };

    private int[] y_button = new int[]
    {
      (int)CustomProgAction.Flip                       
    };

    private int[] o_button = new int[]
    {
      (int)CustomProgAction.BooleanOR                  ,
      (int)CustomProgAction.BooleanAND                 
    };

    private int[] i_button = new int[]
    {
      (int)CustomProgAction.YesNoGoto                  ,
      (int)CustomProgAction.NoYesGoto                  ,
      (int)CustomProgAction.YesNoReturn                ,
      (int)CustomProgAction.NoYesReturn                ,
    };
    private int[] shi_button = new int[] {
      (int)CustomProgAction.YesNoNextRow,
      (int)CustomProgAction.NoYesNextRow,

      (int)CustomProgAction.YesNoGotoStart,
      (int)CustomProgAction.NoYesGotoStart,
      (int)CustomProgAction.YesNoTerminate,
      (int)CustomProgAction.NoYesTerminate,
    };

    private int[] del_button = new int[1];

    private int[] back_button;

    private int[] t_button = new int[]
    {
      (int)CustomProgAction.STDDig                     ,
      (int)CustomProgAction.STDBlock                   ,
      (int)CustomProgAction.STDHeal                    ,
      (int)CustomProgAction.STDTunnel                  
    };

    private int[] v_button = new int[]{// last version
      (int)CustomProgAction.VarEqualsNumber            ,
      (int)CustomProgAction.VarNotEqualsNumber         ,
      (int)CustomProgAction.VarGreaterThanNumber       ,
      (int)CustomProgAction.VarGreaterThanOrEqualNumber,
      (int)CustomProgAction.VarLessThanNumber          ,
      (int)CustomProgAction.VarLessThanOrEqualNumber   ,
    };
    private int[] shv_button = new int[] {//last version
      (int)CustomProgAction.VarEqualsState,
      (int)CustomProgAction.VarNotEqualsState,
      (int)CustomProgAction.VarGreaterThanState,
      (int)CustomProgAction.VarGreaterThanOrEqualsState,
      (int)CustomProgAction.VarLessThanState,
      (int)CustomProgAction.VarLessThanOrEqualState,
    };
    private int[] b_button = new int[]{//last version
      (int)CustomProgAction.SetNumberToVar,
      (int)CustomProgAction.AddNumberToVar,
      (int)CustomProgAction.SubNumberToVar,
      (int)CustomProgAction.MultNumberToVar,
      (int)CustomProgAction.DivNumberToVar,
    };
    private int[] shb_button = new int[] {//last version
      (int)CustomProgAction.AddVarToVar,
      (int)CustomProgAction.SubVarToVar,
      (int)CustomProgAction.MultVarToVar,
      (int)CustomProgAction.DivVarToVar,
    };
    private int[] n_button = new int[] {//last version
      (int)CustomProgAction.WriteStateToVar, 
      (int)CustomProgAction.ReadVarToState,
      (int)CustomProgAction.AddStateToVar,
      (int)CustomProgAction.SubStateToVar,
      (int)CustomProgAction.MultStateToVar,
      (int)CustomProgAction.DivStateToVar,
    };

    private int[] shn_button = new int[] {//last version
      (int)CustomProgAction.VarRound,
      (int)CustomProgAction.VarCeil,
      (int)CustomProgAction.VarFloor,
      (int)CustomProgAction.Var_UNK_128,
      (int)CustomProgAction.Var_UNK_129,
      (int)CustomProgAction.Var_UNK_130,
    };

    private int[] g_button = new int[]
    {
      (int)CustomProgAction.Goto                       ,
      (int)CustomProgAction.Call                       ,
      (int)CustomProgAction.CallArg                    ,
      (int)CustomProgAction.CallState                  ,
    };
    private int[] h_button = new int[]
    {
      (int)CustomProgAction.IsHealthNotFull            ,
      (int)CustomProgAction.IsHealthLessThanHalf       ,
      (int)CustomProgAction.UNKNOWN_MINUS_HEALTH,
      (int)CustomProgAction.UNKNOWN_LESS_HEALTH,
    };
    private int[] j_button = new int[]
    {
      (int)CustomProgAction.IsInsideGun                ,
      (int)CustomProgAction.ChargeGun                  
    };
    private int[] q_button = new int[]
    {
      (int)CustomProgAction.Return                     ,
      (int)CustomProgAction.ReturnArg                  ,
      (int)CustomProgAction.ReturnState                
    };

    public static ProgrammatorView THIS;

    private static int ROWS = 12;

    private static int COLS = 16;

    private static int PAGES = 16;

    public static int[] codes = new int[ProgrammatorView.PAGES * ProgrammatorView.ROWS * ProgrammatorView.COLS];

    public static string[] nums = new string[ProgrammatorView.PAGES * ProgrammatorView.ROWS * ProgrammatorView.COLS];

    public static string[] code_labels = new string[ProgrammatorView.PAGES * ProgrammatorView.ROWS * ProgrammatorView.COLS];

    public static ProgAction[] actions = new ProgAction[ProgrammatorView.ROWS * ProgrammatorView.COLS];

    public static bool active = false;

    public static int programId = 0;

    public static string title = "";

    private int prevPage;

    private int position;

    private Dictionary<string, int> labels;

    private string preprocessedProgramText;

    private int currentCharIndex;

    private int currentCellIndex;

    private int remainingColsInRow;

    private int remainingCellsOnPage;

    private string[] strmas1 = new string[]
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


    private string[] strmas2 = new string[]
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

    private string sampleProgram1;

    private string sampleProgram2;

    private string sampleProgram3;
}

