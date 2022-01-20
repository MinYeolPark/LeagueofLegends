using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Turret : BaseStructure, IAttackable
{
    //Debug
    public TextMeshProUGUI testText;
    public LineRenderer lr;

    [Header("Attack Components")]
    public GameObject curTarget;
    public List<BaseUnits> InRangeUnits;
    public Transform firePoint;
    public GameObject rangedProjectile;

    [Header("Cash Component")]
    public LayerMask minionLayer;
    public LayerMask structureLayer;
    public LayerMask championLayer;

    [Header("Animator parameter Hash import")]
    private readonly int hashDetect = Animator.StringToHash("IsDetecting");
    private readonly int hashAttack = Animator.StringToHash("IsAttack");
    private readonly int hashDie = Animator.StringToHash("Die");

    protected override void Awake()
    {
        lr = GetComponent<LineRenderer>();
        lr.startWidth = 0.2f;
        lr.endWidth = 0.2f;

        base.Awake();        

        OnDestroy += () => Destroy(gameObject, 2f);

        //Check Turret State
        StartCoroutine(CheckTurretState());
        StartCoroutine(TurretAction());

        state =States.Idle;

        // Adds the sphere collider to the game object
        SphereCollider sc = gameObject.AddComponent<SphereCollider>();
        //sc.center = new Vector3(0, 0, 0);
        //sc.radius = stats.attackRange;
        //sc.isTrigger = true;
    }


    IEnumerator CheckTurretState()
    {
        while (state != States.Dead)
        {
            testText.text = state.ToString();
            yield return new WaitForSeconds(0.2f);

            if (state == States.Dead) yield break;

            else
            {
                //>>:Enemy Detecting Process
                Collider[] detectedObj = Physics.OverlapSphere(transform.position, stats.attackRange);

                foreach (var obj in detectedObj)
                {
                    if (obj.gameObject == this.gameObject) yield return null;

                    if (obj.TryGetComponent<BaseStats>(out BaseStats objStats))
                    {
                        Debug.Log(obj);

                        if (objStats.teamID != stats.teamID)
                        {
                            if (obj.GetComponent<BaseUnits>().state != BaseUnits.States.Dead)
                            {
                                InRangeUnits.Add(obj.GetComponent<BaseUnits>());
                            }
                        }
                    }
                }                

                GameObject closeObj = null;
                float shortestDist = Mathf.Infinity;

                foreach (var obj in InRangeUnits)
                {
                    if (obj != null && obj.GetComponent<BaseUnits>().state != BaseUnits.States.Dead)
                    {
                        float temp = Vector3.Distance(transform.position, obj.transform.position);

                        if (temp < shortestDist)
                        {
                            shortestDist = temp;
                            closeObj = obj.gameObject;

                            if (closeObj.GetComponent<BaseStats>().teamID != stats.teamID)
                            {
                                curTarget = closeObj.gameObject;
                            }
                        }
                    }
                }
                //<<

                //>>:Declare Turret Actions
                if (curTarget!=null&&curTarget.GetComponent<BaseUnits>().state!=BaseUnits.States.Dead)
                {
                    lr.enabled = false;
                    float distance = Vector3.Distance(transform.position, curTarget.transform.position);

                    if (distance <= stats.attackRange)
                    {
                        state = States.Attacking;
                    }
                }
                else
                {
                    curTarget = null;
                    state = States.Idle;
                }
                //<<
            }
        }
    }
    IEnumerator TurretAction()
    {
        while (state != States.Dead)
        {
            switch (state)
            {
                case States.Idle:
                    curTarget = null;
                    StopAttack();
                    break;
                case States.Attacking:
                    //If InRangeUnit Destory and TriggerExit, Remove from list
                    InRangeUnits.RemoveAll(GameObject => GameObject == null);

                    if (curTarget != null && curTarget.GetComponent<BaseUnits>().state != BaseUnits.States.Dead) 
                    {
                        StartAttack();
                        yield return new WaitForSeconds(5 / stats.attackSpeed);
                    }                   
                    break;
                case States.Damaged:
                    break;
                case States.Dead:
                    StopAllCoroutines();
                    break;
            }
            yield return new WaitForSeconds(0.2f);
        }
    }

    protected override void Destroy()
    {
        base.Destroy();
        anim.SetTrigger(hashDie);
        Debug.Log("anim.SetTrigger");
    }

    protected IEnumerator RangeAttack()
    {
        yield return null;
        Debug.Log($"{this.gameObject} Start to attack");
        if (curTarget!=null&&curTarget.GetComponent<BaseUnits>().state!=BaseUnits.States.Dead)
        {
            if (rangedProjectile != null)
            {
                GameObject bullet = Instantiate(rangedProjectile, firePoint.transform.position, firePoint.transform.rotation);

                bullet.GetComponent<RangedProjectile>().SetDamage(gameObject);

                RangedProjectile projectile = bullet.GetComponent<RangedProjectile>();

                if (projectile != null)
                {
                    projectile.Seek(curTarget.gameObject);
                }
            }
        }
    }

    public void StartAttack()
    {
        StartCoroutine(RangeAttack());

        lr.enabled = true;
        lr.SetPosition(0, firePoint.transform.position);
        lr.SetPosition(1, curTarget.transform.position);
    }

    public void StopAttack()
    {
        StopCoroutine(RangeAttack());

        lr.enabled = false;
        lr.SetPosition(0, new Vector3(0, 0, 0));
        lr.SetPosition(1, new Vector3(0, 0, 0));
    }
}
