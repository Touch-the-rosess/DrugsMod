using System;
using UnityEngine;
using UnityEngine.UI;

public class MiniMapViewer : MonoBehaviour
{
    private void Start()
    {
        MiniMapViewer.THIS = this;
        this.texture = new Texture2D(this.width, this.height, TextureFormat.RGBA32, false);
        this.texture.filterMode = FilterMode.Bilinear;
        this.colors = new Color[this.width * this.height];
        Sprite sprite = Sprite.Create(this.texture, new Rect(0f, 0f, (float)this.width, (float)this.height), new Vector2(0.5f, 0.5f));
        this.mapImage.sprite = sprite;
        this.mapImage.SetNativeSize();
        if (!MapViewer.colorsInited)
        {
            MapViewer.InitColorTable();
        }
    }

    public static Color RGBQ(int r, int g, int b)
    {
        return new Color((float)r / 256f, (float)g / 256f, (float)b / 256f);
    }

    public void Show()
    {
        this.mapX = ClientController.THIS.view_x;
        this.mapY = ClientController.THIS.view_y;
        this.lastBotX = this.mapX;
        this.lastBotY = this.mapY;
        this.UpdateMap();
        base.gameObject.SetActive(true);
    }

    public void UpdateMap()
    {
        if (TerrainRendererScript.map == null)
        {
            return;
        }
        int num = this.mapX - this.width / 2;
        int num2 = this.mapY + this.height / 2;
        for (int i = 0; i < this.width; i++)
        {
            for (int j = 0; j < this.height; j++)
            {
                this.colors[i + j * this.width] = MapViewer.colorTable[TerrainRendererScript.map.GetCell(num + i, num2 - j)];
            }
        }
        this.texture.SetPixels(this.colors);
        this.texture.Apply();
    }

    private void Update()
    {
        if (base.gameObject.activeSelf && this.lastUpdateTime < Time.time - 0.05f)
        {
            this.UpdateMap();
            this.lastUpdateTime = Time.time;
        }
        this.mapX = ClientController.THIS.view_x;
        this.mapY = ClientController.THIS.view_y;
    }
        
	public Image mapImage;

	private Texture2D texture;

	private Color[] colors;

	private int width = 100;

	private int height = 100;

	public static MiniMapViewer THIS;

	private float lastUpdateTime;

	private int mapX;

	private int mapY;

	private int lastBotX;

	private int lastBotY;
}

