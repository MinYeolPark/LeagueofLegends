using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class RecommendItemPanel : MonoBehaviour
{
    public GridLayoutGroup gridLayout;
    public GameObject itemSlotPrefab;
    UIShopPanel uiShopPanel;
    LeagueItemDataBase itemDataBase;
    LeagueInventoryData inventory;

    Dictionary<GameObject, InventorySlot> itemsDisplayed = new Dictionary<GameObject, InventorySlot>();

    //public item
    void Start()
    {
        uiShopPanel = GetComponentInParent<UIShopPanel>();
        itemDataBase = uiShopPanel.allItemData;
        inventory = uiShopPanel.inventory;

        CreateSlots();
    }

    public void CreateSlots()
    {
        itemsDisplayed = new Dictionary<GameObject, InventorySlot>();

        for (int i = 0; i < itemDataBase.leagueItems.Count; i++)
        {            
            var obj = Instantiate(itemSlotPrefab, Vector3.zero, Quaternion.identity, transform);            
            obj.GetComponent<ItemShopSlot>().itemData = itemDataBase.leagueItems[i];
            //obj.GetComponent<ItemShopSlot>().itemData = itemDataBase.leagueItems[0].CreateItem();
            //obj.GetComponent<ItemShopSlot>().itemIcon.sprite = itemDataBase.leagueItems[i].portraitImage;
            
        }
    }
}

    #region
//    public void UpdateSlots()
//    {
//        foreach (KeyValuePair<GameObject, InventorySlot> _slot in itemsDisplayed)
//        {
//            if (_slot.Value.ID >= 0)
//            {
//                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.dataBase.getItem[_slot.Value.item.Id].portraitImage;
//                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
//                _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = _slot.Value.amount == 1 ? "" : _slot.Value.amount.ToString("n0");
//            }
//            else
//            {
//                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
//                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
//                _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = "";
//            }
//        }
 //   }


//    public void OnEnter(GameObject obj)
//    {

//    }
//    public void OnExit(GameObject obj)
//    {

//    }
//    public void OnDragStart(GameObject obj)
//    {
//        var mouseObject = new GameObject();
//        var rt = mouseObject.AddComponent<RectTransform>();
//        rt.sizeDelta = new Vector2(50, 50);
//        mouseObject.transform.SetParent(transform.parent);

//        if(itemsDisplayed[obj].ID>=0)
//        {
//            var img = mouseObject.AddComponent<Image>();
//            img.sprite = inventory.dataBase.getItem[itemsDisplayed[obj].ID].portraitImage;
//            img.raycastTarget = false;
//        }

//        mouseItem.obj = mouseObject;
//        mouseItem.item= itemsDisplayed[obj];
//    }
//    public void OnDragEnd(GameObject obj)
//    {

//    }
//    public void OnDrag(GameObject obj)
//    {
//        if(mouseItem.obj!=null)
//        {
//            mouseItem.obj.GetComponent<RectTransform>().position = Input.mousePosition;
//        }
//    }

//    //public Vector3 GetPosition(int i)
//    //{
//    //    return new Vector3(X_START + (X_SPACE_BETWEEN_ITEM * (i % NUMBER_OF_COLUMN)), Y_START + (-Y_SPACE_BETWEEN_ITEMS * (i / NUMBER_OF_COLUMN)), 0f);
//    //}
//}

//public class MouseItem
//{
//    public GameObject obj;
//    public InventorySlot item;
//    public InventorySlot hoverItem;
//    public GameObject hoverObj;
//}
#endregion