using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class UI_Inventory : MonoBehaviour
{
    [SerializeField]Inventory inventory;
    [SerializeField] Image inventorySlot;
    readonly List<Transform> inventorySlots = new List<Transform>(40);

    public Button applyButton;
    public TextMeshProUGUI applyButtonText;

    public static UI_Inventory Instance;

    private void Awake()
    {
        Instance = this;

        for (int i = 0; i < 35; i++)
        {
            Image slot = Instantiate(inventorySlot , this.transform);
            inventorySlots.Add(slot.transform);
        }
        SetInventory(transform.GetComponent<Inventory>());
    }

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
        RefreshInventoryItems();
    }

    public void RefreshInventoryItems()
    {
        int i = 0;
        foreach (Item item in inventory.GetItemlist())
        {
            GameObject _item = Instantiate(item.itemPrefab, inventorySlots[i]);
            _item.transform.localPosition = new Vector2(47.5f, 47.5f);
            _item.GetComponent<Image>().sprite = item.sprite;
            _item.GetComponent<ItemPrefab>().item = item;
            i++;
        }
    }
}
