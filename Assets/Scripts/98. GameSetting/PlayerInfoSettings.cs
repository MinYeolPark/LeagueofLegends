using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfoSettings : Singleton<PlayerInfoSettings>
{
    public string myID="Jongro Monkey";
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

    //Temp variable value
    public void SpellSetup()
    {
        mySpell1 = 0;
        mySpell2 = 1;
    }
    
}
