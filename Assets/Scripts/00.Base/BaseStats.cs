using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStats : MonoBehaviour
{
    [SerializeField] public LeagueObjectData localData;

    public GameDataSettings.TEAM teamID = GameDataSettings.TEAM.NEAUTRAL;       //Default

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
    public float criticalStrike;    //ġ��Ÿ��
    public float lifeSteal;         //����� ���
    public float lethality;         //���������

    [Space(5)]
    [Header("Defense")]
    public float armor;
    public float magicResist;

    [Space(5)]
    [Header("Ability")]
    public float abilityPower;
    public float abilityHaste;
    public float magicPenetration;  //���������
    public float resource;          //�ڿ�: ��ų�� ����ϴµ� �ʿ��� �ڿ�
    public float resourceRegen;

    [Space(5)]
    [Header("Utilities")]
    public float moveSpeed;
    public float curExp;
    public int sightRange;

    [Space(5)]
    [Header("Buffs")]
    public float disablingEffect;   //������

    [Space(5)]
    [Header("ChampsStats")]
    public int level;
    public int gold;
    public int kills;
    public int deaths;
    public int assists;
    public int minionScore;

    private void Start()
    {
        SetStats();
    }
    public void SetStats()
    {        
        maxHealth=localData.maxHealth;
        health = localData.health;
        healthRegen = localData.healthRegen;
        attackDamage = localData.attackDamage;
        attackSpeed = localData.attackSpeed;
        attackRange = localData.attackRange;
        criticalStrike = localData.criticalStrike;
        lifeSteal = localData.lifeSteal;
        lethality = localData.lethality;
        armor = localData.armor;
        magicResist = localData.magicResist;
        abilityPower = localData.abilityPower;
        abilityHaste = localData.abilityHaste;
        magicPenetration = localData.magicPenetration;  
        resource = localData.resource;          
        resourceRegen = localData.resourceRegen;
        moveSpeed = localData.moveSpeed;
        sightRange = localData.sightRange;
        disablingEffect = localData.disablingEffect;   
        curExp = localData.curExp;
        level = localData.level;
        gold = localData.gold;
        kills = localData.kills;
        deaths = localData.deaths;
        assists = localData.assists;
        minionScore = localData.minionScore;
    }            
}
