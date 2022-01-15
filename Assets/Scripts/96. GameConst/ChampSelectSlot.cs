using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChampSelectSlot : MonoBehaviour
{    
    public void OnClickCharacterPick(int whichCharacter)
    {
        if (PlayerInfoSettings.Instance != null) 
        {
            PlayerInfoSettings.Instance.myChamp = whichCharacter;
            PlayerPrefs.SetInt("MyChampion", whichCharacter);
        }
    }
}
