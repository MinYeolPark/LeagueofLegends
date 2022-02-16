using ExitGames.Client.Photon;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;

public class PlayerListEntry : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI PlayerNameText;

    public GameDataSettings.TEAM ownerTeam;
    public int ownerId;
    public int ownerTeamId;

    #region Unity
    public void OnEnable()
    {
        PlayerNumbering.OnPlayerNumberingChanged += OnPlayerNumberingChanged;
    }
    public void OnDisable()
    {
        PlayerNumbering.OnPlayerNumberingChanged -= OnPlayerNumberingChanged;
    }

    #endregion

    public void Initialize(int actorNum, string playerName)
    {
        ownerId = actorNum;
        PlayerNameText.text = playerName;

        //ownerTeamId:1 => RedTeam, ownerTeamId:2 => BlueTeam
        //ownerTeamId = PhotonNetwork.CountOfPlayersInRooms;
        if(PhotonTeamsManager.Instance.TryGetTeamByName("Red", out PhotonTeam team))
        {
            ownerTeamId = GameDataSettings.RED_TEAM;
            ownerTeam = GameDataSettings.TEAM.RED_TEAM;
        }
        else
        {
            ownerTeamId = GameDataSettings.BLUE_TEAM;
            ownerTeam = GameDataSettings.TEAM.BLUE_TEAM;
        }
        //ownerTeamId = actorNum % 2 == 1 ? GameDataSettings.RED_TEAM : GameDataSettings.BLUE_TEAM;
        Debug.Log("ownerId=" + ownerId);
        Debug.Log("playerId=" + actorNum);
        Debug.Log("TeamId=" + ownerTeamId);
    }

    private void OnPlayerNumberingChanged()
    {
        foreach (Player p in PhotonNetwork.PlayerList)
        {
            if (p.ActorNumber == ownerId)
            {
                //To Do:.. Get properties set
               // p.SetTeam(GameDataSettings.GetTeam((int)p.GetPlayerNumber());
            }
        }
    }

}
