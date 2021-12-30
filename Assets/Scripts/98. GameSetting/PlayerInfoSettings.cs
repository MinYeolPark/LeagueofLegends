using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfoSettings : Singleton<PlayerInfoSettings>
{
    public int mySelectedChampion;
    public int myTeam;
    public int mySpell1;
    public int mySpell2;

    private void Start()
    {
        SpellSetup();
        if (PlayerPrefs.HasKey("MyCharacter"))
        {
            mySelectedChampion = PlayerPrefs.GetInt("MyCharacter");
        }
        else
        {
            mySelectedChampion = 0;
            PlayerPrefs.SetInt("MyCharacter", mySelectedChampion);
        }
    }

    public void SpellSetup()
    {
        mySpell1 = 0;
        mySpell2 = 1;
    }
    
}
