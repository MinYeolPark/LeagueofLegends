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

        StartCoroutine(CheckMinionState());
    }
    IEnumerator CheckMinionState()
    {        
        while(state!=States.Dead)
        {
            //<::Moving Process
            float speed = agent.velocity.magnitude / agent.speed;
            anim.SetFloat("Speed", speed, motionSmoothTime, Time.deltaTime);

            //ROTATION
            Quaternion rotationToLookAt = Quaternion.LookRotation(path[pathIdx].transform.position - transform.position);
            float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y,
                rotationToLookAt.eulerAngles.y,
                ref rotateVelocity,
                rotateSpeedMovement * (Time.deltaTime * 1));

            transform.eulerAngles = new Vector3(0, rotationY, 0);
            //::>

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
                        FindCloseMinion();
                        agent.stoppingDistance = 0;
                    }
                    else
                    {
                        //Need to search every type of enemies
                        //FindCloseStructure();
                        //FindCloseChamp();
                        //<<
                        agent.stoppingDistance = stats.attackRange;
                        state = States.Targetting;
                    }                    
                    break;
                case States.Targetting:
                    agent.SetDestination(curTarget.transform.position);

                    if (agent.remainingDistance < Vector3.Distance(transform.position, curTarget.transform.position))
                    {
                        if (curTarget.TryGetComponent(out BaseStats obj))
                        {
                            state = States.Attacking;
                        }
                    }                    

                    break;
                case States.Attacking:
                    StartCoroutine(StartAttack());
                    if (!HasTarget)
                    {
                        state = States.Idle;
                        StartCoroutine(StopAttack());
                    }
                    
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
        {
            yield break;
        }

        agent.SetDestination(path[pathIdx].transform.position);

        //If Minions arrived to destination. update Path Idx
        if (agent.remainingDistance<=localData.sightRange)
        {          
            if(pathIdx<=path.Capacity)
            {
                //pathIdx++;
                yield return null;
            }
        }            
        
        if(pathIdx>=path.Count)
        {
            yield break;
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

        foreach (var item in minions)
        {
            Debug.Log(item.name);
        }
        float shortestDistance = Mathf.Infinity;
        GameObject nearestMinion = null;
        ///Minion Target Process>>
        
        foreach (var targetMinion in minions)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, targetMinion.transform.position);

            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestMinion = targetMinion.gameObject;

                //TO DO: Enemy targetting rule update            
                if (nearestMinion.GetComponent<BaseStats>().teamID != GetComponent<BaseStats>().teamID)
                {
                    if (nearestMinion != null && shortestDistance <= GetComponent<BaseStats>().attackRange)
                    {
                        curTarget = nearestMinion;
                    }
                }
            }
           
        }        
    }

    void FindCloseStructure()
    {
        Collider[] structure = Physics.OverlapSphere(transform.position, localData.attackRange, structureLayer);

        float shortestDistance = Mathf.Infinity;
        GameObject targetStructure = null;

        ///Structure Target Process>>
        foreach (var targetMinion in structure)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, targetMinion.transform.position);

            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                targetStructure = targetMinion.gameObject;

                if (targetStructure.GetComponent<BaseStats>().teamID != GetComponent<BaseStats>().teamID)
                {
                    curTarget = targetStructure;
                }
            }
        }       
    }

    void FindCloseChamp()
    {
        Collider[] champions = Physics.OverlapSphere(transform.position, localData.attackRange, championLayer);

        float shortestDistance = Mathf.Infinity;

        GameObject targetChamp = null;
        
        foreach (var targetMinion in champions)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, targetMinion.transform.position);

            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                targetChamp = targetMinion.gameObject;

                if (targetChamp.GetComponent<BaseStats>().teamID != GetComponent<BaseStats>().teamID)
                {
                    curTarget = targetChamp;
                }
            }
        }
    }
    public IEnumerator StartAttack()
    {
        if (curTarget.TryGetComponent<BaseUnits>(out BaseUnits obj))
        {
            if (obj.state != States.Dead)
            {
                anim.SetBool("BaseAttack", true);
            }

            yield return new WaitForSeconds(stats.attackRange / ((100 + stats.attackSpeed) * 0.01f));

            if (obj.state == States.Dead)
            {
                state = States.Idle;
                anim.SetBool("BaseAttack", false);
                curTarget = null;
            }
        }

        ////Range Process
        //if (rangedProjectile != null)
        //{
        //    GameObject bullet = Instantiate(rangedProjectile, firePoint.transform);
        //    RangedProjectile projectile = bullet.GetComponent<RangedProjectile>();

        //    if (projectile != null)
        //    {
        //        projectile.Seek(curTarget);
        //    }
        //}
    }

    public IEnumerator StopAttack()
    {
        state = States.Idle;

        yield return null; ;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position,gameObject.GetComponent<BaseStats>().sightRange);
    }

    public void CheckMinionActionStart()
    {
        if (agent.isActiveAndEnabled && agent.isStopped == false)
        {
            state = States.Casting;
            agent.isStopped = true;
            agent.updateRotation = false;
        }
    }
    public void CheckMinionActionEnd()
    {
        if (agent.isActiveAndEnabled && agent.isStopped == true)
        {
            state = States.Idle;
            agent.isStopped = false;
            agent.updateRotation = true;
        }
    }
}
