using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager Instance;

    public BaseChampController localChamp;

    public TeamManager redTeam;
    public TeamManager blueTeam;
    
    private GameState curGameState;
    public float GameTime;

    private void Awake()
    {
        Instance = this;
    }
    void Update()
    {
        GameTime += Time.deltaTime;
    }

    private void Start()
    {
        Hashtable props = new Hashtable
            {
                {GameDataSettings.PLAYER_LOADED_LEVEL, true}
            };
        PhotonNetwork.LocalPlayer.SetCustomProperties(props);

        //Launcher Info Setting to GameScene
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
    public override void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }
    public override void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    public void SetState(GameState newGameState)
    {
        curGameState = newGameState;
        StartCoroutine(curGameState.GameStart());
    }

    private void SetupBattle()
    {
        SetState(new GameStartState(this));
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }

        if (changedProps.ContainsKey(GameDataSettings.PLAYER_LOADED_LEVEL))
        {
            if (CheckAllPlayerLoadedLevel())
            {
                //if (!startTimeIsSet)
                //{
                //    CountdownTimer.SetStartTime();
                //}
            }
            else
            {
                // not all players loaded yet. wait:
                //Debug.Log("setting text waiting for players! ", this.InfoText);
                //InfoText.text = "Waiting for other players...";
            }
        }
    }

    private bool CheckAllPlayerLoadedLevel()
    {
        foreach (Player p in PhotonNetwork.PlayerList)
        {
            object playerLoadedLevel;

            if (p.CustomProperties.TryGetValue(GameDataSettings.PLAYER_LOADED_LEVEL, out playerLoadedLevel))
            {
                if ((bool)playerLoadedLevel)
                {
                    continue;
                }
            }

            return false;
        }

        return true;
    }
    
}
