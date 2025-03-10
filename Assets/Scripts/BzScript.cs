using System;
using UnityEngine;

public class BzScript : MonoBehaviour
{
    public void SetAnimation(int type, int _index, Vector3 pos)
    {
        base.transform.position = pos;
        this.frame = 0;
        this.index = _index;
        switch (type)
        {
            case 0:
                this.spriteCollection = BzScript.bzSprites;
                this.framesInAnimation = 56;
                this.updatesPerFrame = 4;
                return;
            case 1:
                this.spriteCollection = BzScript.hurtSprites;
                this.framesInAnimation = 16;
                this.updatesPerFrame = 3;
                return;
            case 2:
                this.spriteCollection = BzScript.expSprites;
                this.framesInAnimation = 40;
                this.updatesPerFrame = 3;
                return;
            default:
                return;
        }
    }

    public static void Init()
    {
        if (!BzScript.inited)
        {
            BzScript.bzSprites = Resources.LoadAll<Sprite>("fx");
            BzScript.hurtSprites = Resources.LoadAll<Sprite>("hurt");
            BzScript.expSprites = Resources.LoadAll<Sprite>("explosion");
            BzScript.inited = true;
        }
    }

    private void Start()
    {
        this.sr = base.GetComponent<SpriteRenderer>();
        this.sr.color = new Color(1f, 1f, 1f);
    }

    private void Update()
    {
        this.frame++;
        if (this.sr != null)
        {
            this.sr.sprite = this.spriteCollection[this.frame / this.updatesPerFrame]; //Debug.Log("1");
        }
        if (this.frame >= this.framesInAnimation + this.updatesPerFrame - 1)
        {
            this.spriteCollection = BzScript.bzSprites;
            this.framesInAnimation = 56;
            this.updatesPerFrame = 4;
            this.sr.sprite = this.spriteCollection[0];
            ClientController.THIS.bzPool.Free(this.index);
            
        }
    }

	public static Sprite[] bzSprites;

	public static Sprite[] hurtSprites;

	public static Sprite[] expSprites;

	public static bool inited;

	public static int all_fx_num;

	private int index;

	private int num;

	private int framesInAnimation = 1;

	private int updatesPerFrame = 4;

	private SpriteRenderer sr;

	private Sprite[] spriteCollection;

	private int frame;
}
