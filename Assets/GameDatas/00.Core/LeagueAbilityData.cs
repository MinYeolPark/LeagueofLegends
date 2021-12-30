using UnityEngine;

[CreateAssetMenu(fileName = "NewSkill", menuName = "League of Legends/LeagueAbility Data")]
public class LeagueAbilityData : ScriptableObject
{
    public enum Ability
    {
        Passive,
        Ability1,
        Ability2,
        Ability3,
        Ability4,
    }
    public enum AbilityState
    {
        Ready,
        Active,
        CoolDown
    }

    public float coolDownTime;
    public float activeTime;

    public float cost;
    public float range;

    [Tooltip("If this Action describes a player ability, this is the ability's iconic representation")]
    public Sprite Icon;

    [Tooltip("If this Action describes a player ability, this is the ability's iconic representation")]
    public GameObject RangeIndicator;

    [Tooltip("If this Action describes a player ability, this is the name we show for the ability")]
    public string DisplayedName;

    [Tooltip("If this Action describes a player ability, this is the tooltip description we show for the ability")]
    [Multiline]
    public string Description;

    public virtual void Activate() { Debug.Log("Abilitydata"); }
    public virtual void CoolDown() { }
}
