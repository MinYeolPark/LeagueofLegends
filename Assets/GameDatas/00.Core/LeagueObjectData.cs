using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewObject", menuName = "League of Legends/LeagueObject Data")]
public class LeagueObjectData : ScriptableObject
{
    [Header("Critical")]
    public LeagueObjectData localData;
    public Team team;
    public enum Team
    {
        Neutral,
        RedTeam,
        BlueTeam
    }
    
    public enum ObjType
    {
        Champion,
        Minion,
        Structure,        
        Creep,
    }
    public enum AttackType
    {
        Melee, Range
    }

    [Header("Common")]
    public ObjType objType;
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

    [Space(5)]
    [Header("Utilities")]
    public int gold;
    public int level;
    public float expValue;
    public int kills;
    public int deaths;
    public int assists;
    public int minionScore;
    public int sightRange;

    [Space(5)]
    [Header("Audio Clips")]
    public List<AudioClip> attackClip, dieClip, idleClips, emotionClips;
    public List<LeagueAbilityData> localChampionAbilities;
}
