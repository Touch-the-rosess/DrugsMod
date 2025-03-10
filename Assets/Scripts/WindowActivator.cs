using System;
using UnityEngine;

public class WindowActivator : MonoBehaviour
{
    private void Start()
    {
        this.programmatorWindow.gameObject.SetActive(true);
        this.mapWindow.gameObject.SetActive(true);
        this.okWindow.gameObject.SetActive(true);
        this.aysWindow.gameObject.SetActive(true);
    }

    private void Update()
    {
    }
        
	public GameObject programmatorWindow;

	public GameObject mapWindow;

	public GameObject okWindow;

	public GameObject aysWindow;
}
