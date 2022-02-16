using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Minion : BaseUnits, IAttackable
{
    [SerializeField] private GameObject hitEffecet;

    [Header("Cash Component")]
    public Transform firePoint;
    public LayerMask minionLayer;
    public LayerMask structureLayer;
    public LayerMask championLayer;

    [Header("Animator parameter Hast import")]
    private readonly int hashSpeed = Animator.StringToHash("Speed");
    private readonly int hashTrace = Animator.StringToHash("IsTrace");
    private readonly int hashAttack = Animator.StringToHash("IsAttack");
    private readonly int hashDie = Animator.StringToHash("Die");

    float motionSmoothTime = 0.1f;
    public float rotateSpeedMovement = 0.1f;
    public float rotateVelocity;

    [Header("Minion Move")]
    public List<Transform> path;
    public int pathIdx = 0;

    protected override void Awake()
    {
        base.Awake();
        if (localData.attackType == LeagueObjectData.AttackType.Range)
            rangedProjectile = localData.projectile;
        OnDestroy += () => Destroy(gameObject, 2f);

        //Check Monster State
        StartCoroutine(CheckMinionState());
        StartCoroutine(MinionAction());
    }
    private void Start()
    {
        //if (localData.attackType == LeagueObjectData.AttackType.Range)
        //    rangedProjectile = localData.projectile;
    }

    private void FixedUpdate()
    {
        //<::Moving Process
        float speed = agent.velocity.magnitude / agent.speed;
        anim.SetFloat(hashSpeed, speed, motionSmoothTime, Time.deltaTime);

        //ROTATION
        Quaternion rotationToLookAt = Quaternion.LookRotation(path[pathIdx].transform.position - transform.position);
        float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y,
            rotationToLookAt.eulerAngles.y,
            ref rotateVelocity,
            rotateSpeedMovement * (Time.deltaTime * 1));

        transform.eulerAngles = new Vector3(0, rotationY, 0);

        if(HasTarget)
        {
            transform.LookAt(curTarget.transform.position);
        }
    }

    IEnumerator CheckMinionState()
    {
        while (state != States.Dead)
        {
            yield return new WaitForSeconds(0.3f);

            //::>
            if (state == States.Dead) yield break;

            if (path.Count <= 0)
            {
                state = States.Idle;
                yield break;
            }
            else
            {
                //If curTarget is null, Moving on path and Find Enemies
                if (!HasTarget)
                {
                    state = States.Moving;
                    agent.SetDestination(path[pathIdx].position);

                    //>>: Find Enemies
                    Collider[] minions = Physics.OverlapSphere(transform.position, stats.sightRange, minionLayer);

                    float shortestDistance = Mathf.Infinity;
                    GameObject nearestMinion = null;
                    foreach (var targetMinion in minions)
                    {
                        if (targetMinion.GetComponent<BaseStats>().teamID != GetComponent<BaseStats>().teamID)
                        {
                            float distanceToEnemy = Vector3.Distance(transform.position, targetMinion.transform.position);

                            if (distanceToEnemy < shortestDistance)
                            {
                                shortestDistance = distanceToEnemy;
                                nearestMinion = targetMinion.gameObject;

                                if (nearestMinion != null)
                                {
                                    if (nearestMinion.GetComponent<BaseStats>().teamID != stats.teamID)
                                    {
                                        curTarget = nearestMinion.gameObject;
                                    }
                                }
                            }
                        }
                    }
                    //<<                
                    //if (agent.remainingDistance <= 1f && pathIdx < path.Count)
                    //{
                    //    pathIdx++;
                    //    agent.SetDestination(path[pathIdx].position);
                    //}
                }

                //If hasTarget, approach to curTarget
                else
                {
                    float distance = Vector3.Distance(transform.position, curTarget.transform.position);

                    if (distance <= stats.attackRange)
                    {
                        state = States.Attacking;
                    }
                    else if (distance <= stats.sightRange)
                    {
                        state = States.Tracing;
                    }
                    else
                    {
                        state = States.Moving;
                    }
                }
            }
        }
    }
    IEnumerator MinionAction()
    {
        while (state != States.Dead)
        {
            switch (state)
            {
                case States.Idle:
                    agent.isStopped = true;
                    anim.SetBool(hashTrace, false);
                    break;
                case States.Moving:
                    agent.SetDestination(path[pathIdx].position);
                    agent.isStopped = false;
                    agent.stoppingDistance = 0;

                    anim.SetBool(hashTrace, true);
                    anim.SetBool(hashAttack, false);
                    break;
                case States.Tracing:
                    if(HasTarget)
                    {
                        agent.SetDestination(curTarget.transform.position);
                        gameObject.transform.LookAt(curTarget.transform);
                        agent.isStopped = false;
                        agent.stoppingDistance = stats.attackRange;
                    }                    
                    break;
                case States.Attacking:
                    if (HasTarget)
                    {
                        agent.isStopped = true;
                        agent.stoppingDistance = stats.attackRange;

                        transform.LookAt(curTarget.transform);
                        anim.SetBool(hashAttack, true);
                        anim.SetBool(hashTrace, false); 
                    }                        
                    break;
                case States.Casting:
                    break;
                case States.Damaged:
                    break;
                case States.Dead:                    
                    break;
            }
            yield return new WaitForSeconds(0.2f);
        }
    }
    protected override void Destroy()
    {
        base.Destroy();
        anim.SetTrigger(hashDie);
    }

    public void CheckMinionActionStart()
    {

    }
    public void CheckMinionActionEnd()
    {

    }

    public void HitTarget()
    {
        if(HasTarget)
        {
            switch(localData.attackType)
            {
                case LeagueObjectData.AttackType.Melee:
                    curTarget.GetComponent<BaseUnits>().OnDamage(curTarget.GetComponent<BaseUnits>(), stats.attackDamage, hitPoint, hitNormal);

                    if (hitEffecet != null)
                    {
                        GameObject effectIns = (GameObject)Instantiate(hitEffecet, transform.position, transform.rotation);

                        Destroy(effectIns, 2f);
                    }
                    break;
                case LeagueObjectData.AttackType.Range:
                    if (rangedProjectile != null)
                    {
                        GameObject bullet = Instantiate(rangedProjectile, firePoint.transform.position, firePoint.transform.rotation);
                        bullet.GetComponent<RangedProjectile>().SetDamage(gameObject);
                        RangedProjectile projectile = bullet.GetComponent<RangedProjectile>();

                        if (projectile != null)
                        {
                            projectile.Seek(curTarget);
                        }
                    }
                    break;
            }            
        }        
    }
    public void StartAttack()
    {
    }

    public void StopAttack()
    {
    }
}
    //void UpdatePath()
    //{
    //    if (path.Count == 0)
    //    {
    //        return;
    //    }

    //    agent.SetDestination(path[pathIdx].transform.position);

    //    //If Minions arrived to destination. update Path Idx
    //    if (agent.remainingDistance<=localData.sightRange)
    //    {          
    //        if(pathIdx<=path.Capacity)
    //        {
    //            //pathIdx++;
    //            return;
    //        }
    //    }            

    //    if(pathIdx>=path.Count)
    //    {
    //        return;
    //    }
    //}
