using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfoSettings : Singleton<PlayerInfoSettings>
{
    public string myID="Jongro Monkey";
    public GameDataSettings.CHAMPIONS myChampion;
    public GameDataSettings.TEAM myTeam;
    public GameDataSettings.SPELL mySpell1;
    public GameDataSettings.SPELL mySpell2;

    private void Start()
    {
        SpellSetup();

        if (PlayerPrefs.HasKey("MyCharacter"))
        {
           // mySelectedChampion = PlayerPrefs.GetInt("MyCharacter");
        }
        else
        {
            //mySelectedChampion = 0;
            //PlayerPrefs.SetInt("MyCharacter", mySelectedChampion);
        }
    }

    //Temp variable value
    public void SpellSetup()
    {
      
    }
    
}
