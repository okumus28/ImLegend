using UnityEngine;

public class BombSpecial : ISpecialBlockType 
{
    public int range;

    private void Awake()
    {
        myBlock = GetComponent<Block>();
    }

    public override void BlastItem()
    {
        for (int i = myBlock.x - range; i <= myBlock.x + range; i++)
        {
            if (i < 0 || i > GameManager.Instance.column) continue;

            for (int j = myBlock.y - range; j <= myBlock.y + range; j++)
            {
                if (j < 0 || j > GameManager.Instance.row) continue;

                Block _block = GameManager.Instance.blocks[i, j];

                _block.id = 15;

                _block.DestroyAnim();
                //Destroy(_block.gameObject);
            }
        }
    }

    public override void CreateItem(int x, int y)
    {
        myBlock.transform.localPosition = new Vector2(x, y);
        myBlock.GetComponent<SpriteRenderer>().sortingOrder = y;
        myBlock.x = x;
        myBlock.y = y;
        GameManager.Instance.blocks[x, y] = myBlock;
        myBlock.gameObject.name = x.ToString() + "." + y.ToString();
    }
}