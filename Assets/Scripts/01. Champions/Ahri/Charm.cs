using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSkill", menuName = "League of Legends/LeagueAbility Data/Ahri/Charm")]
public class Charm : LeagueAbilityData
{
    public override IEnumerator Initialize(GameObject obj)
    {
        Debug.Log("Charm Init");

        yield return null;
    }
    public override IEnumerator TriggerAbility(GameObject obj)
    {
        yield return null;

        Debug.Log("Charm Activate");
    }
}