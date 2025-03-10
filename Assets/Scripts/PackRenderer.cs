using System;
using System.Collections.Generic;
using UnityEngine;

public class PackRenderer : MonoBehaviour
{
    public bool IsPackOn(int x, int y)
    {
        if (ClientController.map == null)
        {
            return false;
        }
        int key = (x >> 5) + (y >> 5) * (ClientController.map.width >> 5);
        if (this.objectsInBlock.ContainsKey(key))
        {
            foreach (GameObject gameObject in this.objectsInBlock[key])
            {
                ObjectModel obj = gameObject.GetComponent<PackSpriteScript>()._Obj;
                if (obj.x == x && obj.y == y)
                {
                    return true;
                }
            }
            return false;
        }
        return false;
    }

    public void RemoveObjectInBlock(int blockId)
    {
        if (this.objectsInBlock.ContainsKey(blockId))
        {
            foreach (GameObject obj in this.objectsInBlock[blockId])
            {
                UnityEngine.Object.Destroy(obj);
            }
            this.objectsInBlock[blockId].Clear();
            return;
        }
        this.objectsInBlock[blockId] = new List<GameObject>();
    }

    public void AddObject(ObjectModel obj, int blockId)
    {
        if (!this.objectsInBlock.ContainsKey(blockId))
        {
            this.objectsInBlock[blockId] = new List<GameObject>();
        }
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.packSpritePrefab);
        gameObject.transform.SetParent(this.RenderWrapper.transform, false);
        Vector3 position = gameObject.transform.position;
        position.x = (float)obj.x;
        position.y = (float)(-(float)obj.y);
        position.z = -6f;
        gameObject.transform.position = position;
        this.objectsInBlock[blockId].Add(gameObject);
        gameObject.GetComponent<PackSpriteScript>().SetObj(obj);
    }

    private void ObjectsGarbageCollector()
    {
        new List<int>();
        foreach (KeyValuePair<int, List<GameObject>> keyValuePair in this.objectsInBlock)
        {
            int key = keyValuePair.Key;
            int num = TerrainRendererScript.map.width / 32;
            int num2 = TerrainRendererScript.map.height / 32;
            int num3 = 32 * (key % num);
            int num4 = 32 * Mathf.FloorToInt((float)(key / num));
            float gx = (float)ClientController.THIS.myBot.gx;
            int gy = ClientController.THIS.myBot.gy;
            float num5 = gx - (float)num3;
            float num6 = (float)(gy - num4);
            if (num5 * num5 + num6 * num6 > 40000f)
            {
                this.RemoveObjectInBlock(key);
            }
        }
    }

    private void Start()
    {
        PackRenderer.THIS = this;
    }

    private void Update()
    {
        if (Time.unscaledTime > this.lastObjectGCTime + 10f)
        {
            this.ObjectsGarbageCollector();
            this.lastObjectGCTime = Time.unscaledTime;
        }
    }
    
	public static ObjectMapModel objects;

	public GameObject packSpritePrefab;

	private float lastObjectGCTime;

	private Dictionary<int, List<GameObject>> objectsInBlock = new Dictionary<int, List<GameObject>>();

	public static PackRenderer THIS;

	public GameObject RenderWrapper;
}

