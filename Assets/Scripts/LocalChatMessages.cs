using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalChatMessages : MonoBehaviour
{
    private void Start()
    {
        LocalChatMessages.THIS = this;
    }

    public void AddLocalMessage(int bid, int x, int y, string str)
    {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.messagePrefab);
        gameObject.transform.SetParent(this.canvas.transform);
        gameObject.GetComponentInChildren<Text>().text = str;
        LocalMessage LocalMessage = default(LocalMessage);
        LocalMessage.go = gameObject;
        LocalMessage.x = (float)x;
        LocalMessage.y = (float)y;
        LocalMessage.timeExpired = Time.unscaledTime + 3f;
        if (RobotRenderer.THIS.bots.ContainsKey(bid))
        {
            LocalMessage.rt = RobotRenderer.THIS.bots[bid].transform;
            Vector3 position = Camera.main.WorldToScreenPoint(new Vector3(LocalMessage.rt.position.x, LocalMessage.rt.position.y));
            LocalMessage.go.transform.position = position;
        }
        else
        {
            LocalMessage.rt = null;
            Vector3 position2 = Camera.main.WorldToScreenPoint(new Vector3((float)x, (float)(-(float)y)));
            LocalMessage.go.transform.position = position2;
        }
        if (this.messages.ContainsKey(bid))
        {
            this.RemoveMessage(bid);
        }
        this.messages[bid] = LocalMessage;
    }

    private void RemoveMessage(int id)
    {
        UnityEngine.Object.Destroy(this.messages[id].go);
        this.messages.Remove(id);
    }

    private void Update()
    {
        List<int> list = new List<int>();
        foreach (KeyValuePair<int, LocalMessage> keyValuePair in this.messages)
        {
            if (keyValuePair.Value.rt != null)
            {
                Vector3 a = Camera.main.WorldToScreenPoint(new Vector3(keyValuePair.Value.rt.position.x - 0.5f, keyValuePair.Value.rt.position.y));
                Vector3 vector = keyValuePair.Value.go.transform.position;
                vector = 0.3f * a + 0.7f * vector;
                keyValuePair.Value.go.transform.position = vector;
            }
            else
            {
                Vector3 a2 = Camera.main.WorldToScreenPoint(new Vector3(keyValuePair.Value.x - 0.5f, -keyValuePair.Value.y));
                Vector3 vector2 = keyValuePair.Value.go.transform.position;
                vector2 = 0.3f * a2 + 0.7f * vector2;
                keyValuePair.Value.go.transform.position = vector2;
            }
            if (Time.unscaledTime > keyValuePair.Value.timeExpired)
            {
                list.Add(keyValuePair.Key);
            }
        }
        foreach (int id in list)
        {
            this.RemoveMessage(id);
        }
    }    

	public GameObject messagePrefab;

	public GameObject canvas;

	public static LocalChatMessages THIS;

	private Dictionary<int, LocalMessage> messages = new Dictionary<int, LocalMessage>();
}
