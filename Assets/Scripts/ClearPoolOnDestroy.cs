using System;
using UnityEngine;

public class ClearPoolOnDestroy : MonoBehaviour
{
    private void Start()
    {
    }

    private void Update()
    {
    }

    private void OnDestroy()
    {
        this.pool.Free(this.index);
    }

    public GOPool pool;

	public int index;
}
