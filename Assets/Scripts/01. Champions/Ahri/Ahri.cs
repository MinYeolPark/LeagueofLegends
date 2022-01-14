using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Events;

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

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out RaycastHit raycastHit, Mathf.Infinity))
            {
                //ROTATION
                Quaternion rotationToLookAt = Quaternion.LookRotation(raycastHit.point - transform.position);
                float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y,
                    rotationToLookAt.eulerAngles.y,
                    ref rotateVelocity,
                    rotateSpeedMovement * (Time.deltaTime * 5));

                transform.eulerAngles = new Vector3(0, rotationY, 0);
            }           
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
}
