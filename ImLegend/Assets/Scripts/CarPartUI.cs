using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CarPartUI : MonoBehaviour , ISelectHandler , IDeselectHandler
{
    public CarPartData carPartData;

    public Transform partCarTransform;

    Properties tempProp;
    int tempIndex;

    UI_Property ui_Property;

    [SerializeField]
    bool purchase;

    private void OnEnable()
    {
        tempIndex = PlayerPrefs.GetInt("Current" + partCarTransform + CarSelection.car.carData.name);
        purchase = PlayerPrefs.GetInt("_PurchasePart" + carPartData.name + CarSelection.car.carData.name) == 1;

        Debug.Log(("_PurchasePart" + carPartData.name + CarSelection.car.carData.name));
        Debug.Log(tempIndex + " " + purchase);

        //Kilit Resmi On/Off
        if (purchase)
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
        if (tempIndex == carPartData.partTransformIndex)
        {
            transform.GetChild(1).gameObject.SetActive(true);
            GetComponent<Button>().Select();
        }

        ui_Property = UI_Property.instance;

    }

    public void OnSelect(BaseEventData eventData)
    {
        tempIndex = PlayerPrefs.GetInt("Current" + partCarTransform + CarSelection.car.carData.name);
        purchase = PlayerPrefs.GetInt("_PurchasePart" + carPartData.name + CarSelection.car.carData.name) == 1;

        if (purchase)
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
            GarageUI.instance.appplyButton.interactable = GarageUI.instance.cash >= carPartData.price;
            GarageUI.instance.appplyButtonText.text = carPartData.price.ToString();
            GarageUI.instance.appplyButton.onClick.RemoveAllListeners();
            GarageUI.instance.appplyButton.onClick.AddListener(() => BuyEvent());
        }

        tempProp = CarSelection.car.properties.GetValues();

        SelectedPart();

        PropUpdate();
    }

    public void EquipEvent()
    {

        for (int i = 0; i < transform.parent.childCount; i++)
        {
            transform.parent.GetChild(i).GetChild(1).gameObject.SetActive(false);
        }
        SelectedPart();
        transform.GetChild(1).gameObject.SetActive(true);
        PlayerPrefs.SetInt("Current" + partCarTransform + CarSelection.car.carData.name , carPartData.partTransformIndex);
        tempProp = CarSelection.car.properties.GetValues();
        GetComponent<Button>().Select();
    }

    public void BuyEvent()
    {
        GarageUI.instance.Cash(-carPartData.price);
        PlayerPrefs.SetInt("_PurchasePart" + carPartData.name + CarSelection.car.carData.name, 1);
        transform.GetChild(0).gameObject.SetActive(false);
        EquipEvent();
    }


    void SelectedPart()
    {
        for (int i = 0; i < partCarTransform.childCount; i++)
        {   
            partCarTransform.GetChild(i).gameObject.SetActive(false);
        }
        partCarTransform.GetChild(carPartData.partTransformIndex).gameObject.SetActive(true);
    }

    void GetActivePartIndex()
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

        ui_Property.speedFillBar.color = Color.blue;
        ui_Property.armorFillBar.color = Color.blue;
        ui_Property.fuelTankFillBar.color = Color.blue;
        ui_Property.zombieResistFillBar.color = Color.blue;
        ui_Property.monsterDurationFillBar.color = Color.blue;
        ui_Property.comboDurationFillBar.color = Color.blue;
    }

    void PropUpdate()
    {
        if (tempProp.speed < CarSelection.car.properties.speed)
        {
            ui_Property.speedFillBar.color = Color.green;
            ui_Property.speedText.text = tempProp.speed.ToString() + "+" + (CarSelection.car.properties.speed - tempProp.speed).ToString();
        }
        else if (tempProp.speed > CarSelection.car.properties.speed)
        {
            ui_Property.speedFillBar.color = Color.red;
            ui_Property.speedText.text = tempProp.speed.ToString() + "-" + (tempProp.speed - CarSelection.car.properties.speed).ToString();
        }

        if (tempProp.armor < CarSelection.car.properties.armor)
        {
            ui_Property.armorFillBar.color = Color.green;
            ui_Property.armorText.text = tempProp.armor.ToString() + "+" + (CarSelection.car.properties.armor - tempProp.armor).ToString();
        }
        else if (tempProp.armor > CarSelection.car.properties.armor)
        {
            ui_Property.armorFillBar.color = Color.red;
            ui_Property.armorText.text = tempProp.armor.ToString() + "-" + (tempProp.armor - CarSelection.car.properties.armor).ToString();
        }

        if (tempProp.fuelTank < CarSelection.car.properties.fuelTank)
        {
            ui_Property.fuelTankFillBar.color = Color.green;
            ui_Property.fuelTankText.text = tempProp.fuelTank.ToString() + "+" + (CarSelection.car.properties.fuelTank - tempProp.fuelTank).ToString();
        }
        else if (tempProp.fuelTank > CarSelection.car.properties.fuelTank)
        {
            ui_Property.fuelTankFillBar.color = Color.red;
            ui_Property.fuelTankText.text = tempProp.fuelTank.ToString() + "-" + (tempProp.fuelTank - CarSelection.car.properties.fuelTank).ToString();
        }

        if (tempProp.zombieResist < CarSelection.car.properties.zombieResist)
        {
            ui_Property.zombieResistFillBar.color = Color.green;
            ui_Property.zombieResistText.text = tempProp.zombieResist.ToString() + "+" + (CarSelection.car.properties.zombieResist - tempProp.zombieResist).ToString();
        }
        else if (tempProp.zombieResist > CarSelection.car.properties.zombieResist)
        {
            ui_Property.zombieResistFillBar.color = Color.red;
            ui_Property.zombieResistText.text = tempProp.zombieResist.ToString() + "-" + (tempProp.zombieResist - CarSelection.car.properties.zombieResist).ToString();
        }

        if (tempProp.monsterDuration < CarSelection.car.properties.monsterDuration)
        {
            ui_Property.monsterDurationFillBar.color = Color.green;
            ui_Property.monsterDurationText.text = tempProp.monsterDuration.ToString("f1") + "+" + (CarSelection.car.properties.monsterDuration - tempProp.monsterDuration).ToString("f1");
        }
        else if (tempProp.monsterDuration > CarSelection.car.properties.monsterDuration)
        {
            ui_Property.monsterDurationFillBar.color = Color.red;
            ui_Property.monsterDurationText.text = tempProp.monsterDuration.ToString("f1") + "-" + (tempProp.monsterDuration - CarSelection.car.properties.monsterDuration).ToString("f1");
        }

        if (tempProp.comboDuration < CarSelection.car.properties.comboDuration)
        {
            ui_Property.comboDurationFillBar.color = Color.green;
            ui_Property.comboDurationText.text = tempProp.comboDuration.ToString("f1") + "+" + (CarSelection.car.properties.comboDuration - tempProp.comboDuration).ToString("f1");
        }
        else if (tempProp.comboDuration > CarSelection.car.properties.comboDuration)
        {
            ui_Property.comboDurationFillBar.color = Color.red;
            ui_Property.comboDurationText.text =tempProp.comboDuration.ToString("f1") + "-" + (tempProp.comboDuration - CarSelection.car.properties.comboDuration).ToString("f1");
        }
    }
}
