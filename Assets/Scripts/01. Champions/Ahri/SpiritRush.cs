using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSkill", menuName = "League of Legends/LeagueAbility Data/Ahri/Spirit Rush")]
public class SpiritRush : LeagueAbilityData
{
    public override IEnumerator Initialize(GameObject obj)
    {
        yield return null;
        Debug.Log("SpiritRush Init");
    }
    public override IEnumerator TriggerAbility(GameObject obj)
    {
        yield return null;

        Debug.Log("SpiritRush Activate");
    }
}
