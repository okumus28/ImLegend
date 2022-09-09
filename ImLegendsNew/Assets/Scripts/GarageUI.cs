using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GarageUI : MonoBehaviour
{
    public static GarageUI instance { get; private set; }

    public TextMeshProUGUI carName;

    public Transform fbbSprite;
    public Transform bbbSprite;
    public Transform sidesSprite;
    public Transform windowsSprite;
    public Transform cowlingsSprite;
    public Transform rimsSprite;

    public GameObject spritePrefab;

    public Button appplyButton;
    public TextMeshProUGUI appplyButtonText;

    public Button firstSelect;

    [Header ("Car Selection")]
    public Button carSelectButton;
    public TextMeshProUGUI carSelectButtonText;
    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        firstSelect.interactable = false;
    }

    public void SpriteUpdate(Transform _transformPart , Transform _transformSprites)
    {
        for (int i = 1; i < _transformSprites.childCount; i++)
        {
            Destroy(_transformSprites.GetChild(i).gameObject);
        }
        _transformSprites.GetComponent<RectTransform>().sizeDelta = new Vector2(100 + (_transformPart.childCount * 105), 100);
        for (int i = 0; i < _transformPart.childCount; i++)
        {
            //GameObject g = Instantiate(spritePrefab, _transformSprites);
            spritePrefab.GetComponent<CarPartUI>().carPartData = _transformPart.GetChild(i).GetComponent<CarPart>().carPartData;
            spritePrefab.GetComponent<CarPartUI>().partCarTransform = _transformPart;
            Instantiate(spritePrefab, _transformSprites);
        }
    }

    public void LetsGoButton()
    {

    }
}
