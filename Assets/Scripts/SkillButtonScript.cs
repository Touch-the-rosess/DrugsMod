using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillButtonScript : MonoBehaviour
{
    public static void InitColors()
    {
        if (!SkillButtonScript.inited)
        {
            SkillButtonScript.sprites = Resources.LoadAll<Sprite>("skills");
            SkillButtonScript.inited = true;
        }
        SkillButtonScript.colors[0] = SkillButtonScript.SKILL_COLOR_PINK;
        SkillButtonScript.colors[10] = SkillButtonScript.SKILL_COLOR_PINK;
        SkillButtonScript.colors[13] = SkillButtonScript.SKILL_COLOR_PINK;
        SkillButtonScript.colors[55] = SkillButtonScript.SKILL_COLOR_CYAN;
        SkillButtonScript.colors[16] = SkillButtonScript.SKILL_COLOR_GREY;
        SkillButtonScript.colors[11] = SkillButtonScript.SKILL_COLOR_GREY;
        SkillButtonScript.colors[20] = SkillButtonScript.SKILL_COLOR_GREY;
        SkillButtonScript.colors[17] = SkillButtonScript.SKILL_COLOR_GREY;
        SkillButtonScript.colors[47] = SkillButtonScript.SKILL_COLOR_GREY;
        SkillButtonScript.colors[48] = SkillButtonScript.SKILL_COLOR_GREY;
        SkillButtonScript.colors[50] = SkillButtonScript.SKILL_COLOR_GREY;
        SkillButtonScript.colors[49] = SkillButtonScript.SKILL_COLOR_GREY;
        SkillButtonScript.colors[51] = SkillButtonScript.SKILL_COLOR_GREY;
        SkillButtonScript.colors[3] = SkillButtonScript.SKILL_COLOR_WHITE;
        SkillButtonScript.colors[33] = SkillButtonScript.SKILL_COLOR_WHITE;
        SkillButtonScript.colors[42] = SkillButtonScript.SKILL_COLOR_RED;
        SkillButtonScript.colors[19] = SkillButtonScript.SKILL_COLOR_CYAN;
        SkillButtonScript.colors[24] = SkillButtonScript.SKILL_COLOR_CYAN;
        SkillButtonScript.colors[25] = SkillButtonScript.SKILL_COLOR_WHITE;
        SkillButtonScript.colors[29] = SkillButtonScript.SKILL_COLOR_GREEN;
        SkillButtonScript.colors[40] = SkillButtonScript.SKILL_COLOR_GREEN;
        SkillButtonScript.colors[38] = SkillButtonScript.SKILL_COLOR_GREEN;
        SkillButtonScript.colors[30] = SkillButtonScript.SKILL_COLOR_GREEN;
        SkillButtonScript.colors[32] = SkillButtonScript.SKILL_COLOR_GREEN;
        SkillButtonScript.colors[39] = SkillButtonScript.SKILL_COLOR_GREEN;
        SkillButtonScript.colors[31] = SkillButtonScript.SKILL_COLOR_GREEN;
        SkillButtonScript.colors[21] = SkillButtonScript.SKILL_COLOR_GREEN;
        SkillButtonScript.colors[27] = SkillButtonScript.SKILL_COLOR_GREEN;
        SkillButtonScript.colors[46] = SkillButtonScript.SKILL_COLOR_GREEN;
        SkillButtonScript.colors[58] = SkillButtonScript.SKILL_COLOR_GREEN;
        SkillButtonScript.colors[22] = SkillButtonScript.SKILL_COLOR_BLUE;
        SkillButtonScript.colors[35] = SkillButtonScript.SKILL_COLOR_BLUE;
        SkillButtonScript.colors[14] = SkillButtonScript.SKILL_COLOR_ORANGE;
        SkillButtonScript.colors[15] = SkillButtonScript.SKILL_COLOR_ORANGE;
        SkillButtonScript.colors[4] = SkillButtonScript.SKILL_COLOR_ORANGE;
        SkillButtonScript.colors[28] = SkillButtonScript.SKILL_COLOR_ORANGE;
        SkillButtonScript.colors[23] = SkillButtonScript.SKILL_COLOR_ORANGE;
        SkillButtonScript.colors[5] = SkillButtonScript.SKILL_COLOR_ORANGE;
        SkillButtonScript.colors[37] = SkillButtonScript.SKILL_COLOR_ORANGE;
        SkillButtonScript.colors[2] = SkillButtonScript.SKILL_COLOR_ORANGE;
        SkillButtonScript.colors[34] = SkillButtonScript.SKILL_COLOR_ORANGE;
        SkillButtonScript.colors[44] = SkillButtonScript.SKILL_COLOR_ORANGE;
        SkillButtonScript.colors[41] = SkillButtonScript.SKILL_COLOR_ORANGE;
        SkillButtonScript.colors[18] = SkillButtonScript.SKILL_COLOR_ORANGE;
        SkillButtonScript.colors[56] = SkillButtonScript.SKILL_COLOR_WHITE;
        SkillButtonScript.colors[57] = SkillButtonScript.SKILL_COLOR_WHITE;
        SkillButtonScript.colors[43] = SkillButtonScript.SKILL_COLOR_WHITE;
        SkillButtonScript.colors[54] = SkillButtonScript.SKILL_COLOR_WHITE;
        SkillButtonScript.colors[53] = SkillButtonScript.SKILL_COLOR_WHITE;
        SkillButtonScript.colors[12] = SkillButtonScript.SKILL_COLOR_YELLOW;
        SkillButtonScript.colors[26] = SkillButtonScript.SKILL_COLOR_YELLOW;
        SkillButtonScript.colors[9] = SkillButtonScript.SKILL_COLOR_YELLOW;
        SkillButtonScript.colors[8] = SkillButtonScript.SKILL_COLOR_YELLOW;
        SkillButtonScript.colors[36] = SkillButtonScript.SKILL_COLOR_YELLOW;
        SkillButtonScript.colors[1] = SkillButtonScript.SKILL_COLOR_YELLOW;
        SkillButtonScript.colors[7] = SkillButtonScript.SKILL_COLOR_YELLOW;
        SkillButtonScript.colors[6] = SkillButtonScript.SKILL_COLOR_YELLOW;
        SkillButtonScript.colors[45] = SkillButtonScript.SKILL_COLOR_YELLOW;
        SkillButtonScript.colors[52] = SkillButtonScript.SKILL_COLOR_YELLOW;
        SkillButtonScript.skillShorts["d"] = 12;
        SkillButtonScript.skillShorts["L"] = 16;
        SkillButtonScript.skillShorts["F"] = 22;
        SkillButtonScript.skillShorts["M"] = 19;
        SkillButtonScript.skillShorts["t"] = 24;
        SkillButtonScript.skillShorts["p"] = 29;
        SkillButtonScript.skillShorts["l"] = 13;
        SkillButtonScript.skillShorts["b"] = 30;
        SkillButtonScript.skillShorts["c"] = 31;
        SkillButtonScript.skillShorts["g"] = 40;
        SkillButtonScript.skillShorts["r"] = 38;
        SkillButtonScript.skillShorts["v"] = 32;
        SkillButtonScript.skillShorts["w"] = 39;
        SkillButtonScript.skillShorts["m"] = 14;
        SkillButtonScript.skillShorts["G"] = 5;
        SkillButtonScript.skillShorts["B"] = 4;
        SkillButtonScript.skillShorts["R"] = 15;
        SkillButtonScript.skillShorts["W"] = 37;
        SkillButtonScript.skillShorts["V"] = 28;
        SkillButtonScript.skillShorts["C"] = 23;
        SkillButtonScript.skillShorts["Y"] = 20;
        SkillButtonScript.skillShorts["E"] = 11;
        SkillButtonScript.skillShorts["O"] = 47;
        SkillButtonScript.skillShorts["Q"] = 17;
        SkillButtonScript.skillShorts["A"] = 48;
        SkillButtonScript.skillShorts["a"] = 0;
        SkillButtonScript.skillShorts["k"] = 1;
        SkillButtonScript.skillShorts["z"] = 9;
        SkillButtonScript.skillShorts["o"] = 41;
        SkillButtonScript.skillShorts["q"] = 18;
        SkillButtonScript.skillShorts["j"] = 2;
        SkillButtonScript.skillShorts["u"] = 10;
        SkillButtonScript.skillShorts["x"] = 7;
        SkillButtonScript.skillShorts["P"] = 21;
        SkillButtonScript.skillShorts["h"] = 27;
        SkillButtonScript.skillShorts["H"] = 46;
        SkillButtonScript.skillShorts["y"] = 8;
        SkillButtonScript.skillShorts["Z"] = 26;
        SkillButtonScript.skillShorts["D"] = 6;
        SkillButtonScript.skillShorts["f"] = 45;
        SkillButtonScript.skillShorts["U"] = 3;
        SkillButtonScript.skillShorts["S"] = 35;
        SkillButtonScript.skillShorts["X"] = 36;
        SkillButtonScript.skillShorts["e"] = 42;
        SkillButtonScript.skillShorts["J"] = 34;
        SkillButtonScript.skillShorts["i"] = 44;
        SkillButtonScript.skillShorts["*U"] = 25;
        SkillButtonScript.skillShorts["*M"] = 33;
        SkillButtonScript.skillShorts["*L"] = 50;
        SkillButtonScript.skillShorts["*D"] = 43;
        SkillButtonScript.skillShorts["*B"] = 49;
        SkillButtonScript.skillShorts["*A"] = 51;
        SkillButtonScript.skillShorts["*T"] = 52;
        SkillButtonScript.skillShorts["*J"] = 54;
        SkillButtonScript.skillShorts["*u"] = 53;
        SkillButtonScript.skillShorts["*I"] = 55;
        SkillButtonScript.skillShorts["*a"] = 56;
        SkillButtonScript.skillShorts["*d"] = 57;
        SkillButtonScript.skillShorts["*g"] = 58;
    }

    public void SetIcon(int type, bool isUp, int level, bool lockbool)
    {
        this.viewUpdated = false;
        this.type = type;
        this.isUp = isUp;
        this.level = level;
        this.lockbool = lockbool;
    }

    private void Start()
    {
    }

    private void Update()
    {
        if (!this.viewUpdated)
        {
            if (this.type == -1)
            {
                this.icon.gameObject.SetActive(false);
                this.up.gameObject.SetActive(false);
                this.levelTF.gameObject.SetActive(false);
                this.levelImage.gameObject.SetActive(false);
            }
            else
            {
                this.icon.gameObject.SetActive(true);
                this.levelTF.gameObject.SetActive(true);
                this.levelImage.gameObject.SetActive(true);
                this.icon.sprite = SkillButtonScript.sprites[this.type];
                this.up.gameObject.SetActive(this.isUp);
                if (this.lockbool)
                {
                    this.up.sprite = this.lockedSprite;
                }
                else
                {
                    this.up.sprite = this.upSprite;
                }
                this.levelTF.text = this.level.ToString();

                this.levelImage.color = SkillButtonScript.colors[this.type];
            }
            this.viewUpdated = true;
        }
        if (this.blinking)
        {
            base.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.5f + 0.5f * Mathf.Sin(15f * Time.time));
        }
    }
        
	public static Sprite[] sprites;

	public static bool inited = false;

	private const int SKILL_AACD = 0;

	public const int SKILL_ABLK = 1;

	public const int SKILL_ADJA = 2;

	public const int SKILL_AGUN = 10;

	public const int SKILL_ANIG = 7;

	public const int SKILL_BLDG = 16;

	public const int SKILL_BLDQ = 17;

	public const int SKILL_BLDR = 11;

	public const int SKILL_BLDY = 20;

	public const int SKILL_COMP = 21;

	public const int SKILL_CRYS = 8;

	public const int SKILL_DEAC = 26;

	public const int SKILL_DECN = 9;

	public const int SKILL_DEST = 6;

	public const int SKILL_DETE = 18;

	public const int SKILL_DIGG = 12;

	public const int SKILL_FRAC = 45;

	public const int SKILL_FRIG = 22;

	public const int SKILL_GEOL = 3;

	public const int SKILL_HCMP = 27;

	public const int SKILL_LIVE = 13;

	public const int SKILL_MAGN = 36;

	public const int SKILL_MINB = 4;

	public const int SKILL_MINC = 23;

	public const int SKILL_MINE = 14;

	public const int SKILL_MING = 5;

	public const int SKILL_MINR = 15;

	public const int SKILL_MINV = 28;

	public const int SKILL_MINW = 37;

	public const int SKILL_MORO = 24;

	public const int SKILL_X_UPGR = 25;

	public const int SKILL_MOTO = 19;

	public const int SKILL_NANO = 46;

	public const int SKILL_OPOR = 47;

	public const int SKILL_PACK = 29;

	public const int SKILL_PAKB = 30;

	public const int SKILL_PAKC = 31;

	public const int SKILL_PAKG = 40;

	public const int SKILL_PAKR = 38;

	public const int SKILL_PAKV = 32;

	public const int SKILL_PAKW = 39;

	public const int SKILL_RECO = 41;

	public const int SKILL_X_MONY = 33;

	public const int SKILL_REPA = 42;

	public const int SKILL_ROAD = 48;

	public const int SKILL_X_BLDU = 49;

	public const int SKILL_X_MINE = 43;

	public const int SKILL_SORT = 34;

	public const int SKILL_SUBL = 35;

	public const int SKILL_WASH = 44;

	public const int SKILL_X_WARB = 50;

	public const int SKILL_X_ARCH = 51;

	public const int SKILL_X_TODS = 52;

	public const int SKILL_X_ULTR = 53;

	public const int SKILL_X_JEWL = 54;

	public const int SKILL_X_INDU = 55;

	public const int SKILL_X_ACID = 56;

	public const int SKILL_X_DEEP = 57;

	public const int SKILL_X_GLUO = 58;

	public static Color SKILL_COLOR_ORANGE = new Color(1f, 0.398f, 0f);

	public static Color SKILL_COLOR_YELLOW = new Color(1f, 1f, 0f);

	public static Color SKILL_COLOR_RED = new Color(1f, 0.33f, 0.33f);

	public static Color SKILL_COLOR_WHITE = new Color(0.93f, 0.93f, 0.93f);

	public static Color SKILL_COLOR_PINK = new Color(1f, 0f, 1f);

	public static Color SKILL_COLOR_GREEN = new Color(0f, 1f, 0f);

	public static Color SKILL_COLOR_BLUE = new Color(0.16f, 0.5f, 1f);

	public static Color SKILL_COLOR_CYAN = new Color(0f, 1f, 0.9f);

	public static Color SKILL_COLOR_GREY = new Color(0.57421875f, 0.671875f, 0.65234375f);

	public Image icon;

	public Image up;

	public Sprite lockedSprite;

	public Sprite upSprite;

	public Image levelImage;

	public Text levelTF;

	private bool viewUpdated = true;

	private int type;

	private bool isUp;

	private bool lockbool;

	private int level;

	public static Dictionary<string, int> skillShorts = new Dictionary<string, int>();

	public static Dictionary<int, Color> colors = new Dictionary<int, Color>();

	public bool blinking;
}
