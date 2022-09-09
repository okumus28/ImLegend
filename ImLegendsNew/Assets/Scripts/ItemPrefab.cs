using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemPrefab : MonoBehaviour , ISelectHandler , IDeselectHandler
{
    public Data item;

    public CarData itemCar;
    public CarPartData itemPart;
    public ChestData itemChest;

    [SerializeField] int descriptionPanelHeight;    

    public void PartDecription()
    {
        descriptionPanelHeight = 0;
        if (itemPart == null)
        {
            return;
        }
        item.description += item.name + "\n";
        item.description += "Level " + itemPart.partLevel + "\n\n";

        descriptionPanelHeight = 120;

        if (itemPart.properties.speed != 0)
        {
            item.description += "Speed : " + itemPart.properties.speed + "\n";
            descriptionPanelHeight += 40;
        }
        if (itemPart.properties.armor != 0)
        {
            item.description += "Armor : " + itemPart.properties.armor + "\n";
            descriptionPanelHeight += 40;
        }
        if (itemPart.properties.fuelTank != 0)
        {
            item.description += "Fuel Tank : " + itemPart.properties.fuelTank + "\n";
            descriptionPanelHeight += 40;
        }
        if (itemPart.properties.zombieResist != 0)
        {
            item.description += "Zombie Resist : " + itemPart.properties.zombieResist + "\n";
            descriptionPanelHeight += 40;
        }
        if (itemPart.properties.monsterDuration != 0)
        {
            item.description += "Monster D. : " + itemPart.properties.monsterDuration + "\n";
            descriptionPanelHeight += 40;
        }
        if (itemPart.properties.comboDuration != 0)
        {
            item.description += "Combo D. : " + itemPart.properties.comboDuration + "\n";
            descriptionPanelHeight += 40;
        }
    }

    //public void CarDecription()
    //{
    //    if (itemCar == null)
    //    {
    //        return;
    //    }
    //    if (itemCar.properties.speed != 0)
    //    {
    //        item.description += "Speed : " + itemCar.properties.speed + "\n";
    //    }
    //    if (itemCar.properties.armor != 0)
    //    {
    //        item.description += "Armor : " + itemCar.properties.armor + "\n";
    //    }
    //    if (itemCar.properties.fuelTank != 0)
    //    {
    //        item.description += "Fuel Tank : " + itemCar.properties.fuelTank + "\n";
    //    }
    //    if (itemCar.properties.zombieResist != 0)
    //    {
    //        item.description += "Zombie Resist : " + itemCar.properties.zombieResist + "\n";
    //    }
    //    if (itemCar.properties.monsterDuration != 0)
    //    {
    //        item.description += "Monster D. : " + itemCar.properties.monsterDuration + "\n";
    //    }
    //    if (itemCar.properties.comboDuration != 0)
    //    {
    //        item.description += "Combo D. : " + itemCar.properties.comboDuration + "\n";
    //    }
    //}

    public void ApplyButtton()
    {
        switch (item.itemType)
        {
            case Data.ItemType.chest:
                Debug.Log("CHEST OPEN");
                break;
            case Data.ItemType.part:
                ModifyPart(CarSelection.car, itemPart);
                break;
            case Data.ItemType.car:
                Debug.Log("CAR UNLOCK");
                break;
            case Data.ItemType.color:
                Debug.Log("COLOR CHANGE");
                break;
            default:
                break;
        }
    }

    void ModifyPart(Car _car , CarPartData _partData)
    {
        //switch (_partData.partTransform)
        //{
        //    case CarPartData.PartTransform.fBumper:
        //        //UpdateParts(_car, _car.fBumperTransform, _partData);
        //        break;
        //    case CarPartData.PartTransform.bBumper:
        //        UpdateParts(_car, _car.bBumperTransform, _partData);
        //        break;
        //    case CarPartData.PartTransform.side:
        //        UpdateParts(_car, _car.sidesTransform, _partData);
        //        break;
        //    case CarPartData.PartTransform.window:
        //        UpdateParts(_car, _car.windowsTransform, _partData);
        //        break;
        //    case CarPartData.PartTransform.cowling:
        //        UpdateParts(_car, _car.cowlingTransform, _partData);
        //        break;
        //    case CarPartData.PartTransform.rim:
        //        UpdateParts(_car, _car.rimsTransform, _partData);
        //        break;
        //    default:
        //        break;
        //}
    }

    void UpdateParts(Car _car , Transform _partsTransform , CarPartData _carPartData)
    {
        //for (int i = 0; i < _partsTransform.childCount; i++)
        //{
        //    _car.fBumperTransform.GetChild(i).gameObject.SetActive(i == _carPartData.partTransformIndex);
        //}
    }

    public void OnSelect(BaseEventData eventData)
    {
        UI_GarageInventory.Instance.applyButton.onClick.RemoveAllListeners();
        UI_GarageInventory.Instance.applyButton.onClick.AddListener(() => ApplyButtton());

        item.description = "";
        PartDecription();
        UI_GarageInventory.Instance.inventoryInfoText.transform.parent.GetComponent<RectTransform>().sizeDelta = new Vector2(300, descriptionPanelHeight);
        UI_GarageInventory.Instance.inventoryInfoText.text = item.description;

        UI_GarageInventory.Instance.inventoryInfoText.transform.parent.gameObject.SetActive(true);
        Debug.Log("select");

        //if (itemPart.partModel != null)
        //{
        //    switch (itemPart.partTransform)
        //    {
        //        case CarPartData.PartTransform.fBumper:
        //            for (int i = 0; i < CarSelection.car.fBumperTransform.childCount; i++)
        //            {
        //                CarSelection.car.fBumperTransform.GetChild(i).gameObject.SetActive(i == itemPart.partTransformIndex);
        //            }
        //            CarSelection.car.fBumperTransform.GetChild(itemPart.partTransformIndex).GetComponent<CarPart>().carPartData = itemPart;
        //            break;
        //        case CarPartData.PartTransform.bBumper:
        //            break;
        //        case CarPartData.PartTransform.side:
        //            break;
        //        case CarPartData.PartTransform.window:
        //            break;
        //        case CarPartData.PartTransform.cowling:
        //            break;
        //        case CarPartData.PartTransform.rim:
        //            break;
        //        default:
        //            break;
        //    }
        //}
    }

    public void OnDeselect(BaseEventData eventData)
    {
        UI_GarageInventory.Instance.inventoryInfoText.transform.parent.gameObject.SetActive(false);
        Debug.Log("deselect");
    }
}
