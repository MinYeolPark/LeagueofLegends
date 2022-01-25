using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSkill", menuName = "League of Legends/LeagueAbility Data/Ahri/Spirit Rush")]
public class SpiritRush : LeagueAbilityData
{
    public int triggerCount = 3;

    public override IEnumerator Initialize(GameObject obj)
    {
        yield return null;
    }
    public override IEnumerator TriggerAbility(GameObject obj)
    {
        yield return null;
    }
}
