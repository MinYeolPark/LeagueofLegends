using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using UnityEngine.AI;


public class BaseChampController : BaseUnits,IAttackable
{
    //Critical
    public BaseUnits target;

    //Movement
    float motionSmoothTime = .1f;
    public float rotateSpeedMovement = 0.1f;
    public float rotateVelocity;



    //Inputs
    private PlayerInput playerInput;
    private InputMaster inputManager;
    private BaseSkillController skillController;

    private float indicatorOffset=0.5f;
    [SerializeField] private GameObject baseIndicator;
    [SerializeField] private CinemachineVirtualCamera localCamera;

    private void Awake()
    {
        inputManager = new InputMaster();
        playerInput = GetComponent<PlayerInput>();
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        
        skillController = GetComponent<BaseSkillController>();
        GetComponent<BaseStats>().SetStats();
    }

    private void OnEnable()
    {
        inputManager.Enable();
    }
    private void OnDisable()
    {
        inputManager.Disable();
    }
    private void Update()
    {
        float speed = agent.velocity.magnitude / agent.speed;
        anim.SetFloat("Speed", speed, motionSmoothTime, Time.deltaTime);     
    }

    //protected override IEnumerator RangeAttack()
    //{
    //    yield return null;
    //}

    protected virtual void OnPassive() { }
    protected virtual void OnAbility1(InputValue value) 
    {
        if(localData.localChampionAbilities[1].abilityState!=LeagueAbilityData.AbilityState.CoolDown)
        {
            localData.localChampionAbilities[1].abilityState = LeagueAbilityData.AbilityState.Active;
        }
    }

    protected virtual void OnAbility2(InputValue value) 
    {
        if (localData.localChampionAbilities[2].abilityState != LeagueAbilityData.AbilityState.CoolDown)
        {
            localData.localChampionAbilities[2].abilityState = LeagueAbilityData.AbilityState.Active;
        }
    }

    protected virtual void OnAbility3(InputValue value) 
    {
        if (localData.localChampionAbilities[3].abilityState != LeagueAbilityData.AbilityState.CoolDown)
        { 
            localData.localChampionAbilities[3].abilityState = LeagueAbilityData.AbilityState.Active;
        }
    }

    protected virtual void OnAbility4(InputValue value)
    {
        if (localData.localChampionAbilities[4].abilityState != LeagueAbilityData.AbilityState.CoolDown)
        {
            localData.localChampionAbilities[4].abilityState = LeagueAbilityData.AbilityState.Active;
        }

    }

    protected virtual void OnSummonersSpell1(InputValue value)
    {
        Debug.Log("Spell 1");
    }
    protected virtual void OnSummonersSpell2(InputValue value)
    {
        Debug.Log("Spell 2");
    }

    protected virtual void OnRecall(InputValue value)
    {
        Debug.Log("On Recall");

        anim.SetBool("Recall", true);
    }

    protected virtual void OnRightClick(InputValue value)
    {
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (Physics.Raycast(ray, out RaycastHit raycastHit, Mathf.Infinity))
            {
                state = States.Moving;

                //Target Initialize when move
                anim.SetBool("BaseAttack", false);
                target = null;

                //Move
                agent.SetDestination(raycastHit.point);
                agent.stoppingDistance = 0;

                //ROTATION
                Quaternion rotationToLookAt = Quaternion.LookRotation(raycastHit.point - transform.position);
                float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y,
                    rotationToLookAt.eulerAngles.y,
                    ref rotateVelocity,
                    rotateSpeedMovement * (Time.deltaTime * 5));

                transform.eulerAngles = new Vector3(0, rotationY, 0);

                if (raycastHit.collider.TryGetComponent(out BaseUnits other))
                {
                    state = States.Targetting;
                    target = other;
                    agent.stoppingDistance = localData.attackRange;

                    StartCoroutine(StartAttack());
                }
            }
        }
    }

    protected override IEnumerator RangeAttack()
    {
        if(target.state==States.Dead||target==null)
        {
            StartCoroutine(StopAttack());

            anim.SetBool("BaseAttack", false);
            target = null;
        }

        yield return null;
        if (localData.attackType == LeagueObjectData.AttackType.Range && state == States.Attacking)
        {
            if (rangedProjectile!=null)
            {                
                GameObject bullet = Instantiate(rangedProjectile, transform.position, transform.rotation);
                RangedProjectile projectile = bullet.GetComponent<RangedProjectile>();
                Debug.Log($"bullet={bullet}, projectile={projectile}");
                if(projectile!=null)
                {
                    projectile.Seek(target);
                }
            }
        }
    }


    protected virtual void OnAttack(InputValue value)
    {
        GameObject attkRange = Instantiate(baseIndicator, 
            new Vector3(transform.position.x,transform.position.y+ indicatorOffset, transform.position.z), 
            transform.rotation, gameObject.transform) as GameObject;
    }
    public void OnTab(InputValue value)
    {
        UIStatusBoard statusBoard=FindObjectOfType<UIStatusBoard>().GetComponent<UIStatusBoard>();
        statusBoard.OnBoardPopup();
    }

    public IEnumerator StartAttack()
    {
        if (target.state != States.Dead)
        {
            state = States.Attacking;

            anim.SetBool("BaseAttack", true);
        }
        yield return new WaitForSeconds(stats.attackRange / ((100 + stats.attackSpeed) * 0.01f));

        if(target!=null)
        {
            if (target.state == States.Dead)
            {
                state = States.Idle;
                anim.SetBool("BaseAttack", false);
                target = null;
            }
        }        
    }

    public IEnumerator StopAttack()
    {
        yield return null;
        state = States.Idle;
        anim.SetBool("BaseAttack", false);
    }
}
