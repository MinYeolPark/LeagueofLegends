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
    public LeagueItemDataBase itemDataBase;
    public LeagueInventoryData inventory;

    Dictionary<GameObject, InventorySlot> itemsDisplayed = new Dictionary<GameObject, InventorySlot>();

    //public item
    void Start()
    {
        CreateSlots();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSlots();
    }
    public void CreateSlots()
    {
        itemsDisplayed = new Dictionary<GameObject, InventorySlot>();

        for (int i = 0; i < itemDataBase.leagueItems.Count; i++)
        {
            var obj = Instantiate(itemSlotPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<ItemShopSlot>().itemIcon.sprite = itemDataBase.leagueItems[i].portraitImage;
            Debug.Log(obj.gameObject);
        }
        for (int i = 0; i < inventory.inventory.items.Length; i++)
        {
            //var obj = Instantiate(itemSlotPrefab, Vector3.zero, Quaternion.identity, transform);

            //obj.GetComponent<RectTransform>().localPosition = GetPosition(i);

            //AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
            //AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
            //AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });
            //AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj); });
            //AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });


            //itemsDisplayed.Add(obj, inventory.inventory.items[i]);
        }
    }
    public void UpdateSlots()
    {
        foreach (KeyValuePair<GameObject, InventorySlot> _slot in itemsDisplayed)
        {
            if (_slot.Value.ID >= 0)
            {
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.dataBase.getItem[_slot.Value.item.Id].portraitImage;
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
                _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = _slot.Value.amount == 1 ? "" : _slot.Value.amount.ToString("n0");
            }
            else
            {
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
                _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = "";
            }
        }
    }
    private void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        var eventTrigger = new EventTrigger.Entry();
        eventTrigger.eventID = type;
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
    }

    //public Vector3 GetPosition(int i)
    //{
    //    return new Vector3(X_START + (X_SPACE_BETWEEN_ITEM * (i % NUMBER_OF_COLUMN)), Y_START + (-Y_SPACE_BETWEEN_ITEMS * (i / NUMBER_OF_COLUMN)), 0f);
    //}
}
