using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSkill", menuName = "League of Legends/LeagueAbility Data/Flash")]
public class Flash : LeagueAbilityData
{
    public GameObject startVFX;
    public GameObject endVFX;

    #region SUMMONER'S SPELL
    Vector3 dashTarget;
    float dashRadius = 3f;
    #endregion

    public override IEnumerator Initialize(GameObject obj)
    {
        yield return null;
    }
    public override IEnumerator TriggerAbility(GameObject obj)
    {
        Plane plane = new Plane(Vector3.up, obj.transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        float point = 0f;
        if (plane.Raycast(ray, out point))
        {
            dashTarget = ray.GetPoint(point);
        }

        //VFX Instantiate
        var vfx=Instantiate(startVFX, obj.transform.position, obj.transform.rotation);

        yield return new WaitForSeconds(0.05f);

        if (Vector3.Distance(obj.transform.position, dashTarget) <= dashRadius)
        {
            obj.transform.position = Vector3.Lerp(obj.transform.position, dashTarget, 1f);
        }

        else if (Vector3.Distance(obj.transform.position, dashTarget) > dashRadius)
        {
            Vector3 dir = dashTarget - obj.transform.position;

            dir.Normalize();
            dashTarget = obj.transform.position + dashRadius * dir;

            obj.transform.position = Vector3.Lerp(obj.transform.position, dashTarget, 1f);
        }


        if(vfx!=null)
        {
            Destroy(vfx,continuousTime);
        }
    }
}
