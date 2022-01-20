using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using UnityEngine.AI;

//[RequireComponent(typeof(MouseInteractive))]
public class BaseChampController : BaseUnits,IAttackable
{
    public LeagueInventoryData inventory;
    //Critical
    public GameObject target;

    //Movement
    float motionSmoothTime = .1f;
    public float rotateSpeedMovement = 0.1f;
    public float rotateVelocity;

    //Inputs
    private PlayerInput playerInput;
    private InputMaster inputManager;

    private float indicatorOffset=0.5f;
    [SerializeField] private GameObject baseIndicator;
    [SerializeField] private CinemachineVirtualCamera localCamera;

    
    protected override void Awake()
    {
        inputManager = new InputMaster();
        playerInput = GetComponent<PlayerInput>();
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        stats = GetComponent<BaseStats>();
        stats.SetStats();
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


                if (raycastHit.collider.TryGetComponent(out BaseStats obj))
                {
                    state = States.Tracing;
                    target = obj.gameObject;
                    agent.stoppingDistance = localData.attackRange;

                    StartAttack();
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
        Debug.Log("On Tab");
        UIStatusBoard statusBoard=FindObjectOfType<UIStatusBoard>();
        statusBoard.OnBoardPopup();
    }

    public void StartAttack()
    {
        if (target.TryGetComponent<BaseUnits>(out BaseUnits obj))
        {
            if(obj.state!=States.Dead)
            {
                state = States.Attacking;

                anim.SetBool("BaseAttack", true);
            }

            //yield return new WaitForSeconds(stats.attackRange / ((100 + stats.attackSpeed) * 0.01f));

            if(obj.state==States.Dead)
            {
                state = States.Idle;
                anim.SetBool("BaseAttack", false);
                target = null;
            }
        }           
    }

    public void StopAttack()
    {
        state = States.Idle;
        anim.SetBool("BaseAttack", false);
    }

    public void PurchaseItem(GameObject newItem)
    {
        newItem.GetComponent<Item>();

        if(newItem)
        {

        }
    }

    public void CheckActionStart(LeagueAbilityData whichSkill)
    {
        if (!whichSkill.canMove)
        {
            if (agent.isActiveAndEnabled && agent.isStopped == false)
            {
                state = States.Casting;
                agent.isStopped = true;
                agent.updateRotation = false;
            }
        }
    }
    public void CheckActionEnd()
    {
        if (agent.isActiveAndEnabled && agent.isStopped == true)
        {
            state = States.Idle;
            agent.isStopped = false;
            agent.updateRotation = true;
        }
    }
}
