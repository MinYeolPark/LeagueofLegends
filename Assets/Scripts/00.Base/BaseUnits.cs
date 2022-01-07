using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public class BaseUnits : MonoBehaviour, IDamagable
{
    [Tooltip("Access to Audio Clips, Stats, UI components")]
    public LeagueObjectData localData;
    public Animator anim;
    protected NavMeshAgent agent;
    
    public event UnityAction OnDestroy;
    protected BaseStats stats;
    protected GameObject curTarget;
    protected List<BaseUnits> inRangeObject;

    protected Vector3 hitPoint;
    protected Vector3 hitNormal;
    protected ParticleSystem hitEffect;
    protected GameObject basePrjoectile;

    protected bool canMove = true;
    protected bool CanMove { get { return canMove; } set { canMove = value; } }

    protected GameObject rangedProjectile;

    public virtual States state { get; set; }
    public enum States
    {
        Idle,           //Initialize
        Moving,         //Update Path
        Targetting,     //Update Target
        Attacking,      //Start Attack
        Casting,        //Skill or Spell Casting Animation, not moving
        Damaged,        //Decision for cut Casting
        Dead            //Dead animation, before removal from play field
    }


    public bool HasTarget
    {
        get
        {
            //추적할 대상이 존재하고, 대상이 사망하지 않았다면 true
            if (curTarget != null && curTarget.GetComponent<BaseUnits>().state != States.Dead)
            {
                return true;
            }
            else
            {
                curTarget = null;
            }

            return false;
        }
    }
    public bool IsTargetInRange
    {
        get
        {
            if (HasTarget)
            {
                return (transform.position - curTarget.transform.position).sqrMagnitude <= stats.attackRange * stats.attackRange;
            }
            return false;
        }
    }


    protected virtual void Start()
    {
        //사망하지 않은 IDLE 상태로 시작
        state = States.Idle;

        //healthBar = GetComponentInChildren<HealthBar>();
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>(); //will be disabled until Activate is called
        stats = GetComponent<BaseStats>();
        stats.SetStats();

        OnDestroy += () => Destroy(gameObject, 2f);

        if (localData.attackType==LeagueObjectData.AttackType.Range)
        {
            rangedProjectile = localData.projectile;
        }
        else
        {
            return;
        }
    }

    protected virtual IEnumerator MeleeAttack()
    {
        Debug.Log("MeleeAttack Coroutine");
        yield return null;
    }

    protected virtual IEnumerator RangeAttack()
    {
        Debug.Log("RangeAttack Coroutine");
        
        yield return null;
    }

    protected virtual IEnumerator CombatInterval()
    {
        yield return null;
    }

    //계속해서 데미지를 주는 주체, 데미지를 받아온다
    public void OnDamage(BaseUnits unit, float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        state = States.Damaged;

        stats.health -= damage;

        if (stats.health <= 0 && state != States.Dead)
        {
            Destroy();
        }
    }
    protected virtual void Destroy()
    {
        state = States.Dead;

        if (OnDestroy != null)
        {
            OnDestroy();
            Debug.Log("Destroy Object");
        }

        agent.isStopped = true;
        agent.enabled = false;

        //If animation is playing
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            return;
        }
        else
        {
            anim.SetTrigger("Dead");
        }
        Collider[] colliders = GetComponents<Collider>();

        foreach (Collider col in colliders)
        {
            col.enabled = false;
        }
    }

    protected void SetTarget(GameObject target)
    {
        curTarget = target;
    }

}
