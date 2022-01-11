using UnityEngine;

[CreateAssetMenu(fileName = "NewObject", menuName = "League of Legends/LeagueItem Data")]
public class LeagueItemData : ScriptableObject
{
    [Header("Critical")]
    public LeagueItemData localData;

    public Sprite portraitImage;
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
    public float lifeSteal;         //����� ���
    public float criticalStrike;    //ġ��Ÿ��
    public float lethality;         //���������

    [Space(5)]
    [Header("Defense")]
    public float armor;
    public float magicResist;
    public float disablingEffect;   //������

    [Space(5)]
    [Header("Ability")]
    public float abilityPower;
    public float abilityHaste;
    public float magicPenetration;  //���������
    public float resource;          //�ڿ�: ��ų�� ����ϴµ� �ʿ��� �ڿ�
    public float resourceRegen;
    public float moveSpeed;
}
