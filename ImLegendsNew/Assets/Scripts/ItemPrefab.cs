using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemPrefab : MonoBehaviour , IPointerClickHandler
{
    public Item item;
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("ON CLÝCKK");
        switch (item.itemType)
        {
            
            case Item.ItemType.chest:
                UI_Inventory.Instance.applyButtonText.text = "OPEN";
                break;
            case Item.ItemType.part:
                UI_Inventory.Instance.applyButtonText.text = "MODIFY";
                break;
            case Item.ItemType.car:
                UI_Inventory.Instance.applyButtonText.text = "UNLOCK / SELL";
                break;
            case Item.ItemType.color:
                UI_Inventory.Instance.applyButtonText.text = "PAINT";
                break;
            default:
                break;
        }
    }
}
