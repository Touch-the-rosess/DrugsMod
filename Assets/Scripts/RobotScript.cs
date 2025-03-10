using System;
using UnityEngine;

public class RobotScript : MonoBehaviour
{
    public int gx
    {
        get
        {
            return this._gx;
        }
    }

    public int gy
    {
        get
        {
            return this._gy;
        }
    }

    private void Start()
    {
        if (!RobotScript.inited)
        {
            RobotScript.sprites = Resources.LoadAll<Sprite>("skins");
            RobotScript.inited = true;
        }
        this.deathPingTime = -1f;
        this.body.GetComponent<SpriteRenderer>().sprite = RobotScript.sprites[0];
        MeshFilter component = this.tail.GetComponent<MeshFilter>();
        this.mesh = new Mesh();
        this.mesh.MarkDynamic();
        component.mesh = this.mesh;
        this.boundsSize.x = 90f;
        this.boundsSize.y = 90f;
        this.boundsSize.z = 90f;
        this._triangles = new int[3 * this.NUM_TAILS * this.NUM_SECTORS + 3 * this.NUM_TAILS];
        this._vertices = new Vector3[4 * this.NUM_TAILS * this.NUM_SECTORS];
        this._uvs = new Vector2[4 * this.NUM_TAILS * this.NUM_SECTORS];
        this._colors = new Color[4 * this.NUM_TAILS * this.NUM_SECTORS];
        this._tailx = new Vector2[this.NUM_TAILS * this.NUM_SECTORS];
        this._tailxs = new Vector2[this.NUM_TAILS * this.NUM_SECTORS];
        for (int i = 0; i < this.NUM_TAILS; i++)
        {
            for (int j = 0; j < this.NUM_SECTORS; j++)
            {
                this._triangles[3 * (j + i * this.NUM_SECTORS)] = 4 * (j + i * this.NUM_SECTORS);
                this._triangles[3 * (j + i * this.NUM_SECTORS) + 1] = 4 * (j + i * this.NUM_SECTORS) + 1;
                this._triangles[3 * (j + i * this.NUM_SECTORS) + 2] = 4 * (j + i * this.NUM_SECTORS) + 2;
                if (j == 0)
                {
                    this._triangles[3 * this.NUM_SECTORS * this.NUM_TAILS + 3 * i] = 4 * (j + i * this.NUM_SECTORS) + 1;
                    this._triangles[3 * this.NUM_SECTORS * this.NUM_TAILS + 1 + 3 * i] = 4 * (j + i * this.NUM_SECTORS) + 3;
                    this._triangles[3 * this.NUM_SECTORS * this.NUM_TAILS + 2 + 3 * i] = 4 * (j + i * this.NUM_SECTORS) + 2;
                }
                this._uvs[4 * (j + i * this.NUM_SECTORS)] = new Vector2(0f, 0f);
                this._uvs[4 * (j + i * this.NUM_SECTORS) + 1] = new Vector2(1f, 0f);
                this._uvs[4 * (j + i * this.NUM_SECTORS) + 2] = new Vector2(0f, 1f);
                this._uvs[4 * (j + i * this.NUM_SECTORS) + 3] = new Vector2(1f, 1f);
            }
        }
        this.mesh.vertices = this._vertices;
        this.mesh.colors = this._colors;
        this.mesh.triangles = this._triangles;
        this.mesh.uv = this._uvs;
        this.HideTail();
        this.lastPingTime = Time.unscaledTime;
    }

    public void SetClan(int _cid)
    {
        this.clan.changeClanToId(_cid);
    }

    public void SetSkin(int _skin)
    {
        this.skin = _skin;
        if (this._tailx != null && this.skin == 1)
        {
            this.HideTail();
        }
    }

    public void HideTail()
    {
        if (this._tailx != null)
        {
            for (int i = 0; i < this.NUM_TAILS; i++)
            {
                for (int j = 0; j < this.NUM_SECTORS; j++)
                {
                    this._tailx[j + i * this.NUM_SECTORS] = base.gameObject.transform.position;
                    this._tailxs[j + i * this.NUM_SECTORS] = base.gameObject.transform.position;
                }
            }
        }
    }

    private void Update()
    {
        this.PositionUpdate();
        this.BodyUpdate();
        if (RobotRenderer.THIS.bots.Count > 60 || this.skin == 1)
        {
            this.tail.SetActive(false);
            return;
        }
        this.tail.SetActive(true);
        this.TailUpdate();
    }

    private void PositionUpdate()
    {
        Vector3 position = base.gameObject.transform.position;
        this.renderDistance = Vector2.Distance(position, this.gamePosition);
        float num = 0.9f - this.renderDistance * 0.01f;
        if (num < 0.5f)
        {
            num = 0.5f;
        }
        if (this.renderDistance > 28f)
        {
            this.SyncXY();
            this.HideTail();
        }
        float num2 = 1f - num;
        position.x = num * position.x + num2 * this.gamePosition.x;
        position.y = num * position.y + num2 * this.gamePosition.y;
        position.z = -this.layerZ;
        if (this.tremor > 0.01f)
        {
            this.tremor *= 0.8f;
            position.x += this.tremor * (UnityEngine.Random.value - 0.5f);
            position.y += this.tremor * (UnityEngine.Random.value - 0.5f);
        }
        base.gameObject.transform.position = position;
        Quaternion rotation = this.body.transform.rotation;
        Vector3 eulerAngles = rotation.eulerAngles;
        this.nowRotationAngle = eulerAngles.z;
        if (this.nowRotationAngle - this.rotationAngle > 180f)
        {
            this.rotationAngle += 360f;
        }
        if (this.nowRotationAngle - this.rotationAngle < -180f)
        {
            this.rotationAngle -= 360f;
        }
        float num3 = Vector2.Distance(position, this.gamePosition);
        float num4 = 12f * Time.unscaledDeltaTime;
        this.nowRotationAngle = (1f - num4) * this.nowRotationAngle + num4 * this.rotationAngle;
        if (this.skin != 1)
        {
            this.nowRotationAngle += 6.6f * num3 * (0.5f - UnityEngine.Random.value);
        }
        eulerAngles.z = this.nowRotationAngle;
        rotation.eulerAngles = eulerAngles;
        this.body.transform.rotation = rotation;
    }

    public void SyncXY()
    {
        Vector3 position = base.gameObject.transform.position;
        position.x = this.gamePosition.x;
        position.y = this.gamePosition.y;
        base.gameObject.transform.position = position;
    }

    public void SetRotation(int dir)
    {
        this.dir = dir;
        this.SetRotationDegrees((float)(-90 * dir + 180));
    }

    public void SetRotationDegrees(float degrees)
    {
        this.rotationAngle = degrees;
    }

    public void SetXY(float x, float y)
    {
        this.gamePosition = new Vector2(x + 0.5f, -y - 0.5f);
        this._gx = (int)x;
        this._gy = (int)y;
    }

    private void BodyUpdate()
    {
        this.body.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        int num = this.skin;
        if (num != 1)
        {
            if (num == 2)
            {
                this.body.GetComponent<SpriteRenderer>().sprite = RobotScript.sprites[3];
                this.layerZ = 2f;
                return;
            }
            this.body.GetComponent<SpriteRenderer>().sprite = RobotScript.sprites[this.skin];
            this.layerZ = 2f;
        }
        else
        {
            this.layerZ = 1f;
            this.body.GetComponent<SpriteRenderer>().sprite = RobotScript.sprites[1];
            if (UnityEngine.Random.value < 0.01f)
            {
                this.body.GetComponent<SpriteRenderer>().sprite = RobotScript.sprites[2];
            }
            if (!CellModel.isEmpty[TerrainRendererScript.map.GetCell(this.gx, this.gy)])
            {
                this.body.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
                return;
            }
        }
    }

    private void UpdateTailVertices(int t, int s, Vector2 from, Vector2 to)
    {
        Vector2 vector = to - from;
        Vector2 vector2 = new Vector2(-vector.y, vector.x);
        vector2.Normalize();
        vector2 /= 16f;
        this._vertices[4 * (s + t * this.NUM_SECTORS)] = from + vector2 - this.gamePosCache;
        this._vertices[4 * (s + t * this.NUM_SECTORS) + 1] = to + vector2 - this.gamePosCache;
        this._vertices[4 * (s + t * this.NUM_SECTORS) + 2] = from - vector2 - this.gamePosCache;
        this._vertices[4 * (s + t * this.NUM_SECTORS) + 3] = to - vector2 - this.gamePosCache;
    }

    private void TailUpdate()
    {
        if (Time.unscaledTime > this.lastTailUpdateTime + 0.0166666675f)
        {
            this.lastTailUpdateTime = Time.unscaledTime;
            this.gamePosCache = base.gameObject.transform.position;
            float num = Vector2.Distance(this._tailxs[this.NUM_SECTORS - 1], this.gamePosCache);
            for (int i = 0; i < this.NUM_SECTORS; i++)
            {
                for (int j = 0; j < this.NUM_TAILS; j++)
                {
                    float num2 = 0.35f + 800f / (2000f + (16f + 2f * (float)j) * num * num);
                    if (num > 10f)
                    {
                        num2 = 0.2f;
                    }
                    if (num > 20f)
                    {
                        num2 = 0.1f;
                    }
                    float num3 = 1f - num2;
                    this.pOut = num2;
                    this.color.a = 1f;
                    if (i == 0)
                    {
                        this._tailx[i + j * this.NUM_SECTORS] = num2 * num2 * num2 * this._tailx[i + j * this.NUM_SECTORS] + (1f - num2 * num2 * num2) * this.gamePosCache;
                        this._tailx[i + j * this.NUM_SECTORS] += num3 * 2.5f * new Vector2(UnityEngine.Random.value - 0.5f, UnityEngine.Random.value - 0.5f);
                    }
                    else
                    {
                        this._tailx[i + j * this.NUM_SECTORS] += (num3 - 0.15f) * 3.5f * new Vector2(UnityEngine.Random.value - 0.5f, UnityEngine.Random.value - 0.5f);
                        this._tailx[i + j * this.NUM_SECTORS] = num2 * this._tailx[i + j * this.NUM_SECTORS] + num3 * this._tailx[i + j * this.NUM_SECTORS - 1];
                    }
                    this._tailxs[i + j * this.NUM_SECTORS] = num2 * this._tailxs[i + j * this.NUM_SECTORS] + num3 * this._tailx[i + j * this.NUM_SECTORS];
                    if (i == 0)
                    {
                        this.UpdateTailVertices(j, i, this.gamePosCache, this._tailxs[i + j * this.NUM_SECTORS]);
                    }
                    else
                    {
                        this.UpdateTailVertices(j, i, this._tailxs[i - 1 + j * this.NUM_SECTORS], this._tailxs[i + j * this.NUM_SECTORS]);
                    }
                    this._colors[4 * (i + j * this.NUM_SECTORS)] = this.color;
                    this._colors[4 * (i + j * this.NUM_SECTORS) + 1] = this.color;
                    this._colors[4 * (i + j * this.NUM_SECTORS) + 2] = this.color;
                    this._colors[4 * (i + j * this.NUM_SECTORS) + 3] = this.color;
                }
            }
            this.mesh.vertices = this._vertices;
            this.mesh.colors = this._colors;
        }
    }

    public ClanSpriteScript clan;

	public GameObject body;

	public GameObject tail;

	public static Sprite[] sprites;

	public static bool inited;

	public int id;

	private int _bodyType;

	private int _tailType;

	private Mesh mesh;

	private Vector3[] _vertices;

	private Vector2[] _uvs;

	private Color[] _colors;

	private int[] _triangles;

	private Vector2[] _tailx;

	private Vector2[] _tailxs;

	private int NUM_TAILS = 4;

	private int NUM_SECTORS = 4;

	public float lastPingTime;

	public float deathPingTime = -1f;

	public float tremor;

	private int _gx;

	private int _gy;

	private Vector2 gamePosition;

	private float rotationAngle;

	private float nowRotationAngle;

	private int skin;

	private float layerZ;

	public float renderDistance;

	public int dir = 2;

	private float lastTailUpdateTime;

	private Vector2 gamePosCache;

	private Bounds bounds;

	private Vector3 boundsCenter;

	private Vector3 boundsSize;

	private Color color = new Color(0.6f, 0.4f, 0.2f, 1f);

	public float pOut;
}

