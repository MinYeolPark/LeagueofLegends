using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;
using Photon.Pun.UtilityScripts;

public class GameStartState : GameState
{
    public GameStartState(GameManager gameState) : base(gameState)
    {
    }

    public override IEnumerator GameStart()
    {
        Debug.Log("Game Start");
        
        if(PhotonNetwork.IsConnected)
        {
            foreach (Photon.Realtime.Player player in Photon.Pun.PhotonNetwork.PlayerList)
            {
                Debug.Log(player.GetPhotonTeam().Name);
                if (player.GetPhotonTeam().Name == "Red")
                {
                    int rand = Random.Range(0, 5);
                    GameObject champ = PhotonNetwork.Instantiate(Path.Combine($"0.Champions/{PlayerAvatar.Instance.myChampion}"),
                        GameManager.Instance.redTeam.champSpawnPoints[rand].position, Quaternion.identity);

                    GameManager.Instance.redTeam.champions.Add(champ);
                    champ.GetComponent<BaseStats>().teamID = (GameDataSettings.TEAM)GameDataSettings.RED_TEAM;
                }
                else
                {
                    int rand = Random.Range(0, 5);
                    GameObject champ = PhotonNetwork.Instantiate(Path.Combine($"0.Champions/{PlayerAvatar.Instance.myChampion}"),
                        GameManager.Instance.blueTeam.champSpawnPoints[rand].position, Quaternion.identity);

                    GameManager.Instance.blueTeam.champions.Add(champ);
                    champ.GetComponent<BaseStats>().teamID = (GameDataSettings.TEAM)GameDataSettings.BLUE_TEAM;
                }
            }            
        }

        return base.GameStart();
    }
}
