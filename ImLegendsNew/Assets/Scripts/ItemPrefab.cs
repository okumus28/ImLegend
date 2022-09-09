using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemPrefab : MonoBehaviour 
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
}
