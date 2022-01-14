using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewInventoryData", menuName = "League of Legends/LeagueItem Data/Inventory Data")]
public class LeagueInventoryData : ScriptableObject
{
    public static readonly int inventorySize=7;
    public Inventory inventory;
    public LeagueItemDataBase dataBase;

    public void AddItem(Item _item, int _amount)
    {
        if (_item.buffs.Length > 0)
        {
            SetEmptySlot(_item, _amount);
            return;
        }

        for (int i = 0; i < inventory.items.Length; i++)
        {
            if (inventory.items[i].slotID == _item.itemId)
            {
                inventory.items[i].AddAmount(_amount);
                return;
            }
        }
        SetEmptySlot(_item, _amount);

    }
    public InventorySlot SetEmptySlot(Item _item, int _amount)
    {
        for (int i = 0; i < inventory.items.Length; i++)
        {
            if (inventory.items[i].slotID <= -1)
            {
                inventory.items[i].UpdateSlot(_item.itemId, _item, _amount);
                return inventory.items[i];
            }
        }
        //set up functionality for full inventory
        return null;
    }

    public void MoveItem(InventorySlot item1, InventorySlot item2)
    {
        InventorySlot temp = new InventorySlot(item2.slotID, item2.item, item2.amount);
        item2.UpdateSlot(item1.slotID, item1.item, item1.amount);
        item1.UpdateSlot(temp.slotID, temp.item, temp.amount);
    }


    public void RemoveItem(Item _item)
    {
        for (int i = 0; i < inventory.items.Length; i++)
        {
            if (inventory.items[i].item == _item)
            {
                inventory.items[i].UpdateSlot(-1, null, 0);
            }
        }
    }
}

[System.Serializable]
public class Inventory
{
    public InventorySlot[] items = new InventorySlot[LeagueInventoryData.inventorySize];
}

[System.Serializable]
public class InventorySlot
{
    public int slotID = -1;
    public Item item;
    public int amount;
    public InventorySlot()
    {
        slotID = -1;
        item = null;
        amount = 0;
    }
    public InventorySlot(int _id, Item _item, int _amount)
    {
        slotID = _id;
        item = _item;
        amount = _amount;
    }
    public void UpdateSlot(int _id, Item _item, int _amount)
    {
        slotID = _id;
        item = _item;
        amount = _amount;
    }
    public void AddAmount(int value)
    {
        amount += value;
    }
}
