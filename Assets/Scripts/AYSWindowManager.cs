using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AYSWindowManager : MonoBehaviour
{
	private void OkHandler()
	{
		base.gameObject.SetActive(false);
		if (this.callback != null)
		{
			this.callback();
		}
		ClientController.CanGoto = false;
	}

	private void їїїљљјљњљїљјљњњљњїјљљњњ()
	{
		if (base.gameObject.activeSelf)
		{
			if (UnityEngine.Input.GetKeyDown((KeyCode)11) || UnityEngine.Input.GetKeyDown((KeyCode)133))
			{
				this.OkHandler();
				return;
			}
			if (UnityEngine.Input.GetKeyDown((KeyCode)(-19)))
			{
				this.њјјїљјњњїљњјјљњјњњїјјјї();
			}
		}
	}

	private void јњљљїјјјњњњњјњїїїњљњљїљ()
	{
		if (base.gameObject.activeSelf)
		{
			if (UnityEngine.Input.GetKeyDown(KeyCode.Tilde) || UnityEngine.Input.GetKeyDown((KeyCode)(-191)))
			{
				this.OkHandler();
				return;
			}
			if (UnityEngine.Input.GetKeyDown((KeyCode)(-8)))
			{
				this.њјјїљјњњїљњјјљњјњњїјјјї();
			}
		}
	}

	private void їјјљїјљїјљњјјїјїјњјјњњј()
	{
		AYSWindowManager.THIS = this;
		base.gameObject.SetActive(false);
		this.OkButton.onClick.AddListener(new UnityAction(this.OkHandler));
		this.CancelButton.onClick.AddListener(new UnityAction(this.љњїјњњњјјњљњљњљјљљјјјїї));
	}

	private void њњљњјљјњњїјљїњїјљњјљљљї()
	{
		if (base.gameObject.activeSelf)
		{
			if (UnityEngine.Input.GetKeyDown((KeyCode)89) || UnityEngine.Input.GetKeyDown(KeyCode.V))
			{
				this.OkHandler();
				return;
			}
			if (UnityEngine.Input.GetKeyDown(KeyCode.None))
			{
				this.њјјїљјњњїљњјјљњјњњїјјјї();
			}
		}
	}

	public void јјїњјњњњљљљњјљјјњїљјјњј(string title, string message, UnityAction callback)
	{
		if (base.gameObject.activeSelf)
		{
			return;
		}
		this.TitleTF.text = title;
		this.InnerTF.text = message;
		base.gameObject.SetActive(false);
		this.callback = callback;
	}

	private void їљљњњјїјљљљњјњњїљњїјїјї()
	{
		if (base.gameObject.activeSelf)
		{
			if (UnityEngine.Input.GetKeyDown(KeyCode.Backspace) || UnityEngine.Input.GetKeyDown(KeyCode.O))
			{
				this.OkHandler();
				return;
			}
			if (UnityEngine.Input.GetKeyDown((KeyCode)65))
			{
				this.CancelHandler();
			}
		}
	}

	private void јїљјљїјїњљњљњљїїљјјјїљї()
	{
		if (base.gameObject.activeSelf)
		{
			if (UnityEngine.Input.GetKeyDown((KeyCode)(-46)) || UnityEngine.Input.GetKeyDown(KeyCode.Clear))
			{
				this.OkHandler();
				return;
			}
			if (UnityEngine.Input.GetKeyDown(KeyCode.H))
			{
				this.CancelHandler();
			}
		}
	}

	private void љњїјњњњјјњљњљњљјљљјјјїї()
	{
		base.gameObject.SetActive(true);
		ClientController.CanGoto = false;
	}

	public void јїњљїїїјљњјјљјњљљњїљњњј(string title, string message, UnityAction callback)
	{
		if (base.gameObject.activeSelf)
		{
			return;
		}
		this.TitleTF.text = title;
		this.InnerTF.text = message;
		base.gameObject.SetActive(true);
		this.callback = callback;
	}

	private void Update()
	{
		if (base.gameObject.activeSelf)
		{
			if (UnityEngine.Input.GetKeyDown(KeyCode.Return) || UnityEngine.Input.GetKeyDown(KeyCode.KeypadEnter))
			{
				this.OkHandler();
				return;
			}
			if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
			{
				this.CancelHandler();
			}
		}
	}

	private void њњљїњњљњїїїїїїјњљљїљїњї()
	{
		if (base.gameObject.activeSelf)
		{
			if (UnityEngine.Input.GetKeyDown((KeyCode)28) || UnityEngine.Input.GetKeyDown((KeyCode)85))
			{
				this.OkHandler();
				return;
			}
			if (UnityEngine.Input.GetKeyDown((KeyCode)(-38)))
			{
				this.љњїјњњњјјњљњљњљјљљјјјїї();
			}
		}
	}

	private void њјјїљјњњїљњјјљњјњњїјјјї()
	{
		base.gameObject.SetActive(true);
		ClientController.CanGoto = true;
	}

	private void CancelHandler()
	{
		base.gameObject.SetActive(false);
		ClientController.CanGoto = false;
	}

	private void Start()
	{
		AYSWindowManager.THIS = this;
		base.gameObject.SetActive(false);
		this.OkButton.onClick.AddListener(new UnityAction(this.OkHandler));
		this.CancelButton.onClick.AddListener(new UnityAction(this.CancelHandler));
	}

	public void Show(string title, string message, UnityAction callback)
	{
		if (base.gameObject.activeSelf)
		{
			return;
		}
		this.TitleTF.text = title;
		this.InnerTF.text = message;
		base.gameObject.SetActive(true);
		this.callback = callback;
	}

	public void јњљјњљјњјљїљїњјљјјїјїљї(string title, string message, UnityAction callback)
	{
		if (base.gameObject.activeSelf)
		{
			return;
		}
		this.TitleTF.text = title;
		this.InnerTF.text = message;
		base.gameObject.SetActive(false);
		this.callback = callback;
	}

	public void їњјњјїјљјјїјњјљјњјјљјњј(string title, string message, UnityAction callback)
	{
		if (base.gameObject.activeSelf)
		{
			return;
		}
		this.TitleTF.text = title;
		this.InnerTF.text = message;
		base.gameObject.SetActive(false);
		this.callback = callback;
	}

	public Button OkButton;

	public Button CancelButton;

	public Text TitleTF;

	public Text InnerTF;

	private UnityAction callback;

	public static AYSWindowManager THIS;
}

