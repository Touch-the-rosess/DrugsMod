using Assets.Scripts.DrugsMod;
using System;
using UnityEngine;

public class PackSpriteScript : MonoBehaviour
{
    public void SetObj(ObjectModel value)
    {
        this._Obj = value;
        if (this.instanceInited)
        {
            this.ChangeSpriteToModel();
        }
    }

    public static void InitSprites()
    {
        if (!PackSpriteScript.inited)
        {
            PackSpriteScript.sprites = Resources.LoadAll<Sprite>("packs");
            PackSpriteScript.volc_sprites = Resources.LoadAll<Sprite>("volcano");
            PackSpriteScript.inited = true;
        }
    }

    private void Start()
    {
        this.ChangeSpriteToModel();
        this.instanceInited = true;
    }

    public void ChangeSpriteToModel()
    {
        if (!PackSpriteScript.inited)
        {
            return;
        }
        int num = 0;
        if (this._Obj.type == 'T')
        {
            num = 0;
            if (this._Obj.off == 0)
            {
                num = 17;
            }
        }
        if (this._Obj.type == 'U')
        {
            num = 1;
        }
        if (this._Obj.type == 'M')
        {
            num = 5;
        }
        if (this._Obj.type == 'R')
        {
            num = 3;
            if (this._Obj.off == 0)
            {
                num = 20;
            }
        }
        if (this._Obj.type == 'C')
        {
            num = -1;
        }
        if (this._Obj.type == 'B')
        {
            num = 6;
            if (this._Obj.off == 1)
            {
                num += 30;
            }
            if (this._Obj.off == 2)
            {
                num += 32;
            }
        }
        if (this._Obj.type == 'b')
        {
            num = 8;
        }
        if (this._Obj.type == 'N')
        {
            num = 25;
        }
        if (this._Obj.type == 'P')
        {
            num = 26;
            GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.portPrefab);
            gameObject.transform.SetParent(base.gameObject.transform, false);
            gameObject.transform.position = new Vector3(base.gameObject.transform.position.x, 3.65f, -0.17f);
        }
        if (this._Obj.type == 'F')
        {
            num = 4;
            if (this._Obj.off > 0)
            {
                int num2 = this._Obj.off % 50 - 1;
                GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.craftIconPrefab);
                gameObject2.GetComponent<SpriteRenderer>().sprite = InventoryItem.sprites[num2];
                gameObject2.transform.SetParent(base.gameObject.transform, false);
                gameObject2.transform.localPosition = new Vector3(0.08f, -0.03f, -1f);
                if (this._Obj.off > 50)
                {
                    num = 34;
                }
            }
        }
        if (this._Obj.type == 'W')
        {
            num = 28;
            GameObject gameObject3 = UnityEngine.Object.Instantiate<GameObject>(this.leviPrefab);
            gameObject3.transform.SetParent(base.gameObject.transform, false);
            gameObject3.transform.localPosition = new Vector3(0.1f, 0f, 0f);
        }
        if (this._Obj.type == 'Q')
        {
            num = 27;
            Vector3 localPosition = base.gameObject.transform.localPosition;
            localPosition.x += 0.5f;
            localPosition.y -= 0.5f;
            localPosition.z = -0.5f;
            base.gameObject.transform.localPosition = localPosition;
        }
        if (this._Obj.type == 'Y')
        {
            num = 27;
        }
        if (this._Obj.type == 'J')
        {
            num = 31;
        }
        if (this._Obj.type == 'O')
        {
            num = 32;
        }
        if (this._Obj.type == 'D')
        {
            num = 26;
        }
        if (this._Obj.type == 'Z')
        {
            num = 33;
        }
        if (this._Obj.type == 'L')
        {
            num = 18;
            if (this._Obj.off == 0)
            {
                num = 18;
            }
            if (this._Obj.off == 1)
            {
                num = 19;
            }
            if (this._Obj.off == 2)
            {
                num = 21;
            }
            if (this._Obj.off == 3)
            {
                num = 22;
            }
            if (this._Obj.off == 4)
            {
                num = 23;
            }
            if (this._Obj.off == 5)
            {
                num = 24;
            }
        }
        if (this._Obj.type == 'c')
        {
            num = 9;
        }
        if (this._Obj.type == 'G')
        {
            num = 11;
            if (!this.additionalSpritedAdded)
            {
                GameObject gameObject4 = UnityEngine.Object.Instantiate<GameObject>(this.lampPrefab);
                gameObject4.transform.SetParent(base.gameObject.transform);
                gameObject4.transform.localPosition = new Vector3(0.08f, -0.08f, -1f);
                gameObject4.GetComponent<SpriteRenderer>().sprite = PackSpriteScript.sprites[this._Obj.off + 13];
                if (this._Obj.off > 0)
                {
                    this.gunRadius = UnityEngine.Object.Instantiate<GameObject>(this.gunRadiusPrefab);
                    this.gunRadius.transform.GetChild(0).gameObject.SetActive(DMGlobalVariables.GunRadius_First );
                    this.gunRadius.transform.GetChild(1).gameObject.SetActive(DMGlobalVariables.GunRadius_Second);
                    this.gunRadius.transform.GetChild(2).gameObject.SetActive(DMGlobalVariables.GunRadius_Third );
                    this.gunRadius.transform.SetParent(base.gameObject.transform);
                    this.gunRadius.transform.localPosition = new Vector3(0.08f, -0.08f, -1f);
                    this.gunRadius.GetComponent<SpriteRenderer>().color = this.zoneColors[this._Obj.off];
                }
            }
        }
        base.GetComponent<SpriteRenderer>().sprite = PackSpriteScript.sprites[num];
        if (!this.additionalSpritedAdded && this._Obj.cid != 0)
        {
            GameObject gameObject5 = UnityEngine.Object.Instantiate<GameObject>(this.clanPrefab);
            gameObject5.transform.SetParent(base.gameObject.transform);
            if (this._Obj.type == 'G')
            {
                gameObject5.transform.localPosition = new Vector3(0.25f, -0.07f, -1f);
            }
            if (this._Obj.type == 'L')
            {
                gameObject5.transform.localPosition = new Vector3(0.35f, 0.04f, -1f);
            }
            if (this._Obj.type == 'T' || this._Obj.type == 'R' || this._Obj.type == 'F')
            {
                gameObject5.transform.localPosition = new Vector3(0.35f, -0.04f, -1f);
            }
            gameObject5.GetComponent<ClanSpriteScript>().changeClanToId(this._Obj.cid);
        }
        this.additionalSpritedAdded = true;
    }

    private void Update()
    {
        if (this._Obj != null)
        {
            if (this._Obj.type == 'Q')
            {
                this.volcFrame++;
                this.volcFrame %= 75;
                base.GetComponent<SpriteRenderer>().sprite = PackSpriteScript.volc_sprites[this.volcFrame / 5];
            }
            if (this._Obj.type == 'B')
            {
                int num = 6 + Mathf.FloorToInt(1f + 0.99f * Mathf.Cos(Time.time * 33f));
                if (this._Obj.off == 1)
                {
                    num += 30;
                }
                if (this._Obj.off == 2)
                {
                    num += 32;
                }
                if (num < 0 || num >= PackSpriteScript.sprites.Length)
                {
                    UnityEngine.Debug.Log("badid" + num.ToString());
                }
                base.GetComponent<SpriteRenderer>().sprite = PackSpriteScript.sprites[num];
            }
            else if (this._Obj.type == 'I')
            {
                int num2 = 28 + Mathf.FloorToInt(1.5f + 1.499f * Mathf.Cos(Time.time * 33f));
                base.GetComponent<SpriteRenderer>().sprite = PackSpriteScript.sprites[num2];
            }
        }
        if (this.gunRadius != null ) {
            this.gunRadius.gameObject.SetActive(ClientConfig.gunRadius);
            this.gunRadius.transform.GetChild(0).gameObject.SetActive(DMGlobalVariables.GunRadius_First);
            this.gunRadius.transform.GetChild(1).gameObject.SetActive(DMGlobalVariables.GunRadius_Second);
            this.gunRadius.transform.GetChild(2).gameObject.SetActive(DMGlobalVariables.GunRadius_Third);
        }
        //if (this.gunRadius != null && this.lastGunUpdate < Time.unscaledTime)
        //{
        //    if (ClientConfig.gunRadius)
        //    {
        //        this.lastGunUpdate = Time.unscaledTime + 0.051f;
        //        this.gunFrame++;
        //        if (this.gunFrame == 4)
        //        {
        //            this.gunFrame = 0;
        //        }
        //        switch (this.gunFrame)
        //        {
        //            case 0:
        //                this.gunRadius.transform.localScale = new Vector3(0.16f, 0.16f, 1f);
        //                return;
        //            case 1:
        //                this.gunRadius.transform.localScale = new Vector3(-0.16f, 0.16f, 1f);
        //                return;
        //            case 2:
        //                this.gunRadius.transform.localScale = new Vector3(-0.16f, -0.16f, 1f);
        //                return;
        //            case 3:
        //                this.gunRadius.transform.localScale = new Vector3(0.16f, -0.16f, 1f);
        //                return;
        //            default:
        //                return;
        //        }
        //    }
        //    else
        //    {
        //        this.gunRadius.transform.localScale = new Vector3(0f, -0f, 0f);
        //    }
        //}
    }

    public static Sprite[] sprites;

	public static Sprite[] volc_sprites;

	public static bool inited;

	public GameObject portPrefab;

	public GameObject clanPrefab;

	public GameObject lampPrefab;

	public GameObject gunRadiusPrefab;

	public GameObject leviPrefab;

	public GameObject craftIconPrefab;

	public GameObject RenderWrapper;

	public Color[] zoneColors = new Color[]
	{
		new Color(0f, 0f, 0f),
		new Color(0f, 1f, 1f),
		new Color(0f, 1f, 1f),
		new Color(0f, 1f, 1f)
	};

	public ObjectModel _Obj;

	private GameObject gunRadius;

	private bool additionalSpritedAdded;

	private bool instanceInited;

	private float lastGunUpdate;

	private int gunFrame;

	private int volcFrame;
}
