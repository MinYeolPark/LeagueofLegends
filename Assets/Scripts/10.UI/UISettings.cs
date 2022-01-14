using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISettings : MonoBehaviour
{
    [SerializeField] private GameObject playerOptionPanel;

    [SerializeField] private Button smartPointerButton;
    [SerializeField] private Button manulPointerButton;
    private void OnEnable()
    {
        switch(PlayerOptionSettings.pointerType)
        {
            case EPointerType.SmartPointer:

                break;
            case EPointerType.ManualPointer:
                break;
        }
    }

    public void SetPointerMode(int ptrType)
    {
        PlayerOptionSettings.pointerType = (EPointerType)ptrType;

        switch(PlayerOptionSettings.pointerType)
        {
            case EPointerType.SmartPointer:
                break;

            case EPointerType.ManualPointer:
                break;
        }
    }

    public void OnOptionOpenButton()
    {
        playerOptionPanel.SetActive(true);
    }
    public void OnOptionCloseButton()
    {
        playerOptionPanel.SetActive(false);
    }
}
