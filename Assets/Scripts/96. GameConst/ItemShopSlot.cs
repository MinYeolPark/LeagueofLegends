using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class ItemShopSlot : MonoBehaviour
{
    public Image itemImg;
    public LeagueItemData itemData;
    public LeagueInventoryData inventory;

    Item item;
    private void Start()
    {
        item=itemData.CreateItem();
        itemImg.sprite = itemData.portraitImage;
    }
    public void OnPurchaseButton()
    {
        Debug.Log("Purchase Item");
        //inventory.AddItem()
    }
}
