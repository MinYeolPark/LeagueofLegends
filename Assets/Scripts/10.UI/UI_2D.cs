using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UI_2D : MonoBehaviour
{
    public GameObject statsBoardExtension;
    public GameObject optionExtension;
    public GameObject statusBoardExtension;
    InputMaster inputManager;

    private void Awake()
    {
        inputManager = new InputMaster();
    }

    private void OnEnable()
    {
        inputManager.Enable();
    }
    private void OnDisable()
    {
        inputManager.Disable();
    }
    public void OnTab(InputValue value)
    {
        Debug.Log("On Tab");
        if (statusBoardExtension.activeSelf == false)
        {
            statusBoardExtension.SetActive(true);
        }
        statusBoardExtension.SetActive(false);
    }

    public void OnStatsButton()
    {
        if (statsBoardExtension.activeSelf == false)
        {
            statsBoardExtension.SetActive(true);
        }
        statsBoardExtension.SetActive(false);
    }

    public void OnOptionButton()
    {
        if(optionExtension.activeSelf==false)
        {
            optionExtension.SetActive(true);
        }
        optionExtension.SetActive(false);
    }
    public void OnRuneButton()
    {

    }

    public void OnStoneButton()
    {

    }
}
