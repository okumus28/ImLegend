using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoLamb : Grid
{
    public Color[] colors;

    public void BlastItem()
    {
        for (int i = 0; i < GameManager.Instance.column; i++)
        {
            for (int j = 0; j < GameManager.Instance.row; j++)
            {
                Grid g = GameManager.Instance.grids[i, j];
                if (g.id == id)
                {
                    Destroy(g.gameObject);
                }
            }
        }
    }
}
