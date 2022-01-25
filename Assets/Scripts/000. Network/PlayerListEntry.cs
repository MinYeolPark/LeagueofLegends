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
    public int ownerId;
    public int ownerTeamId;

    public void OnEnable()
    {
        PlayerNumbering.OnPlayerNumberingChanged += OnPlayerNumberingChanged;
    }
    public void OnDisable()
    {
        PlayerNumbering.OnPlayerNumberingChanged -= OnPlayerNumberingChanged;
    }

    public void Initialize(int playerId, string playerName)
    {
        ownerId = playerId;
        PlayerNameText.text = playerName;

        //ownerTeamId:1 => RedTeam, ownerTeamId:2 => BlueTeam
        //Temport
        ownerTeamId = playerId % 2 == 1 ? GameDataSettings.RED_TEAM : GameDataSettings.BLUE_TEAM;
    }

    private void OnPlayerNumberingChanged()
    {
        foreach (Player p in PhotonNetwork.PlayerList)
        {
            if (p.ActorNumber == ownerId)
            {
                //To Do:.. Get properties set
            }
        }
    }

}
