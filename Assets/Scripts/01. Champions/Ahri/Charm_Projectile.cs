using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charm_Projectile : MonoBehaviour
{
    [SerializeField] private GameObject hitEffecet;

    private BaseUnits fireObj;
    private GameObject targetObj;
    public float speed = 3f;
    public float damage;
    private void Start()
    {
        fireObj = FindObjectOfType<BaseChampController>();

        damage = fireObj.GetComponent<BaseStats>().attackDamage;
    }

    private void Update()
    {
        Vector3 dir = Input.mousePosition - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    private void OnCollisionEnter(Collision other)
    {
        Vector3 hitPoint = other.gameObject.GetComponent<Collider>().ClosestPoint(transform.position);
        Vector3 hitNormal = transform.position - other.transform.position;

        BaseUnits target = other.gameObject.GetComponent<BaseUnits>();
        Debug.Log(other.gameObject.GetComponent<BaseUnits>());

        if (other.gameObject.GetComponent<BaseUnits>() == target)
        {
            target.OnDamage(target, damage, hitPoint, hitNormal);
            Debug.Log(other.gameObject.GetComponent<BaseUnits>());
            Debug.Log($"맞은방향={hitNormal},맞은부위={hitPoint}");

            if (hitEffecet != null)
            {
                GameObject effectIns = (GameObject)Instantiate(hitEffecet, transform.position, transform.rotation);

                Destroy(effectIns, 2f);
            }

            Destroy(gameObject, 1f);
        }
    }
}
