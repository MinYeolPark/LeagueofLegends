using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    public Transform[] inventorySlots;
    public GameObject[] inventoryItems;
    public GameObject[] quickItems;

    public GameObject emyptyObject;
    public int inventorySize = 7;

    List<KeyValuePair<int, GameObject>> items = new List<KeyValuePair<int, GameObject>>();
    List<KeyValuePair<int, int>> itemCount = new List<KeyValuePair<int, int>>();

    private void Start()
    {
        InitilizeInventory();
    }

    public void OnActiveItem1()
    {
        GameObject test = new GameObject();
        test.name = "Doran Sword";
        AddToInventory(3, test);
        Debug.Log("OnActiveItem1");
    }
    public void OnActiveItem2() {
        GameObject test = new GameObject();
        test.name = "Doran Sword";
        RemoveFromInventory(2, test);
        Debug.Log("OnActiveItem1");
    }
    public void OnActiveItem3() { }
    public void OnActiveItem4() { }
    public void OnActiveItem5() { }
    public void OnActiveItem6() { }
    public void OnActiveItem7() { }

    void InitilizeInventory()
    {
        inventoryItems = new GameObject[inventorySize];

        for (int i = 0; i < inventorySize; i++)
        {
            inventoryItems[i] = emyptyObject;
            items.Add(new KeyValuePair<int, GameObject>(i, inventoryItems[i]));
            itemCount.Add(new KeyValuePair<int, int>(i, 0));

            if(i<quickItems.Length)
            {
                quickItems[i] = inventoryItems[i];
            }
        }        
    }

    void AddToInventory(int howMany, GameObject newItem)
    {
        for (int i = 0; i < inventoryItems.Length;i++)
        {
            //If Inventory Gameobject equals Empty
            if(inventoryItems[i].name!="Empty")                     
            {
                //Empty to translate new Name
                if(inventoryItems[i].name==newItem.name)
                {
                    //Add how many new item
                    int val = itemCount[i].Value + howMany;
                    itemCount[i] = new KeyValuePair<int, int>(itemCount[i].Key, val);
                }
                break;
            }
            else
            {
                int val = itemCount[i].Value + howMany;
                inventoryItems[i] = newItem;

                //Consumable item value add
                items.Add(new KeyValuePair<int, GameObject>(i, newItem));
                itemCount.Add(new KeyValuePair<int, int>(i, val));
                break;
            }
        }
    }

    void RemoveFromInventory(int howMany, GameObject Item)
    {
        for (int i = 0; i < items.Capacity; i++)
        {
            if (inventoryItems[i].name != "Empty")
            {
                if (inventoryItems[i].name == Item.name)
                {
                    int val = itemCount[i].Value - howMany;
                    itemCount[i] = new KeyValuePair<int, int>(itemCount[i].Key, val);
                }
                if (itemCount[i].Value <= 0)
                {
                    inventoryItems[i] = emyptyObject;
                    items[i] = new KeyValuePair<int, GameObject>(i, emyptyObject);
                    itemCount[i] = new KeyValuePair<int, int>(itemCount[i].Key, 0);
                }
                break;
            }            
        }
    }

    void UpdateQuickItem(GameObject newItem, int quickInput)
    { 
        if(quickItems[quickInput].name!=newItem.name)
        {
            if(quickInput<quickItems.Length)
            {
                quickItems[quickInput] = newItem;
            }
        }
    }
}
