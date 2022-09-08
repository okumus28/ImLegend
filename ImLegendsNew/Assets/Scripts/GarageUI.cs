using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class GarageUI : MonoBehaviour
{
    public static GarageUI instance { get; private set; }

    public Transform fbbSprite;
    public Transform bbbSprite;
    public Transform sidesSprite;
    public Transform windowsSprite;
    public Transform cowlingsSprite;
    public Transform rimsSprite;

    public GameObject spritePrefab;

    private void Awake()
    {
        instance = this;
    }

    public void SpriteUpdate(Transform _transformPart , Transform _transformSprites)
    {
        _transformSprites.GetComponent<RectTransform>().sizeDelta = new Vector2(100 + (_transformPart.childCount * 105), 100);
        for (int i = 0; i < _transformPart.childCount; i++)
        {
            GameObject g = Instantiate(spritePrefab, _transformSprites);
            g.GetComponent<CarPartUI>().carPartData = _transformPart.GetChild(i).GetComponent<CarPart>().carPartData;
            g.GetComponent<CarPartUI>().partCarTransform = _transformPart;
        }
    }
}
