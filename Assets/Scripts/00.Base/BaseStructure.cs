using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BaseStructure : MonoBehaviour, IDamagable
{
    [Tooltip("Access to Audio Clips, Stats, UI components")]
    public LeagueObjectData localData;
    public Animator anim;

    public event UnityAction OnDestroy;
    protected BaseStats stats;    

    public virtual States state { get; set; }
    public enum States
    {
        Idle,           //Initialize
        Attacking,            
        Damaged,
        Dead            //Dead animation, before removal from play field
    }

    protected virtual void Awake()
    {
        state = States.Idle;
        anim = GetComponent<Animator>();
        stats = GetComponent<BaseStats>();
        stats.SetStats();

        OnDestroy += () => Destroy(gameObject, 2f);
    }
    public virtual void OnDamage(BaseUnits unit, float damage)
    {
        state = States.Damaged;

        stats.health -= damage;

        DamagePopup.DamageFloat(gameObject.transform.position, damage, false);

        if (stats.health <= 0 && state != States.Dead)
        {
            state = States.Dead;
            Destroy();
        }
    }

    public virtual void OnDamage(BaseUnits unit, float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        state = States.Damaged;

        stats.health -= damage;

        DamagePopup.DamageFloat(gameObject.transform.position, damage, false);

        if (stats.health <= 0 && state != States.Dead)
        {
            state = States.Dead;
            Destroy();
        }
    }

    protected virtual void Destroy()
    {
        if (OnDestroy != null)
        {
            OnDestroy();
            OnDestroy -= () => Destroy(gameObject, 2f);
        }

        StopAllCoroutines();
        gameObject.GetComponent<Collider>().enabled = false;
    }
}
