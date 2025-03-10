using System;
using UnityEngine;
using UnityEngine.UI;

public class MissionPad : MonoBehaviour
{
    private void Start()
    {
        MissionPad.THIS = this;
        base.GetComponent<Button>().onClick.AddListener(delegate ()
        {
            ServerTime.THIS.SendTypicalMessage(-1, "Miso", 0, 0, "0");
            TutorialNavigation.CheckHide("_MISO");
        });
        base.gameObject.SetActive(false);
    }

    public void UpdateMissionProgress(int exp, int max)
    {
        float x = this.bar.gameObject.transform.parent.parent.gameObject.GetComponent<RectTransform>().sizeDelta.x;
        Vector2 sizeDelta = this.bar.rectTransform.sizeDelta;
        sizeDelta.x = x * (float)exp / (float)max;
        this.bar.rectTransform.sizeDelta = sizeDelta;
        this.progressText = exp.ToString() + " / " + max.ToString();
        if (max == 1)
        {
            this.progressText = "";
        }
        this.tf.text = this.macroReplacedText();
    }

    public void UpdateMissionPanel(string url, int imgx, int imgy, int progress, string text)
    {
        if (text == "")
        {
            base.gameObject.SetActive(false);
            return;
        }
        base.gameObject.SetActive(true);
        this.macroText = text;
        if (text.Contains("%T%"))
        {
            string s = text.Substring(text.IndexOf("%T%") + 3, text.LastIndexOf('%') - text.IndexOf("%T%") - 3);
            this.endTime = Time.unscaledTime + (float)int.Parse(s);
            this.macroText = text.Substring(0, text.IndexOf("%T%") + 3) + text.Substring(text.LastIndexOf('%') + 1);
            this.needUpdateTimer = true;
        }
        else
        {
            this.needUpdateTimer = false;
        }
        this.progressText = "? / ?";
        this.tf.text = this.macroReplacedText();
        LayoutRebuilder.ForceRebuildLayoutImmediate(base.GetComponent<RectTransform>());
        if (url != "")
        {
            this.webImage.gameObject.SetActive(true);
            this.webImage.SetSizeAndUrl(imgx, imgy, url);
        }
        else
        {
            this.webImage.gameObject.SetActive(false);
        }
        float x = this.bar.gameObject.transform.parent.parent.gameObject.GetComponent<RectTransform>().sizeDelta.x;
        Vector2 sizeDelta = this.bar.rectTransform.sizeDelta;
        sizeDelta.x = x * (float)progress / 100f;
        this.bar.rectTransform.sizeDelta = sizeDelta;
    }

    private string macroReplacedText()
    {
        return this.macroText.Replace("%T%", this.timeLeft()).Replace("%P%", this.progressText);
    }

    private void Update()
    {
        if (this.needUpdateTimer)
        {
            this.tf.text = this.macroReplacedText();
        }
    }

    private string timeLeft()
    {
        int num = Mathf.CeilToInt(this.endTime - Time.unscaledTime);
        if (num < 0)
        {
            num = 0;
        }
        int num2 = Mathf.FloorToInt((float)(num / 3600));
        int s = Mathf.FloorToInt((float)(num / 60)) % 60;
        int s2 = num % 60;
        if (num2 == 0)
        {
            return this.pad(s) + ":" + this.pad(s2);
        }
        return string.Concat(new string[]
        {
            this.pad(num2),
            ":",
            this.pad(s),
            ":",
            this.pad(s2)
        });
    }

    private string pad(int s)
    {
        if (s < 10)
        {
            return "0" + s;
        }
        return s.ToString();
    }

   	public WebImage webImage;

	public Text tf;

	public RawImage bar;

	public static MissionPad THIS;

	private bool needUpdateTimer;

	private string macroText = "";

	private float endTime;

	private string progressText = "? / ?";
}
