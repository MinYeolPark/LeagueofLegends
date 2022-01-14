using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySytstem : MonoBehaviour
{
    private Dictionary<LeagueItemData, InventoryItem> itemDictionary;
    public List<InventoryItem> inventory { get; private set; }

    private void Awake()
    {
        itemDictionary = new Dictionary<LeagueItemData, InventoryItem>();
        inventory = new List<InventoryItem>();
    }

    public void Add(LeagueItemData refItemData)
    {
        if (itemDictionary.TryGetValue(refItemData, out InventoryItem value))
        {
            value.AddToStack();
        }
        else
        {
            InventoryItem newItem = new InventoryItem(refItemData);
            inventory.Add(newItem);
            itemDictionary.Add(refItemData, newItem);
        }
    }

    public void Remove(LeagueItemData refItemData)
    {
        if (itemDictionary.TryGetValue(refItemData, out InventoryItem value))
        {
            value.RemoveFromStack();

            if (value.stackSize == 0)
            {
                inventory.Remove(value);
                itemDictionary.Remove(refItemData);
            }
        }
    }

    public InventoryItem Get(LeagueItemData refItemData)
    {
        if (itemDictionary.TryGetValue(refItemData, out InventoryItem value))
        {
            return value;
        }

        return null;
    }
}

    [SerializeField]
public class InventoryItem
{
    public LeagueItemData data { get; private set; }
    public int stackSize { get; private set; }

    public InventoryItem(LeagueItemData itemData)
    {
        data = itemData;
        AddToStack();
    }

    public void AddToStack()
    {
        stackSize++;
    }

    public void RemoveFromStack()
    {
        stackSize--;
    }    
}