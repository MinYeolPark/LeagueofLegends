using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class UIShopPanel : MonoBehaviour
{
    public GameObject shopItemPrefab;
    public ItemShop itemShop;
    private List<LeagueItemData> shopInventory;
    public GameObject RecommendedItemsPanel;
    public GameObject AllItemsPanel;
    public GameObject ItemSetsPanel;

    public void OnRecommendedItems()
    {
        RecommendedItemsPanel.SetActive(true);
        AllItemsPanel.SetActive(false);
        ItemSetsPanel.SetActive(false);
        Debug.Log("OnRecommendItems");
        if (RecommendedItemsPanel.activeSelf == true)
        {
            RecommendedItemsPanel.SetActive(false);
        }
        else
        {
            RecommendedItemsPanel.SetActive(true);
        }
        
    }
    public void OnAllItems()
    {
        RecommendedItemsPanel.SetActive(false);
        AllItemsPanel.SetActive(true);
        ItemSetsPanel.SetActive(false);
        Debug.Log("OnAllItem");
        if (AllItemsPanel.activeSelf == true)
        {
            AllItemsPanel.SetActive(false);
        }
        else
        {
            AllItemsPanel.SetActive(true);
        }
    }

    public void OnItemSets()
    {
        RecommendedItemsPanel.SetActive(false);
        AllItemsPanel.SetActive(false);
        ItemSetsPanel.SetActive(true);

        Debug.Log("OnItemSets");
        if (ItemSetsPanel.activeSelf == true)
        {
            ItemSetsPanel.SetActive(false);
        }
        else
        {
            ItemSetsPanel.SetActive(true);
        }
    }    
}
