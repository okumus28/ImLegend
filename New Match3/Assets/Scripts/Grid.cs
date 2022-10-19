using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class Grid : MonoBehaviour 
{
    public GameObject specialType;

    public int id;

    public bool dropping = false;

    [SerializeField] float dropSpeed = 3f;
    public int x, y;

    public List<Grid> jointGrids;

    SpriteController spriteController;

    bool onClicked;

    private void Start()
    {
        jointGrids.Add(this);
        spriteController = GetComponent<SpriteController>();
    }
    private void Update()
    {
        if (dropping)
        {            
            transform.localPosition = Vector2.Lerp(transform.localPosition , new Vector2 (x,y) , dropSpeed * Time.deltaTime);

            if (transform.localPosition.y - y <= .2f)
            {
                dropping = false;
                transform.localPosition = new Vector2(x,y);

                Control();
            }
        }
    }

    public void OnMouseDown()
    {
        if (GetComponent<ISpecialGridType>() != null)
        {
            GetComponent<ISpecialGridType>().BlastItem();
            Destroy(gameObject);
            return;
        }
        if (jointGrids.Count < 2) return;

        if (jointGrids.Count > 4)
        {            
            onClicked = true;

            //GameObject s = Instantiate(specialType.gameObject, this.transform.parent);
            //s.GetComponent<ISpecialGridType>().sId = id;
            //s.GetComponent<ISpecialGridType>().CreateItem(x, y);
        }

        for (int i = 0; i < jointGrids.Count; i++)
        {
            jointGrids[i].DestroyAnim();
            Destroy(jointGrids[i].gameObject , .5f);

        }
    }


    private void OnDestroy()
    {

        if (onClicked)
        {
            print("ONCLÝCKED");            
        }
        else
        {
            for (int i = y + 1; i < GameManager.Instance.column; i++)
            {
                Grid _grid = GameManager.Instance.grids[x, i];

                GameManager.Instance.AddAffectedGrids(_grid);
                GameManager.Instance.grids[x, i - 1] = _grid;

                GridUpdate(_grid);
            }

            GameManager.Instance.ASpawnGrid(x, GameManager.Instance.column - 1);
            GameManager.Instance.AffectedGridsControl();

        }
    }

    public void DestroyAnim()
    {
        DOTween.To(GetScale, SetScale, Vector2.zero, .5f);
    }

    Vector2 GetScale() { return transform.localScale; }
    void SetScale(Vector2 _scale) { transform.localScale = _scale; }

    void GridUpdate(Grid _grid)
    {
        _grid.y -= 1;
        _grid.dropping = true;
        _grid.GetComponent<SpriteRenderer>().sortingOrder = _grid.y;
        _grid.gameObject.name = _grid.x.ToString() + "." + _grid.y.ToString();
    }


    public void Control()
    {
        TopJointGridControl();
        BottomJointGridControl();
        RightJointGridControl();
        LeftJointGridControl();

        jointGrids.RemoveAll(grid => grid == null);
        jointGrids = jointGrids.Distinct().ToList();


        if (spriteController != null)
        {
            spriteController.SpecialGridsControl();
        }
    }

    void TopJointGridControl()
    {
        if (y+1 < GameManager.Instance.column && GameManager.Instance.grids[x, y + 1] != null)
        {
            Grid topGrid = GameManager.Instance.grids[x, y + 1];

            AddJointGrid(topGrid);
        }
    }    
    void BottomJointGridControl()
    {
        if (y -1  >= 0 && GameManager.Instance.grids[x, y -1] != null)
        {
            Grid bottomGrid = GameManager.Instance.grids[x, y - 1];

            AddJointGrid(bottomGrid);
        }
    }
    void RightJointGridControl()
    {
        if (x+1 < GameManager.Instance.row && GameManager.Instance.grids[x + 1, y] != null)
        {
            Grid leftGrid = GameManager.Instance.grids[x + 1, y];

            AddJointGrid(leftGrid);
        }
    }
    void LeftJointGridControl()
    {
        if (x-1 >= 0 && GameManager.Instance.grids[x - 1, y] != null)
        {
            Grid rightGrid = GameManager.Instance.grids[x - 1, y];

            AddJointGrid(rightGrid);
        }
    }

    void AddJointGrid(Grid _grid)
    {
        //jointGrids.Add(this);

        if (this.id == _grid.id)
        {
            if (!this.jointGrids.SequenceEqual(_grid.jointGrids))
            {
                for (int j = 0; j < _grid.jointGrids.Count; j++)
                {
                    this.jointGrids.Add(_grid.jointGrids[j]);
                }

                for (int k = 0; k < jointGrids.Count; k++)
                {
                    //joints[k].joints = this.joints;
                    if (jointGrids[k].jointGrids != this.jointGrids)
                    {
                        jointGrids[k].jointGrids.Clear();
                        for (int m = 0; m < this.jointGrids.Count; m++)
                        {
                            jointGrids[k].jointGrids.Add(this.jointGrids[m]);
                        }
                    }
                }
            }
        }
    }    
}