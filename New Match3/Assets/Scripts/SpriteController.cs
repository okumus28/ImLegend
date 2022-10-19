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

    Grid myGrid;
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
        myGrid = GetComponent<Grid>();
    }


    public void SpecialGridsControl()
    {
        int jointCount = myGrid.jointGrids.Count;

        if (jointCount > minCountForDisco)
        {
            ChangeSprite(disco, SpecialItems.Instance.discoPrefab);
        }
        else if (jointCount > minCountForBomb)
        {
            ChangeSprite(bomb, SpecialItems.Instance.bombPrefab);
        }
        else if (jointCount > minCountForRocket)
        {
            ChangeSprite(rocket, SpecialItems.Instance.rocketPrefab);
        }
        else
        {
            ChangeSprite(def , null);
            //myGrid.specialType = null;
        }
    }

    public void ChangeSprite(Sprite sprite , GameObject specialObject)
    {
        for (int i = 0; i < myGrid.jointGrids.Count; i++)
        {
            //_sr.sprite = sprite;
            myGrid.jointGrids[i].specialType = specialObject;
            myGrid.jointGrids[i].GetComponent<SpriteRenderer>().sprite = sprite;
        }
    }
}
