using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
public class PlayerAvatar : Singleton<PlayerAvatar>
{
    public GameDataSettings.TEAM myTeam;
    public GameDataSettings.CHAMPIONS myChampion;
    public GameDataSettings.SPELL mySpell1;
    public GameDataSettings.SPELL mySpell2;

    private void Awake()
    {
        if(PhotonNetwork.IsConnected)
        {
            object playerInfo;
            if (PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue(GameDataSettings.PLAYER_CHAMPION, out playerInfo))
            {
                myChampion = (GameDataSettings.CHAMPIONS)playerInfo;
            }

            if (PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue(GameDataSettings.PLAYER_SPELL1, out playerInfo))
            {
                mySpell1 = (GameDataSettings.SPELL)playerInfo;
            }

            if (PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue(GameDataSettings.PLAYER_SPELL2, out playerInfo))
            {
                mySpell2 = (GameDataSettings.SPELL)playerInfo;
            }

            if (PhotonNetwork.LocalPlayer.GetPhotonTeam().Name == "Red")
            {
                myTeam = (GameDataSettings.TEAM)GameDataSettings.RED_TEAM;
            }
            else
            {
                myTeam = (GameDataSettings.TEAM)GameDataSettings.BLUE_TEAM;
            }

        }
    }
}
