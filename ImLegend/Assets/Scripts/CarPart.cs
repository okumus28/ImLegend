using UnityEngine;
using UnityEngine.SceneManagement;

public class CarPart : MonoBehaviour
{
    public CarPartData carPartData;
    private void OnEnable()
    {
        CarSelection.car.properties = Properties.AddPropertiesValue(CarSelection.car.properties, carPartData.properties , 1);
        if (SceneManager.GetActiveScene().name == "GarageScene 1")
            CarSelection.car.uiProperty.UI_PropertiesUpdate(CarSelection.car.properties);
    }

    private void OnDisable()
    {
        CarSelection.car.properties = Properties.AddPropertiesValue(CarSelection.car.properties, carPartData.properties , -1);
        if (SceneManager.GetActiveScene().name == "GarageScene 1")
            CarSelection.car.uiProperty.UI_PropertiesUpdate(CarSelection.car.properties);
    }
}
