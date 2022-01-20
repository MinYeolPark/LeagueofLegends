using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerListEntry : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI PlayerNameText;
    public int ownerId;
    public int ownerTeamId;

    public void OnEnable()
    {
        //PlayerNumbering.OnPlayerNumberingChanged += OnPlayerNumberingChanged;
    }

    public void Initialize(int playerId, string playerName)
    {
        ownerId = playerId;
        PlayerNameText.text = playerName;

        //ownerTeamId:1 => RedTeam, ownerTeamId:2 => BlueTeam
        ownerTeamId = playerId % 2 == 1 ? GameDataSettings.RED_TEAM : GameDataSettings.BLUE_TEAM;
    }
}
