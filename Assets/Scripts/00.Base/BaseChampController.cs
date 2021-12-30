using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using UnityEngine.AI;


public class BaseChampController : BaseUnits
{
    //Critical
    public BaseStats target;


    float motionSmoothTime = .1f;
    public float rotateSpeedMovement = 0.1f;
    public float rotateVelocity;

    InputMaster inputManager;
    BaseSkillController skillController;

    private float indicatorOffset=0.5f;
    [SerializeField] private GameObject baseIndicator;
    [SerializeField] private CinemachineVirtualCamera localCamera;

    private void Awake()
    {
        inputManager = new InputMaster();
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

    protected virtual void OnAbility1(InputValue value)
    {
        Debug.Log("Ability1!!!!!!!!!");

        localData.localChampionAbilityies[0].Activate();
    }
    protected virtual void OnAbility2(InputValue value)
    {
        Debug.Log("Ability2");
    }
    protected virtual void OnAbility3(InputValue value)
    {
        Debug.Log("Ability3");

        if(PlayerSettings.pointerType==EPointerType.SmartPointer)
        {
            GameObject Indicator=Instantiate(localData.localChampionAbilityies[0].RangeIndicator);
            Debug.Log(Indicator.name);
        }
        Debug.Log(localData.localChampionAbilityies[0].DisplayedName.ToString());
    }
    protected virtual void OnAbility4(InputValue value)
    {
        Debug.Log("Ability4");
    }
    protected virtual void OnSummonersSpell1(InputValue value)
    {
        Debug.Log("Spell 1");
       // GameDataHolder.Instance.abilityDatas[0].Activate();
    }
    protected virtual void OnSummonersSpell2(InputValue value)
    {
        Debug.Log("Spell 2");
    }

    protected virtual void OnRightClick(InputValue value)
    {
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (Physics.Raycast(ray, out RaycastHit raycastHit, Mathf.Infinity))
            {
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

                if (raycastHit.collider.TryGetComponent(out BaseStats other))
                {
                    target = other;
                    agent.stoppingDistance = localData.attackRange;
                    Debug.Log($"{other.name}");

                    StartCoroutine(StartAttack());
                }
            }
        }
    }
    public override IEnumerator StartAttack()
    {
        Debug.Log("StartAttack");
        anim.SetBool("BaseAttack", true);

        yield return new WaitForSeconds(stats.attackRange / ((100 + stats.attackSpeed) * 0.01f));

        if(target==null)
        {
            anim.SetBool("BaseAttack", false);
            target = null;
        }
    }

    protected virtual void OnAttack(InputValue value)
    {
        GameObject attkRange = Instantiate(baseIndicator, new Vector3(transform.position.x,transform.position.y+ indicatorOffset, transform.position.z), transform.rotation, gameObject.transform) as GameObject;

    }
    public void OnTab(InputValue value)
    {
        Debug.Log("On Tab");

        UIStatusBoard statusBoard=FindObjectOfType<UIStatusBoard>().GetComponent<UIStatusBoard>();
        statusBoard.OnBoardPopup();
    }
}
