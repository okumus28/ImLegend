using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CarPartUI : MonoBehaviour , ISelectHandler , IDeselectHandler
{
    public CarPartData carPartData;

    public Transform partCarTransform;

    Properties tempProp;
    int tempIndex;

    UI_Property uý_Property;

    private void OnEnable()
    {
        //PlayerPrefs.SetInt(CarSelection.car.name + carPartData.name,0);

        //Kilit Resmi On/Off
        if (PlayerPrefs.GetInt(CarSelection.car.name + carPartData.name) == 1)
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }

        uý_Property = UI_Property.Instance;

    }

    public void OnSelect(BaseEventData eventData)
    {
        GetACtivePartIndex();

        if (PlayerPrefs.GetInt(CarSelection.car.name + carPartData.name) == 1)
        {
            if (tempIndex == carPartData.partTransformIndex)
            {
                GarageUI.instance.appplyButton.interactable = false;
                GarageUI.instance.appplyButtonText.text = "Equiped";
            }
            else
            {
                GarageUI.instance.appplyButton.interactable = true;
                GarageUI.instance.appplyButtonText.text = "Equip";
                GarageUI.instance.appplyButton.onClick.RemoveAllListeners();
                GarageUI.instance.appplyButton.onClick.AddListener(() => EquipEvent());
            }
        }
        else
        {
            GarageUI.instance.appplyButton.interactable = true;
            GarageUI.instance.appplyButtonText.text = "Buy";
            GarageUI.instance.appplyButton.onClick.RemoveAllListeners();
            GarageUI.instance.appplyButton.onClick.AddListener(() => BuyEvent());
        }

        tempProp = CarSelection.car.properties.GetValues();

        SelectedPart();

        PropUpdate();
    }

    public void EquipEvent()
    {
        CarSelection.car.SetIndex(partCarTransform, carPartData.partTransformIndex);
        tempProp = CarSelection.car.properties.GetValues();
        GetComponent<Button>().Select();
    }

    public void BuyEvent()
    {
        PlayerPrefs.SetInt(CarSelection.car.name + carPartData.name, 1);
        transform.GetChild(0).gameObject.SetActive(false);
        CarSelection.car.SetIndex(partCarTransform, carPartData.partTransformIndex);
        tempProp = CarSelection.car.properties.GetValues();
        GetComponent<Button>().Select();
    }


    void SelectedPart()
    {
        for (int i = 0; i < partCarTransform.childCount; i++)
        {          
            partCarTransform.GetChild(i).gameObject.SetActive(i == carPartData.partTransformIndex);
        }
    }

    void GetACtivePartIndex()
    {
        tempIndex = 10;

        for (int i = 0; i < partCarTransform.childCount; i++)
        {
            if (partCarTransform.GetChild(i).gameObject.activeSelf == true)
            {
                tempIndex = i;
            }
        }
    }

    public void OnDeselect(BaseEventData eventData)
    {
        for (int i = 0; i < partCarTransform.childCount; i++)
        {
            partCarTransform.GetChild(i).gameObject.SetActive(i == tempIndex);
        }

        uý_Property.speedFillBar.color = Color.blue;
        uý_Property.armorFillBar.color = Color.blue;
        uý_Property.fuelTankFillBar.color = Color.blue;
        uý_Property.zombieResistFillBar.color = Color.blue;
        uý_Property.monsterDurationFillBar.color = Color.blue;
        uý_Property.comboDurationFillBar.color = Color.blue;
    }

    void PropUpdate()
    {
        if (tempProp.speed < CarSelection.car.properties.speed)
        {
            uý_Property.speedFillBar.color = Color.green;
            uý_Property.speedText.text = tempProp.speed.ToString() + "+" + (CarSelection.car.properties.speed - tempProp.speed).ToString();
        }
        else if (tempProp.speed > CarSelection.car.properties.speed)
        {
            uý_Property.speedFillBar.color = Color.red;
            uý_Property.speedText.text = tempProp.speed.ToString() + "-" + (tempProp.speed - CarSelection.car.properties.speed).ToString();
        }

        if (tempProp.armor < CarSelection.car.properties.armor)
        {
            uý_Property.armorFillBar.color = Color.green;
            uý_Property.armorText.text = tempProp.armor.ToString() + "+" + (CarSelection.car.properties.armor - tempProp.armor).ToString();
        }
        else if (tempProp.armor > CarSelection.car.properties.armor)
        {
            uý_Property.armorFillBar.color = Color.red;
            uý_Property.armorText.text = tempProp.armor.ToString() + "-" + (tempProp.armor - CarSelection.car.properties.armor).ToString();
        }

        if (tempProp.fuelTank < CarSelection.car.properties.fuelTank)
        {
            uý_Property.fuelTankFillBar.color = Color.green;
            uý_Property.fuelTankText.text = tempProp.fuelTank.ToString() + "+" + (CarSelection.car.properties.fuelTank - tempProp.fuelTank).ToString();
        }
        else if (tempProp.fuelTank > CarSelection.car.properties.fuelTank)
        {
            uý_Property.fuelTankFillBar.color = Color.red;
            uý_Property.fuelTankText.text = tempProp.fuelTank.ToString() + "-" + (tempProp.fuelTank - CarSelection.car.properties.fuelTank).ToString();
        }

        if (tempProp.zombieResist < CarSelection.car.properties.zombieResist)
        {
            uý_Property.zombieResistFillBar.color = Color.green;
            uý_Property.zombieResistText.text = tempProp.zombieResist.ToString() + "+" + (CarSelection.car.properties.zombieResist - tempProp.zombieResist).ToString();
        }
        else if (tempProp.zombieResist > CarSelection.car.properties.zombieResist)
        {
            uý_Property.zombieResistFillBar.color = Color.red;
            uý_Property.zombieResistText.text = tempProp.zombieResist.ToString() + "-" + (tempProp.zombieResist - CarSelection.car.properties.zombieResist).ToString();
        }

        if (tempProp.monsterDuration < CarSelection.car.properties.monsterDuration)
        {
            uý_Property.monsterDurationFillBar.color = Color.green;
            uý_Property.monsterDurationText.text = tempProp.monsterDuration.ToString("f1") + "+" + (CarSelection.car.properties.monsterDuration - tempProp.monsterDuration).ToString("f1");
        }
        else if (tempProp.monsterDuration > CarSelection.car.properties.monsterDuration)
        {
            uý_Property.monsterDurationFillBar.color = Color.red;
            uý_Property.monsterDurationText.text = tempProp.monsterDuration.ToString("f1") + "-" + (tempProp.monsterDuration - CarSelection.car.properties.monsterDuration).ToString("f1");
        }

        if (tempProp.comboDuration < CarSelection.car.properties.comboDuration)
        {
            uý_Property.comboDurationFillBar.color = Color.green;
            uý_Property.comboDurationText.text = tempProp.comboDuration.ToString("f1") + "+" + (CarSelection.car.properties.comboDuration - tempProp.comboDuration).ToString("f1");
        }
        else if (tempProp.comboDuration > CarSelection.car.properties.comboDuration)
        {
            uý_Property.comboDurationFillBar.color = Color.red;
            uý_Property.comboDurationText.text =tempProp.comboDuration.ToString("f1") + "-" + (tempProp.comboDuration - CarSelection.car.properties.comboDuration).ToString("f1");
        }
    }
}
