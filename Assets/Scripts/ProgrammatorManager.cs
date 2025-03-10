using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ProgrammatorManager : MonoBehaviour
{
    private void Start()
    {
        this.StartButton.onClick.AddListener(new UnityAction(this.OnStartButton));
        this.ExitButton.onClick.AddListener(new UnityAction(this.OnExitButton));
        this.ClearButton.onClick.AddListener(new UnityAction(this.OnClearButton));
        this.MenuButton.onClick.AddListener(new UnityAction(this.OnMenuButton));
        this.CopyButton.onClick.AddListener(new UnityAction(this.OnCopyButton));
        this.RenameButton.onClick.AddListener(new UnityAction(this.OnRenameButton));
        this.OpenHelpButton.onClick.AddListener(new UnityAction(this.OnOpenHelp));
        this.ExitHelpButton.onClick.AddListener(new UnityAction(this.OnExitHelp));
        this.OpenWikiButton.onClick.AddListener(delegate ()
        {
            string text = "http://minesgame.ru/wiki";
            ServerController.THIS.OpenURLHandler(ref text);
        });
        base.Invoke("OnExitButton", 0.01f);
    }

    private void OnOpenHelp()
    {
        this.HelpPanel.SetActive(!this.HelpPanel.activeSelf);
    }

    private void OnExitHelp()
    {
        this.HelpPanel.SetActive(false);
    }

    private void OnClearButton()
    {
        ClientController.CanGoto = false;
        AYSWindowManager.THIS.Show("УДАЛЕНИЕ ПРОГРАММЫ", "<color=#ff8888ff>Программа будет удалена навсегда.</color>\n\nУдалить программу?", new UnityAction(this.ClearButtonEnable));
    }

    private void ClearButtonEnable()
    {
        base.gameObject.SetActive(false);
        ProgrammatorView.active = false;
        ServerTime.THIS.SendTypicalMessage(-1, "PDEL", 0, 0, ProgrammatorView.programId.ToString());
    }

    private void OnStartButton()
    {
        ClientController.CanGoto = false;
        base.gameObject.SetActive(false);
        ProgrammatorView.active = false;
        ProgrammatorView.unsaved = false;
        ProgrammatorView.THIS.titleTF.text = ProgrammatorView.title;
        ProgrammatorView.THIS.SendAndStartProgram();
    }

    private void OnRenameButton()
    {
        ClientController.CanGoto = false;
        base.gameObject.SetActive(false);
        ProgrammatorView.active = false;
        ServerTime.THIS.SendTypicalMessage(-1, "PREN", 0, 0, ProgrammatorView.programId.ToString());
    }

    private void OnMenuButton()
    {
        ClientController.CanGoto = false;
        if (ProgrammatorView.unsaved)
        {
            AYSWindowManager.THIS.Show("НЕСОХРАНЕННЫЕ ИЗМЕНЕНИЯ", "Вы собираетесь выйти в меню.\nНесохраненные изменения потеряются.\nЧтобы сохранить программу, запустите ее.\n\nВыйти и потерять изменения?", delegate
            {
                ExitToMenu();
                ProgrammatorView.unsaved = false;
            });
        }
        this.ExitToMenu();
    }

    private void ExitToMenu()
    {
        ClientController.CanGoto = false;
        base.gameObject.SetActive(false);
        ProgrammatorView.active = false;
        ProgrammatorView.opened = false;
        ServerTime.THIS.SendTypicalMessage(-1, "Pope", 0, 0, "=");
    }

    private void OnCopyButton()
    {
        ClientController.CanGoto = false;
        string message = "Вы собираетесь создать копию программы\nОна появится в общем списке программ\n\nСоздать копию?";
        if (ProgrammatorView.unsaved)
        {
            message = "Вы собираетесь создать копию программы\nОна появится в общем списке программ\n\n<color=#ff8888ff>ПРОГРАММА НЕ СОХРАНЕНА\nИЗМЕНЕНИЯ ПОТЕРЯЮТСЯ</color>\n\nСоздать копию?";
        }
        AYSWindowManager.THIS.Show("СОЗДАНИЕ КОПИИ ПРОГРАММЫ", message, new UnityAction(this.OnCopyProgramm));
    }

    private void OnCopyProgramm()
    {
        base.gameObject.SetActive(false);
        ProgrammatorView.active = false;
        ServerTime.THIS.SendTypicalMessage(-1, "PCOP", 0, 0, ProgrammatorView.programId.ToString());
    }

    private void OnExitButton()
    {
        ClientController.CanGoto = false;
        base.gameObject.SetActive(false);
        ProgrammatorView.active = false;
    }

    public Button StartButton;

	public Button ExitButton;

	public Button ClearButton;

	public Button MenuButton;

	public Button CopyButton;

	public Button RenameButton;

	public Button OpenHelpButton;

	public GameObject HelpPanel;

	public Button OpenWikiButton;

	public Button ExitHelpButton;
}

