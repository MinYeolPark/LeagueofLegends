using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewObject", menuName = "League of Legends/LeagueObject Data")]
public class LeagueObjectData : ScriptableObject,ISerializationCallbackReceiver
{
    [Header("Critical")]
    public LeagueObjectData localData;

    public Sprite portraitImage;

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

    public float maxHealthPerLvl;
    public float healthPerLvl;
    public float healthRegenPerLvl;

    [Space(5)]
    [Header("Offense")]
    public float attackDamage;
    public float attackSpeed;
    public float attackRange;
    public float lifeSteal;         //����� ���
    public float criticalStrike;    //ġ��Ÿ��
    public float lethality;         //���������

    public float attackDamagePerLvl;
    public float attackSpeedPerLvl;
    public float attackRangePerLvl;
    public float lifeStealPerLvl;         //����� ���
    public float criticalStrikePerLvl;    //ġ��Ÿ��
    public float lethalityPerLvl;         //���������

    [Space(5)]
    [Header("Defense")]
    public float armor;
    public float magicResist;
    public float disablingEffect;   //������

    public float armorPerLvl;
    public float magicResistPerLvl;
    public float disablingEffectPerLvl;   //������
    [Space(5)]
    [Header("Ability")]
    public float abilityPower;
    public float abilityHaste;
    public float magicPenetration;  //���������
    public float resource;          //�ڿ�: ��ų�� ����ϴµ� �ʿ��� �ڿ�
    public float resourceRegen;
    public float moveSpeed;

    public float abilityPowerPerLvl;
    public float abilityHastePerLvl;
    public float magicPenetrationPerLvl;  //���������
    public float resourcePerLvl;          //�ڿ�: ��ų�� ����ϴµ� �ʿ��� �ڿ�
    public float resourceRegenPerLvl;
    public float moveSpeedPerLvl;

    [Space(5)]
    [Header("Utilities")]
    public int gold;
    public int level;
    public float returnExp;
    public float curExp;
    public float needExp;
    public int kills;
    public int deaths;
    public int assists;
    public int minionScore;
    public int sightRange;

    [Space(5)]
    [Header("Audio Clips")]
    public List<AudioClip> attackClip, dieClip, idleClips, emotionClips;
    public List<LeagueAbilityData> localChampionAbilities;


    public float InitialValue;

    [System.NonSerialized] //����ȭ ��� ���� �� �ν����� �信 ���� �ȵ�
    public float RuntimeValue;

    public void OnBeforeSerialize()
    {
    }
    public void OnAfterDeserialize()
    {
    }
}
