using System;
using UnityEngine;

public class OverlayRenderer : MonoBehaviour
{
    private void Start()
    {
        OverlayRenderer.THIS = this;
    }

    public void HideGrid()
    {
        foreach (object obj in this.grid.transform)
        {
            UnityEngine.Object.Destroy(((Transform)obj).gameObject);
        }
    }

    public void AddGrid(int w, int h, int[] codes, int dx, int dy, int d)
    {
        this.dx = dx;
        this.dy = dy;
        this.d = d;
        this.HideGrid();
        for (int i = 0; i < h; i++)
        {
            for (int j = 0; j < w; j++)
            {
                if (codes[j + i * w] > 0)
                {
                    GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.gridCellPrefab);
                    gameObject.transform.SetParent(this.grid.transform);
                    gameObject.transform.localPosition = new Vector3((float)j, (float)(-(float)i), 0f);
                }
            }
        }
    }

    private void Update()
    {
        int num = 0;
        int num2 = 0;
        if (ClientController.THIS.myBot != null)
        {
            switch (ClientController.THIS.myBot.dir)
            {
                case 0:
                    num2 = -this.d;
                    break;
                case 1:
                    num = -this.d;
                    break;
                case 2:
                    num2 = this.d;
                    break;
                case 3:
                    num = this.d;
                    break;
            }
            this.grid.SetActive(ClientController.THIS.myBot.renderDistance < 0.1f);
        }
        this.grid.transform.position = new Vector3((float)(ClientController.THIS.view_x - this.dx) + 0.5f + (float)num, (float)(-(float)ClientController.THIS.view_y + this.dy) - 0.5f + (float)num2, -5f);
    }
        
	public GameObject grid;

	public GameObject gridCellPrefab;

	public static OverlayRenderer THIS;

	private int dx = 1;

	private int dy = 1;

	private int d = 2;
}
