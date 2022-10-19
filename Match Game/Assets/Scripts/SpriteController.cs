using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteController : MonoBehaviour
{
    public Sprite def;
    public Sprite bomb;
    public Sprite rocket;
    public Sprite disco;

    public int minCountForRocket, minCountForBomb , minCountForDisco ;

    [SerializeField] SpriteRenderer _sr;

    Block myBlock;
    private void Awake()
    {
        minCountForRocket = LevelManager.Instance.a;
        minCountForBomb = LevelManager.Instance.b;
        minCountForDisco = LevelManager.Instance.c;
    }
    // Start is called before the first frame update
    void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
        myBlock = GetComponent<Block>();
    }


    public void SpecialBlocksControl()
    {
        int neighborsCount = myBlock.neighborsBlocks.Count;

        if (neighborsCount > minCountForDisco)
        {
            ChangeSprite(disco, SpecialItems.Instance.discoPrefab);
        }
        else if (neighborsCount > minCountForBomb)
        {
            ChangeSprite(bomb, SpecialItems.Instance.bombPrefab);
        }
        else if (neighborsCount > minCountForRocket)
        {
            ChangeSprite(rocket, SpecialItems.Instance.rocketPrefab[Random.Range(0,2)]);
        }
        else
        {
            ChangeSprite(def , null);
            //myBlock.specialType = null;
        }
    }

    public void ChangeSprite(Sprite sprite , GameObject specialObject)
    {
        for (int i = 0; i < myBlock.neighborsBlocks.Count; i++)
        {
            //_sr.sprite = sprite;
            myBlock.neighborsBlocks[i].specialObject = specialObject;
            myBlock.neighborsBlocks[i].GetComponent<SpriteRenderer>().sprite = sprite;
        }
    }
}
