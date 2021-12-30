using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public class BaseUnits : MonoBehaviour, IClickable
{
    [Tooltip("Access to Audio Clips, Stats, UI components")]
    [SerializeField] protected LeagueObjectData localData;

    public Animator anim;
    protected NavMeshAgent agent;
    
    public event UnityAction OnDestroy;
    protected BaseStats stats;
    protected BaseUnits curTarget;
    protected List<BaseUnits> inRangeObject;

    protected Vector3 hitPoint;
    protected Vector3 hitNormal;
    protected ParticleSystem hitEffect;

    protected HealthBar healthBar;

    private States state { get; set; }
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
    protected bool HasTarget
    {
        get
        {
            //추적할 대상이 존재하고, 대상이 사망하지 않았다면 true
            if (curTarget != null && curTarget.state != States.Dead)
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
    protected bool IsTargetInRange
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

    private void Start()
    {
        //사망하지 않은 IDLE 상태로 시작
        state = States.Idle;

        healthBar = GetComponentInChildren<HealthBar>();
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>(); //will be disabled until Activate is called
        stats = GetComponent<BaseStats>();
        stats.SetStats();
    }
    public virtual IEnumerator StartAttack()
    {
        state = States.Attacking;
        yield return null;
    }

    public virtual IEnumerator StopAttack()
    {
        state = States.Idle;
        yield return null;
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
    public virtual void OnDamage(BaseUnits unit, float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        stats.health -= damage;

        if (stats.health <= 0 && state != States.Dead)
        {
            Destroy();
        }
    }
    protected virtual void Destroy()
    {
        if (OnDestroy != null)
        {
            OnDestroy();
        }

        state = States.Dead;
    }
    protected void SetTarget(BaseUnits target)
    {
        curTarget = target;
    }

    public void OnLeftClick()
    {
    }

    public void OnRightClick()
    {
    }

    public void OnHoverEnter()
    {
        Debug.Log("On Hover Enter");

        //1. Change cursor
        //2. Check team
        //3. Hitted Gameobejct Info
    }

    public void OnHoverExit()
    {
        Debug.Log("On Hover End !!");
    }
}
