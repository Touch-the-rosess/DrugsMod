using System;
using UnityEngine;
using UnityEngine.UI;

public class TutorialNavigation : MonoBehaviour
{
    private void Start()
    {
        TutorialNavigation.THIS = this;
        base.gameObject.SetActive(false);
        this.lens.SetActive(false);
        this.pad = base.GetComponent<Image>();
        this.NaviArrowRT = this.NaviArrow.GetComponent<RectTransform>();
    }

    public void SetNaviArrow(int dx, int dy)
    {
        this.NaviArrow.SetActive(true);
        this.arrowX = dx;
        this.arrowY = dy;
        this.showArrow = true;
    }

    public void SetNavi(string text, int dx, int dy, int anchorType, string hideReason)
    {
        TutorialNavigation.hideReason = hideReason;
        if (TutorialNavigation.hideReason.StartsWith(":"))
        {
            int num = int.Parse(TutorialNavigation.hideReason.Split(new char[]
            {
                ':'
            })[1]);
            this.hideTime = Time.unscaledTime + (float)num;
        }
        if (anchorType < 10)
        {
            ClientController.THIS.stopAutoMove();
        }
        if (text == "")
        {
            base.gameObject.SetActive(false);
            this.lens.SetActive(false);
            TutorialNavigation.hideReason = "";
            this.hideTime = 0f;
            return;
        }
        base.gameObject.SetActive(true);
        this.lens.SetActive(anchorType < 10);
        this.tf.text = text;
        RectTransform component = base.GetComponent<RectTransform>();
        this.pad = base.GetComponent<Image>();
        switch (anchorType % 10)
        {
            case 0:
                this.pad.sprite = this.rightUpSprite;
                component.anchorMin = new Vector2(1f, 1f);
                component.anchorMax = new Vector2(1f, 1f);
                component.pivot = new Vector2(1f, 1f);
                component.anchoredPosition = new Vector3((float)(-(float)dx), (float)(-(float)dy));
                break;
            case 1:
                this.pad.sprite = this.rightDownSprite;
                component.anchorMin = new Vector2(1f, 0f);
                component.anchorMax = new Vector2(1f, 0f);
                component.pivot = new Vector2(1f, 0f);
                component.anchoredPosition = new Vector3((float)(-(float)dx), (float)dy);
                break;
            case 2:
                this.pad.sprite = this.leftDownSprite;
                component.anchorMin = new Vector2(0f, 0f);
                component.anchorMax = new Vector2(0f, 0f);
                component.pivot = new Vector2(0f, 0f);
                component.anchoredPosition = new Vector3((float)dx, (float)dy);
                break;
            case 3:
                this.pad.sprite = this.leftUpSprite;
                component.anchorMin = new Vector2(0f, 1f);
                component.anchorMax = new Vector2(0f, 1f);
                component.pivot = new Vector2(0f, 1f);
                component.anchoredPosition = new Vector3((float)dx, (float)(-(float)dy));
                break;
            case 4:
                this.pad.sprite = this.rightUpSprite;
                component.anchorMin = new Vector2(0.5f, 0.5f);
                component.anchorMax = new Vector2(0.5f, 0.5f);
                component.pivot = new Vector2(1f, 1f);
                component.anchoredPosition = new Vector3((float)dx, (float)dy);
                break;
            case 5:
                this.pad.sprite = this.rightDownSprite;
                component.anchorMin = new Vector2(0.5f, 0.5f);
                component.anchorMax = new Vector2(0.5f, 0.5f);
                component.pivot = new Vector2(1f, 0f);
                component.anchoredPosition = new Vector3((float)dx, (float)(-(float)dy));
                break;
            case 6:
                this.pad.sprite = this.leftDownSprite;
                component.anchorMin = new Vector2(0.5f, 0.5f);
                component.anchorMax = new Vector2(0.5f, 0.5f);
                component.pivot = new Vector2(0f, 0f);
                component.anchoredPosition = new Vector3((float)(-(float)dx), (float)(-(float)dy));
                break;
            case 7:
                this.pad.sprite = this.leftUpSprite;
                component.anchorMin = new Vector2(0.5f, 0.5f);
                component.anchorMax = new Vector2(0.5f, 0.5f);
                component.pivot = new Vector2(0f, 1f);
                component.anchoredPosition = new Vector3((float)(-(float)dx), (float)dy);
                break;
        }
        RectTransform component2 = this.lens.GetComponent<RectTransform>();
        component2.anchorMax = component.anchorMax;
        component2.anchorMin = component.anchorMin;
        component2.anchoredPosition = component.anchoredPosition;
        this.lens.transform.position = base.gameObject.transform.position;
        LayoutRebuilder.ForceRebuildLayoutImmediate(base.gameObject.GetComponent<RectTransform>());
    }

    private void hide()
    {
        base.gameObject.SetActive(false);
        this.lens.SetActive(false);
        TutorialNavigation.hideReason = "";
        this.hideTime = 0f;
        this.HideArrow();
    }

    public static void CheckHide(string marker)
    {
        if (TutorialNavigation.hideReason.Contains(marker))
        {
            ServerTime.THIS.SendTypicalMessage(-1, "THID", 0, 0, marker);
            TutorialNavigation.THIS.hide();
        }
    }

    private void Update()
    {
        if (this.hideTime != 0f && Time.unscaledTime > this.hideTime)
        {
            ServerTime.THIS.SendTypicalMessage(-1, "THID", 0, 0, "TIMEOVER");
            this.hide();
        }
        if (base.gameObject.activeSelf)
        {
            this.pad.color = new Color(1f, 1f, 1f, 0.9f + 0.1f * Mathf.Sin(12f * Time.time));
        }
    }

    public void UpdateArrow()
    {
        if (this.showArrow)
        {
            float num = Camera.main.transform.position.x - 0.5f;
            float num2 = -Camera.main.transform.position.y - 0.5f;
            float num3 = Mathf.Atan2((float)this.arrowY - num2, (float)this.arrowX - num);
            float num4 = Mathf.Sqrt(((float)this.arrowY - num2) * ((float)this.arrowY - num2) + ((float)this.arrowX - num) * ((float)this.arrowX - num));
            float num5 = Mathf.Min(num4 - 2f, 10f) - 0.5f * Mathf.Sin(9f * Time.time);
            this.NaviArrowRT.rotation = Quaternion.Euler(0f, 0f, -180f * num3 / 3.14159274f);
            this.NaviArrowRT.localPosition = new Vector3(16f * num5 * Mathf.Cos(num3), -16f * num5 * Mathf.Sin(num3), 0f);
            if (num4 < 4f)
            {
                this.HideArrow();
            }
        }
    }

    private void HideArrow()
    {
        this.NaviArrow.SetActive(false);
        this.showArrow = false;
    }

    public Sprite leftUpSprite;

	public Sprite rightUpSprite;

	public Sprite leftDownSprite;

	public Sprite rightDownSprite;

	public GameObject lens;

	public Text tf;

	public static TutorialNavigation THIS;

	public GameObject NaviArrow;

	private RectTransform NaviArrowRT;

	public static string hideReason = "";

	private bool showArrow;

	private int arrowX;

	private int arrowY;

	private float hideTime;

	private Image pad;
}
