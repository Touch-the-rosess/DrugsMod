using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class OKWindowManager : MonoBehaviour
{
    private void Start()
    {
        OKWindowManager.THIS = this;
        base.gameObject.SetActive(false);
        this.ExitButton.onClick.AddListener(new UnityAction(this.ExitHandler));
    }

    private void ExitHandler()
    {
        base.gameObject.SetActive(false);
        this.CheckQueue();
        ClientController.CanGoto = false;
    }

    public void AddMessage(OKMessage msg)
    {
        this.msgQueue.Enqueue(msg);
        this.CheckQueue();
    }

    public void CheckQueue()
    {
        if (this.msgQueue.Count > 0 && !base.gameObject.activeSelf)
        {
            OKMessage OKMessage = this.msgQueue.Dequeue();
            this.TitleTF.text = OKMessage.title;
            this.InnerTF.text = OKMessage.message;
            base.gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        if (base.gameObject.activeSelf && !AYSWindowManager.THIS.gameObject.activeSelf && (UnityEngine.Input.GetKeyDown(KeyCode.Return) || UnityEngine.Input.GetKeyDown(KeyCode.KeypadEnter)))
        {
            this.ExitHandler();
        }
    }
    
	public Button ExitButton;

	public Text TitleTF;

	public Text InnerTF;

	private Queue<OKMessage> msgQueue = new Queue<OKMessage>();

	public static OKWindowManager THIS;
}
