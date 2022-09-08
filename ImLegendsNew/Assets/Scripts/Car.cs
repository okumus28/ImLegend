using UnityEngine;

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

    [SerializeField] GarageUI garageUI;

    private void OnEnable()
    {
        properties = carData.properties.GetValues();

        garageUI.SpriteUpdate(fBumperTransform , garageUI.fbbSprite);
        garageUI.SpriteUpdate(bBumperTransform , garageUI.bbbSprite);
        garageUI.SpriteUpdate(sidesTransform , garageUI.sidesSprite);
        garageUI.SpriteUpdate(windowsTransform , garageUI.windowsSprite);
        garageUI.SpriteUpdate(cowlingTransform , garageUI.cowlingsSprite);
        garageUI.SpriteUpdate(rimsTransform , garageUI.rimsSprite);

        fBumperTransform.GetChild(0).gameObject.SetActive(true);
        bBumperTransform.GetChild(3).gameObject.SetActive(true);
        sidesTransform.GetChild(4).gameObject.SetActive(true);

        //UI_Property.Instance.UI_PropertiesUpdate(properties);
    }

    private void OnDisable()
    {
        properties = carData.properties;
    }
}
