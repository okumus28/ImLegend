using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] List<Data> itemList;
    [SerializeField] List<Data> partItemList;

    public void AddItem(Data item)
    {
        itemList.Add(item);
    }

    public void RemoveItem(Data item)
    {
        itemList.Remove(item);
    }

    public List<Data> GetItemlist()
    {
        return itemList;
    }

    public List<Data> GetGarageItemList()
    {
        partItemList.Clear();
        for (int i = 0; i < itemList.Count; i++)
        {            
            if (itemList[i].itemType == Data.ItemType.part || itemList[i].itemType == Data.ItemType.color)
            {                
                partItemList.Add(itemList[i]);
            }
        }
        return partItemList;
    }
}
