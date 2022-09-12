using JetBrains.Annotations;
using UnityEditor.Purchasing;
using UnityEngine;
using UnityEngine.UI;

public class Car : MonoBehaviour
{
    [System.NonSerialized] public Transform fBumperTransform;
    [System.NonSerialized] public Transform bBumperTransform;
    [System.NonSerialized] public Transform sidesTransform;
    [System.NonSerialized] public Transform windowsTransform;
    [System.NonSerialized] public Transform cowlingTransform;
    [System.NonSerialized] public Transform rimsTransform;

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
    public bool purchase;

    private void Awake()
    {
        fBumperTransform = partTransformParent.GetChild(0);
        bBumperTransform = partTransformParent.GetChild(1);
        sidesTransform = partTransformParent.GetChild(2);
        windowsTransform = partTransformParent.GetChild(3);
        cowlingTransform = partTransformParent.GetChild(4);
        rimsTransform = partTransformParent.GetChild(5);

<<<<<<< Updated upstream
        purchase = PlayerPrefs.GetInt(carData.carName + "purchase") == 1;

    }
    private void OnEnable()
    {
        CarSelectButtonText();
=======
        purchase = PlayerPrefs.GetInt("_purchase" + carData.name) == 1;

        Debug.Log("CAR AWAKE");
        
    }
    private void OnEnable()
    {
        Debug.Log("CAR ONENABLE");
        purchase = PlayerPrefs.GetInt("_purchase" + carData.name) == 1;


        garageUI.carsButton.onClick.Invoke();


        if (purchase)
        {
            garageUI.carSelectButton.gameObject.SetActive(false);
            garageUI.carLock.gameObject.SetActive(false);
            garageUI.garageButton.interactable = true;
        }
        else
        {
            garageUI.carLock.gameObject.SetActive(true);
            garageUI.garageButton.interactable = false;

            garageUI.carPrice.text = carData.price.ToString();
            garageUI.carSelectButton.gameObject.SetActive(true);
            garageUI.carSelectButton.onClick.RemoveAllListeners();
            garageUI.carSelectButton.onClick.AddListener(() => CarBuyEvent());
            garageUI.carSelectButtonText.text = "BUY";
        }
>>>>>>> Stashed changes

        garageUI.carName.text = this.carData.name;
        properties = carData.properties.GetValues();


        GetSavedParts();
        UI_PartButtons();
    }

    void CarBuyEvent()
    {
        PlayerPrefs.SetInt("_purchase" + carData.name, 1);
        garageUI.carSelectButton.gameObject.SetActive(false);
        garageUI.carLock.gameObject.SetActive(false);
        garageUI.garageButton.interactable = true;
        PlayerPrefs.SetInt("CurrentCarIndex", transform.GetSiblingIndex());
    }

    private void OnDisable()
    {
        properties = carData.properties.GetValues();
    }

    void GetSavedParts()
    {
        Debug.Log("LOAD car saved PART s");

        currentFBumper = PlayerPrefs.GetInt("CurrentPart" + carData.name + fBumperTransform);
        currentBBumper = PlayerPrefs.GetInt("CurrentPart" + carData.name + bBumperTransform);
        currentSides = PlayerPrefs.GetInt("CurrentPart" + carData.name + sidesTransform);
        currentWindows = PlayerPrefs.GetInt("CurrentPart" + carData.name + windowsTransform);
        currentCowling = PlayerPrefs.GetInt("CurrentPart" + carData.name + cowlingTransform);
        currentRims = PlayerPrefs.GetInt("CurrentPart" + carData.name + rimsTransform);

        fBumperTransform.GetChild(currentFBumper).gameObject.SetActive(true);
        bBumperTransform.GetChild(currentBBumper).gameObject.SetActive(true);
        sidesTransform.GetChild(currentSides).gameObject.SetActive(true);
        windowsTransform.GetChild(currentWindows).gameObject.SetActive(true);
        cowlingTransform.GetChild(currentCowling).gameObject.SetActive(true);
        rimsTransform.GetChild(currentRims).gameObject.SetActive(true);
    }

<<<<<<< Updated upstream
    void BuyCar()
    {
        purchase = true;
        PlayerPrefs.SetInt(carData.carName + "purchase", 1);
        CarSelectButtonText();
    }
    void SelectCar()
    {
        PlayerPrefs.SetInt("CurrentCarIndex" ,CarSelection.currentCarIndex);
        CarSelectButtonText();
    }

    public void CarSelectButtonText()
    {
        if (purchase)
        {
            if (CarSelection.currentCarIndex == PlayerPrefs.GetInt("CurrentCarIndex"))
            {
                garageUI.carSelectButtonText.text = "Selected";
                garageUI.carSelectButton.interactable = false;
            }
            else
            {
                garageUI.carSelectButtonText.text = "Select";
                garageUI.carSelectButton.interactable = true;
                garageUI.carSelectButton.onClick.RemoveAllListeners();
                garageUI.carSelectButton.onClick.AddListener(() => SelectCar());
            }
        }
        else
        {
            garageUI.carSelectButtonText.text = "Buy";
            garageUI.carSelectButton.interactable = true;
            garageUI.carSelectButton.onClick.RemoveAllListeners();
            garageUI.carSelectButton.onClick.AddListener(() => BuyCar());
        }
    }

    private void OnDisable()
=======
    void UI_PartButtons()
>>>>>>> Stashed changes
    {
        garageUI.SpriteUpdate(fBumperTransform, garageUI.fbbSprite);
        garageUI.SpriteUpdate(bBumperTransform, garageUI.bbbSprite);
        garageUI.SpriteUpdate(sidesTransform, garageUI.sidesSprite);
        garageUI.SpriteUpdate(windowsTransform, garageUI.windowsSprite);
        garageUI.SpriteUpdate(cowlingTransform, garageUI.cowlingsSprite);
        garageUI.SpriteUpdate(rimsTransform, garageUI.rimsSprite);
    }

    public void SetIndex(Transform _transform , int index)
    {
        if (_transform == fBumperTransform)
        {
            currentFBumper = index;
        }
        if (_transform == bBumperTransform)
        {
            currentBBumper = index;
        }
        if (_transform == sidesTransform)
        {
            currentSides = index;
        }
        if (_transform == windowsTransform)
        {
            currentWindows = index;
        }
        if (_transform == cowlingTransform)
        {
            currentCowling = index;
        }
        if (_transform == rimsTransform)
        {
            currentRims = index;
        }

        ChangeActivePart(_transform , index);
    }

    void ChangeActivePart(Transform _transform , int _index)
    {
        for (int i = 0; i < _transform.childCount; i++)
        {
            _transform.GetChild(i).gameObject.SetActive(i == _index);
        }
    }
}
