using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedProjectile : MonoBehaviour
{
    [SerializeField] private GameObject hitEffecet;

    private GameObject fireObj;
    private GameObject targetObj;
    public float speed = 70f;
    public float damage;
    private void Start()
    {

    }
    public void Seek(GameObject curTarget)
    {
        targetObj = curTarget;
    }

    public void SetDamage(GameObject parentObj)
    {
        fireObj = parentObj;
        damage = fireObj.GetComponent<BaseStats>().attackDamage;
    }

    private void Update()
    {
        if (targetObj == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = targetObj.transform.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    void HitTarget()
    {
        GameObject target = targetObj;

        Vector3 hitPoint = target.GetComponent<Collider>().ClosestPoint(transform.position);
        Vector3 hitNormal = transform.position - target.GetComponent<Collider>().transform.position;

        target.GetComponent<BaseUnits>().OnDamage(target.GetComponent<BaseUnits>(), damage, hitPoint, hitNormal);


        if (hitEffecet != null)
        {
            GameObject effectIns = (GameObject)Instantiate(hitEffecet, transform.position, transform.rotation);

            Destroy(effectIns, 2f);
        }

        Destroy(gameObject);
    }
    private void OnCollisionEnter(Collision obj)
    {
        Vector3 pos = obj.GetContact(0).point;

        Quaternion rot = Quaternion.LookRotation(-obj.GetContact(0).normal);

        //ShowEffect()
    }

    //void ShowHittedEffect(Vector3 pos, Quaternion rot)
    //{
    //    GameObject blood = Instantiate<GameObject>(blood, pos, rot, mosterTransform);
    //    Destroy(blood, 1.0f);
    //}
        
    

}
