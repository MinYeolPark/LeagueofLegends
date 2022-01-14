using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedProjectile : MonoBehaviour
{
    [SerializeField] private GameObject hitEffecet;

    private BaseUnits fireObj;
    private GameObject targetObj;
    public float speed = 70f;
    public float damage;
    private void Start()
    {
        fireObj= FindObjectOfType<BaseChampController>();

        damage = fireObj.GetComponent<BaseStats>().attackDamage;
    }
    public void Seek(GameObject curTarget)
    {
        targetObj = curTarget;
        Debug.Log($"Target={curTarget}");
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
    //private void OnCollisionEnter(Collision other)
    //{
    //    Vector3 hitPoint = other.gameObject.GetComponent<Collider>().ClosestPoint(transform.position);
    //    Vector3 hitNormal = transform.position - other.transform.position;

    //    BaseUnits target = targetStats.GetComponent<BaseUnits>();
    //    Debug.Log(other.gameObject.GetComponent<BaseUnits>());

    //    if (other.gameObject.GetComponent<BaseUnits>() == target)
    //    {
    //        target.OnDamage(target, damage, hitPoint, hitNormal);
    //        Debug.Log(other.gameObject.GetComponent<BaseUnits>());
    //        Debug.Log($"맞은방향={hitNormal},맞은부위={hitPoint}");

    //        if (hitEffecet != null)
    //        {
    //            GameObject effectIns = (GameObject)Instantiate(hitEffecet, transform.position, transform.rotation);

    //            Destroy(effectIns, 2f);
    //        }

    //        Destroy(gameObject, 1f);
    //    }
    //}
}
