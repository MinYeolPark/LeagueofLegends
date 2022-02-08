using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSkill", menuName = "League of Legends/LeagueAbility Data/Garen/DecisiveStrike")]
public class DecisiveStrike : LeagueAbilityData
{
    public override IEnumerator Initialize(GameObject obj)
    {
        Debug.Log("DecisiveStrike Init");

        yield return null;
    }

    public override IEnumerator TriggerAbility(GameObject obj)
    {
        Debug.Log("DecisiveStrike Active!!");

        yield return null;
    }
}
