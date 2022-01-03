using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedProjectile : MonoBehaviour
{
    [SerializeField] GameObject hitEffecet;

    private BaseStats thisStats;
    private BaseStats targetStats;
    public float speed = 70f;
    public float damage;
    private void Start()
    {
        thisStats = FindObjectOfType<BaseChampController>().GetComponent<BaseStats>();

        damage = thisStats.attackDamage;
    }
    public void Seek(BaseStats curTarget)
    {
        targetStats = curTarget;
        Debug.Log($"Target={curTarget}");
    }

    private void Update()
    {
        if (targetStats == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = targetStats.transform.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        //if (dir.magnitude <= distanceThisFrame)
        //{
        //    HitTarget();
        //    return;
        //}

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    void HitTarget()
    {
        BaseUnits target = targetStats.GetComponent<BaseUnits>();
               
        
        Vector3 hitPoint = target.GetComponent<Collider>().ClosestPoint(transform.position);
        Vector3 hitNormal = transform.position - target.GetComponent<Collider>().transform.position;

        target.OnDamage(target, damage, hitPoint, hitNormal);


        if (hitEffecet != null)
        {
            GameObject effectIns = (GameObject)Instantiate(hitEffecet, transform.position, transform.rotation);

            Destroy(effectIns, 2f);
        }

        Destroy(gameObject);
    }
    private void OnCollisionEnter(Collision other)
    {        
        Vector3 hitPoint = other.gameObject.GetComponent<Collider>().ClosestPoint(transform.position);
        Vector3 hitNormal = transform.position - other.transform.position;

        BaseUnits target = targetStats.GetComponent<BaseUnits>();
        target.OnDamage(target, damage, hitPoint, hitNormal);

        Debug.Log($"맞은방향={hitNormal},맞은부위={hitPoint}");

        if (hitEffecet != null)
        {
            GameObject effectIns = (GameObject)Instantiate(hitEffecet, transform.position, transform.rotation);

            Destroy(effectIns, 2f);
        }

        Destroy(gameObject,1f);
    }
}
