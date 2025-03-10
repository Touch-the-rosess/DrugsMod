using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniSkillScript : MonoBehaviour
{
    private void Start()
    {
        this.InitGfx();
    }

    public void SetModel(int progress, string code)
    {
        this.code = code;
        this.iconNum = SkillButtonScript.skillShorts[code];
        this.p = (float)progress / 100f;
        this.color = SkillButtonScript.colors[this.iconNum];
        if (progress >= 200)
        {
            this.p = 2f;
        }
    }

    public void InitGfx()
    {
        this.icon.sprite = SkillButtonScript.sprites[this.iconNum];
        if (this.p >= 1f)
        {
            this.up.gameObject.SetActive(true);
            this.up.color = this.color;
            this.bar.color = new Color(1f, 0f, 0f);
            this.ttl = 1E+07f;
            return;
        }
        this.up.gameObject.SetActive(false);
        this.bar.gameObject.SetActive(true);
        this.bar.color = this.color;
        this.ttl = Time.time + 5f;
    }

    private void Update()
    {
        if (this.p >= 1f)
        {
            Vector3 localPosition = this.up.transform.localPosition;
            localPosition.y = 14f + 1f * Mathf.Sin(10f * Time.time);
            this.up.transform.localPosition = localPosition;
            Vector2 sizeDelta = this.bar.rectTransform.sizeDelta;
            sizeDelta.y = 20f * (0.98f * (this.p - 1f) + 0.02f * (1f * Time.time - Mathf.Floor(1f * Time.time)));
            this.bar.rectTransform.sizeDelta = sizeDelta;
        }
        else
        {
            Vector2 sizeDelta2 = this.bar.rectTransform.sizeDelta;
            sizeDelta2.y = 20f * (0.8f * this.p + 0.2f * (1f * Time.time - Mathf.Floor(1f * Time.time)));
            this.bar.rectTransform.sizeDelta = sizeDelta2;
        }
        if (Time.time > this.ttl)
        {
            UnityEngine.Object.Destroy(base.gameObject);
            MiniSkillScript.minis.Remove(this.code);
        }
    }
        
	public static Dictionary<string, MiniSkillScript> minis = new Dictionary<string, MiniSkillScript>();

	public Image icon;

	public RawImage bar;

	public RawImage up;

	private float ttl;

	private float p;

	private int iconNum = -1;

	private string code;

	private Color color;
}
