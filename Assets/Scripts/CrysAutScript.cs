using System;
using UnityEngine;

public class CrysAutScript : MonoBehaviour
{
    public void SetCrys(int x, int y, string crys, int dx, int dy)
    {
        this.crys = crys;
        this.x = x;
        this.y = y;
        this.dy = dx - 50;
        this.dx = dy - 50;
    }

    private void Start()
    {
        /*if (!CrysAutScript.inited)
        {
            CrysAutScript.sprites = Resources.LoadAll<Sprite>("crysfont");
            CrysAutScript.inited = true;
        }
        int num = 0;
        string text = this.crys;
        uint num2 = _003CPrivateImplementationDetails_003E.ComputeStringHash(text);
        if (num2 <= 3876335077u)
        {
            if (num2 != 3792446982u)
            {
                if (num2 != 3859557458u)
                {
                    if (num2 == 3876335077u)
                    {
                        if (text == "b")
                        {
                            num = 3;
                        }
                    }
                }
                else if (text == "c")
                {
                    num = 5;
                }
            }
            else if (text == "g")
            {
                num = 0;
            }
        }
        else if (num2 <= 4077666505u)
        {
            if (num2 != 4060888886u)
            {
                if (num2 == 4077666505u)
                {
                    if (text == "v")
                    {
                        num = 2;
                    }
                }
            }
            else if (text == "w")
            {
                num = 4;
            }
        }
        else if (num2 != 4144776981u)
        {
            if (num2 == 4278997933u)
            {
                if (text == "z")
                {
                    num = 6;
                }
            }
        }
        else if (text == "r")
        {
            num = 1;
        }
        base.gameObject.transform.position = new Vector3((float)(this.x + this.dx) + 0.5f, (float)(-(float)this.y - this.dy) - 0.5f, -2f);
        float num3 = 0f;
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.cryPrefab);
        gameObject.transform.SetParent(base.gameObject.transform);
        gameObject.transform.localPosition = new Vector3(num3, 0f, 0f);
        if (this.crys == "z")
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = InventoryItem.sprites[23];
            gameObject.transform.localScale = new Vector3(2f, 2f, 1f);
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = CrysAutScript.sprites[num];
            gameObject.transform.localScale = new Vector3(6.25f, 6.25f, 1f);
        }
        num3 += 0.75f;
        this.start = Time.unscaledTime;
        this.delayStart = Time.unscaledTime;*/
        if (!CrysAutScript.inited)
        {
            CrysAutScript.sprites = Resources.LoadAll<Sprite>("crysfont");
            CrysAutScript.inited = true;
        }
        int num = 0;
        switch (crys)
        {
            case "g":
                num = 0;
                break;
            case "r":
                num = 1;
                break;
            case "v":
                num = 2;
                break;
            case "b":
                num = 3;
                break;
            case "w":
                num = 4;
                break;
            case "c":
                num = 5;
                break;
        }
        base.gameObject.transform.position = new Vector3((float)(x + dx) + 0.5f, (float)(-y - dy) - 0.5f, -2f);
        float num2 = 0f;
        GameObject gameObject = UnityEngine.Object.Instantiate(cryPrefab);
        gameObject.transform.SetParent(base.gameObject.transform);
        gameObject.transform.localPosition = new Vector3(num2, 0f, 0f);
        gameObject.GetComponent<SpriteRenderer>().sprite = sprites[num];
        num2 += 0.75f;
        start = Time.unscaledTime;
        delayStart = Time.unscaledTime;
    }

    private void Update()
    {
        Vector3 vector = base.gameObject.transform.position;
        this.lerpLevel = 0.9f;
        vector = this.lerpLevel * vector + (1f - this.lerpLevel) * new Vector3((float)this.x + 0.5f, (float)(-(float)this.y) - 0.5f, -2f);
        base.gameObject.transform.position = vector;
        if (Time.unscaledTime > this.start + 0.5f)
        {
            UnityEngine.Object.Destroy(base.gameObject);
        }
    }

    public static bool inited;

	public static Sprite[] sprites;

	public GameObject cryPrefab;

	private string crys = "g";

	private int num;

	private int x;

	private int y;

	private int dx;

	private int dy;

	private Color[] colors = new Color[]
	{
		new Color(0.5f, 1f, 0.5f),
		new Color(1f, 0.5f, 0.5f),
		new Color(1f, 0.1f, 1f),
		new Color(0.5f, 0.5f, 1f),
		new Color(1f, 1f, 1f),
		new Color(0.1f, 1f, 1f)
	};

	private float start;

	private float delayStart;

	private float lerpLevel = 1f;
}
