using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class StringSelectorScript : MonoBehaviour
{
    public void SetStrings(string[] strings)
    {
        if (strings == null)
        {
            throw new Exception("StringSelectorScript no strings");
        }
        if (strings.Length == 0)
        {
            throw new Exception("StringSelectorScript strings len = 0");
        }
        this.labels = strings;
        if (this.inited)
        {
            this.UpdateLabels();
        }
    }

    private void Start()
    {
        this.inited = true;
        if (this.labels != null)
        {
            this.UpdateLabels();
        }
        this.LessButton.onClick.AddListener(new UnityAction(this.OnLess));
        this.MoreButton.onClick.AddListener(new UnityAction(this.OnMore));
    }

    private void OnLess()
    {
        if (this.labels != null)
        {
            this.current--;
            if (this.current < 0)
            {
                this.current = this.labels.Length - 1;
            }
            this.UpdateCurrentLabel();
            this.ChangeEvent.Invoke();
        }
    }

    private void OnMore()
    {
        if (this.labels != null)
        {
            this.current++;
            if (this.current > this.labels.Length - 1)
            {
                this.current = 0;
            }
            this.UpdateCurrentLabel();
            this.ChangeEvent.Invoke();
        }
    }

    private void UpdateLabels()
    {
        if (this.current > this.labels.Length)
        {
            this.current = 0;
        }
        this.UpdateCurrentLabel();
    }

    private void UpdateCurrentLabel()
    {
        base.gameObject.GetComponent<Text>().text = this.labels[this.current];
    }

    private void Update()
    {
    }

    public Button LessButton;

	public Button MoreButton;

	public UnityEvent ChangeEvent = new UnityEvent();

	public int current;

	private string[] labels;

	private bool inited;
}
