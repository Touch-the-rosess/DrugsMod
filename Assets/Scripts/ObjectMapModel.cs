using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMapModel
{
    public int width
    {
        get
        {
            return this._width;
        }
    }

    public int height
    {
        get
        {
            return this._height;
        }
    }

    public ObjectMapModel(int width, int height)
    {
        this._width = width;
        this._height = height;
        this._blocksW = Mathf.FloorToInt((float)(width / 32));
        this._objBlocks = new Dictionary<int, ObjectModel>[Mathf.FloorToInt((float)(width / 32)) * Mathf.FloorToInt((float)(height / 32))];
    }

    public Dictionary<int, ObjectModel> GetObjects(int x, int y)
    {
        if (y < 0)
        {
            return null;
        }
        if (x < 0)
        {
            return null;
        }
        if (x >= this._width)
        {
            return null;
        }
        if (y >= this._height)
        {
            return null;
        }
        int num = (x >> 5) + (y >> 5) * this._blocksW;
        if (this._objBlocks[num] == null)
        {
            return null;
        }
        return this._objBlocks[num];
    }

    public int IdFromXY(int x, int y)
    {
        return x + y * this._width;
    }

    public void SetObject(ObjectModel obj)
    {
        int x = obj.x;
        int y = obj.y;
        if (x < 0)
        {
            return;
        }
        if (x >= this._width)
        {
            return;
        }
        if (y < 0)
        {
            return;
        }
        if (y >= this._height)
        {
            return;
        }
        int num = (x >> 5) + (y >> 5) * this._blocksW;
        if (this._objBlocks[num] == null)
        {
            this._objBlocks[num] = new Dictionary<int, ObjectModel>();
        }
        this._objBlocks[num][this.IdFromXY(x, y)] = obj;
    }
    
	private int _width;

	private int _height;

	private Dictionary<int, ObjectModel>[] _objBlocks;

	private int _blocksW;
}
