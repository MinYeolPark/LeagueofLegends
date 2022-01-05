using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSkill", menuName = "League of Legends/LeagueAbility Data/Teleport")]
public class Teleport : LeagueAbilityData
{
    public float recallCastTime = 8f;
        
    Vector3 recallPos;
    GameObject recallVfxPrefab;
    
    public override IEnumerator Initialize(GameObject obj)
    {
        yield return null;
    }
    public override IEnumerator TriggerAbility(GameObject obj)
    {
        yield return null;

        if(!recallVfxPrefab)
        {
            Instantiate(recallVfxPrefab, obj.transform.position, obj.transform.rotation);
        }        
        yield return new WaitForSeconds(recallCastTime);
        obj.transform.position = recallPos;
    }
}
