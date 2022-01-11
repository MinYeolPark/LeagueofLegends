using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIShopPanel : MonoBehaviour
{
    public GameObject shopItemPrefab;
    public GridLayout shopLayout;
    public ItemShop itemShop;
    private List<LeagueItemData> shopInventory;

    private void Start()
    {
        shopInventory = itemShop.shopInventory;

        OnAllItems();
    }
    public void OnRecommendedItems()
    {

    }
    public void OnAllItems()
    {
        for(int i=0;i<shopInventory.Capacity;i++)
        {
            GameObject item = Instantiate(shopItemPrefab, transform);

            ItemShopSlot shopSlot = item.GetComponent<ItemShopSlot>();
            item.GetComponent<ItemShopSlot>().itemIcon.sprite = shopInventory[i].portraitImage;
        }
        
    }

    public void OnItemSets()
    {

    }    
}
