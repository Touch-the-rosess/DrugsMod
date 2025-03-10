using System;
using UnityEngine;

public class BoomScript : MonoBehaviour
{
    public void Setup(int x, int y, int њїјїјїљњњїљјїјїїљљњјњїњ, int color, int _index)
    {
        this.index = _index;
        this.x = x;
        this.y = y;
        this.size = њїјїјїљњњїљјїјїїљљњјњїњ;
        this.color = color;
        this.BOOM_TIME = 0.12f * Mathf.Sqrt((float)this.size);
        this.startTime = Time.unscaledTime;
        if (this.inited)
        {
            base.transform.position = new Vector3((float)x + 0.5f, (float)(-(float)y) - 0.5f, -2f);
            base.transform.localScale = new Vector3(0f, 0f, 1f);
            switch (color)
            {
                case 0:
                    base.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 0.8f, 0f);
                    return;
                case 1:
                    base.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 0.2f, 1f);
                    return;
                case 2:
                    base.gameObject.GetComponent<SpriteRenderer>().color = new Color(0f, 1f, 1f);
                    break;
                default:
                    return;
            }
        }
    }

    private void Start()
    {
        this.inited = true;
        this.startTime = Time.unscaledTime;
        base.transform.position = new Vector3((float)this.x + 0.5f, (float)(-(float)this.y) - 0.5f, -2f);
        base.transform.localScale = new Vector3(0f, 0f, 1f);
        switch (this.color)
        {
            case 0:
                base.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 0.8f, 0f);
                return;
            case 1:
                base.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 0.2f, 1f);
                return;
            case 2:
                base.gameObject.GetComponent<SpriteRenderer>().color = new Color(0f, 1f, 1f);
                return;
            default:
                return;
        }
    }

    private void Update()
    {
        float num = Time.unscaledTime - this.startTime;
        float num2 = num / this.BOOM_TIME;
        num2 = Mathf.Sqrt(num2);
        if ((double)num2 > 0.5)
        {
            Color color = base.gameObject.GetComponent<SpriteRenderer>().color;
            color.a = 2f * (1f - num2);
            base.gameObject.GetComponent<SpriteRenderer>().color = color;
        }
        base.transform.localScale = new Vector3(num2 * (float)this.size / 3f, num2 * (float)this.size / 3f, 1f);
        if (num > this.BOOM_TIME)
        {
            ClientController.THIS.boomPool.Free(this.index);
        }
    }


	private int x = 4;

	private int y = 4;

	private int size = 4;

	private int color = 4;

	private int index = -1;

	private bool inited;

	private float startTime;

	private float BOOM_TIME = 0.5f;
}

