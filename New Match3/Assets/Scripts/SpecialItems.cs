using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialItems : MonoBehaviour
{
    public static SpecialItems Instance;


    public GameObject rocketPrefab;
    public GameObject bombPrefab;
    public GameObject discoPrefab;

    private void Awake()
    {
        Instance = this;
    }

    public void SpawnBomb(int x, int y)
    {
        Grid p = Instantiate(SpecialItems.Instance.bombPrefab, this.transform).GetComponent<Grid>();
        p.transform.localPosition = new Vector2(x, y);
        p.GetComponent<SpriteRenderer>().sortingOrder = y;
        p.x = x;
        p.y = y;
        GameManager.Instance.grids[x, y] = p;
        p.gameObject.name = x.ToString() + "." + y.ToString();
    }
    
    public void SpawnDisco(int x, int y)
    {
        DiscoLamb p = Instantiate(SpecialItems.Instance.discoPrefab, this.transform).GetComponent<DiscoLamb>();
        p.id = GameManager.Instance.grids[x, y].id;
        p.GetComponent<SpriteRenderer>().color = p.colors[GameManager.Instance.grids[x, y].id];
        p.transform.localPosition = new Vector2(x, y);
        p.GetComponent<SpriteRenderer>().sortingOrder = y;
        p.x = x;
        p.y = y;
        GameManager.Instance.grids[x, y] = p;
        p.gameObject.name = x.ToString() + "." + y.ToString();
    }

}
