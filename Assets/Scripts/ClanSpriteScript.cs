using System;
using UnityEngine;

public class ClanSpriteScript : MonoBehaviour
{
    public static void Init()
    {
        if (!ClanSpriteScript.inited)
        {
            ClanSpriteScript.sprites = Resources.LoadAll<Sprite>("clans");
            ClanSpriteScript.inited = true;
        }
    }

    private void Start()
    {
        this.changeClanToId(-1);
    }

    public void changeClanToId(int id)
    {
        if (id == -1)
        {
            if (this._id >= 0)
            {
                id = this._id;
            }
            else
            {
                id = 0;
            }
        }
        else if (this._id == id)
        {
            return;
        }
        this._id = id;
        if (id == 0)
        {
            base.gameObject.SetActive(false);
            return;
        }
        base.gameObject.SetActive(true);
        base.GetComponent<SpriteRenderer>().sprite = ClanSpriteScript.sprites[id - 1];
    }

    private void Update()
    {
    }

    public static Sprite[] sprites;

	public static bool inited;

	private int _id = -1;
}
