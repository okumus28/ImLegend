using UnityEngine;

public class DiscoSpecial : ISpecialBlockType
{
    public Color32[] colors;
    private void Awake()
    {
        myBlock = GetComponent<Block>();
        x = myBlock.x;
        y = myBlock.y;
    }

    public override void BlastItem()
    {
        for (int i = 0; i < GameManager.Instance.column; i++)
        {
            for (int j = 0; j < GameManager.Instance.row; j++)
            {
                Block g = GameManager.Instance.blocks[i, j];
                if (g.id == sId)
                {
                    g.DestroyAnim();
                    //Destroy(g.gameObject ,.15f);
                }
            }
        }
        Destroy(gameObject);
    }

    public override void CreateItem(int x, int y)
    {
        transform.localPosition = new Vector2(x, y);
        GetComponent<SpriteRenderer>().sortingOrder = y;
        GetComponent<SpriteRenderer>().color = colors[sId];
        myBlock.x = x;
        myBlock.y = y;
        GameManager.Instance.blocks[x, y] = myBlock;
        gameObject.name = x.ToString() + "." + y.ToString();
    }
}
