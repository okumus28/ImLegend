using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoSpecial : ISpecialGridType
{
    public Color32[] colors;
    private void Awake()
    {
        myGrid = GetComponent<Grid>();
        x = myGrid.x;
        y = myGrid.y;
    }

    public override void BlastItem()
    {
        for (int i = 0; i < GameManager.Instance.column; i++)
        {
            for (int j = 0; j < GameManager.Instance.row; j++)
            {
                Grid g = GameManager.Instance.grids[i, j];
                if (g.id == sId)
                {
                    Destroy(g.gameObject);
                }
            }
        }
    }

    public override void CreateItem(int x, int y)
    {
        transform.localPosition = new Vector2(x, y);
        GetComponent<SpriteRenderer>().sortingOrder = y;
        GetComponent<SpriteRenderer>().color = colors[sId];
        myGrid.x = x;
        myGrid.y = y;
        GameManager.Instance.grids[x, y] = myGrid;
        gameObject.name = x.ToString() + "." + y.ToString();
        Debug.Log("CREATE");
    }
}
