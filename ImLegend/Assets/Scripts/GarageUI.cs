using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    public Button letsGo;

    public Button appplyButton;
    public TextMeshProUGUI appplyButtonText;

    public Button firstSelect;

    [Header ("Car Selection")]
    public Button carSelectButton;
    public TextMeshProUGUI carSelectButtonText;

    public Image carLock;
    public TextMeshProUGUI carPrice;

    public Button garageButton;
    public Button carsButton;

    public TextMeshProUGUI cashText;
    public TextMeshProUGUI goldText;

    public int cash;
    public int gold;

    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        cash = PlayerPrefs.GetInt("PlayerCash");
        gold = PlayerPrefs.GetInt("PlayerGold");

        firstSelect.interactable = false;
        Cash(0);
        Gold(0);
    }

    public void SpriteUpdate(Transform _transformPart , Transform _transformSprites)
    {
        for (int i = 0; i < _transformSprites.childCount; i++)
        {
            Destroy(_transformSprites.GetChild(i).gameObject);
        }
        _transformSprites.GetComponent<RectTransform>().sizeDelta = new Vector2((_transformPart.childCount * 105), 100);
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
        GetComponent<AudioSource>().Play();
        Invoke(nameof(LoadGameScene), 1.5f);
        //SceneManager.LoadScene(1);
    }
    void LoadGameScene()
    {
        SceneManager.LoadScene(1);
    }

    public void Cash(int c)
    {
        cash += c;
        cashText.text = cash.ToString();
        PlayerPrefs.SetInt("PlayerCash" , cash);
    }
    
    public void Gold(int g)
    {
        gold += g;
        goldText.text = gold.ToString();
        PlayerPrefs.SetInt("PlayerGold" , gold);
    }
}
