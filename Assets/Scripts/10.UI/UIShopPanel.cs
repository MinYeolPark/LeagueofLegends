using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class UIShopPanel : MonoBehaviour
{
    public LeagueItemDataBase allItemData;
    public LeagueInventoryData inventory;

    public GameObject shopItemPrefab;    
    public GameObject RecommendedItemsPanel;
    public GameObject AllItemsPanel;
    public GameObject ItemSetsPanel;

    public Button recommendButton;
    public Button allItemButton;
    public Button itemSetsButton;
    public void OnRecommendedItems()
    {
        RecommendedItemsPanel.SetActive(true);
        AllItemsPanel.SetActive(false);
        ItemSetsPanel.SetActive(false);
        //recommendButton.image.color = new Color(0f, 0f, 0f, .0f);        
    }
    public void OnAllItems()
    {
        RecommendedItemsPanel.SetActive(false);
        AllItemsPanel.SetActive(true);
        ItemSetsPanel.SetActive(false);
    }

    public void OnItemSets()
    {
        RecommendedItemsPanel.SetActive(false);
        AllItemsPanel.SetActive(false);
        ItemSetsPanel.SetActive(true);
    }    
}
