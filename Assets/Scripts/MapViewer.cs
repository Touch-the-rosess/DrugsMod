using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MapViewer : MonoBehaviour
{
    public static void ResetCustomTable()
    {
        for (int i = 0; i < 255; i++)
        {
            int num = i + 50;
            MapViewer.customTable[i] = new Color((float)num / 312f, (float)num / 312f, (float)num / 312f);
            MapViewer.customBlinkRateTable[i] = 0f;
            if (i <= 39)
            {
                MapViewer.customTable[i] = new Color(0.04f, 0.045f, 0.045f, 1f);
            }
        }
    }

    public static void InitColorTable()
    {
        MapViewer.ResetCustomTable();
        for (int i = 0; i < 255; i++)
        {
            MapViewer.colorTable[i] = new Color((float)i / 512f, (float)i / 256f, 0.01f);
            MapViewer.aliveColorTable[i] = new Color((float)i / 512f, (float)i / 256f, 0.01f);
            MapViewer.transparentTable[i] = new Color(0f, 0f, 0f, 1f);
            if (i > 39)
            {
                MapViewer.transparentTable[i] = new Color(0.4f, 0.45f, 0.45f, 1f);
            }
            if (i < 120 && CellRender.isSandCache[i])
            {
                MapViewer.transparentTable[i] = new Color(0.3f, 0.35f, 0.35f, 1f);
            }
        }
        MapViewer.transparentTable[35] = new Color(0.4f, 0.01f, 0.1f, 1f);
        MapViewer.transparentTable[36] = new Color(0.4f, 0.1f, 0.01f, 1f);
        MapViewer.transparentTable[39] = new Color(0.4f, 0.01f, 0.01f, 1f);
        MapViewer.transparentTable[30] = new Color(1f, 1f, 0f, 1f);
        MapViewer.transparentTable[114] = new Color(0.6f, 0.6f, 0.6f, 1f);
        MapViewer.transparentTable[115] = new Color(0.6f, 0.6f, 0.6f, 1f);
        MapViewer.transparentTable[117] = new Color(1f, 1f, 1f, 1f);
        MapViewer.transparentTable[106] = new Color(1f, 1f, 1f, 1f);
        MapViewer.transparentTable[119] = new Color(0.8f, 1f, 1f, 1f);
        MapViewer.transparentTable[80] = new Color(0f, 1f, 1f, 1f);
        MapViewer.transparentTable[81] = new Color(0f, 0.7f, 0.7f, 1f);
        MapViewer.colorTable[0] = new Color(0f, 0f, 0f, 0.5f);
        MapViewer.colorTable[1] = new Color(0f, 0f, 0f, 0.5f);
        MapViewer.colorTable[32] = MapViewer.RGBQ(0, 0, 0);
        MapViewer.colorTable[33] = MapViewer.RGBQ(15, 11, 3);
        MapViewer.colorTable[34] = MapViewer.RGBQ(29, 25, 18);
        MapViewer.colorTable[35] = MapViewer.RGBQ(68, 68, 68);
        MapViewer.colorTable[36] = MapViewer.RGBQ(85, 68, 34);
        MapViewer.colorTable[37] = MapViewer.RGBQ(68, 0, 0);
        MapViewer.colorTable[38] = MapViewer.RGBQ(51, 68, 0);
        MapViewer.colorTable[40] = MapViewer.RGBQ(255, 97, 107);
        MapViewer.colorTable[41] = MapViewer.RGBQ(255, 107, 97);
        MapViewer.colorTable[42] = MapViewer.RGBQ(255, 107, 107);
        MapViewer.colorTable[43] = MapViewer.RGBQ(255, 187, 251);
        MapViewer.colorTable[44] = MapViewer.RGBQ(191, 241, 251);
        MapViewer.colorTable[45] = MapViewer.RGBQ(207, 203, 241);
        MapViewer.colorTable[48] = MapViewer.RGBQ(255, 255, 255);
        MapViewer.colorTable[49] = MapViewer.RGBQ(101, 150, 126);
        MapViewer.colorTable[50] = MapViewer.RGBQ(101, 255, 255);
        MapViewer.aliveColorTable[50] = MapViewer.RGBQ(101, 255, 255);
        MapViewer.colorTable[51] = MapViewer.RGBQ(255, 51, 51);
        MapViewer.aliveColorTable[51] = MapViewer.RGBQ(255, 51, 51);
        MapViewer.colorTable[52] = MapViewer.RGBQ(255, 101, 255);
        MapViewer.aliveColorTable[52] = MapViewer.RGBQ(255, 101, 255);
        MapViewer.colorTable[53] = MapViewer.RGBQ(34, 101, 255);
        MapViewer.aliveColorTable[53] = MapViewer.RGBQ(255, 138, 255);
        MapViewer.colorTable[54] = MapViewer.RGBQ(238, 254, 255);
        MapViewer.aliveColorTable[54] = MapViewer.RGBQ(238, 254, 255);
        MapViewer.colorTable[55] = MapViewer.RGBQ(238, 254, 255);
        MapViewer.aliveColorTable[55] = MapViewer.RGBQ(238, 254, 255);
        MapViewer.colorTable[56] = MapViewer.RGBQ(225, 254, 255);
        MapViewer.colorTable[57] = MapViewer.RGBQ(226, 254, 255);
        MapViewer.colorTable[58] = MapViewer.RGBQ(227, 254, 255);
        MapViewer.colorTable[59] = MapViewer.RGBQ(228, 254, 255);
        MapViewer.colorTable[60] = MapViewer.RGBQ(204, 204, 204);
        MapViewer.colorTable[61] = MapViewer.RGBQ(221, 221, 221);
        MapViewer.colorTable[62] = MapViewer.RGBQ(255, 204, 204);
        MapViewer.colorTable[63] = MapViewer.RGBQ(255, 221, 221);
        MapViewer.colorTable[64] = MapViewer.RGBQ(170, 170, 170);
        MapViewer.colorTable[65] = MapViewer.RGBQ(187, 187, 187);
        MapViewer.colorTable[66] = MapViewer.RGBQ(184, 153, 51);
        MapViewer.colorTable[67] = MapViewer.RGBQ(184, 136, 187);
        MapViewer.colorTable[68] = MapViewer.RGBQ(119, 68, 68);
        MapViewer.colorTable[69] = MapViewer.RGBQ(34, 68, 153);
        MapViewer.colorTable[70] = MapViewer.RGBQ(243, 241, 152);
        MapViewer.colorTable[71] = MapViewer.RGBQ(71, 215, 100);
        MapViewer.colorTable[72] = MapViewer.RGBQ(101, 134, 247);
        MapViewer.colorTable[73] = MapViewer.RGBQ(247, 82, 67);
        MapViewer.colorTable[74] = MapViewer.RGBQ(132, 238, 247);
        MapViewer.colorTable[75] = MapViewer.RGBQ(255, 135, 231);
        MapViewer.colorTable[82] = MapViewer.RGBQ(17, 102, 102);
        MapViewer.colorTable[83] = MapViewer.RGBQ(50, 135, 152);
        MapViewer.colorTable[86] = MapViewer.RGBQ(184, 255, 17);
        MapViewer.colorTable[90] = MapViewer.RGBQ(238, 238, 238);
        MapViewer.colorTable[91] = MapViewer.RGBQ(255, 90, 0);
        MapViewer.colorTable[92] = MapViewer.RGBQ(193, 187, 187);
        MapViewer.colorTable[93] = MapViewer.RGBQ(187, 193, 187);
        MapViewer.colorTable[94] = MapViewer.RGBQ(187, 187, 193);
        MapViewer.colorTable[95] = MapViewer.RGBQ(184, 255, 34);
        MapViewer.colorTable[96] = MapViewer.RGBQ(184, 255, 68);
        MapViewer.colorTable[97] = MapViewer.RGBQ(112, 160, 183);
        MapViewer.colorTable[98] = MapViewer.RGBQ(112, 187, 207);
        MapViewer.colorTable[99] = MapViewer.RGBQ(219, 209, 125);
        MapViewer.colorTable[100] = MapViewer.RGBQ(181, 168, 57);
        MapViewer.colorTable[101] = MapViewer.RGBQ(76, 191, 0);
        MapViewer.colorTable[102] = MapViewer.RGBQ(208, 206, 0);
        MapViewer.colorTable[103] = MapViewer.RGBQ(133, 81, 166);
        MapViewer.colorTable[104] = MapViewer.RGBQ(153, 153, 136);
        MapViewer.colorTable[105] = MapViewer.RGBQ(198, 0, 0);
        MapViewer.colorTable[106] = MapViewer.RGBQ(136, 136, 136);
        MapViewer.colorTable[107] = MapViewer.RGBQ(8, 215, 100);
        MapViewer.colorTable[108] = MapViewer.RGBQ(255, 0, 0);
        MapViewer.colorTable[109] = MapViewer.RGBQ(0, 0, 255);
        MapViewer.colorTable[110] = MapViewer.RGBQ(255, 0, 255);
        MapViewer.colorTable[111] = MapViewer.RGBQ(238, 238, 255);
        MapViewer.colorTable[112] = MapViewer.RGBQ(0, 255, 255);
        MapViewer.colorTable[113] = MapViewer.RGBQ(211, 159, 166);
        MapViewer.colorTable[114] = MapViewer.RGBQ(119, 119, 119);
        MapViewer.colorTable[115] = MapViewer.RGBQ(56, 118, 65);
        MapViewer.colorTable[116] = MapViewer.RGBQ(17, 17, 255);
        MapViewer.aliveColorTable[116] = MapViewer.RGBQ(161, 162, 255);
        MapViewer.colorTable[117] = MapViewer.RGBQ(170, 119, 119);
        MapViewer.colorTable[118] = MapViewer.RGBQ(100, 98, 21);
        MapViewer.colorTable[119] = MapViewer.RGBQ(170, 255, 255);
        MapViewer.aliveColorTable[119] = MapViewer.RGBQ(170, 255, 255);
        MapViewer.colorTable[120] = MapViewer.RGBQ(227, 191, 120);
        MapViewer.colorTable[121] = MapViewer.RGBQ(163, 136, 72);
        MapViewer.colorTable[122] = MapViewer.RGBQ(51, 153, 120);
        MapViewer.colorsInited = true;
    }

    private void Start()
    {
        MapViewer.THIS = this;
        this.texture = new Texture2D(this.width, this.height, TextureFormat.RGBA32, false);
        this.texture.filterMode = FilterMode.Point;
        this.colors = new Color[this.width * this.height];
        this.texture_x2 = new Texture2D(2 * this.width, 2 * this.height, TextureFormat.RGBA32, false);
        this.texture_x2.filterMode = FilterMode.Point;
        this.colors_x2 = new Color[4 * this.width * this.height];
        this.mapSprite_x2 = Sprite.Create(this.texture_x2, new Rect(0f, 0f, (float)(2 * this.width), (float)(2 * this.height)), new Vector2(0.5f, 0.5f));
        this.mapSprite = Sprite.Create(this.texture, new Rect(0f, 0f, (float)this.width, (float)this.height), new Vector2(0.5f, 0.5f));
        this.mapImage.sprite = this.mapSprite;
        this.mapImage.SetNativeSize();
        this.mapImage.rectTransform.sizeDelta = new Vector2((float)(this.width * 2), (float)(this.height * 2));
        base.gameObject.SetActive(false);
        this.exitButton.onClick.AddListener(new UnityAction(this.OnExit));
        if (!MapViewer.colorsInited)
        {
            MapViewer.InitColorTable();
        }
        this.allmapButton.onClick.AddListener(new UnityAction(this.OnClick));

        for (int i = 0; i < 255; i++)
        {
            MapViewer.atable[i] = MapViewer.RGBQ(0, 0, 0);
        }
        MapViewer.atable[119] = MapViewer.RGBQ(170, 255, 255); //Ghypnoskal
        MapViewer.atable[117] = MapViewer.RGBQ(170, 119, 119); //krasnoskal
        MapViewer.atable[116] = MapViewer.RGBQ(77, 166, 255);  //Alive blue
        MapViewer.atable[114] = MapViewer.RGBQ(119, 119, 119); //chiornoskal
        //MapViewer.atable[106] = MapViewer.RGBQ(136, 136, 136); //Nevidymay nerazrushimyi block
        MapViewer.atable[90]  = MapViewer.RGBQ(255, 255, 255); //Box
        MapViewer.atable[87]  = MapViewer.RGBQ(255, 255, 0);   //Alive super rainbow
        //MapViewer.atable[81]  = MapViewer.RGBQ(0, 230, 230);   //VB
        MapViewer.atable[55]  = MapViewer.RGBQ(255, 255, 77);  //Alive rainbow
        //MapViewer.atable[54]  = MapViewer.RGBQ(68, 0, 0);      //Alive white
        //MapViewer.atable[53]  = MapViewer.RGBQ(68, 0, 0);      //Alive black
        //MapViewer.atable[52]  = MapViewer.RGBQ(221, 51, 255);  //Alive fio
        //MapViewer.atable[51]  = MapViewer.RGBQ(255, 102, 102); //Alive red
        //MapViewer.atable[50]  = MapViewer.RGBQ(25, 25, 255);   //Alive blue
        //MapViewer.atable[49]  = MapViewer.RGBQ(233, 236, 242); //Opora
        //MapViewer.atable[48]  = MapViewer.RGBQ(68, 0, 0);      //Kvadro
        //MapViewer.atable[39]  = MapViewer.RGBQ(0, 255, 0);     //Polymer doroga
        MapViewer.atable[38]  = MapViewer.RGBQ(153, 102, 0);   //Ugol ot zdanya
        MapViewer.atable[37]  = MapViewer.RGBQ(153, 102, 0);   //Dvery so zdanya
        //MapViewer.atable[36]  = MapViewer.RGBQ(85, 68, 34);    //Zolotaya doroga
        //MapViewer.atable[35]  = MapViewer.RGBQ(204, 204, 210); //Doroga
        MapViewer.atable[30]  = MapViewer.RGBQ(255, 247, 204); //Vorota

        modeDropdown.options.Add(new Dropdown.OptionData("Скалы."));
    }

    public static Color RGBQ(int r, int g, int b)
    {
        return new Color((float)r / 256f, (float)g / 256f, (float)b / 256f);
    }

    public void OnExit()
    {
        TutorialNavigation.CheckHide("_CMAP");
        ClientController.CanGoto = false;
        base.gameObject.SetActive(false);
    }

    public void OnClick()
    {
        if (this.lastClickTime + 0.5f < Time.unscaledTime)
        {
            this.lastClickTime = Time.unscaledTime;
            return;
        }
        if (this.cursDx > 0 && this.cursDy > 0 && this.lastdrag < 5f)
        {
            ClientConfig.mouseNoDig = true;
            ClientConfig.mouseR = ClientConfig.mouseMapR;
            ClientConfig.mouseMaxLen = ClientConfig.mouseMapMaxLen;
            ClientConfig.mouseMaxStack = ClientConfig.mouseMapMaxStack;
            ClientController.THIS.FromMapGoto(this.cursDx, this.cursDy);
            this.mapX = ClientController.THIS.view_x;
            this.mapY = ClientController.THIS.view_y;
            ClientConfig.mouseNoDig = false;
            ClientConfig.mouseR = ClientConfig.mouseDefR;
            ClientConfig.mouseMaxLen = ClientConfig.mouseDefMaxLen;
            ClientConfig.mouseMaxStack = ClientConfig.mouseDefMaxStack;
        }
    }

    public void Show()
	{
		if (!base.gameObject.activeSelf)
		{
			this.isDragging = false;
			this.mapX = ClientController.THIS.view_x;
			this.mapY = ClientController.THIS.view_y;
			this.lastBotX = this.mapX;
			this.lastBotY = this.mapY;
			this.UpdateMap();
			base.gameObject.SetActive(true);
			return;
		}
		this.OnExit();
	}

    public void UpdateMap()
    {
        int num = this.width;
        int num2 = this.height;
        Color[] array = this.colors;
        if (this.modeDropdown.value == 1)
        {
            num *= 2;
            num2 *= 2;
            this.mapImage.sprite = this.mapSprite_x2;
            array = this.colors_x2;
        }
        else
        {
            this.mapImage.sprite = this.mapSprite;
        }
        int num3 = this.mapX - num / 2;
        int num4 = this.mapY + num2 / 2;
        for (int i = 0; i < num; i++)
        {
            for (int j = 0; j < num2; j++)
            {
                if (num4 - j < 0)
                {
                    float num5 = 0.3f * Mathf.Max(1f - (float)(j - num4) / 100f, 0f);
                    if (this.modeDropdown.value == 1)
                    {
                        array[i + j * num] = new Color(num5 * 0.5f, num5, 0f);
                    }
                    else
                    {
                        this.colors[i + j * num] = new Color(num5 * 0.5f, num5, 0f);
                    }
                }
                else if (this.modeDropdown.value == 2)
                {
                    this.colors[i + j * num] = MapViewer.aliveColorTable[TerrainRendererScript.map.GetCell(num3 + i, num4 - j)];
                    if (((num3 + i) / 32 + (num4 - j) / 32) % 2 == 0)
                    {
                        Color[] array2 = this.colors;
                        int num6 = i + j * num;
                        array2[num6].b = array2[num6].b + 0.3f;
                    }
                }
                else if (this.modeDropdown.value == 3)
                {
                    float num7 = (float)(num3 + i - this.lastBotX);
                    float num8 = (float)(num4 - j - this.lastBotY);
                    this.colors[i + j * num] = MapViewer.transparentTable[TerrainRendererScript.map.GetCell(num3 + i, num4 - j)];
                }
                else if (this.modeDropdown.value < 2)
                {
                    array[i + j * num] = MapViewer.colorTable[TerrainRendererScript.map.GetCell(num3 + i, num4 - j)];
                }
                else if (this.modeDropdown.value == 4)
                {
                    int num9 = TerrainRendererScript.map.GetCell(num3 + i, num4 - j);
                    if (MapViewer.customBlinkRateTable[num9] == 0f)
                    {
                        array[i + j * num] = MapViewer.customTable[num9];
                    }
                    else
                    {
                        array[i + j * num] = MapViewer.customTable[num9] * (0.5f + 0.5f * Mathf.Sin(Time.time * MapViewer.customBlinkRateTable[num9]));
                    }
                }
                else if (this.modeDropdown.value == 5) {
                    array[i + j * num] = MapViewer.atable[TerrainRendererScript.map.GetCell(num3 + i, num4 - j)];
                }
            }
        }
        if (this.modeDropdown.value == 1)
        {
            this.texture_x2.SetPixels(array);
            this.texture_x2.Apply();
            return;
        }
        this.texture.SetPixels(this.colors);
        this.texture.Apply();
    }

    private void Update()
    {
        if (!WorldInitScript.inited)
        {
            return;
        }
        if (this.modeDropdown.value == 1)
        {
            this.marker.transform.localPosition = new Vector3(-1f * (float)(this.mapX - ClientController.THIS.view_x), 1f * (float)(this.mapY - ClientController.THIS.view_y));
        }
        else
        {
            this.marker.transform.localPosition = new Vector3(-2f * (float)(this.mapX - ClientController.THIS.view_x), 2f * (float)(this.mapY - ClientController.THIS.view_y));
        }
        this.marker.color = new Color(1f, 0f, 0f, 0.5f + 0.5f * Mathf.Sin(15f * Time.time));
        if (base.gameObject.activeSelf && this.lastUpdateTime < Time.time - 0.05f)
        {
            this.UpdateMap();
            this.lastUpdateTime = Time.time;
            int num = (int)(UnityEngine.Input.mousePosition.x - this.mapImage.transform.position.x);
            int num2 = (int)(UnityEngine.Input.mousePosition.y - this.mapImage.transform.position.y);
            if (this.modeDropdown.value == 3)
            {
                this.marker.gameObject.SetActive(false);
                this.mapImage.color = new Color(1f, 1f, 1f, 1f);
                base.gameObject.GetComponent<Image>().color = new Color(0f, 0f, 0f, 0f);
            }
            else
            {
                this.marker.gameObject.SetActive(true);
                this.mapImage.color = new Color(1f, 1f, 1f, 1f);
                base.gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            }
            if (this.modeDropdown.value != 1)
            {
                num /= 2;
                num2 /= 2;
            }
            num += this.mapX;
            num2 = this.mapY - num2;
            this.CoordText.text = num + ":" + num2 + " cell:" + TerrainRendererScript.map.GetCell(num, num2).ToString();
            this.cursDx = num;
            this.cursDy = num2;
        }
        if (base.gameObject.activeSelf)
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
            {
                this.OnExit();
            }
            if (Input.GetMouseButtonDown(0) && !this.isDragging)
            {
                this.isDragging = true;
                this.startMapX = this.mapX;
                this.startMapY = this.mapY;
                this.startMouseX = (int)Input.mousePosition.x;
                this.startMouseY = (int)Input.mousePosition.y;
                this.lastdrag = 0f;
            }
            if (Input.GetMouseButtonUp(0))
            {
                this.isDragging = false;
                this.lastdrag = Mathf.Abs(UnityEngine.Input.mousePosition.x - (float)this.startMouseX) + Mathf.Abs(UnityEngine.Input.mousePosition.y - (float)this.startMouseY);
            }
            if (this.isDragging)
            {
                if (this.modeDropdown.value == 1)
                {
                    this.mapX = this.startMapX - (int)((UnityEngine.Input.mousePosition.x - (float)this.startMouseX) / 1f);
                    this.mapY = this.startMapY + (int)((UnityEngine.Input.mousePosition.y - (float)this.startMouseY) / 1f);
                }
                else
                {
                    this.mapX = this.startMapX - (int)((UnityEngine.Input.mousePosition.x - (float)this.startMouseX) / 2f);
                    this.mapY = this.startMapY + (int)((UnityEngine.Input.mousePosition.y - (float)this.startMouseY) / 2f);
                }
                this.lastdrag = Mathf.Abs(UnityEngine.Input.mousePosition.x - (float)this.startMouseX) + Mathf.Abs(UnityEngine.Input.mousePosition.y - (float)this.startMouseY);
            }
            if (this.lastBotX != ClientController.THIS.myBot.gx || this.lastBotY != ClientController.THIS.myBot.gy)
            {
                this.mapX += ClientController.THIS.myBot.gx - this.lastBotX;
                this.mapY += ClientController.THIS.myBot.gy - this.lastBotY;
                this.lastBotX = ClientController.THIS.myBot.gx;
                this.lastBotY = ClientController.THIS.myBot.gy;
            }
        }
    }

    public Image mapImage;

	public Image marker;

	private Texture2D texture;

	private Texture2D texture_x2;

	private Color[] colors;

	private Color[] colors_x2;

	public static Color[] aliveColorTable = new Color[255];

	public static Color[] colorTable = new Color[255];

	public static Color[] transparentTable = new Color[255];

	public static Color[] customTable = new Color[255];

    public static Color[] atable = new Color[255];

	public static float[] customBlinkRateTable = new float[255];

	public static bool colorsInited = false;

	public Button allmapButton;

	public Button exitButton;

	public Dropdown modeDropdown;

	public Text CoordText;

	private int width = 380;

	private int height = 230;

	public static MapViewer THIS;

	private Sprite mapSprite;

	private Sprite mapSprite_x2;

	private float lastClickTime;

	private float lastUpdateTime;

	private bool isDragging;

	private int startMouseX;

	private int startMouseY;

	private int startMapX;

	private int startMapY;

	private int mapX;

	private int mapY;

	private int lastBotX;

	private int lastBotY;

	private int cursDx;

	private int cursDy;

	private float lastdrag;
}

