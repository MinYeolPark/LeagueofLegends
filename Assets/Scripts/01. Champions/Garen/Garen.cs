using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Garen : BaseChampController
{    
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void OnAbility1(InputValue value)
    {
        base.OnAbility1(value);
        localData.localChampionAbilities[1].SetActivate();
        if (localData.localChampionAbilities[1].abilityState == LeagueAbilityData.AbilityState.Active)
        {
            anim.SetBool(hashAttack, false);
            anim.SetBool(hashAbility1, true);
            anim.SetBool(hashAbility2, false);
            anim.SetBool(hashAbility3, false);
            anim.SetBool(hashAbility4, false);
        }
    }
    protected override void OnAbility2(InputValue value)
    {
        base.OnAbility2(value);

        if (localData.localChampionAbilities[2].abilityState == LeagueAbilityData.AbilityState.Active)
        {
            anim.SetBool(hashAttack, false);
            anim.SetBool(hashAbility1, false);
            anim.SetBool(hashAbility2, true);
            anim.SetBool(hashAbility3, false);
            anim.SetBool(hashAbility4, false);
        }
    }
    protected override void OnAbility3(InputValue value)
    {
        base.OnAbility3(value);

        if (localData.localChampionAbilities[3].abilityState == LeagueAbilityData.AbilityState.Active)
        {
            anim.SetBool(hashAttack, false);
            anim.SetBool(hashAbility1, false);
            anim.SetBool(hashAbility2, false);
            anim.SetBool(hashAbility3, true);
            anim.SetBool(hashAbility4, false);
        }
    }
    protected override void OnAbility4(InputValue value)
    {
        base.OnAbility4(value);

        if (localData.localChampionAbilities[4].abilityState == LeagueAbilityData.AbilityState.Active)
        {
            anim.SetBool(hashAttack, false);
            anim.SetBool(hashAbility1, false);
            anim.SetBool(hashAbility2, false);
            anim.SetBool(hashAbility3, false);
            anim.SetBool(hashAbility4, true);
        }
    }
}
