using System;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.GameClasses;

public class ProgAction : MonoBehaviour
{
    public static void InitSprites()
    {
        if (!ProgAction.inited)
        {
            ProgAction.sprites = Resources.LoadAll<Sprite>("programmator");
            ProgAction.inited = true;
        }
    }

    private void Start()
	{
		ProgAction.InitSprites();
	}

    public void ChangeTo(int _id)
	{
		ProgAction.InitSprites();
		this.id = _id;
		base.GetComponent<Image>().sprite = ProgAction.sprites[this.id];
		base.GetComponent<Image>().SetNativeSize();
		if (this.input != null)
		{
			this.updateInput();
			return;
		}
		this.inputInited = false;
	}

    private void updateInput()
    {
      input.gameObject.SetActive(false);
      inputVarLower.gameObject.SetActive(false);
      numInput.gameObject.SetActive(false);

      // Label / Goto / Call targets (text only)
      if (id == (int)CustomProgAction.Label ||                    // 40
          id == (int)CustomProgAction.Goto ||                     // 24
          id == (int)CustomProgAction.Call ||                     // 25
          id == (int)CustomProgAction.CallArg ||                  // 26
          id == (int)CustomProgAction.CallState ||                // 137
          id == (int)CustomProgAction.CallWhenDied ||             // 166
          id == (int)CustomProgAction.YesNoGoto ||                // 139
          id == (int)CustomProgAction.NoYesGoto ||// 140
          id == (int)CustomProgAction.DebugPause||
          id == (int)CustomProgAction.DebugShow )                  
      {
        input.gameObject.SetActive(true);

        // Slight position variations depending on the command
        if (id == (int)CustomProgAction.Label)
          input.gameObject.transform.localPosition = new Vector3(-5f, 0f);
        else if (id == (int)CustomProgAction.Goto)
          input.gameObject.transform.localPosition = new Vector3(6f, 0f);
        else if (id == (int)CustomProgAction.CallWhenDied ||
            id == (int)CustomProgAction.YesNoGoto ||
            id == (int)CustomProgAction.NoYesGoto)
          input.gameObject.transform.localPosition = new Vector3(-1f, -9f);
        else
          input.gameObject.transform.localPosition = new Vector3(1f, 0f);
      }

      // Variable comparison commands that need both text (variable name) + number
      else if (id == (int)CustomProgAction.VarGreaterThanNumber ||     // 119
          id == (int)CustomProgAction.VarLessThanNumber ||        // 120
          id == (int)CustomProgAction.VarGreaterThanOrEqualNumber || // 121
          id == (int)CustomProgAction.VarLessThanOrEqualNumber || // 122
          id == (int)CustomProgAction.VarEqualsNumber ||          // 123
          id == (int)CustomProgAction.VarNotEqualsNumber ||       // 124
          id == (int)CustomProgAction.Var_UNK_128)                // 128 ?
      {
        input.gameObject.SetActive(true);
        numInput.gameObject.SetActive(true);

        input.gameObject.transform.localPosition    = new Vector3(-4f, 4f);
        numInput.gameObject.transform.localPosition = new Vector3(-1f, -10f);
      }
      else if (id == (int)CustomProgAction.AddVarToVar  ||
               id == (int)CustomProgAction.SubVarToVar  ||
               id == (int)CustomProgAction.MultVarToVar ||
               id == (int)CustomProgAction.DivVarToVar  ||
               id == (int)CustomProgAction.Var_UNK_128  ||
               id == (int)CustomProgAction.Var_UNK_129  ||
               id == (int)CustomProgAction.Var_UNK_130
          )
      {
        input.gameObject.SetActive(true);
        inputVarLower.gameObject.SetActive(true);

        input.gameObject.transform.localPosition    = new Vector3(-2.64f,8.03f);
        inputVarLower.gameObject.transform.localPosition = new Vector3(-1f, -10f);
      }

      // Math / rounding operations on variables (text input only)
      else if (id == (int)CustomProgAction.VarRound ||    // 125
          id == (int)CustomProgAction.VarCeil ||     // 126
          id == (int)CustomProgAction.VarFloor)      // 127
      {
        input.gameObject.SetActive(true);
        input.gameObject.transform.localPosition = new Vector3(0f, 0f);
      }

      // Variable ← state / state ← variable operations
      else if (id == (int)CustomProgAction.WriteStateToVar ||     // 97
          id == (int)CustomProgAction.ReadVarToState)        // 98
      {
        input.gameObject.SetActive(true);
        input.gameObject.transform.localPosition = new Vector3(1f, -5f);
      }

      // Variable comparisons with state (text only)
      else if (id == (int)CustomProgAction.VarLessThanState ||            // 112
          id == (int)CustomProgAction.VarGreaterThanState ||         // 113
          id == (int)CustomProgAction.VarGreaterThanOrEqualsState || // 114
          id == (int)CustomProgAction.VarLessThanOrEqualState ||     // 115
          id == (int)CustomProgAction.VarEqualsState ||              // 116
          id == (int)CustomProgAction.VarNotEqualsState)             // 117
      {
        input.gameObject.SetActive(true);
        input.gameObject.transform.localPosition = new Vector3(2f, -4f);
      }

      // Arithmetic: var ← var +/-/×/÷ state/var
      else if (id == (int)CustomProgAction.AddStateToVar ||     // 104
          id == (int)CustomProgAction.MultStateToVar ||    // 105
          id == (int)CustomProgAction.DivStateToVar ||     // 106
          id == (int)CustomProgAction.SubStateToVar)       // 107
      {
        input.gameObject.SetActive(true);
        input.gameObject.transform.localPosition = new Vector3(2f, -8f);
      }

      // Arithmetic: var ← var +/-/×/÷ number
      else if (id == (int)CustomProgAction.SetNumberToVar ||    // 99
          id == (int)CustomProgAction.AddNumberToVar ||    // 100
          id == (int)CustomProgAction.MultNumberToVar ||   // 101
          id == (int)CustomProgAction.DivNumberToVar ||    // 102
          id == (int)CustomProgAction.SubNumberToVar)      // 103
      {
        input.gameObject.SetActive(true);
        numInput.gameObject.SetActive(true);

        input.gameObject.transform.localPosition    = new Vector3(0f, -9f);
        numInput.gameObject.transform.localPosition = new Vector3(0f, 7f);
      }

      // Arithmetic: var ← var +/-/×/÷ var
      else if (id == (int)CustomProgAction.AddVarToVar ||     // 108
          id == (int)CustomProgAction.MultVarToVar ||    // 109
          id == (int)CustomProgAction.DivVarToVar ||     // 110
          id == (int)CustomProgAction.SubVarToVar)       // 111
      {
        input.gameObject.SetActive(true);
        input.gameObject.transform.localPosition = new Vector3(-1f, 8f);
      }

      // Unknown / debug variable ops
      else if (id == (int)CustomProgAction.Var_UNK_129 ||     // 129
          id == (int)CustomProgAction.Var_UNK_130)       // 130
      {
        input.gameObject.SetActive(true);
        input.gameObject.transform.localPosition = new Vector3(-4f, 4f);
      }

      inputInited = true;
    }

    private void Update()
    {
        if (!this.inputInited)
        {
            this.updateInput();
        }
    }

    public string getString(bool who)
    {
      switch (who){
        case false: return this.input.text;
        case true:  return this.inputVarLower.text;
      }
      return "";
    }

    public void setString(string label,bool who)// 0 - upper one, 1 - lower one
    {
      switch (who){
        case false: this.input.text         = label; break;
        case true:  this.inputVarLower.text = label; break;
      }
    }

    public int getNum()
    {
        int num;
        if (!int.TryParse(this.numInput.text, out num))
        {
            return 0;
        }
        return int.Parse(this.numInput.text);
    }

    public void setNum(int label)
    {
        this.numInput.text = label.ToString();
    }
    
	public static Sprite[] sprites;

	public static bool inited;

	public InputField numInput;

	public InputField input;
	public InputField inputVarLower;

	public int id;

	public bool inputInited;
}
