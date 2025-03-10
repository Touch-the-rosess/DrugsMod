using System;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    public static void InitSprites()
    {
        if (InventoryItem.spritesLoaded)
        {
            return;
        }
        InventoryItem.sprites = Resources.LoadAll<Sprite>("inventory");
        InventoryItem.spritesLoaded = true;
    }

    private void Start()
    {
        if (!InventoryItem.spritesLoaded)
        {
            InventoryItem.InitSprites();
        }
        this.inited = true;
        this.UpdateItemView();
    }

    public void Setup(int id, int num, bool frame = false, string upstr = "", string downstr = "")
    {
        this.id = id;
        this.num = num;
        this.frame = frame;
        this.upString = upstr;
        this.downString = downstr;
        if (this.inited)
        {
            this.UpdateItemView();
        }
    }

    private void UpdateItemView()
    {
        this.frameImage.gameObject.SetActive(this.frame);
        if (this.id == -1)
        {
            this.itemImage.gameObject.SetActive(false);
            this.numText.gameObject.SetActive(false);
            return;
        }
        this.itemImage.gameObject.SetActive(true);
        this.numText.gameObject.SetActive(true);
        if (this.id >= 2000)
        {
            this.itemImage.sprite = SkillButtonScript.sprites[this.id - 2000];
            this.itemImage.SetNativeSize();
            this.itemImage.rectTransform.sizeDelta = 0.5f * this.itemImage.rectTransform.sizeDelta;
        }
        else if (this.id > 200)
        {
            this.itemImage.sprite = ClanSpriteScript.sprites[this.id - 200 - 1];
            this.itemImage.SetNativeSize();
            this.itemImage.rectTransform.sizeDelta = 3f * this.itemImage.rectTransform.sizeDelta;
        }
        else
        {
            this.itemImage.sprite = InventoryItem.sprites[this.id];
            this.itemImage.SetNativeSize();
        }
        if (this.num > 0)
        {
            this.numText.text = this.num.ToString();
        }
        else if (this.num == 0)
        {
            this.numText.text = "";
        }
        else
        {
            this.numText.text = (-this.num).ToString();
            this.itemImage.color = new Color(1f, 1f, 1f, 0.4f);
            this.numText.color = new Color(0.8f, 0.4f, 0.4f, 0.9f);
        }
        if (this.upString.StartsWith("@"))
        {
            this.numText.text = "";
            this.itemImage.color = new Color(1f, 1f, 1f, 0.4f);
            this.numText.color = new Color(0.8f, 0.4f, 0.4f, 0.9f);
            this.upString = this.upString.Substring(1);
        }
        if (this.upString.StartsWith("^"))
        {
            this.upString = this.upString.Substring(1);
            this.upText.color = new Color(0.6f, 0.5f, 0.5f, 1f);
        }
        if (this.upString.StartsWith("!"))
        {
            this.upText.color = new Color(0.3f, 0.4f, 0.3f, 1f);
            this.upString = this.upString.Substring(1);
        }
        if (this.downString.StartsWith("^"))
        {
            this.downString = this.downString.Substring(1);
            this.numText.color = new Color(0.6f, 0.5f, 0.5f, 1f);
        }
        if (this.downString.StartsWith("!"))
        {
            this.downString = this.downString.Substring(1);
            this.numText.color = new Color(0.3f, 0.4f, 0.3f, 1f);
        }
        if (this.upString != "")
        {
            this.upText.gameObject.SetActive(true);
            this.upText.text = this.upString;
        }
        if (this.downString != "")
        {
            this.numText.gameObject.SetActive(true);
            this.numText.text = this.downString;
        }
    }

    private void Update()
    {
    }
        
	public static Sprite[] sprites;

	public Image itemImage;

	public Text numText;

	public Text upText;

	private int id = -1;

	private int num;

	private string upString = "";

	private string downString = "";

	public Image frameImage;

	private static bool spritesLoaded;

	private bool inited;

	private bool frame;
}
