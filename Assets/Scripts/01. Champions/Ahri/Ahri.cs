using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Events;

public class Ahri : BaseChampController
{
    [Header("Ability 1")]
    public Canvas S1_Canvas;
    public Image S1_Indicator;
    
    [Header("Ability 2")]
    public Canvas S2_Canvas;
    public Image S2_Indicator;

    [Header("Ability 3")]
    public Canvas S3_Canvas;
    public Image S3_Indicator;

    [Header("Ability 3")]
    public Canvas S4_Canvas;
    public Image S4_Indicator;

    //public UnityEvent OnAbility1Active;
    //public UnityEvent OnAbility2Active;
    //public UnityEvent OnAbility3Active;
    //public UnityEvent OnAbility4Active;

    protected override void OnPassive() 
    {
        base.OnPassive();
    }
    protected override void OnAbility1(InputValue value)
    {
        base.OnAbility1(value);

        if (localData.localChampionAbilities[1].abilityState == LeagueAbilityData.AbilityState.Active)
        {
            anim.SetBool("BaseAttack", false);
            anim.SetTrigger("Ability1");
        }
    }
    protected override void OnAbility2(InputValue value)
    {
        base.OnAbility2(value);
        if (localData.localChampionAbilities[2].abilityState == LeagueAbilityData.AbilityState.Active)
        {
            anim.SetBool("BaseAttack", false);
            anim.SetTrigger("Ability2");
        }
    }
    protected override void OnAbility3(InputValue value)
    {
        base.OnAbility3(value);

        if (localData.localChampionAbilities[3].abilityState == LeagueAbilityData.AbilityState.Active)
        {
            anim.SetBool("BaseAttack", false);
            anim.SetTrigger("Ability3");
        }

    }
    protected override void OnAbility4(InputValue value)
    {
        base.OnAbility4(value);

        if (localData.localChampionAbilities[4].abilityState == LeagueAbilityData.AbilityState.Active)
        {
            anim.SetBool("BaseAttack", false);
            anim.SetTrigger("Ability4");
        }
    }
    public void CheckActionStart(LeagueAbilityData whichSkill)
    {
        if (!whichSkill.canMove)
        {
            if (agent.isActiveAndEnabled && agent.isStopped == false)
            {
                state = States.Casting;
                agent.isStopped = true;
                agent.updateRotation = false;
            }
        }       
    }
    public void CheckActionEnd()
    {
        if(agent.isActiveAndEnabled&&agent.isStopped==true)
        {
            state = States.Idle;
            agent.isStopped = false;
            agent.updateRotation = true;
        }
    }  
    
}
