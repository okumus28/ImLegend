using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ISpecialGridType : MonoBehaviour
{
    public int x, y;
    public Grid myGrid;
    public int jointCount;
    public int sId;
    public abstract void BlastItem();
    public abstract void CreateItem(int x, int y);

}
