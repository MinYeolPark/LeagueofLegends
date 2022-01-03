using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSkill", menuName = "League of Legends/LeagueAbility Data/Ahri/Orb of Deception")]
public class OrbOfDeception : LeagueAbilityData
{
    public override IEnumerator Initialize(GameObject obj)
    {
        yield return null;
        Debug.Log("OrbOfDeception Init");
    }
    public override IEnumerator TriggerAbility(GameObject obj)
    {
        yield return null;

        Debug.Log("OrbOfDeception Activate");
    }
}
