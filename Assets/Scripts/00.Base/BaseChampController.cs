using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using UnityEngine.AI;

//[RequireComponent(typeof(MouseInteractive))]
public class BaseChampController : BaseUnits,IAttackable
{
    public static BaseChampController Instance;

    public LeagueInventoryData inventory;
    //Critical
    public GameObject target;

    //Movement
    float motionSmoothTime = 0.1f;
    public float rotateSpeedMovement = 0.1f;
    public float rotateVelocity;

    float angularSpeed = 1000f;
    float accelation = 1000f;

    //Inputs
    private InputMaster inputManager;

    [SerializeField] private GameObject baseIndicator;

    bool bOnIndicator = false;
    public bool bAnimIsPlaying = false;
    //UI Binding
    SkillPanel skillPanel;

    [Header("Animator parameter Hast import")]
    public readonly int hashSpeed = Animator.StringToHash("Speed");
    public readonly int hashAttack = Animator.StringToHash("IsAttack");
    public readonly int hashCritical = Animator.StringToHash("IsCritical");
    public readonly int hashDie = Animator.StringToHash("Die");
    public readonly int hashAbilityPassive = Animator.StringToHash("Passive");
    public readonly int hashAbility1 = Animator.StringToHash("Ability1");
    public readonly int hashAbility2 = Animator.StringToHash("Ability2");
    public readonly int hashAbility3 = Animator.StringToHash("Ability3");
    public readonly int hashAbility4 = Animator.StringToHash("Ability4");

    protected override void Awake()
    {
        base.Awake();

        Instance = this;

        inputManager = new InputMaster();
        skillPanel = FindObjectOfType<SkillPanel>();

        agent.speed = stats.moveSpeed;
        agent.angularSpeed = angularSpeed;
        agent.acceleration = accelation;

        //baseIndicator.transform.localScale = new Vector3(stats.attackRange / 2, stats.attackRange / 2, stats.attackRange / 2);
        //baseIndicator.SetActive(false);
        //bOnIndicator = false;   
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
        anim.SetFloat(hashSpeed, speed, motionSmoothTime, Time.deltaTime);
    }

    //protected override IEnumerator RangeAttack()
    //{
    //    yield return null;
    //}
    protected override void Destroy()
    {
        base.Destroy();
        anim.SetTrigger(hashDie);
    }
    protected virtual void OnPassive() 
    {
        if (skillPanel.abilityCoolDown[0].coolDownComplete)
        {
            skillPanel.abilityCoolDown[0].AbilityReady();
            skillPanel.abilityCoolDown[0].ButtonTriggered();
        }
        else
        {
            skillPanel.abilityCoolDown[0].CoolDown();
        }
    }
    protected virtual void OnAbility1(InputValue value) 
    {
        if (skillPanel.abilityCoolDown[1].coolDownComplete)
        {
            skillPanel.abilityCoolDown[1].AbilityReady();
            skillPanel.abilityCoolDown[1].ButtonTriggered();
        }
        else
        {
            skillPanel.abilityCoolDown[1].CoolDown();
        }

        if (localData.localChampionAbilities[1].abilityState!=LeagueAbilityData.AbilityState.CoolDown)
        {
            localData.localChampionAbilities[1].abilityState = LeagueAbilityData.AbilityState.Active;
        }

    }

    protected virtual void OnAbility2(InputValue value) 
    {
        if(skillPanel.abilityCoolDown[2].coolDownComplete)
        {
            skillPanel.abilityCoolDown[2].AbilityReady();
            skillPanel.abilityCoolDown[2].ButtonTriggered();
        }
        else
        {
            skillPanel.abilityCoolDown[2].CoolDown();
        }

        if (localData.localChampionAbilities[2].abilityState != LeagueAbilityData.AbilityState.CoolDown)
        {
            localData.localChampionAbilities[2].abilityState = LeagueAbilityData.AbilityState.Active;
        }
    }

    protected virtual void OnAbility3(InputValue value) 
    {
        if (skillPanel.abilityCoolDown[3].coolDownComplete)
        {
            //skillPanel.abilityCoolDown[3].AbilityReady();
            skillPanel.abilityCoolDown[3].ButtonTriggered();
        }
        else
        {
            skillPanel.abilityCoolDown[3].CoolDown();
        }

        if (localData.localChampionAbilities[3].abilityState != LeagueAbilityData.AbilityState.CoolDown)
        { 
            localData.localChampionAbilities[3].abilityState = LeagueAbilityData.AbilityState.Active;
        }
    }

    protected virtual void OnAbility4(InputValue value)
    {
        if (skillPanel.abilityCoolDown[4].coolDownComplete)
        {
            skillPanel.abilityCoolDown[4].AbilityReady();
            skillPanel.abilityCoolDown[4].ButtonTriggered();
        }
        else
        {
            skillPanel.abilityCoolDown[4].CoolDown();
        }

        if (localData.localChampionAbilities[4].abilityState != LeagueAbilityData.AbilityState.CoolDown)
        {
            localData.localChampionAbilities[4].abilityState = LeagueAbilityData.AbilityState.Active;
        }
    }

    protected virtual void OnSummonersSpell1(InputValue value)
    {
        Debug.Log("Spell 1");
        skillPanel.abilityCoolDown[5].ButtonTriggered();
    }
    protected virtual void OnSummonersSpell2(InputValue value)
    {
        Debug.Log("Spell 2");
        skillPanel.abilityCoolDown[6].ButtonTriggered();
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
                anim.SetBool(hashAttack, false);
                target = null;

                //Move
                agent.SetDestination(raycastHit.point);
                agent.stoppingDistance = 0;

                //ROTATION
                //Quaternion rotationToLookAt = Quaternion.LookRotation(raycastHit.point - transform.position);
                //float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y,
                //    rotationToLookAt.eulerAngles.y,
                //    ref rotateVelocity,
                //    rotateSpeedMovement * (Time.deltaTime * 5));

                //transform.eulerAngles = new Vector3(0, rotationY, 0);

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
        if(baseIndicator.activeSelf==false)
        {
            baseIndicator.SetActive(true);
            bOnIndicator = true;
        }
        else
        {
            baseIndicator.SetActive(false);
            bOnIndicator = false;
            return;
        }
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
                transform.LookAt(target.transform);

                anim.SetBool(hashAttack, true);
            }

            //yield return new WaitForSeconds(stats.attackRange / ((100 + stats.attackSpeed) * 0.01f));

            if(obj.state==States.Dead)
            {
                state = States.Idle;
                anim.SetBool(hashAttack, false);
                target = null;
            }
        }           
    }

    public void StopAttack()
    {
        state = States.Idle;
        anim.SetBool(hashAttack, false);
    }

    public void PurchaseItem(GameObject newItem)
    {
        newItem.GetComponent<Item>();

        if(newItem)
        {

        }
    }


    /// <summary>
    /// Checking animation is playing
    /// </summary>
    /// <param name="whichSkill"></param>
    public void CheckActionStart(LeagueAbilityData whichSkill)
    {
        bAnimIsPlaying = true;
         
        if (!whichSkill.canMove)
        {
            if (agent.isActiveAndEnabled && agent.isStopped == false && agent.updateRotation == true)
            {
                state = States.Casting;
                agent.isStopped = true;
                agent.updatePosition = false;
                agent.updateRotation = false;

                agent.angularSpeed = 0f;
            }
        }
    }
    
    /// <summary>
    /// Checking animation is end
    /// </summary>
    public void CheckActionEnd()
    {
        bAnimIsPlaying = false;

        if (agent.isActiveAndEnabled && agent.isStopped == true && agent.updateRotation == false)
        {
            state = States.Idle;
            
            agent.isStopped = false;
            agent.updatePosition = true;
            agent.updateRotation = true;

            agent.angularSpeed = angularSpeed;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Trigger Entered" + other);
    }
}
