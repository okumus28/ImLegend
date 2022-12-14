using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Car : MonoBehaviour
{
    public Transform fBumperTransform;
    public Transform bBumperTransform;
    public Transform sidesTransform;
    public Transform windowsTransform;
    public Transform cowlingTransform;
    public Transform rimsTransform;

    public Transform partTransformParent;

    public int currentFBumper;
    public int currentBBumper;
    public int currentSides;
    public int currentWindows;
    public int currentCowling;
    public int currentRims;

    public CarData carData;
    public Properties properties;

    public bool purchase;

    [SerializeField] GarageUI garageUI;
    public UI_Property uiProperty;

    private void Awake()
    {
        if (PlayerPrefs.GetInt("PlayerCash") <= 0)
        {
            PlayerPrefs.SetInt("PlayerCash", 55000);
            PlayerPrefs.SetInt("_purchasePickUp" , 1);
        }

        fBumperTransform = partTransformParent.GetChild(0);
        bBumperTransform = partTransformParent.GetChild(1);
        sidesTransform = partTransformParent.GetChild(2);
        windowsTransform = partTransformParent.GetChild(3);
        cowlingTransform = partTransformParent.GetChild(4);
        rimsTransform = partTransformParent.GetChild(5);

        purchase = PlayerPrefs.GetInt("_purchase" + carData.name) == 1;
        //uiProperty = UI_Property.instance;

    }
    private void OnEnable()
    {
        if (SceneManager.GetActiveScene().name == "GarageScene 1")
        {
            purchase = PlayerPrefs.GetInt("_purchase" + carData.name) == 1;

            if (purchase)
            {
                Debug.Log("purchase ");
                PlayerPrefs.SetInt("CurrentCarIndex", transform.GetSiblingIndex());
            }

            garageUI.carsButton.onClick.Invoke();

            CarSelectButtonText();

            garageUI.carName.text = this.carData.name;

            //uiProperty.UI_PropertiesUpdate(this.properties);

            UpdatePartsUIButtons();
        }
            properties = carData.properties.GetValues();
        GetCurrentParts();
    }
    void UpdatePartsUIButtons()
    {
        garageUI.SpriteUpdate(fBumperTransform, garageUI.fbbSprite);
        garageUI.SpriteUpdate(bBumperTransform, garageUI.bbbSprite);
        garageUI.SpriteUpdate(sidesTransform, garageUI.sidesSprite);
        garageUI.SpriteUpdate(windowsTransform, garageUI.windowsSprite);
        garageUI.SpriteUpdate(cowlingTransform, garageUI.cowlingsSprite);
        garageUI.SpriteUpdate(rimsTransform, garageUI.rimsSprite);
    }
    public void GetCurrentParts()
    {
        currentFBumper = PlayerPrefs.GetInt("Current" + fBumperTransform + carData.name);
        currentBBumper = PlayerPrefs.GetInt("Current" + bBumperTransform + carData.name);
        currentSides = PlayerPrefs.GetInt("Current" + sidesTransform + carData.name);
        currentWindows = PlayerPrefs.GetInt("Current" + windowsTransform + carData.name);
        currentCowling = PlayerPrefs.GetInt("Current" + cowlingTransform + carData.name);
        currentRims = PlayerPrefs.GetInt("Current" + rimsTransform + carData.name);

        fBumperTransform.GetChild(currentFBumper).gameObject.SetActive(true);
        bBumperTransform.GetChild(currentBBumper).gameObject.SetActive(true);
        sidesTransform.GetChild(currentSides).gameObject.SetActive(true);
        windowsTransform.GetChild(currentWindows).gameObject.SetActive(true);
        cowlingTransform.GetChild(currentCowling).gameObject.SetActive(true);
        rimsTransform.GetChild(currentRims).gameObject.SetActive(true);
    }
    public void CarSelectButtonText()
    {
        if (purchase)
        {
            garageUI.carSelectButton.gameObject.SetActive(false);
            garageUI.carLock.gameObject.SetActive(false);
            garageUI.garageButton.interactable = true;
            garageUI.letsGo.interactable = true;
        }
        else
        {
            garageUI.letsGo.interactable = false;
            garageUI.carLock.gameObject.SetActive(true);
            garageUI.garageButton.interactable = false;

            garageUI.carPrice.text = carData.price.ToString();
            garageUI.carSelectButton.gameObject.SetActive(true);

            garageUI.carSelectButton.interactable = garageUI.cash >= carData.price;

            garageUI.carSelectButton.onClick.RemoveAllListeners();
            garageUI.carSelectButton.onClick.AddListener(() => CarBuyEvent());
            garageUI.carSelectButtonText.text = "BUY";
        }
    }
    void CarBuyEvent()
    {
        garageUI.Cash(-carData.price);
        PlayerPrefs.SetInt("_purchase" + carData.name, 1);
        garageUI.carSelectButton.gameObject.SetActive(false);
        garageUI.carLock.gameObject.SetActive(false);
        garageUI.garageButton.interactable = true;
        garageUI.letsGo.interactable = true;
        PlayerPrefs.SetInt("CurrentCarIndex", transform.GetSiblingIndex());
    }
    private void OnDisable()
    {
        Debug.Log("car disable " + carData.name);
        properties = carData.properties.GetValues();
    }
}
