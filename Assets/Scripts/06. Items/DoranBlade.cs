using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment Object", menuName = "League of Legends/LeagueItem Data/Tier_Start/Doran Blade")]
public class DoranBlade : LeagueItemData
{
    public void Awake()
    {
        startingAmount = 1;
        purchaseCost = 450;
        sellCost = 180;
        unique = "None";
        displayedName = "Doran Blade";
        isActive = IsActive.Passive;
        itemCategory = ItemCategory.Equipment;
        canUse = CanUse.Both;
        itemType = ItemType.None;
    }    
}
