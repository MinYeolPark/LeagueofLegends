using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PlayerInfoSettings : Singleton<PlayerInfoSettings>
{
    public string myID="Jongro Monkey";
    public int myChamp;
    public GameDataSettings.CHAMPIONS myChampion;
    public GameDataSettings.TEAM myTeam;
    public GameDataSettings.SPELL mySpell1;
    public GameDataSettings.SPELL mySpell2;

    private void Start()
    {
        SpellSetup();
        Debug.Log(PhotonNetwork.LocalPlayer.UserId);
        if (PlayerPrefs.HasKey("MyCharacter"))
        {
            Debug.Log(PhotonNetwork.LocalPlayer.UserId);
            myChamp = PlayerPrefs.GetInt("MyCharacter");
        }
        else
        {
            //mySelectedChampion = 0;
            //PlayerPrefs.SetInt("MyCharacter", mySelectedChampion);
            //PlayerPrefs.
        }
    }

    //Temp variable value
    public void SpellSetup()
    {
      
    }
    
}
