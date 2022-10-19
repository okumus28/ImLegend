using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int row;
    public int column;

    public Grid[,] grids;

    int colorCount;
    public GameObject[] normalItemsPrefabs;
    public List<Grid> affectedGrids;

    private void Awake()
    {
        Instance = this;
        column = LevelManager.Instance.columnCount;
        row = LevelManager.Instance.rowCount;
        colorCount = colorCount > normalItemsPrefabs.Length ? normalItemsPrefabs.Length : LevelManager.Instance.colorsCount;
    }

    // Start is called before the first frame update
    void Start()
    {
        grids = new Grid[row , column];
        transform.position = new Vector2(-row / 2 + 0.5f, -column / 2 - 1.5f);
        SpawnGrids();
    }

    void SpawnGrids()
    {
        for (int i = 0; i < row; i++)
        {
            for (int k = 0; k < column; k++)
            {
                ASpawnGrid(i,k);
            }
        }
    }

    public void ASpawnGrid(int x , int y)
    {
        Grid p = Instantiate(normalItemsPrefabs[UnityEngine.Random.Range(0, this.colorCount)], this.transform).GetComponent<Grid>();
        p.transform.localPosition = new Vector2(x, y + 15);
        p.GetComponent<SpriteRenderer>().sortingOrder = y;
        p.x = x;
        p.y = y;
        p.gameObject.name = x.ToString() + "." + y.ToString();
        p.dropping = true;
        grids[x, y] = p;;
    }

    public void AddAffectedGrids(Grid _grid)
    {
        for (int i = 0; i < _grid.jointGrids.Count; i++)
        {
            affectedGrids.Add(_grid.jointGrids[i]);
        }
    }

    public void AffectedGridsControl()
    {
        affectedGrids = affectedGrids.Distinct().ToList();

        for (int i = 0; i < affectedGrids.Count; i++)
        {
            affectedGrids[i].jointGrids.Clear();
            affectedGrids[i].jointGrids.Add(affectedGrids[i]);
        }

        for (int i = 0; i < affectedGrids.Count; i++)
        {
            if (affectedGrids[i] == null) continue;
            affectedGrids[i].Control();
        }

        affectedGrids.Clear();
    }
}