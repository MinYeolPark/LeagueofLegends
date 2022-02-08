using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSkill", menuName = "League of Legends/LeagueAbility Data/Garen/Courage")]
public class Courage : LeagueAbilityData
{
    public override IEnumerator Initialize(GameObject obj)
    {
        return base.Initialize(obj);    
    }

    public override IEnumerator TriggerAbility(GameObject obj)
    {
        return base.TriggerAbility(obj);    
    }
}
