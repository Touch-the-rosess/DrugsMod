using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPanel : MonoBehaviour
{
    private void Start()
    {
        InventoryPanel.THIS = this;
        this.button.GetComponent<Button>().onClick.AddListener(delegate ()
        {
            ClientController.CanGoto = false;
            ServerTime.THIS.SendTypicalMessage(-1, "INVN", 0, 0, "_");
        });
    }

    private void Update()
    {
    }

    private void removeAll()
    {
        foreach (object obj in this.inventoryGrid.transform)
        {
            UnityEngine.Object.Destroy(((Transform)obj).gameObject);
        }
    }

    private void makeGrid(string[] parts)
    {
        for (int i = 0; i < parts.Length; i += 2)
        {
            GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.inventoryItemPrefab);
            gameObject.transform.SetParent(this.inventoryGrid.transform, false);
            int _type = int.Parse(parts[i]);
            int num = int.Parse(parts[i + 1]);
            gameObject.GetComponent<InventoryItem>().Setup(_type, num, this._selected == _type, "", "");
            if (num > 0)
            {
                gameObject.GetComponent<Button>().onClick.AddListener(delegate ()
                {
                    ClientController.CanGoto = false;
                    ServerTime.THIS.SendTypicalMessage(-1, "INCL", 0, 0, _type.ToString());
                });
            }
        }
    }

    public void ShowFullGrid(string grid, int selected)
    {
        Vector3 localScale = this.triangle.transform.localScale;
        localScale.x = -1f;
        this.triangle.transform.localScale = localScale;
        this.removeAll();
        this._selected = selected;
        if (grid == "")
        {
            this.button.SetActive(false);
            return;
        }
        string[] array = grid.Split(new char[]
        {
            '#'
        });
        this.makeGrid(array);
        if (array.Length < 10)
        {
            this.button.SetActive(false);
            return;
        }
        this.button.SetActive(true);
    }

    public void ShowInventory(string grid, int selected, int all)
    {
        Vector3 localScale = this.triangle.transform.localScale;
        localScale.x = 1f;
        this.triangle.transform.localScale = localScale;
        this.removeAll();
        this._selected = selected;
        if (grid == "")
        {
            this.button.SetActive(false);
            return;
        }
        string[] parts = grid.Split(new char[]
        {
            '#'
        });
        this.makeGrid(parts);
        if (all < 5)
        {
            this.button.SetActive(false);
            return;
        }
        this.button.SetActive(true);
    }

    public GameObject inventoryItemPrefab;

	public GameObject button;

	public GameObject inventoryGrid;

	public Image triangle;

	public static InventoryPanel THIS;

	private int _selected = -1;
}
