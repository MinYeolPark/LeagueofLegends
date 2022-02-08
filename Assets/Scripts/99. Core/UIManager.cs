using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : Singleton<UIManager>
{
    public GameObject localChamp;

    public GameScorePanel UI_GameScorePanel;
    public FpsCheckPanel UI_FpsCheckPanel;
    public PlayerPortraitPanel UI_PlayerPortraitPanel;
    public PlayerStatsPanel UI_PlayerStatsPanel;

    public SkillPanel UI_SkillPanel;
    public GameObject UI_PlayerInventoryPanel;

    private void Start()
    {
        if(localChamp==null)
        {
            if(Photon.Pun.PhotonNetwork.IsConnected)
            {
                localChamp = GameManager.Instance.localChamp.gameObject;
            }
        }

        UIInit();
    }

    public void UIInit()
    {
        UI_GameScorePanel.Initialize();
        UI_FpsCheckPanel.Initialize();
        UI_PlayerPortraitPanel.Initialize();
        //UI_SkillPanel.Initialize();
        UI_PlayerStatsPanel.Initialize();
    }

    public bool HasOpended(GameObject whichPanel)
    {
        if (whichPanel.activeSelf==true) { return true; }
        return false;
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
