using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : BaseUnits,IAttackable
{
    public Transform firePoint;
    public LayerMask structureLayer;
    public LayerMask championLayer;

    private void Awake()
    {
        Minion minion = GetComponent<Minion>();
        minion.OnDestroy += () => Destroy(gameObject, 2f);
    }
    protected override void Start()
    {
        base.Start();

        StartCoroutine(UpdateTarget());
    }

    IEnumerator UpdateTarget()
    {
        while (state != States.Dead)
        {
            Collider[] minions = Physics.OverlapSphere(transform.position, localData.attackRange, structureLayer);
            Collider[] champions = Physics.OverlapSphere(transform.position, localData.attackRange, championLayer);

            float shortestDistance = Mathf.Infinity;
            GameObject nearestMinion = null;
            GameObject targetChamp = null;

            ///Minion Target Process>>
            foreach (var targetMinion in minions)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, targetMinion.transform.position);

                Debug.Log($"Enemy={targetMinion} distanceToEnemy={distanceToEnemy}");


                if (distanceToEnemy < shortestDistance)
                {
                    shortestDistance = distanceToEnemy;
                    nearestMinion = targetMinion.gameObject;
                }
            }

            if (nearestMinion != null && shortestDistance <= GetComponent<BaseStats>().attackRange)
            {
                curTarget = nearestMinion.GetComponent<BaseUnits>();
            }
            //<<           

            //>>Attack Process
            if (curTarget != null)
            {
                StartCoroutine(StartAttack());
            }
            //<<
            yield return new WaitForSeconds(0.5f);
        }
    }

    protected override void Destroy()
    {
        base.Destroy();
        //agent.isStopped = true;
        //agent.enabled = false;

        //If animation is playing
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime<1.0f)
        {
            return;
        }
        else
        {
            anim.SetTrigger("Dead");
        }        
        //minionAudioPlayer.PlayOneShot(deathSound);
        Collider[] colliders = GetComponents<Collider>();

        foreach (Collider col in colliders)
        {
            col.enabled = false;
        }
        base.Destroy();
        
        //MinionPool.ReturnMinion(this);        
    }

    public IEnumerator StartAttack()
    {
        if (curTarget.state != States.Dead)
        {
            if (rangedProjectile != null)
            {
                GameObject bullet = Instantiate(rangedProjectile, firePoint.transform);
                RangedProjectile projectile = bullet.GetComponent<RangedProjectile>();

                if (projectile != null)
                {
                    projectile.Seek(curTarget);
                }
            }
        }

        yield return new WaitForSeconds((float)(100 + localData.attackSpeed * 0.01));

        if (curTarget.state == States.Dead)
        {
            StartCoroutine(StopAttack());
        }
    }

    public IEnumerator StopAttack()
    {
        state = States.Idle;

        yield return null; ;
    }
}
