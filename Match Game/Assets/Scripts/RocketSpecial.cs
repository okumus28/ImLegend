using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketSpecial : ISpecialBlockType
{
    public enum Direction { vertical , horizontal}
    public Direction direction;
    private void Awake()
    {
        myBlock = GetComponent<Block>();
    }

    public override void BlastItem()
    {
        if (direction == Direction.vertical)
        {
            for (int i = 0; i < GameManager.Instance.row; i++)
            {
                Block _block = GameManager.Instance.blocks[myBlock.x, i];

                _block.id = 15;

                _block.DestroyAnim();

                //Destroy(_block.gameObject);
            }
        }
        else if (direction == Direction.horizontal)
        {
            for (int i = 0; i < GameManager.Instance.column; i++)
            {
                Block _block = GameManager.Instance.blocks[i, myBlock.y];

                _block.id = 15;

                _block.DestroyAnim();
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
