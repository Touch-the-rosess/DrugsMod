using System;
using System.Collections.Generic;
using UnityEngine;

public class StatePanel : MonoBehaviour
{
    public void RemoveAll()
    {
        while (this.lines.Count > 0)
        {
            using (Dictionary<string, StateLineScript>.Enumerator enumerator = this.lines.GetEnumerator())
            {
                if (enumerator.MoveNext())
                {
                    KeyValuePair<string, StateLineScript> keyValuePair = enumerator.Current;
                    this.RemoveLine(keyValuePair.Key);
                }
            }
        }
    }

    public void RemoveLine(string tag)
    {
        if (this.lines.ContainsKey(tag))
        {
            UnityEngine.Object gameObject = this.lines[tag].gameObject;
            this.lines.Remove(tag);
            UnityEngine.Object.Destroy(gameObject);
        }
    }

    public void AddLine(string tag, string[] text, bool blinking, Color color)
    {
        if (this.lines.ContainsKey(tag))
        {
            this.lines[tag].SetLine(text, blinking, color);
            return;
        }
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.stateLinePrefab);
        gameObject.GetComponent<StateLineScript>().SetLine(text, blinking, color);
        this.lines.Add(tag, gameObject.GetComponent<StateLineScript>());
        if (this.inited)
        {
            gameObject.transform.SetParent(base.gameObject.transform, false);
        }
    }

    private void Start()
    {
        this.UpdateListFull();
        this.inited = true;
    }

    private void UpdateListFull()
    {
        foreach (object obj in base.gameObject.transform)
        {
            UnityEngine.Object.Destroy(((Transform)obj).gameObject);
        }
        foreach (KeyValuePair<string, StateLineScript> keyValuePair in this.lines)
        {
            keyValuePair.Value.gameObject.transform.SetParent(base.gameObject.transform, false);
        }
    }

    private void Update()
    {
    }
    
	public GameObject stateLinePrefab;

	private Dictionary<string, StateLineScript> lines = new Dictionary<string, StateLineScript>();

	private bool inited;
}
