using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CrystalScroller : MonoBehaviour
{
    public void UpdateFromModel()
    {
        switch (this.type)
        {
            case 0:
                this.crys.texture = this.crys_sprites[0].texture;
                this.left.color = new Color(0f, 1f, 0f);
                this.right.color = new Color(0f, 1f, 0f);
                goto IL_248;
            case 1:
                this.crys.texture = this.crys_sprites[1].texture;
                this.left.color = new Color(0.4f, 0.4f, 1f);
                this.right.color = new Color(0.4f, 0.4f, 1f);
                goto IL_248;
            case 2:
                this.crys.texture = this.crys_sprites[2].texture;
                this.left.color = new Color(1f, 0.3f, 0.3f);
                this.right.color = new Color(1f, 0.3f, 0.3f);
                goto IL_248;
            case 3:
                this.crys.texture = this.crys_sprites[3].texture;
                this.left.color = new Color(1f, 0f, 1f);
                this.right.color = new Color(1f, 0f, 1f);
                goto IL_248;
            case 4:
                this.crys.texture = this.crys_sprites[4].texture;
                this.left.color = new Color(1f, 1f, 1f);
                this.right.color = new Color(1f, 1f, 1f);
                goto IL_248;
        }
        this.crys.texture = this.crys_sprites[5].texture;
        this.left.color = new Color(0f, 1f, 1f);
        this.right.color = new Color(0f, 1f, 1f);
        IL_248:
        this.UpdateValues();
        this.desc.text = this.descText;
        this.needUpdate = false;
    }

    private void UpdateValues()
    {
        if (this.d != 0L)
        {
            if (CrystalScroller.BUY_LOGIC)
            {
                this.bar.value = (float)this.value / (float)this.d;
            }
            else
            {
                float b = (float)this.value / (float)this.d;
                if (this.d < 100L)
                {
                    this.bar.value = b;
                }
                else if (this.d < 10000L)
                {
                    this.bar.value = this.invsinch(b);
                }
                else
                {
                    this.bar.value = this.invsinch(this.invsinch(b));
                }
            }
            this.bar.interactable = true;
            this.handle.SetActive(true);
            return;
        }
        this.bar.value = 0f;
        this.bar.interactable = false;
        this.handle.SetActive(false);
    }

    private void Start()
    {
        this.UpdateFromModel();
        this.bar.onValueChanged.AddListener(new UnityAction<float>(this.BarChange));
    }

    private void BarChange(float v)
    {
        this.needUpdate = true;
    }

    private string KKZer(long num)
    {
        if (num < 1000L)
        {
            return num.ToString("##0");
        }
        if (num < 100000L)
        {
            return num.ToString("## ##0");
        }
        if (num < 100000000L)
        {
            return (num / 1000L).ToString("## ##0K");
        }
        if (num < 10000000000L)
        {
            return (num / 1000000L).ToString("## ##0KK");
        }
        return (num / 1000000000L).ToString("## ##0KKK");
    }

    private float sinch(float a)
    {
        return 0.5f - 0.5f * Mathf.Cos(a * 3.14159274f);
    }

    private float invsinch(float b)
    {
        return Mathf.Acos(-2f * (b - 0.5f)) / 3.14159274f;
    }

    private void Update()
    {
        if (CrystalScroller.BUY_LOGIC)
        {
            if (this.needUpdate)
            {
                this.value = (long)((float)this.d * this.bar.value * this.bar.value * this.bar.value);
            }
            this.left.text = this.KKZer(this.leftMin + this.value);
            this.right.text = this.KKZer(this.rightMin + this.value);
            return;
        }
        if (this.needUpdate)
        {
            if (this.d < 100L)
            {
                this.value = (long)((float)this.d * this.bar.value);
            }
            else if (this.d < 10000L)
            {
                this.value = (long)((float)this.d * this.sinch(this.bar.value));
            }
            else
            {
                this.value = (long)((float)this.d * this.sinch(this.sinch(this.bar.value)));
            }
        }
        if (this.value > this.d)
        {
            this.value = this.d;
        }
        this.left.text = this.KKZer(this.leftMin + this.d - this.value);
        this.right.text = this.KKZer(this.rightMin + this.value);
    }

	public Text left;

	public Text right;

	public Text desc;

	public Scrollbar bar;

	public GameObject handle;

	public RawImage crys;

	public Sprite[] crys_sprites;

	public string descText = "";

	public int type;

	public long leftMin = 100L;

	public long rightMin = 100L;

	public long d = 100L;

	public long value = 50L;

	private bool needUpdate;

	public static bool BUY_LOGIC = true;
}
