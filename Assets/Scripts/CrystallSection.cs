using System;
using UnityEngine;
using UnityEngine.UI;

public class CrystallSection : MonoBehaviour
{
    private void Start()
    {
    }

    private void Update()
    {
    }

    public string GetValuesInString()
    {
        return this.lines[0].GetComponent<CrystalScroller>().value + ":" + this.lines[1].GetComponent<CrystalScroller>().value + ":" + this.lines[2].GetComponent<CrystalScroller>().value + ":" + this.lines[3].GetComponent<CrystalScroller>().value + ":" + this.lines[4].GetComponent<CrystalScroller>().value + ":" + this.lines[5].GetComponent<CrystalScroller>().value;
    }

    public GameObject[] lines;

	public Text leftDesc;

	public Text rightDesc;
}
