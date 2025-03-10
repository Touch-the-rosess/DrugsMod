using System;
using UnityEngine;
using UnityEngine.UI;

public class ProgAction : MonoBehaviour
{
    public static void InitSprites()
    {
        if (!ProgAction.inited)
        {
            ProgAction.sprites = Resources.LoadAll<Sprite>("programmator");
            ProgAction.inited = true;
        }
    }

    private void Start()
	{
		ProgAction.InitSprites();
	}

    public void ChangeTo(int _id)
	{
		ProgAction.InitSprites();
		this.id = _id;
		base.GetComponent<Image>().sprite = ProgAction.sprites[this.id];
		base.GetComponent<Image>().SetNativeSize();
		if (this.input != null)
		{
			this.updateInput();
			return;
		}
		this.inputInited = false;
	}

    private void updateInput()
    {
        this.input.gameObject.SetActive(false);
        this.numInput.gameObject.SetActive(false);
        if (this.id == 40)
        {
            this.input.gameObject.SetActive(true);
            this.input.gameObject.transform.localPosition = new Vector3(-5f, 0f);
        }
        else if (this.id == 140 || this.id == 139 || this.id == 166)
        {
            this.input.gameObject.SetActive(true);
            this.input.gameObject.transform.localPosition = new Vector3(-1f, -9f);
        }
        else if (this.id == 25 || this.id == 26 || this.id == 137)
        {
            this.input.gameObject.SetActive(true);
            this.input.gameObject.transform.localPosition = new Vector3(1f, 0f);
        }
        else if (this.id == 24)
        {
            this.input.gameObject.SetActive(true);
            this.input.gameObject.transform.localPosition = new Vector3(6f, 0f);
        }
        else if (this.id == 123 || this.id == 119 || this.id == 120)
        {
            this.input.gameObject.SetActive(true);
            this.input.gameObject.transform.localPosition = new Vector3(-3f, 4f);
            this.numInput.gameObject.SetActive(true);
            this.numInput.gameObject.transform.localPosition = new Vector3(-1f, -10f);
        }
        else if (this.id == 182 || this.id == 181)
        {
            this.input.gameObject.SetActive(true);
            this.input.gameObject.transform.localPosition = new Vector3(0f, -2f);
        }
        this.inputInited = true;
    }

    private void Update()
    {
        if (!this.inputInited)
        {
            this.updateInput();
        }
    }

    public string getString()
    {
        return this.input.text;
    }

    public void setString(string label)
    {
        this.input.text = label;
    }

    public int getNum()
    {
        int num;
        if (!int.TryParse(this.numInput.text, out num))
        {
            return 0;
        }
        return int.Parse(this.numInput.text);
    }

    public void setNum(int label)
    {
        this.numInput.text = label.ToString();
    }
    
	public static Sprite[] sprites;

	public static bool inited;

	public InputField numInput;

	public InputField input;

	public int id;

	public bool inputInited;
}
