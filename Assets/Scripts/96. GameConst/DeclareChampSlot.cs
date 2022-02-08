using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using UnityEngine;
using ExitGames.Client.Photon;
using System;

public class DeclareChampSlot : MonoBehaviourPunCallbacks
{
    public LeagueChampionData champData;    

    public TextMeshProUGUI champNameText;
    public Image portrait;
    public int slotId = 0;
    PlayerInfo playerInfo;

    private void Awake()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(() =>
        {
            //Find Local Player's PlayerInfo gameobject.
            PlayerSetChamp();
        });
    }

    public void Initialize(LeagueChampionData whichChamp)
    {
        champData = whichChamp;
        portrait.sprite = whichChamp.portraitSquare;
        champNameText.text = whichChamp.name;
    }

    void PlayerSetChamp()
    {  
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            PlayerInfo[] players = FindObjectsOfType<PlayerInfo>();

            if(PhotonNetwork.LocalPlayer.ActorNumber==players[i].ownerId)
            {
                players[i].SetChampion(slotId);
            }
        }

        //Hashtable props = new Hashtable() 
        //{ 
        //    { GameDataSettings.PLAYER_CHAMPION, (GameDataSettings.CHAMPIONS)slotId } 
        //};

    }
}
