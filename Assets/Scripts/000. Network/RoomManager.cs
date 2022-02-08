using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RoomManager : Singleton<RoomManager>
{
    public GameObject SpellSelectPanel;
    public int selectedButton;

    public void OnSpellSelectPanel(int newButton)
    {
        selectedButton = newButton;

        if(SpellSelectPanel.gameObject.activeSelf)
        {
            SpellSelectPanel.SetActive(false);
        }
        else
        {
            SpellSelectPanel.SetActive(true);
        }
    }
}
