using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] CarData carData;

    private void OnEnable()
    {
        UI_Property.Instance.UI_PropertiesUpdate(carData.properties);
    }
}
