using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int row;
    public int column;

    public Block[,] blocks;

    int colorCount;
    public GameObject[] normalItemsPrefabs;
    public List<Block> affectedBlocks;

    public int moveCount;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.Find("Canvas").activeSelf == false) SpawnBlocks(); 
    }
    //create column * row blocks
    public void SpawnBlocks()
    {
        blocks = new Block[row , column];
        column = LevelManager.Instance.columnCount;
        row = LevelManager.Instance.rowCount;
        transform.position = new Vector2(-row / 2 + 0.5f, -column / 2 - 1.5f);
        colorCount = colorCount > normalItemsPrefabs.Length ? normalItemsPrefabs.Length : LevelManager.Instance.colorsCount;

        for (int i = 0; i < row; i++)
        {
            for (int k = 0; k < column; k++)
            {
                Block b = ASpawnBlock(i,k,false);
                b.lastDestroy = i == row - 1 && k == column - 1;
            }
        }
    }
    public Block ASpawnBlock(int x , int y , bool ld)
    {
        Block p = Instantiate(normalItemsPrefabs[Random.Range(0, this.colorCount)], this.transform).GetComponent<Block>();
        p.transform.localPosition = new Vector2(x, y + 15);
        p.GetComponent<SpriteRenderer>().sortingOrder = y;
        p.x = x;
        p.y = y;
        p.lastDestroy = ld;
        p.gameObject.name = x.ToString() + "." + y.ToString();
        p.dropping = true;
        blocks[x, y] = p;
        return p;
    }
    public void AddAffectedBlocks(Block _block)
    {
        for (int i = 0; i < _block.neighborsBlocks.Count; i++)
        {
            affectedBlocks.Add(_block.neighborsBlocks[i]);
        }
    }
    public void AffectedBlocksControl()
    {
        affectedBlocks = affectedBlocks.Distinct().ToList();

        for (int i = 0; i < affectedBlocks.Count; i++)
        {
            affectedBlocks[i].neighborsBlocks.Clear();
            affectedBlocks[i].neighborsBlocks.Add(affectedBlocks[i]);
        }

        for (int i = 0; i < affectedBlocks.Count; i++)
        {
            if (affectedBlocks[i] == null) continue;
            affectedBlocks[i].Control();
        }

        affectedBlocks.Clear();
    }    
    public void MoveControl()
    {
        moveCount = 0;
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                if (blocks[i,j].neighborsBlocks.Count >= 2)
                {
                    moveCount++;
                }
            }
        }
        if (moveCount <= 0)
        {
            Invoke(nameof(MixBlocks), 1.5f);
        }
    }
    public void MixBlocks()
    {
        MixBlocksToList();

        Block b;

        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                int rnd = Random.Range(0,affectedBlocks.Count);

                b = affectedBlocks[rnd];

                blocks[i,j] = b;

                b.MixedBlock(i,j);

                //b.lastDestroy = i == row - 1 && j == column - 1;

                affectedBlocks.RemoveAt(rnd);
            }
        }

        AllBlockControl();
        //MoveControl();
    }
    void MixBlocksToList()
    {
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                affectedBlocks.Add(blocks[i, j]);
                //blocks[i, j] = null;
            }
        }
    }
    void AllBlockControl()
    {
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                AddAffectedBlocks(blocks[i,j]);
            }
        }

        blocks[row - 1, column - 1].lastDestroy = true;
        blocks[row - 1, column - 1].dropping = true;

        AffectedBlocksControl();
    }
}