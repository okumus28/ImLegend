using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class CarPartUI : MonoBehaviour , ISelectHandler , IDeselectHandler
{
    public CarPartData carPartData;

    public Transform partCarTransform;

    Properties tempProp;
    int tempIndex;

    UI_Property u�_Property;

    private void OnEnable()
    {
        if (PlayerPrefs.GetInt(CarSelection.car.name + carPartData.name) == 1)
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }

        u�_Property = UI_Property.Instance;

    }

    public void OnSelect(BaseEventData eventData)
    {
        tempProp = CarSelection.car.properties.GetValues();

        SelectedPart();

        PropUpdate();
    }

    void SelectedPart()
    {
        tempIndex = 10;

        for (int i = 0; i < partCarTransform.childCount; i++)
        {
            if (partCarTransform.GetChild(i).gameObject.activeSelf == true)
            {
                tempIndex = i;
            }
            partCarTransform.GetChild(i).gameObject.SetActive(i == carPartData.partTransformIndex);
        }
    }

    public void OnDeselect(BaseEventData eventData)
    {
        for (int i = 0; i < partCarTransform.childCount; i++)
        {
            partCarTransform.GetChild(i).gameObject.SetActive(i == tempIndex);
        }

        u�_Property.speedFillBar.color = Color.blue;
        u�_Property.armorFillBar.color = Color.blue;
        u�_Property.fuelTankFillBar.color = Color.blue;
        u�_Property.zombieResistFillBar.color = Color.blue;
        u�_Property.monsterDurationFillBar.color = Color.blue;
        u�_Property.comboDurationFillBar.color = Color.blue;
    }

    void PropUpdate()
    {
        if (tempProp.speed < CarSelection.car.properties.speed)
        {
            u�_Property.speedFillBar.color = Color.green;
            u�_Property.speedText.text = tempProp.speed.ToString() + "+" + (CarSelection.car.properties.speed - tempProp.speed).ToString();
        }
        else if (tempProp.speed > CarSelection.car.properties.speed)
        {
            u�_Property.speedFillBar.color = Color.red;
            u�_Property.speedText.text = tempProp.speed.ToString() + "-" + (tempProp.speed - CarSelection.car.properties.speed).ToString();
        }

        if (tempProp.armor < CarSelection.car.properties.armor)
        {
            u�_Property.armorFillBar.color = Color.green;
            u�_Property.armorText.text = tempProp.armor.ToString() + "+" + (CarSelection.car.properties.armor - tempProp.armor).ToString();
        }
        else if (tempProp.armor > CarSelection.car.properties.armor)
        {
            u�_Property.armorFillBar.color = Color.red;
            u�_Property.armorText.text = tempProp.armor.ToString() + "-" + (tempProp.armor - CarSelection.car.properties.armor).ToString();
        }

        if (tempProp.fuelTank < CarSelection.car.properties.fuelTank)
        {
            u�_Property.fuelTankFillBar.color = Color.green;
            u�_Property.fuelTankText.text = tempProp.fuelTank.ToString() + "+" + (CarSelection.car.properties.fuelTank - tempProp.fuelTank).ToString();
        }
        else if (tempProp.fuelTank > CarSelection.car.properties.fuelTank)
        {
            u�_Property.fuelTankFillBar.color = Color.red;
            u�_Property.fuelTankText.text = tempProp.fuelTank.ToString() + "-" + (tempProp.fuelTank - CarSelection.car.properties.fuelTank).ToString();
        }

        if (tempProp.zombieResist < CarSelection.car.properties.zombieResist)
        {
            u�_Property.zombieResistFillBar.color = Color.green;
            u�_Property.zombieResistText.text = tempProp.zombieResist.ToString() + "+" + (CarSelection.car.properties.zombieResist - tempProp.zombieResist).ToString();
        }
        else if (tempProp.zombieResist > CarSelection.car.properties.zombieResist)
        {
            u�_Property.zombieResistFillBar.color = Color.red;
            u�_Property.zombieResistText.text = tempProp.zombieResist.ToString() + "-" + (tempProp.zombieResist - CarSelection.car.properties.zombieResist).ToString();
        }

        if (tempProp.monsterDuration < CarSelection.car.properties.monsterDuration)
        {
            u�_Property.monsterDurationFillBar.color = Color.green;
            u�_Property.monsterDurationText.text = tempProp.monsterDuration.ToString("f1") + "+" + (CarSelection.car.properties.monsterDuration - tempProp.monsterDuration).ToString("f1");
        }
        else if (tempProp.monsterDuration > CarSelection.car.properties.monsterDuration)
        {
            u�_Property.monsterDurationFillBar.color = Color.red;
            u�_Property.monsterDurationText.text = tempProp.monsterDuration.ToString("f1") + "-" + (tempProp.monsterDuration - CarSelection.car.properties.monsterDuration).ToString("f1");
        }

        if (tempProp.comboDuration < CarSelection.car.properties.comboDuration)
        {
            u�_Property.comboDurationFillBar.color = Color.green;
            u�_Property.comboDurationText.text = tempProp.comboDuration.ToString("f1") + "+" + (CarSelection.car.properties.comboDuration - tempProp.comboDuration).ToString("f1");
        }
        else if (tempProp.comboDuration > CarSelection.car.properties.comboDuration)
        {
            u�_Property.comboDurationFillBar.color = Color.red;
            u�_Property.comboDurationText.text =tempProp.comboDuration.ToString("f1") + "-" + (tempProp.comboDuration - CarSelection.car.properties.comboDuration).ToString("f1");
        }
    }
}
