using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "NewSkill", menuName = "League of Legends/LeagueAbility Data/Ahri/Charm")]
public class Charm : LeagueAbilityData
{
    public GameObject CharmPrefab;
    public float speed = 70f;
    Vector3 dashTarget;
        
    public override IEnumerator Initialize(GameObject obj)
    {

        yield return null;
    }
    public override IEnumerator TriggerAbility(GameObject obj)
    {
        Transform firePos = obj.GetComponent<Ahri>().ahriFirePoint;
        float rotateSpeedMovement = 0.1f;
        float rotateVelocity=1f;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, Mathf.Infinity))
        {
            //ROTATION
            Quaternion rotationToLookAt = Quaternion.LookRotation(raycastHit.point - obj.transform.position);
            float rotationY = Mathf.SmoothDampAngle(obj.transform.eulerAngles.y,
                rotationToLookAt.eulerAngles.y,
                ref rotateVelocity,
                rotateSpeedMovement * (Time.deltaTime * 5));

            obj.transform.eulerAngles = new Vector3(0, rotationY, 0);
        }           

        GameObject go = Instantiate(CharmPrefab, firePos.transform.position, Quaternion.identity);

        yield return null;
    }   
}