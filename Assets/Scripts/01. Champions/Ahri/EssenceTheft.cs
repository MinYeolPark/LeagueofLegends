using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSkill", menuName = "League of Legends/LeagueAbility Data/Ahri/Essnece Theft")]
public class EssenceTheft : LeagueAbilityData
{
    public override IEnumerator Initialize(GameObject obj)
    {
        yield return null;
        Debug.Log("EssenceTheft Init");
    }
    public override IEnumerator TriggerAbility(GameObject obj)
    {
        yield return null;

        Debug.Log("EssenceTheft Activate");
    }
}
