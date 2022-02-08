using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSkill", menuName = "League of Legends/LeagueAbility Data/Ahri/Orb of Deception")]
public class OrbOfDeception : LeagueAbilityData
{
    [SerializeField] private GameObject OrbofDecptionPrefab;

    float rotateSpeedMovement = 0.1f;
    float rotateVelocity;
    public override IEnumerator Initialize(GameObject obj)
    {
        Debug.Log("Orb Init");

        yield return null;
    }
    public override IEnumerator TriggerAbility(GameObject obj)
    {
        Debug.Log("Orb Trigger");
        Transform firePos = obj.GetComponent<Ahri>().ahriFirePoint;

        //Aim Toward Mouse
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray,out var hitInfo, Mathf.Infinity))
        {
            var direction = hitInfo.point - obj.transform.position;

            direction.y = 0f;
            direction.Normalize();
            obj.transform.forward = direction;
        }

        GameObject go = Instantiate(OrbofDecptionPrefab, firePos.transform.position, Quaternion.identity);

        yield return null;

    }
}
