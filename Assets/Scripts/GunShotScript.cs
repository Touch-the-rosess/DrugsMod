using System;
using UnityEngine;

public class GunShotScript : MonoBehaviour
{
    public void Setup(int gunx, int guny, int bid, int col, int _index)
    {
        this.gunPos = new Vector2((float)gunx, (float)(-(float)guny));
        this.index = _index;
        this.startTime = Time.unscaledTime;
        this.botId = bid;
        this.col = col;
        if (RobotRenderer.THIS.bots.ContainsKey(this.botId))
        {
            this.botPos = RobotRenderer.THIS.bots[this.botId].transform.position;
        }
        else
        {
            this.botId = -1;
        }
        if (this._tailx != null)
        {
            for (int i = 1; i < this.NUM_SECTORS; i++)
            {
                float d = (float)i / (float)this.NUM_SECTORS;
                this._tailx[i] = d * (this.botPos - this.gunPos);
            }
        }
    }

    private void Start()
    {
        this.startTime = Time.unscaledTime;
        MeshFilter component = base.gameObject.GetComponent<MeshFilter>();
        this.mesh = new Mesh();
        this.mesh.MarkDynamic();
        component.mesh = this.mesh;
        this._triangles = new int[6 * this.NUM_SECTORS];
        this._vertices = new Vector3[4 * this.NUM_SECTORS];
        this._uvs = new Vector2[4 * this.NUM_SECTORS];
        this._colors = new Color[4 * this.NUM_SECTORS];
        this._tailx = new Vector2[this.NUM_SECTORS + 1];
        for (int i = 0; i < this.NUM_SECTORS; i++)
        {
            this._triangles[6 * i] = 4 * i;
            this._triangles[6 * i + 1] = 4 * i + 1;
            this._triangles[6 * i + 2] = 4 * i + 2;
            this._triangles[6 * i + 3] = 4 * i + 1;
            this._triangles[6 * i + 4] = 4 * i + 3;
            this._triangles[6 * i + 5] = 4 * i + 2;
            this._uvs[4 * i] = new Vector2(0f, 0f);
            this._uvs[4 * i + 1] = new Vector2(0f, 1f);
            this._uvs[4 * i + 2] = new Vector2(1f, 0f);
            this._uvs[4 * i + 3] = new Vector2(1f, 1f);
        }
        this.mesh.vertices = this._vertices;
        this.mesh.colors = this._colors;
        this.mesh.triangles = this._triangles;
        this.mesh.uv = this._uvs;
        this._tailx[0] = this.gunPos - this.gunPos;
        this._tailx[this.NUM_SECTORS] = this.botPos - this.gunPos;
        for (int j = 1; j < this.NUM_SECTORS; j++)
        {
            float d = (float)j / (float)this.NUM_SECTORS;
            this._tailx[j] = d * (this.botPos - this.gunPos);
        }
    }

    private void Update()
    {
        if (RobotRenderer.THIS.bots.ContainsKey(this.botId))
        {
            this.botPos = RobotRenderer.THIS.bots[this.botId].transform.position;
        }
        this.UpdateMesh();
        if (Time.unscaledTime - this.startTime > 0.7f)
        {
            ClientController.THIS.gunShotPool.Free(this.index);
        }
    }
        
	private void UpdateMesh()
	{
		base.transform.position = new Vector3(this.gunPos.x, this.gunPos.y, -8f);
		this._tailx[0] = new Vector2(0.5f, -0.5f);
		this._tailx[this.NUM_SECTORS] = this.botPos - this.gunPos;
		for (int i = 1; i < this.NUM_SECTORS; i++)
		{
			float d = (float)i / (float)this.NUM_SECTORS;
			Vector2[] array = this._tailx;
			int num = i;
			array[num].x = array[num].x + (UnityEngine.Random.value - 0.5f);
			Vector2[] array2 = this._tailx;
			int num2 = i;
			array2[num2].y = array2[num2].y + (UnityEngine.Random.value - 0.5f);
			this._tailx[i] = 0.9f * this._tailx[i] + 0.1f * (d * (this.botPos - this.gunPos));
		}
		float d2 = 0.5f * Mathf.Sin(7f * (Time.unscaledTime - this.startTime));
		for (int j = 0; j < this.NUM_SECTORS; j++)
		{
			Vector2 vector2;
			Vector2 vector = vector2 = d2 * (this._tailx[j + 1] - this._tailx[j]).normalized;
			if (j > 0)
			{
				vector = d2 * (this._tailx[j + 1] - this._tailx[j - 1]).normalized;
			}
			if (j < this.NUM_SECTORS - 1)
			{
				vector2 = d2 * (this._tailx[j + 2] - this._tailx[j]).normalized;
			}
			this._vertices[4 * j] = new Vector3(this._tailx[j].x + vector.y, this._tailx[j].y - vector.x, -1f);
			this._vertices[4 * j + 1] = new Vector3(this._tailx[j].x - vector.y, this._tailx[j].y + vector.x, -1f);
			this._vertices[4 * j + 2] = new Vector3(this._tailx[j + 1].x + vector2.y, this._tailx[j + 1].y - vector2.x, -1f);
			this._vertices[4 * j + 3] = new Vector3(this._tailx[j + 1].x - vector2.y, this._tailx[j + 1].y + vector2.x, -1f);
			if (this.col == 1)
			{
				this._colors[4 * j] = new Color(1f, 0f, 0f, 0f);
				this._colors[4 * j + 1] = new Color(1f, 0f, 0f, 0f);
				this._colors[4 * j + 2] = new Color(1f, 0f, 0f, 0f);
				this._colors[4 * j + 3] = new Color(1f, 0f, 0f, 0f);
			}
			else
			{
				this._colors[4 * j] = new Color(0f, 1f, 1f, 1f);
				this._colors[4 * j + 1] = new Color(0f, 1f, 1f, 1f);
				this._colors[4 * j + 2] = new Color(0f, 1f, 1f, 1f);
				this._colors[4 * j + 3] = new Color(0f, 1f, 1f, 1f);
			}
			float num3 = UnityEngine.Random.value * 0.9f;
			this._uvs[4 * j] = new Vector2(num3, 0f);
			this._uvs[4 * j + 1] = new Vector2(num3, 1f);
			this._uvs[4 * j + 2] = new Vector2(num3 + 0.1f, 0f);
			this._uvs[4 * j + 3] = new Vector2(num3 + 0.1f, 1f);
		}
		if (this.botId != -1)
		{
			this.boundsCenter = new Vector3(0f, 0f, 0f);
			this.boundsSize = new Vector3(44f, 44f, 44f);
			this.mesh.bounds = new Bounds(this.boundsCenter, this.boundsSize);
			this.mesh.vertices = this._vertices;
			this.mesh.colors = this._colors;
			this.mesh.uv = this._uvs;
		}
	}

	private Mesh mesh;

	private Vector3[] _vertices;

	private Vector2[] _uvs;

	private Color[] _colors;

	private int[] _triangles;

	private Vector2[] _tailx;

	private Vector2 gunPos;

	private Vector2 botPos;

	private int botId;

	private Vector3 boundsCenter;

	private Vector3 boundsSize;

	private int col;

	private int index;

	private int NUM_SECTORS = 7;

	private float startTime;
}
