using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class BaseChampController : BaseUnits
{
    InputMaster inputManager;
    BaseSkillController skillController;
    [SerializeField] private GameObject baseIndicator;

    private void Awake()
    {
        inputManager = new InputMaster();
        anim = GetComponent<Animator>();
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
        GameDataHolder.Instance.abilityDatas[0].Activate();
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
                if (raycastHit.collider.TryGetComponent(out BaseStats other))
                {
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
    }

    protected virtual void OnAttack(InputValue value)
    {
        Debug.Log("Attack Active");

        GameObject attkRange = Instantiate(baseIndicator, transform.position, transform.rotation, gameObject.transform) as GameObject;

    }
    public void OnTab(InputValue value)
    {
        Debug.Log("On Tab");
        //if (statusBoardExtension.activeSelf == false)
        //{
        //    statusBoardExtension.SetActive(true);
        //}
        //statusBoardExtension.SetActive(false);
        
    }
}
