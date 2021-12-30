using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSkillController : MonoBehaviour
{

    public IEnumerator Ability1Process()
    {
        Debug.Log("Ability1 Couroutine Start");
        
        yield return null;

        Debug.Log("Ability1 Couroutine End");
    }



    //[SerializeField] List<AbilityData> abilityDatas;
    //AbilityState state = AbilityState.Ready;


    //private void Update()
    //{
    //    switch(state)
    //    {
    //        case AbilityState.Ready:
    //            //if(Input)
    //            //{
    //            //    Ability.activate();
    //            //    state = AbilityState.Active;
    //            //    AbilityState.activeTime = Ability.activetime;
    //            //}
    //            break;
    //        case AbilityState.Active:
    //            {
    //                //if(activeTime>0)
    //                //{
    //                //    activeTime -= Time.deltaTime;
    //                //}
    //                //else
    //                //{
    //                //    state = AbilityState.CoolDown;
    //                //    cooldowntime = Ability.cooldiwnTime;
    //                //}
    //            }
    //            break;
    //        case AbilityState.CoolDown:
    //            //if(cooldownTime>0)
    //            //{
    //            //    cooldownTime -= Time.deltaTime;
    //            //}
    //            //else
    //            //{
    //            //    state = AbilityState.ready;
    //            //}
    //            break;
    //    }
    //}
}
