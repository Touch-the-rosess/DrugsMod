using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ProgPanel : MonoBehaviour
{
    private void Start()
    {
        ProgPanel.THIS = this;
        this.handModeImage.gameObject.SetActive(false);
        //this.playStopButton.onClick.AddListener(new UnityAction(this.onPlayStopBut));
        playStopButton.onClick.AddListener(delegate
        {
            ClientController.CanGoto = false;
            OnPlayStop();
        });
    }

    private void onPlayStopBut()
    {
        ClientController.CanGoto = false;
        this.OnPlayStop();
    }

    public void OnPlayStop()
    {
        if ((ProgPanel.playing) || (ProgPanel.handMode))
        {
            GUIManager.THIS.OnProgCloseButton();
            return;
        }
        else if (GUIManager.programToSend.StartsWith("@"))
        {
            
            ServerTime.THIS.SendTypicalMessage(-1, "Pope", 0, 0, "@" + GUIManager.programToSend);
            Debug.Log("START!!!");
            return;
        }
        else {ProgrammatorView.THIS.SendAndStartProgram(); Debug.Log("START!!!");}
    }

    private void Update()
    {
        this.handModeImage.gameObject.SetActive(ProgPanel.handMode);
        if ((ProgPanel.playing) || (ProgPanel.handMode))
        {
            this.frame++;
            this.progImage.sprite = this.progs[this.frame / 5 % this.progs.Length];
            this.playStopImage.sprite = this.stop;
            return;
        }
        else {
            this.progImage.sprite = this.progStable;
            this.playStopImage.sprite = this.play;
        }
        
    }
    
	public static bool playing;

	public static bool handMode;

	public Sprite stop;

	public Sprite play;

	public Sprite progStable;

	public Button playStopButton;

	public Sprite[] progs;

	public Image progImage;

	public Image playStopImage;

	public Image handModeImage;

	public static ProgPanel THIS;

	private int frame;
}
