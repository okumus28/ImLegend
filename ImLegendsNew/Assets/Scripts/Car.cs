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

    [SerializeField] CarData carData;
    Properties properties;

    private void OnEnable()
    {
        properties = carData.properties.GetValues();

        for (int i = 0; partTransformParent != null && i < partTransformParent.childCount; i++)
        {
            PartProperties(partTransformParent.GetChild(i));
        }
        UI_Property.Instance.UI_PropertiesUpdate(this.properties);
    }

    private void OnDisable()
    {
        properties = carData.properties;
    }

    void PartProperties(Transform _partTransform)
    {
        for (int i = 0; i < _partTransform.childCount; i++)
        {
            if (_partTransform.GetChild(i).gameObject.activeSelf == true)
            {
                Properties partProp = _partTransform.GetChild(i).GetComponent<CarPart>().carPartData.properties;
                properties.speed += partProp.speed;
                properties.armor += partProp.armor;
                properties.fuelTank += partProp.fuelTank;
                properties.zombieResist += partProp.zombieResist;
                properties.monsterDuration += partProp.monsterDuration;
                properties.comboDuration += partProp.comboDuration;
                return;
            }
        }
    }
}
