using System;
using UnityEngine;
using UnityEngine.UI;

public class FPSCountScript : MonoBehaviour
{
    private void Update()
    {
        this.deltaTime += (Time.deltaTime - this.deltaTime) * 0.1f;
        float num = 1f / this.deltaTime;
        this.fpsText.text = "FPS " + Mathf.Ceil(num).ToString() + FPSCountScript.PING_MESSAGE;
    }

    public Text fpsText;

	public static string txt;

	public float deltaTime;

	public static string PING_MESSAGE = "";

	public static float f = 0f;
}
