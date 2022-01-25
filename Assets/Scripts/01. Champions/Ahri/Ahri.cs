using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class Ahri : BaseChampController
{
    [Space(5)]
    [Header("Character Characteristic")]
    public Transform ahriFirePoint;

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

    protected override void Awake()
    {
        base.Awake();
        rangedProjectile = localData.projectile;
    }
    protected override void OnPassive() 
    {
        base.OnPassive();
    }
    protected override void OnAbility1(InputValue value)
    {
        base.OnAbility1(value);

        if (localData.localChampionAbilities[1].abilityState == LeagueAbilityData.AbilityState.Active)
        {
            anim.SetBool(hashAttack, false);            
            anim.SetTrigger(hashAbility1);
        }
    }
    protected override void OnAbility2(InputValue value)
    {
        base.OnAbility2(value);
        if (localData.localChampionAbilities[2].abilityState == LeagueAbilityData.AbilityState.Active)
        {
            anim.SetBool(hashAttack, false);
            anim.SetTrigger(hashAbility2);
        }
    }
    protected override void OnAbility3(InputValue value)
    {
        base.OnAbility3(value);

        if (localData.localChampionAbilities[3].abilityState == LeagueAbilityData.AbilityState.Active)
        {
            anim.SetBool(hashAttack, false);
            anim.SetTrigger(hashAbility3);
        }

    }
    protected override void OnAbility4(InputValue value)
    {
        base.OnAbility4(value);

        if (localData.localChampionAbilities[4].abilityState == LeagueAbilityData.AbilityState.Active)
        {
            anim.SetBool(hashAttack, false);
            anim.SetTrigger(hashAbility4);
        }
    }

    protected override IEnumerator RangeAttack()
    {
        if (target.GetComponent<BaseUnits>().state == States.Dead || target == null)
        {
            StopAttack();

            anim.SetBool(hashAttack, true);
            target = null;
        }

        yield return null;

        if (localData.attackType == LeagueObjectData.AttackType.Range && state == States.Attacking)
        {
            if (rangedProjectile != null)
            {
                GameObject bullet = Instantiate(rangedProjectile, ahriFirePoint.transform.position, ahriFirePoint.transform.rotation);
                bullet.GetComponent<RangedProjectile>().SetDamage(gameObject);
                RangedProjectile projectile = bullet.GetComponent<RangedProjectile>();

                if (projectile != null)
                {
                    projectile.Seek(target);
                }
            }
        }
    }    
}
