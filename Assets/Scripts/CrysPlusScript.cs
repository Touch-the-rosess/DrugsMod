using System;
using UnityEngine;

public class CrysPlusScript : MonoBehaviour
{
    public void SetCrys(int x, int y, string crys, int num, int bid, int delay, int _index)
    {
        this.crys = crys;
        this.num = num;
        this.x = x;
        this.y = y;
        this.bid = bid;
        this.delay = delay;
        this.index = _index;
        if (this.instanceInited)
        {
            this.UpdateModel();
        }
    }

    private void UpdateModel()
    {
        int num = 0;
        string a = this.crys;
        if (!(a == "g"))
        {
            if (!(a == "r"))
            {
                if (!(a == "v"))
                {
                    if (!(a == "b"))
                    {
                        if (!(a == "w"))
                        {
                            if (a == "c")
                            {
                                num = 5;
                            }
                        }
                        else
                        {
                            num = 4;
                        }
                    }
                    else
                    {
                        num = 3;
                    }
                }
                else
                {
                    num = 2;
                }
            }
            else
            {
                num = 1;
            }
        }
        else
        {
            num = 0;
        }
        base.gameObject.transform.position = new Vector3((float)this.x + 0.5f, (float)(-(float)this.y) - 0.5f, -2f);
        float num2 = 0f;
        this.crysImage.transform.SetParent(base.gameObject.transform);
        this.crysImage.transform.localPosition = new Vector3(num2, 0f, 0f);
        this.crysImage.GetComponent<SpriteRenderer>().sprite = CrysPlusScript.sprites[num];
        num2 += 0.75f;
       // UnityEngine.Debug.Log(this.num);
        if (this.num >= 100)
        {
            this.hundImage.SetActive(true);
            int num3 = Mathf.FloorToInt((float)(this.num / 100));
            this.hundImage.transform.SetParent(base.gameObject.transform);
            this.hundImage.transform.localPosition = new Vector3(num2, 0f, 0f);
            if (num3 == 0)
            {
                num3 = 10;
            }
            this.hundImage.GetComponent<SpriteRenderer>().sprite = CrysPlusScript.sprites[5 + num3];
            this.hundImage.GetComponent<SpriteRenderer>().color = this.colors[num];
            num2 += 0.5f;
        }
        else
        {
            this.hundImage.SetActive(false);
        }
        if (this.num >= 10)
        {
            this.tenImage.SetActive(true);
            int num4 = Mathf.FloorToInt((float)(this.num % 100 / 10));
            this.tenImage.transform.SetParent(base.gameObject.transform);
            this.tenImage.transform.localPosition = new Vector3(num2, 0f, 0f);
            if (num4 == 0)
            {
                num4 = 10;
            }
            this.tenImage.GetComponent<SpriteRenderer>().sprite = CrysPlusScript.sprites[5 + num4];
            this.tenImage.GetComponent<SpriteRenderer>().color = this.colors[num];
            num2 += 0.5f;
        }
        else
        {
            this.tenImage.SetActive(false);
        }
        if (this.num > 1)
        {
            this.singImage.SetActive(true);
            int num5 = Mathf.FloorToInt((float)(this.num % 10));
            this.singImage.transform.SetParent(base.gameObject.transform);
            this.singImage.transform.localPosition = new Vector3(num2, 0f, 0f);
            if (num5 == 0)
            {
                num5 = 10;
            }
            this.singImage.GetComponent<SpriteRenderer>().sprite = CrysPlusScript.sprites[5 + num5];
            this.singImage.GetComponent<SpriteRenderer>().color = this.colors[num];
            num2 += 0.5f;
        }
        else
        {
            this.singImage.SetActive(false);
        }
        this.start = Time.unscaledTime + 0.001f * (float)this.delay;
        this.delayStart = Time.unscaledTime;
    }

    private void Start()
    {
        if (!CrysPlusScript.inited)
        {
            CrysPlusScript.sprites = Resources.LoadAll<Sprite>("crysfont");
            CrysPlusScript.inited = true;
        }
        this.crysImage = UnityEngine.Object.Instantiate<GameObject>(this.cryPrefab);
        this.hundImage = UnityEngine.Object.Instantiate<GameObject>(this.cryPrefab);
        this.tenImage = UnityEngine.Object.Instantiate<GameObject>(this.cryPrefab);
        this.singImage = UnityEngine.Object.Instantiate<GameObject>(this.cryPrefab);
        this.instanceInited = true;
        this.UpdateModel();
    }

    private void Update()
    {
        if (Time.unscaledTime - this.delayStart > 0.001f * (float)this.delay)
        {
            Vector3 position = base.gameObject.transform.position;
            position.z = -2f;
            base.gameObject.transform.position = position;
            Vector3 vector = base.gameObject.transform.position;
            vector.y += 8f * Time.unscaledDeltaTime;
            this.lerpLevel = 1f - 0.5f * (Time.unscaledTime - this.start);
            if (RobotRenderer.THIS.bots.ContainsKey(this.bid))
            {
                vector = this.lerpLevel * vector + (1f - this.lerpLevel) * RobotRenderer.THIS.bots[this.bid].transform.position;
            }
            base.gameObject.transform.position = vector;
            if (Time.unscaledTime > this.start + 0.5f)
            {
                ClientController.THIS.crysPlusPool.Free(this.index);
            }
            return;
        }
        Vector3 position2 = base.gameObject.transform.position;
        position2.z = 2f;
        base.gameObject.transform.position = position2;
    }

    public static bool inited;

	public static Sprite[] sprites;

	public GameObject cryPrefab;

	private string crys = "g";

	private int num;

	private int x;

	private int y;

	private int bid = -1;

	private int delay;

	private int index = -1;

	private bool instanceInited;

	private Color[] colors = new Color[]
	{
		new Color(0.5f, 1f, 0.5f),
		new Color(1f, 0.5f, 0.5f),
		new Color(1f, 0.1f, 1f),
		new Color(0.5f, 0.5f, 1f),
		new Color(1f, 1f, 1f),
		new Color(0.1f, 1f, 1f)
	};

	private GameObject crysImage;

	private GameObject hundImage;

	private GameObject tenImage;

	private GameObject singImage;

	private float start;

	private float delayStart;

	private float lerpLevel = 1f;
}

