using UnityEngine;

public abstract class ISpecialBlockType : MonoBehaviour
{
    public int x, y;
    public Block myBlock;
    public int jointCount;
    public int sId;
    public abstract void BlastItem();
    public abstract void CreateItem(int x, int y);

}
