using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : Singleton<UIManager>
{
    public BaseChampController localChamp;

    public bool HasOpended(GameObject whichPanel)
    {
        if (whichPanel.activeSelf==true) { return true; }
        return false;
    }

    private void Start()
    {
        localChamp = FindObjectOfType<BaseChampController>();

        UIInit();
    }

    public void UIInit()
    {
    }

    public void OnPanelOpenButton(GameObject whichUIPanel)
    {
        if(!HasOpended(whichUIPanel))
        {
            whichUIPanel.SetActive(true);
        }
        else
        {
            whichUIPanel.SetActive(false);
        }
    }

    public void OnPanelCloseButton(GameObject whichUIPanel)
    {
        whichUIPanel.SetActive(false); 
    }

}
