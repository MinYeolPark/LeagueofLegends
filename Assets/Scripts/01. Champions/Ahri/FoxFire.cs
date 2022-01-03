using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSkill", menuName = "League of Legends/LeagueAbility Data/Ahri/FoxFire")]
public class FoxFire : LeagueAbilityData
{
    public override IEnumerator Initialize(GameObject obj)
    {
        yield return null;
        Debug.Log("FoxFire Init");
    }
    public override IEnumerator TriggerAbility(GameObject obj)
    {
        yield return null;

        Debug.Log("FoxFire Activate");
    }
}
