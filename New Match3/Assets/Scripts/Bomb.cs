using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb :  Grid
{
    public int range;
    public void BlastItem(int x , int y)
    {
        for (int i = x - range; i <= x + range; i++)
        {
            if (i < 0 || i > GameManager.Instance.column) continue;

            for (int j = y - range; j <= y + range; j++)
            {
                if (j < 0 || j > GameManager.Instance.row) continue;

                Destroy(GameManager.Instance.grids[i, j].gameObject);
            }
        }
    }
}
