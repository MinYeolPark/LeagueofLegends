using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewObject", menuName = "League of Legends/LeagueObject Data")]
public class LeagueObjectData : ScriptableObject
{
    [Space(5)]
    [Header("Audio Clips")]
    public List<AudioClip> attackClip, dieClip, idleClips, emotionClips;

    public LeagueObjectData localData;
    public List<LeagueAbilityData> localChampionAbilities;
    
    public enum ObjType
    {
        Champion,
        Minion,
        Creep,
        Building,
        Spell
    }
    public enum AttackType
    {
        Melee, Range
    }

    public enum Category
    {
        Tank,
        Bruser,
        ADCarry,
        Mage,
        Assasin
    }
    
    [Header("Common")]
    public ObjType objType;
    public Category category;
    public AttackType attackType;
    public GameObject projectile;

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

    [Space(5)]
    [Header("Utilities")]
    public int gold;
    public int level;
    public float expValue;
    public int kills;
    public int deaths;
    public int assists;
    public int minionScore;   
}
