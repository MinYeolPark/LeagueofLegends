using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment Object", menuName = "League of Legends/LeagueItem Data/Tier_Consumable/Red Postion")]
public class RedPosition: LeagueItemData
{
    public void Awake()
    {
        startingAmount = 1;
        purchaseCost = 50;
        sellCost = 180;
        unique = "None";
        displayedName = "Red Postion";
        isActive = IsActive.Active;
        itemCategory = ItemCategory.Consumable;
        canUse = CanUse.Both;
        itemType = ItemType.None;
    }
}
