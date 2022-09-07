using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_GarageInventory : MonoBehaviour
{
    [SerializeField]Inventory inventory;
    [SerializeField] Image inventorySlot;
    readonly List<Transform> inventorySlots = new List<Transform>(40);

    public Button applyButton;
    public TextMeshProUGUI applyButtonText;

    public TextMeshProUGUI inventoryInfoText;

    public static UI_GarageInventory Instance;

    public Button b;

    private void Awake()
    {
        Instance = this;

        for (int i = 0; i < 40; i++)
        {
            Image slot = Instantiate(inventorySlot , this.transform);
            inventorySlots.Add(slot.transform);
        }
        SetInventory(transform.GetComponent<Inventory>());
    }

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
        RefreshGarageInventoryItems();
    }

    public void RefreshGarageInventoryItems()
    {
        int i = 0;
        foreach (Data data in inventory.GetGarageItemList())
        {
            GameObject _item = Instantiate(data.itemPrefab, inventorySlots[i]);
            _item.transform.localPosition = new Vector2(37.5f, 37.5f);
            _item.GetComponent<Image>().sprite = data.sprite;

            switch (data.itemType)
            {                
                case Data.ItemType.part:
                    _item.GetComponent<ItemPrefab>().itemPart = (CarPartData)data;
                    _item.GetComponent<ItemPrefab>().item = data;
                    break;
                case Data.ItemType.color:
                    //TODO
                    break;
                default:
                    break;
            }
            i++;
        }
    }
}
