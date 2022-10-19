using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BombSpecial : ISpecialGridType 
{
    public int range;

    private void Awake()
    {
        myGrid = GetComponent<Grid>();
    }

    public override void BlastItem()
    {
        for (int i = myGrid.x - range; i <= myGrid.x + range; i++)
        {
            if (i < 0 || i > GameManager.Instance.column) continue;

            for (int j = myGrid.y - range; j <= myGrid.y + range; j++)
            {
                if (j < 0 || j > GameManager.Instance.row) continue;

                Grid _grid = GameManager.Instance.grids[i, j];

                Destroy(_grid.gameObject);
            }
        }
    }

    public override void CreateItem(int x, int y)
    {
        myGrid.transform.localPosition = new Vector2(x, y);
        myGrid.GetComponent<SpriteRenderer>().sortingOrder = y;
        myGrid.x = x;
        myGrid.y = y;
        GameManager.Instance.grids[x, y] = myGrid;
        myGrid.gameObject.name = x.ToString() + "." + y.ToString();
        Debug.Log("CREATE");
    }
}
