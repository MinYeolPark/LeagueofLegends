using UnityEngine;
using System.Collections.Generic;

public abstract class LeagueItemData : ScriptableObject
{
    [Header("Critical")]
    //public LeagueItemData localData;
    public Sprite portraitImage;

    //Tests
    public ItemBuff[] buffs;
    public int Id;

    public enum HasUnique
    {
        True,
        False
    }
    public enum IsActive
    {
        Passive,
        Active
    }
    public enum ItemCategory
    {
        Consumable,
        Equipment,
        Lens
    }
    public enum CanUse
    {
        Both,
        MeleeOnly,
        RangeOnly
    }
    public enum ItemType
    {
        Bruser,
        ADCarry,
        Assasin,
        Mage,
        Tank,
        Support,
        None
    }

    public enum ItemTier
    {
        Starter,
        Trinkets,
        Basic,          //Sheen, Dagger, B.F Sword
        Advanced,       //Giant Belt
        Finished,       //Raylai, DeathCap
        Mythic,         //Dracksare, Trinity Force
        Masterwork,     //Oren Tier
        Diestributed,   //Biscuit, Kalistar
    }

    [Space(5)]
    public int startingAmount;
    public int purchaseCost;
    public int sellCost;
    public string unique;
    public string displayedName;
    [Multiline]
    public string description;

    [Space(5)]
    [Header("Common")]
    public IsActive isActive;
    public ItemCategory itemCategory;
    public CanUse canUse;
    public ItemType itemType;

    [Space(5)]
    [Header("Health")]
    public float maxHealth;
    public float health;
    public float healthRegen;

    [Space(5)]
    [Header("Offense")]
    public float attackDamage;
    public float attackSpeed;
    public float attackRange;
    public float lifeSteal;         //생명력 흡수
    public float criticalStrike;    //치명타율
    public float lethality;         //물리관통력

    [Space(5)]
    [Header("Defense")]
    public float armor;
    public float magicResist;
    public float disablingEffect;   //강인함

    [Space(5)]
    [Header("Ability")]
    public float abilityPower;
    public float abilityHaste;
    public float magicPenetration;  //마법관통력
    public float resource;          //자원: 스킬을 사용하는데 필요한 자원
    public float resourceRegen;
    public float moveSpeed;
    public Item CreateItem()
    {
        Item newItem = new Item(this);
        return newItem;
    }
}

[System.Serializable]
public class Item
{
    public string Name;
    public int Id;
    public ItemBuff[] buffs;
    public Item(LeagueItemData item)
    {
        Name = item.name;
        Id = item.Id;
        buffs = new ItemBuff[item.buffs.Length];
        for (int i = 0; i < buffs.Length; i++)
        {
            buffs[i] = new ItemBuff(item.buffs[i].min, item.buffs[i].max)
            {
                itemType = item.buffs[i].itemType
            };
        }
    }
}

[System.Serializable]
public class ItemBuff
{
    public LeagueItemData.ItemType itemType;
    public int value;
    public int min;
    public int max;
    public ItemBuff(int _min, int _max)
    {
        min = _min;
        max = _max;
        GenerateValue();
    }
    public void GenerateValue()
    {
        value = UnityEngine.Random.Range(min, max);
    }
}