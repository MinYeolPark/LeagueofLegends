using UnityEngine;
using System;
using System.Collections;

public abstract class LeagueAbilityData : ScriptableObject
{    
    public int skillLevel;
    int GetSkillLevel { get { return skillLevel; } set { skillLevel = value; } }

    public DamageType damageType;
    public enum DamageType
    {
        AttackDamage,
        AbilityDamage,
        None        
    }
    public enum AbilityType
    {
        Target,
        Non_Target,
        Self_Cast
    }
    public enum Ability
    {
        Passive,
        Ability1,
        Ability2,
        Ability3,
        Ability4,
    }
    //Init state=Ready
    public AbilityState abilityState = AbilityState.Ready;
    public enum AbilityState
    {
        Ready,
        Active,
        CoolDown,
        Casting
    }

    public string displayedName;
    public float activeTime;
    public float duration;
    public float coolDownTime;
    public float cost;
    public float range;
    public float durationVFX;
    public float damage;
    public bool canMove;
    public bool canRotate;

    public Sprite icon;
    public bool needIndicator;
    public GameObject rangeIndicator;

    public AudioClip characterSound;
    public System.Collections.Generic.List<AudioClip> skillSounds;

    [Multiline]
    public string description;

    public virtual void SetReady() { abilityState = AbilityState.Ready; }
    public virtual void SetActivate() { abilityState = AbilityState.Active; }
    public virtual void SetCoolDown() { abilityState = AbilityState.CoolDown; }

    public virtual  IEnumerator Initialize(GameObject obj) { yield return null; }
    public virtual IEnumerator TriggerAbility(GameObject obj) { yield return null; }

}
