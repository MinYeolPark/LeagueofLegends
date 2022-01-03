using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSkill", menuName = "League of Legends/LeagueAbility Data/Flash")]
public class Flash : LeagueAbilityData
{
    #region SUMMONER'S SPELL
    Vector3 dashTarget;
    float dashRadius = 3f;
    //void Activate()
    //{
    //    Plane plane = new Plane(Vector3.up, transform.position);
    //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

    //    float point = 0f;
    //    if (plane.Raycast(ray, out point))
    //    {
    //        dashTarget = ray.GetPoint(point);
    //    }
    //    Invoke("Flash_Dash", 0.05f);
    //}

    //void Flash_Dash()
    //{
    //    Debug.Log("Flash!!");
    //    if (Vector3.Distance(transform.position, dashTarget) <= dashRadius)
    //    {
    //        transform.position = Vector3.Lerp(transform.position, dashTarget, 1f);
    //    }
    //    else if (Vector3.Distance(transform.position, dashTarget) > dashRadius)
    //    {
    //        Vector3 dir = dashTarget - transform.position;

    //        dir.Normalize();
    //        dashTarget = transform.position + dashRadius * dir;

    //        transform.position = Vector3.Lerp(transform.position, dashTarget, 1f);
    //    }
    //}

    #endregion
    //private List<GameEventListener> listeners =
    //    new List<GameEventListener>();

    //public void Raise()
    //{
    //    for (int i = listeners.Count - 1; i >= 0; i--)
    //        listeners[i].OnEventRaised();
    //}

    //public void RegisterListener(GameEventListener listener)
    //{ listeners.Add(listener); }

    //public void UnregisterListener(GameEventListener listener)
    //{ listeners.Remove(listener); }

    public override IEnumerator Initialize(GameObject obj)
    {
        yield return null;
        Debug.Log("Flash Init");
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
        yield return new WaitForSeconds(0.05f);

        Debug.Log("Flash!!");
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
    }
}
