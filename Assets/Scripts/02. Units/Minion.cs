using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : BaseUnits,IAttackable
{
    public Transform firePoint;
    public LayerMask structureLayer;
    public LayerMask championLayer;

    [Header("Minion Move")]
    public List<Transform> path;
    public int pathIdx = 0;

    private void Awake()
    {
        //Spawner로 이동 고려
        Minion minion = GetComponent<Minion>();        
        minion.OnDestroy += () => Destroy(gameObject, 2f);

        //Initialize
        state = States.Idle;
    }
    protected override void Start()
    {
        base.Start();

        Debug.Log($"Minion agent is enable={agent.isActiveAndEnabled}");
        StartCoroutine(CheckMinionState());
    }
    IEnumerator CheckMinionState()
    {        
        while(state!=States.Dead)
        {
            switch(state)
            {
                case States.Idle:
                    //Minion have to move
                    state = States.Moving;
                    StartCoroutine(UpdatePath());
                    break;
                case States.Moving:
                    StartCoroutine(UpdateTarget());
                    break;
                case States.Targetting:
                    if (IsTargetInRange)
                        state = States.Attacking;                    
                    break;
                case States.Attacking:
                    if(!HasTarget)
                        state = States.Idle;
                    break;
                case States.Damaged:
                    break;
                default:
                    break;
            }
            yield return new WaitForSeconds(0.2f);
        }
    }

    IEnumerator UpdatePath()
    {
        yield return null;
        if (path.Count == 0)
            yield return null;
        agent.SetDestination(path[pathIdx].transform.position);
        //Debug.Log("PathIdx=" + pathIdx);

        ////If Minions arrived to destination. update Path Idx
        //if (agent.remainingDistance<=localData.attackRange)
        //    pathIdx++;

    }

    IEnumerator UpdateTarget()
    {
        while (state != States.Dead)
        {
            Collider[] minions = Physics.OverlapSphere(transform.position, localData.attackRange, structureLayer);
            Collider[] champions = Physics.OverlapSphere(transform.position, localData.attackRange, championLayer);

            float shortestDistance = Mathf.Infinity;
            GameObject nearestMinion = null;
            //GameObject targetChamp = null;

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
        state = States.Attacking;
        if (curTarget.state != States.Dead)
        {

            //Range Process
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
