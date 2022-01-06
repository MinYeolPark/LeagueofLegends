using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : BaseUnits,IAttackable
{
    public Transform firePoint;
    public LayerMask minionLayer;
    public LayerMask structureLayer;
    public LayerMask championLayer;

    float motionSmoothTime = 0.1f;
    public float rotateSpeedMovement = 0.1f;
    public float rotateVelocity;

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
            float speed = agent.velocity.magnitude / agent.speed;
            anim.SetFloat("Speed", speed, motionSmoothTime, Time.deltaTime);

            //ROTATION
            Quaternion rotationToLookAt = Quaternion.LookRotation(path[pathIdx].transform.position - transform.position);
            float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y,
                rotationToLookAt.eulerAngles.y,
                ref rotateVelocity,
                rotateSpeedMovement * (Time.deltaTime * 1));

            transform.eulerAngles = new Vector3(0, rotationY, 0);

            switch (state)
            {
                case States.Idle:
                    //Minion have to move
                    state = States.Moving;
                    break;
                case States.Moving:
                    if (!HasTarget)
                    {
                        StartCoroutine(UpdatePath());
                    }
                    StartCoroutine(UpdateTarget());
                    break;
                case States.Targetting:
                    if (IsTargetInRange)
                        state = States.Attacking;
                    break;
                case States.Attacking:
                    if (!HasTarget)
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
            yield break;
        agent.SetDestination(path[pathIdx].transform.position);
        //Debug.Log("PathIdx=" + pathIdx);

        ////If Minions arrived to destination. update Path Idx
        if (agent.remainingDistance<=localData.sightRange)
            pathIdx++;
        
        if(pathIdx>=path.Count)
        {
            yield break;
        }
    }

    IEnumerator UpdateTarget()
    {
        while (state != States.Dead)
        {
            //Need to search every type of enemies
            FindCloseMinion();
            FindCloseStructure();
            FindCloseChamp();

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
        agent.isStopped = true;
        agent.enabled = false;

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

    void FindCloseMinion()
    {
        Collider[] minions = Physics.OverlapSphere(transform.position, localData.attackRange, minionLayer);

        float shortestDistance = Mathf.Infinity;
        GameObject nearestMinion = null;
        ///Minion Target Process>>
        if (curTarget.GetComponent<BaseUnits>().teamID != teamID)
        {
            foreach (var targetMinion in minions)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, targetMinion.transform.position);

                if (distanceToEnemy < shortestDistance)
                {
                    shortestDistance = distanceToEnemy;
                    nearestMinion = targetMinion.gameObject;
                }
            }
        }


        if (nearestMinion != null && shortestDistance <= GetComponent<BaseStats>().attackRange)
        {
            curTarget = nearestMinion.GetComponent<BaseUnits>();
        }
        //<<  
    }

    void FindCloseStructure()
    {
        Collider[] structure = Physics.OverlapSphere(transform.position, localData.attackRange, structureLayer);

        float shortestDistance = Mathf.Infinity;
        GameObject targetStructure = null;

        ///Structure Target Process>>
        if (curTarget.GetComponent<BaseUnits>().teamID != teamID)
        {
            foreach (var targetMinion in structure)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, targetMinion.transform.position);

                if (distanceToEnemy < shortestDistance)
                {
                    shortestDistance = distanceToEnemy;
                    targetStructure = targetMinion.gameObject;
                }
            }
        }


        if (targetStructure != null && shortestDistance <= GetComponent<BaseStats>().attackRange)
        {
            curTarget = targetStructure.GetComponent<BaseUnits>();
        }

    }

    void FindCloseChamp()
    {
        Collider[] champions = Physics.OverlapSphere(transform.position, localData.attackRange, championLayer);

        float shortestDistance = Mathf.Infinity;

        GameObject targetChamp = null;
        if (curTarget.GetComponent<BaseUnits>().teamID != teamID)
        {
            foreach (var targetMinion in champions)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, targetMinion.transform.position);

                if (distanceToEnemy < shortestDistance)
                {
                    shortestDistance = distanceToEnemy;
                    targetChamp = targetMinion.gameObject;
                }
            }
        }
        if (targetChamp != null && shortestDistance <= GetComponent<BaseStats>().attackRange)
        {
            curTarget = targetChamp.GetComponent<BaseUnits>();
        }
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
