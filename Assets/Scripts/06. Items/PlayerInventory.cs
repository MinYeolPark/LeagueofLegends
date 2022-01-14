using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private LeagueInventoryData inventoryData;
    [SerializeField] private List<InventorySlot> inventorySlots;
    [SerializeField] private LeagueItemDataBase leagueAllItem;

    private void Start()
    {
        InitializeInventory();
    }

    void InitializeInventory()
    {
        for(int i=0;i<inventoryData.inventory.items.Length;i++)
        {
            inventoryData.inventory.items[i].item.itemId = -1;
            inventoryData.inventory.items[i].item.Name = "";
        }
    }
}
