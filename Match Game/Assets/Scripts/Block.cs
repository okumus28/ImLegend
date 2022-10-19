using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class Block : MonoBehaviour 
{
    public GameObject specialObject;

    public int id;

    public bool dropping = false;

    [SerializeField] float dropSpeed = 3f;
    public int x, y;

    public List<Block> neighborsBlocks;

    SpriteController spriteController;

    bool onClicked;
    public bool lastDestroy;

    private void Start()
    {
        neighborsBlocks.Add(this);
        spriteController = GetComponent<SpriteController>();

    }
    private void Update()
    {
        //fall down movement && control neighbors
        if (dropping)
        {
            transform.localPosition = Vector2.Lerp(transform.localPosition, new Vector2(x, y), dropSpeed * Time.deltaTime);

            if (transform.localPosition.y - y <= .2f)
            {
                dropping = false;
                transform.localPosition = new Vector2(x, y);

                Control();

                if (lastDestroy)
                {
                    GameManager.Instance.MoveControl();
                    lastDestroy = false;
                }
            }
        }
    }
    public void OnMouseDown()
    {

        //SpecialItem Click
        if (GetComponent<ISpecialBlockType>() != null && UIElementsScript.Instance.specialItems)
        {
            GetComponent<ISpecialBlockType>().BlastItem();
            //Destroy(gameObject);
            return;
        }

        //min count blast
        if (neighborsBlocks.Count < 2) return;

        //create special item
        if (neighborsBlocks.Count > LevelManager.Instance.a && UIElementsScript.Instance.specialItems)
        {            
            onClicked = true;

            GameObject s = Instantiate(specialObject.gameObject, this.transform.parent);
            s.GetComponent<ISpecialBlockType>().sId = id;
            s.GetComponent<ISpecialBlockType>().CreateItem(x, y);
        }

        // destroy same neigbors
        for (int i = 0; i < neighborsBlocks.Count; i++)
        {
            neighborsBlocks[i].DestroyAnim();
            neighborsBlocks[i].lastDestroy = i == neighborsBlocks.Count - 1;
        }        
    }
    private void OnDestroy()
    {
        if (!onClicked)
        {
            // fall down control
            for (int i = y + 1; i < GameManager.Instance.column; i++)
            {
                Block _block = GameManager.Instance.blocks[x, i];

                GameManager.Instance.AddAffectedBlocks(_block);
                GameManager.Instance.blocks[x, i - 1] = _block;

                BlockUpdate(_block);
            }

            //create new block
            GameManager.Instance.ASpawnBlock(x, GameManager.Instance.column - 1 , lastDestroy);

            GameManager.Instance.AffectedBlocksControl();
        }
    }    
    public void MixedBlock(int x ,int y)
    {
        this.x = x;
        this.y = y;

        this.GetComponent<SpriteRenderer>().sortingOrder = y;
        this.gameObject.name = x.ToString() + "." + y.ToString();

        NewPosAnim(x, y);
        Control();
    }
    public void NewPosAnim(int x , int y)
    {
        Vector3 newPos = new Vector3(LevelManager.Instance.rowCount / 2, LevelManager.Instance.columnCount / 2, 0);
        DOTween.To(() => newPos , a => transform.localPosition = a, new Vector3(x, y, 0), 40 * Time.deltaTime);
    }
    public void DestroyAnim()
    {
        DOTween.To(() => transform.localScale, x => transform.localScale = x, new Vector3(.1f , .1f, .1f), .5f)
            .OnComplete(() => Destroy(transform.gameObject));
    }
    void BlockUpdate(Block _block)
    {
        _block.y -= 1;
        _block.dropping = true;
        _block.GetComponent<SpriteRenderer>().sortingOrder = _block.y;
        _block.gameObject.name = _block.x.ToString() + "." + _block.y.ToString();
    }

    // Neigbor blocks control
    public void Control()
    {
        TopNeighborBlockControl();
        BottomNeighborBlockControl();
        RightNeighborBlockControl();
        LeftNeighborBlockControl();

        neighborsBlocks.RemoveAll(block => block == null);
        neighborsBlocks = neighborsBlocks.Distinct().ToList();


        if (spriteController != null)
        {
            spriteController.SpecialBlocksControl();
        }
    }
    void TopNeighborBlockControl()
    {
        if (y+1 < GameManager.Instance.column && GameManager.Instance.blocks[x, y + 1] != null)
        {
            Block topBlock = GameManager.Instance.blocks[x, y + 1];

            AddNeighborBlock(topBlock);
        }
    }    
    void BottomNeighborBlockControl()
    {
        if (y -1  >= 0 && GameManager.Instance.blocks[x, y -1] != null)
        {
            Block bottomBlock = GameManager.Instance.blocks[x, y - 1];

            AddNeighborBlock(bottomBlock);
        }
    }
    void RightNeighborBlockControl()
    {
        if (x+1 < GameManager.Instance.row && GameManager.Instance.blocks[x + 1, y] != null)
        {
            Block leftBlock = GameManager.Instance.blocks[x + 1, y];

            AddNeighborBlock(leftBlock);
        }
    }
    void LeftNeighborBlockControl()
    {
        if (x-1 >= 0 && GameManager.Instance.blocks[x - 1, y] != null)
        {
            Block rightBlock = GameManager.Instance.blocks[x - 1, y];

            AddNeighborBlock(rightBlock);
        }
    }
    void AddNeighborBlock(Block _block)
    {
        //jointBlocks.Add(this);

        if (this.id == _block.id)
        {
            if (!this.neighborsBlocks.SequenceEqual(_block.neighborsBlocks))
            {
                for (int j = 0; j < _block.neighborsBlocks.Count; j++)
                {
                    this.neighborsBlocks.Add(_block.neighborsBlocks[j]);
                }

                for (int k = 0; k < neighborsBlocks.Count; k++)
                {
                    //joints[k].joints = this.joints;
                    if (neighborsBlocks[k].neighborsBlocks != this.neighborsBlocks)
                    {
                        neighborsBlocks[k].neighborsBlocks.Clear();
                        for (int m = 0; m < this.neighborsBlocks.Count; m++)
                        {
                            neighborsBlocks[k].neighborsBlocks.Add(this.neighborsBlocks[m]);
                        }
                    }
                }
            }
        }
    }    
}