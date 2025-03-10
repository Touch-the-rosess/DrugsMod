using System;
using UnityEngine;
using UnityEngine.UI;

public class StateLineScript : MonoBehaviour
{
    public void SetLine(string[] text, bool blinking, Color color)
    {
        this.texts = text;
        this.blinking = blinking;
        this.color = color;
    }

    private void Start()
    {
        this.inited = true;
    }

    private void Update()
    {
        int num = Mathf.FloorToInt(Time.unscaledTime / 1.5f) % this.texts.Length;
        if (num != this.prevIndex)
        {
            this.prevIndex = num;
            base.gameObject.GetComponentInChildren<Text>().text = this.texts[num];
        }
        if (this.blinking)
        {
            this.color.a = 0.5f + 0.3f * Mathf.Sin(4f * Time.unscaledTime);
        }
        else
        {
            this.color.a = 0.7f;
        }
        base.gameObject.GetComponent<Image>().color = this.color;
    }

    public string[] texts;

	public bool blinking;

	public Color color;

	public bool inited;

	private int prevIndex = -1;
}
