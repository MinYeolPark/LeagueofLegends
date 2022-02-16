using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSkill", menuName = "League of Legends/LeagueAbility Data/Garen/Judgement")]
public class Judgement : LeagueAbilityData
{    
    public override IEnumerator Initialize(GameObject obj)
    {
        yield return null;
    }

    public override IEnumerator TriggerAbility(GameObject obj)
    {
        Animator anim = obj.GetComponent<Animator>();

        
        if(BaseChampController.Instance.bAnimIsPlaying==true)
        {
            Debug.Log("hash3 false");
            BaseChampController.Instance.CheckActionEnd();
            anim.SetBool(BaseChampController.Instance.hashAbility3, false);
        }

        //if(anim.GetCurrentAnimatorStateInfo(0).length>=anim.GetCurrentAnimatorStateInfo(0).normalizedTime)
        //{
        //    Debug.Log("Anim is playing");
        //    anim.SetBool("Ability3", false);
        //}

        
        yield return null;
    }

    
}
