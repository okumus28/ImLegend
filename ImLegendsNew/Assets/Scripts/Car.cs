using UnityEngine;
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

    private void Awake()
    {
        //Get car save data current part index
        currentFBumper = 0;
        currentBBumper = 0;
        currentSides = 0;
        currentWindows = 0;
        currentCowling = 0;
        currentRims = 0;

        purchase = PlayerPrefs.GetInt(carData.carName + "purchase") == 1;

    }
    private void OnEnable()
    {
        CarSelectButtonText();

        garageUI.carName.text = this.carData.name;
        properties = carData.properties.GetValues();

        garageUI.SpriteUpdate(fBumperTransform , garageUI.fbbSprite);
        garageUI.SpriteUpdate(bBumperTransform , garageUI.bbbSprite);
        garageUI.SpriteUpdate(sidesTransform , garageUI.sidesSprite);
        garageUI.SpriteUpdate(windowsTransform , garageUI.windowsSprite);
        garageUI.SpriteUpdate(cowlingTransform , garageUI.cowlingsSprite);
        garageUI.SpriteUpdate(rimsTransform , garageUI.rimsSprite);
        
        UI_Property.Instance.UI_PropertiesUpdate(properties);

        fBumperTransform.GetChild(currentFBumper).gameObject.SetActive(true);
        bBumperTransform.GetChild(currentBBumper).gameObject.SetActive(true);
        sidesTransform.GetChild(currentSides).gameObject.SetActive(true);
        windowsTransform.GetChild(currentWindows).gameObject.SetActive(true);
        cowlingTransform.GetChild(currentCowling).gameObject.SetActive(true);
        rimsTransform.GetChild(currentRims).gameObject.SetActive(true);

        garageUI.fbbSprite.GetChild(currentFBumper+1).GetComponent<Button>().Select();
    }

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
    {
        properties = carData.properties;
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
