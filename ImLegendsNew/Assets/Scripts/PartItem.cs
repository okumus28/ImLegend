using UnityEngine;

[CreateAssetMenu(fileName = "PartItem", menuName = "ScriptableObjects / Item / PartItem")]
public class PartItem : Item
{
    //[System.NonSerialized] public ItemType itemType = ItemType.part;
    public CarPartData carPartData;
}
