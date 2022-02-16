using UnityEngine;
using System;
using System.Collections;

public abstract class LeagueAbilityData : ScriptableObject
{
    public int skillLevel = 0;
    protected int GetSkillLevel { get { return skillLevel; } set { skillLevel = value; } }

    public DamageType damageType;
    public enum DamageType
    {
        AttackDamage,
        AbilityDamage,
        None        
    }
    public enum AbilityType
    {
        Melee,
        MeleeTargeted,
        Range,
        RangeTargeted,
        SelfCast
    }
    public enum Ability
    {
        Passive,
        Ability1,
        Ability2,
        Ability3,
        Ability4
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
    public float coolDownTime;
    public float cost;
    public float range;
    public float damage;

    public bool continuousMovement;
    public GameObject continuousVFX;
    public float continuousTime;

    public bool cancelMovement; // if true, movement is cancelled before playing this action

    public bool canRecast;
    public int reCastCount;

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

    protected bool AnimatorIsPlaying(GameObject obj)
    {
        Animator animator = obj.GetComponent<Animator>();

        Debug.Log(animator.GetCurrentAnimatorStateInfo(0).length >
               animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
        return animator.GetCurrentAnimatorStateInfo(0).length >
               animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }

    public virtual IEnumerator Initialize(GameObject obj) { yield return null; }
    public virtual IEnumerator TriggerAbility(GameObject obj) { yield return null; }

}
