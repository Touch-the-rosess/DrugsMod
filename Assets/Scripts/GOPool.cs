using System;
using UnityEngine;

public class GOPool : MonoBehaviour
{
    // private void Start()
    //{
    //  this.gos = new GameObject[this.max_size];
    //this.isFree = new bool[this.max_size];
    //this.use_size = 0;
    /*this.gos = new GameObject[this.use_size];
    this.isFree = new bool[this.use_size];
    this.max_size = 0;*/
    //}

    //   public GameObject GetFree(out int index)
    //   {
    //       int i;
    //       for (i = 0; i < this.max_size; i++)
    //       {
    //           if (this.isFree[i] && i < this.max_size)
    //           {
    //               index = i;
    //               this.isFree[index] = false;
    //               this.gos[i].SetActive(true);
    //               return this.gos[i];
    //           }
    //       }
    //       if (i >= this.max_size)
    //       {
    //           index = -1;
    //           return null;
    //       }
    //       index = i;
    //       this.isFree[index] = false;
    //       this.max_size = index + 1;
    //       this.gos[i] = UnityEngine.Object.Instantiate<GameObject>(this.prefab);
    //       return this.gos[i];
    //   }

    //   public void Free(int index)
    //{
    //	this.isFree[index] = true;
    //	this.gos[index].SetActive(false);
    //}

    //public GameObject prefab;

    //private GameObject[] gos;

    //private bool[] isFree;

    //public int max_size;


    //   private int max_size1;

    //   private int use_size; //= 4096;
    private void Start()
    {
        this.gos = new GameObject[this.use_size];
        this.isFree = new bool[this.use_size];
        this.max_size1 = 0;
    }
    public GameObject GetFree(out int index)
    {
        int i;
        for (i = 0; i < this.max_size1; i++)
        {
            if (this.isFree[i] && i < this.max_size)
            {
                index = i;
                this.isFree[index] = false;
                this.gos[i].SetActive(true);
                return this.gos[i];
            }
        }
        if (i >= this.max_size)
        {
            index = -1;
            return null;
        }
        index = i;
        this.isFree[index] = false;
        this.max_size1 = index + 1;
        this.gos[i] = UnityEngine.Object.Instantiate<GameObject>(this.prefab);
        return this.gos[i];
    }
    public void Free(int index)
    {
        this.isFree[index] = true;
        this.gos[index].SetActive(false);
    }

    public GameObject prefab;

    // Token: 0x040003E9 RID: 1001
    private GameObject[] gos;

    // Token: 0x040003EA RID: 1002
    private bool[] isFree;

    // Token: 0x040003EB RID: 1003
    public int max_size;

    // Token: 0x040003EC RID: 1004
    private int max_size1;

    // Token: 0x040003ED RID: 1005
    private int use_size = 4096;
}
