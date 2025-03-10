using System;
using UnityEngine;
using UnityEngine.UI;

public class FuelLineScript : MonoBehaviour
{
    public void Setup(int percentage, string left, string right, int crys_type, bool isFirstButton, bool isSecondButton, bool isThirdButton)
    {
        this.percentage = percentage;
        this.left = left;
        this.right = right;
        this.crys_type = crys_type;
        this.isFirstButton = isFirstButton;
        this.isSecondButton = isSecondButton;
        this.isThirdButton = isThirdButton;
        if (this.inited)
        {
            this.UpdateView();
        }
        this.isSetup = true;
    }

    private void Start()
    {
        this.inited = true;
        if (this.isSetup)
        {
            this.UpdateView();
        }
    }

    private void UpdateView()
    {
        this.leftText.color = FuelLineScript.crysColors[this.crys_type];
        this.allText.color = FuelLineScript.crysColors[this.crys_type];
        this.leftText.text = this.left;
        this.allText.text = this.right;
        this.crysImage.sprite = this.crys_sprites[this.crys_type];
        base.gameObject.GetComponentsInChildren<Button>()[0].interactable = this.isFirstButton;
        base.gameObject.GetComponentsInChildren<Button>()[1].interactable = this.isSecondButton;
        base.gameObject.GetComponentsInChildren<Button>()[2].interactable = this.isThirdButton;
        this.lineImage.rectTransform.sizeDelta = new Vector2((float)(1 + 87 * this.percentage / 100), 6f);
        this.lineImage.color = FuelLineScript.crysColors[this.crys_type];
    }

    private void Update()
    {
    }
        
	public Image lineImage;

	public Image crysImage;

	public Text leftText;

	public Text allText;

	public Sprite[] crys_sprites;

	private int percentage;

	private string left;

	private string right;

	private int crys_type;

	private bool isFirstButton;

	private bool isSecondButton;

	private bool isThirdButton;

	private bool isSetup;

	private bool inited;

	public static Color[] crysColors = new Color[]
	{
		new Color(0f, 1f, 0f),
		new Color(0.4f, 0.4f, 1f),
		new Color(1f, 0.3f, 0.3f),
		new Color(1f, 0f, 1f),
		new Color(1f, 1f, 1f),
		new Color(0f, 1f, 1f)
	};
}
