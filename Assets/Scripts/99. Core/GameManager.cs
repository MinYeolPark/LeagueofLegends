using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class GameManager : Singleton<GameManager>
{
    public BaseChampController localChamp;

    public TeamManager redTeam;
    public TeamManager blueTeam;
    
    private GameState curGameState;
    public float GameTime;

    void Update()
    {
        GameTime += Time.deltaTime;
    }

    private void Start()
    {
        SetupBattle();

        BaseChampController[] players;
        players = FindObjectsOfType<BaseChampController>();

        foreach (var controller in players)
        {
            if (controller.GetComponent<PhotonView>().IsMine)
            {
                localChamp = controller;
            }
            else
            {
                break;
            }


        }
    }

    private void SetupBattle()
    {
        SetState(new GameStartState(this));
    }

    public void SetState(GameState newGameState)
    {
        curGameState = newGameState;
        StartCoroutine(curGameState.GameStart());
    }
}
