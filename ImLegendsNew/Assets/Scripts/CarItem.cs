using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CarItem", menuName = "ScriptableObjects / Item / CarItem")]
public class CarItem : Item
{
    //[System.NonSerialized]public ItemType type = ItemType.car;
    public CarData carData;
}
