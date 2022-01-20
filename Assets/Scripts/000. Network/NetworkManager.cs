using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using ExitGames.Client.Photon;
using System.Collections.Generic;
public class NetworkManager : MonoBehaviourPunCallbacks
{
    //Proto
    private readonly string version = "1.0";
    private string userId = "Jongro Monkey";

    /// <summary>
    /// Launcher Components
    /// </summary>
    [Space(5)] 
    [Header("Login Panel Components")]
    public GameObject LoginPanel;
    public Button LoginButton;
    public TMP_InputField IdInputField;

    [Space(5)]
    [Header("Lobby Panel Components")]
    public TextMeshProUGUI LobbyPlayersText;
    public GameObject LobbyPanel;
    public Button JoinRoomButton;

    [Space(5)]
    [Header("Room Panel Components")]
    private Dictionary<int, GameObject> RedListEntries=new Dictionary<int, GameObject>();
    private Dictionary<int, GameObject> BlueListEntries=new Dictionary<int, GameObject>();

    public GameObject RoomPanel;
    public Button ChampSelectButton;
    public Button LeaveRoomButton;
    public TextMeshProUGUI ChatInputField;
    public TextMeshProUGUI[] ChatTexts;
    public GameObject pfPlayerEntry;
    public GameObject RedTeamPos;
    public GameObject BlueTeamPos;
    public Transform ChampSlotsPos;

    [Space(5)]
    [Header("ChampSelect Panel Components")]
    public GameObject ChampSelectPanel;
    public Button LockerButton;
    public Button GameExitButton;
    public GameObject pfDeclareChampSlot;
    public GameObject pfPlayerInfo;
    public GameObject pfOppositePlayerSlot;
    public Transform RedTeamChampPos;
    public Transform BlueTeamChampPos;
    public TextMeshProUGUI CountText;
    float count = 10f;


    private void Awake()
    {
        if (PhotonNetwork.IsConnected == false)
        {
            //TO DO: Singleton

            //Sync with Master Client
            PhotonNetwork.AutomaticallySyncScene = true;
            //Set GameVersion
            PhotonNetwork.GameVersion = version;
            //Set Player Name from Input TextField
            PhotonNetwork.NickName = userId;
            //Enter the Photon Server
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    private void Update()
    {
        count -= Time.deltaTime;
        CountText.text = count.ToString("N0");

        if(count<=0)
        {
            //TO DO: Game Start
        }
    }
    public override void OnConnectedToMaster()
    {
        LoginButton.gameObject.SetActive(true);
    }
    public void Disconnect() => PhotonNetwork.Disconnect();
    public override void OnDisconnected(DisconnectCause cause)
    {
        LoginPanel.SetActive(true);
        LobbyPanel.SetActive(false);
        RoomPanel.SetActive(false);
        ChampSelectPanel.SetActive(false);
    }
    public override void OnJoinedLobby()
    {
        LobbyPlayersText.text = $"Lobby Players = { PhotonNetwork.CountOfPlayers - PhotonNetwork.CountOfPlayersInRooms}" +
            $"/ Players = {PhotonNetwork.CountOfPlayers}\nPlayerName = {PhotonNetwork.NickName}";
    }

    public override void OnJoinedRoom()
    {
        foreach (Player p in PhotonNetwork.PlayerList)
        {
            GameObject entry = Instantiate(pfPlayerEntry);

            //Set TeamId
            entry.GetComponent<PlayerListEntry>().Initialize(p.ActorNumber, p.NickName);
            //Seperate Team Id and set Parent
            if(entry.GetComponent<PlayerListEntry>().ownerTeamId==GameDataSettings.RED_TEAM)
            {
                entry.transform.SetParent(RedTeamPos.transform);
                RedListEntries.Add(p.ActorNumber, entry);
            }
            else
            {
                entry.transform.SetParent(BlueTeamPos.transform);
                BlueListEntries.Add(p.ActorNumber, entry);
            }

            //object isPlayerReady;
            //if (p.CustomProperties.TryGetValue(AsteroidsGame.PLAYER_READY, out isPlayerReady))
            //{
            //    entry.GetComponent<PlayerListEntry>().SetPlayerReady((bool)isPlayerReady);
            //}            
        }

        //StartGameButton.gameObject.SetActive(CheckPlayersReady());

        //Hashtable props = new Hashtable
        //    {
        //        {AsteroidsGame.PLAYER_LOADED_LEVEL, false}
        //    };
        //PhotonNetwork.LocalPlayer.SetCustomProperties(props);



        //GameObject list = Instantiate(pfPlayerEntry, RedTeamPos.gameObject.transform.position, RedTeamPos.gameObject.transform.rotation);
        //list.transform.SetParent(RedTeamPos.transform);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log(message);

        //Setting Room properties
        RoomOptions ro = new RoomOptions();
        ro.MaxPlayers = 10;
        ro.IsOpen = true;
        ro.IsVisible = true;        
        //Make Room
        PhotonNetwork.CreateRoom($"{PhotonNetwork.NickName}'s Room ", ro);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log($"Room Name={PhotonNetwork.CurrentRoom.Name}");
    }

    public override void OnCreateRoomFailed(short returnCode, string message) 
    {
        Debug.Log($"Create Room Failed cause of ={message}");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        ChatRPC("<color=yellow>" + newPlayer.NickName + "has joined the room</color>");

        GameObject entry = Instantiate(pfPlayerEntry);
        entry.GetComponent<PlayerListEntry>().Initialize(newPlayer.ActorNumber, newPlayer.NickName);
        if (entry.GetComponent<PlayerListEntry>().ownerTeamId == GameDataSettings.RED_TEAM)
        {
            entry.transform.SetParent(RedTeamPos.transform);
            RedListEntries.Add(newPlayer.ActorNumber, entry);
        }
        else
        {
            entry.transform.SetParent(BlueTeamPos.transform);
            BlueListEntries.Add(newPlayer.ActorNumber, entry);
        }        
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        ChatRPC("<color=yellow>" + otherPlayer.NickName + "has leved the room</color>");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        //ClearRoomListView();

        //UpdateCachedRoomList(roomList);
        //UpdateRoomListView();
    }

    #region UI BUTTON'S CALLBACK
    public void OnLoginButton()
    {
        LoginPanel.SetActive(false);
        LobbyPanel.SetActive(true);
        RoomPanel.SetActive(false);
        ChampSelectPanel.SetActive(false);

        if (!string.IsNullOrEmpty(IdInputField.text))
        {
            Debug.Log("InputfieldText" + IdInputField.text);
            PhotonNetwork.NickName = IdInputField.text;
        }
        PhotonNetwork.JoinLobby();
    }

    public void OnJoinRoomButton()
    {
        LoginPanel.SetActive(false);
        LobbyPanel.SetActive(false);
        RoomPanel.SetActive(true);
        ChampSelectPanel.SetActive(false);

        PhotonNetwork.JoinRandomRoom();
    }

    public void OnLeaveRoomButton()
    {
        LoginPanel.SetActive(false);
        LobbyPanel.SetActive(true);
        RoomPanel.SetActive(false);
        ChampSelectPanel.SetActive(false);

        PhotonNetwork.LeaveRoom();
    }

    public void OnChampSelectButton()
    {
        count = 10f;

        LoginPanel.SetActive(false);
        LobbyPanel.SetActive(false);
        RoomPanel.SetActive(false);
        ChampSelectPanel.SetActive(true);

        //Player Data Instantiate
        for (int i = 0; i < RedListEntries.Count; i++)
        {
            GameObject p = Instantiate(pfPlayerInfo);
            p.transform.SetParent(RedTeamChampPos);
            p.GetComponent<PlayerInfo>().Initialize(GameDataSettings.RED_TEAM, PhotonNetwork.NickName);
        }
        for (int i = 0; i < BlueListEntries.Count; i++)
        {
            GameObject p = Instantiate(pfPlayerInfo);
            p.transform.SetParent(BlueTeamChampPos);
            p.GetComponent<PlayerInfo>().Initialize(GameDataSettings.BLUE_TEAM, PhotonNetwork.NickName);
        }        

        //Champ Data Instantiate
        for (int i = 0; i < DataContainer.Instance.champDataContainer.championDatasContainer.Capacity; i++)
        {
            GameObject champ = Instantiate(pfDeclareChampSlot, ChampSlotsPos.transform.position, ChampSlotsPos.transform.rotation);
            champ.GetComponent<DeclareChampSlot>().Initialize(DataContainer.Instance.champDataContainer.championDatasContainer[i]);
            champ.GetComponent<DeclareChampSlot>().slotId = i;
            champ.transform.SetParent(ChampSlotsPos);
        }        
    }

    public void OnGameStartButton()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;
            PhotonNetwork.LoadLevel(1);
        }
        
        //TO DO: Loading Scene Progress
    }
    #endregion

    #region CHATTING
    public void OnChatSendButton()
    {
        Debug.Log(ChatInputField.text);
        photonView.RPC("ChatRPC", RpcTarget.All, PhotonNetwork.NickName + " : " + ChatInputField.text);
        ChatInputField.text = "";
    }

    [PunRPC] // RPC는 플레이어가 속해있는 방 모든 인원에게 전달한다
    void ChatRPC(string msg)
    {
        bool isInput = false;
        for (int i = 0; i < ChatTexts.Length; i++)
            if (ChatTexts[i].text == "")
            {
                isInput = true;
                ChatTexts[i].text = msg;
                break;
            }
        if (!isInput) // 꽉차면 한칸씩 위로 올림
        {
            for (int i = 1; i < ChatTexts.Length; i++) ChatTexts[i - 1].text = ChatTexts[i].text;
            ChatTexts[ChatTexts.Length - 1].text = msg;
        }
    }
    #endregion
}

