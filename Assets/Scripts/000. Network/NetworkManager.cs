using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using Photon.Pun.UtilityScripts;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using System.Collections;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public static NetworkManager Instance;

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
    public GameObject RoomPanel;
    public Button ChampSelectButton;
    public Button LeaveRoomButton;
    public TextMeshProUGUI ChatInputField;
    public TextMeshProUGUI[] ChatTexts;
    public GameObject pfPlayerEntry;
    public GameObject RedTeamPos;
    public GameObject BlueTeamPos;
    public Transform ChampSlotsPos;
    public Transform SpellSlotsPos;
    public Button Spell1Button;
    public Button Spell2Button;
    private Dictionary<int, GameObject> playerListEntries;

    [Space(5)]
    [Header("ChampSelect Panel Components")]
    public GameObject ChampSelectPanel;
    public Button LockerButton;
    public Button GameExitButton;
    public GameObject pfDeclareChampSlot;
    public GameObject pfDeclareSpellSlot;
    public GameObject pfPlayerInfo;
    public GameObject pfOppositePlayerSlot;
    public Transform RedTeamChampPos;
    public Transform BlueTeamChampPos;
    public TextMeshProUGUI CountText;
    float count = 10f;

    [Space(5)]
    [Header("Loading Panel Components")]
    public GameObject LoadingScreenPanel;
    public GameObject pfPlayerLoadingSlot;

    //Loading Bar
    public Transform slotLayerPos;
    public Image loadingFillImage;
    public TextMeshProUGUI loadingPercentText;

    private void Awake()
    {
        Instance = this;

        if (PhotonNetwork.IsConnected == false)
        {
            LoginButton.interactable = false;

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
        LoginButton.interactable = true;
    }
    public void Disconnect() => PhotonNetwork.Disconnect();
    public override void OnDisconnected(DisconnectCause cause)
    {
        LoginPanel.SetActive(true);
        LobbyPanel.SetActive(false);
        RoomPanel.SetActive(false);
        ChampSelectPanel.SetActive(false);

        JoinRoomButton.interactable = false;
        Debug.LogError($"Disconnected from server = {cause}");        
    }

    public override void OnJoinedLobby()
    {
        LobbyPlayersText.text = $"Lobby Players = { PhotonNetwork.CountOfPlayers - PhotonNetwork.CountOfPlayersInRooms}" +
            $"/ Players = {PhotonNetwork.CountOfPlayers}\nPlayerName = {PhotonNetwork.NickName}";
    }

    public override void OnJoinedRoom()
    {
        if(!PhotonNetwork.IsMasterClient)
        {
            ChampSelectButton.gameObject.SetActive(false);
        }       

        if (playerListEntries == null)
        {
            playerListEntries = new Dictionary<int, GameObject>();
        }

        foreach (Player p in PhotonNetwork.PlayerList)
        {
            GameObject entry = Instantiate(pfPlayerEntry);

            //Set TeamId
            entry.GetComponent<PlayerListEntry>().Initialize(p.ActorNumber, p.NickName);
            //Seperate Team Id and set Parent
            if(entry.GetComponent<PlayerListEntry>().ownerTeamId==GameDataSettings.RED_TEAM)
            {
                entry.transform.SetParent(RedTeamPos.transform);
                PhotonNetwork.LocalPlayer.JoinTeam("Red");
            }
            else
            {
                entry.transform.SetParent(BlueTeamPos.transform);
                PhotonNetwork.LocalPlayer.JoinTeam("Blue");
            }

            playerListEntries.Add(p.ActorNumber, entry);

            //object isPlayerReady;
            //if (p.CustomProperties.TryGetValue(GameDataSettings.PLAYER_READY, out isPlayerReady))
            //{
            //    //entry.GetComponent<PlayerListEntry>().SetPlayerReady((bool)isPlayerReady);
            //}
        }
    }

    public override void OnLeftRoom()
    {
        LobbyPanel.SetActive(true);        

        foreach (GameObject entry in playerListEntries.Values)
        {
            Destroy(entry.gameObject);
        }
        
        playerListEntries.Clear();
        playerListEntries = null;
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
        }
        else
        {
            entry.transform.SetParent(BlueTeamPos.transform);
        }        
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        ChatRPC("<color=yellow>" + otherPlayer.NickName + "has leved the room</color>");

        Destroy(playerListEntries[otherPlayer.ActorNumber].gameObject);
        playerListEntries.Remove(otherPlayer.ActorNumber);
    }

    //Spell, Champion Setting Update
    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        if (playerListEntries == null)
        {
            playerListEntries = new Dictionary<int, GameObject>();
        }
    }

    #region UI BUTTON'S CALLBACK
    public void OnLoginButton()
    {
        LoginButton.interactable = false;

        if(PhotonNetwork.IsConnected)
        {
            LoginPanel.SetActive(false);
            LobbyPanel.SetActive(true);
            RoomPanel.SetActive(false);
            ChampSelectPanel.SetActive(false);
        }        

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
        if(PhotonNetwork.IsMasterClient)
        {            
            photonView.RPC("ChampSelect", RpcTarget.All);
        }
    }

    [PunRPC]
    void ChampSelect()
    {
        count = 10f;

        LoginPanel.SetActive(false);
        LobbyPanel.SetActive(false);
        RoomPanel.SetActive(false);
        ChampSelectPanel.SetActive(true);

        //>>: Player Data Instantiate

        foreach (Player player in PhotonNetwork.PlayerList)
        {
            if(player.GetPhotonTeam().Name=="Red")
            {
                GameObject p = Instantiate(pfPlayerInfo);
                p.transform.SetParent(RedTeamChampPos);
                p.GetComponent<PlayerInfo>().Initialize(GameDataSettings.RED_TEAM, player.ActorNumber, player.NickName);
            }
            else
            {
                GameObject p = Instantiate(pfPlayerInfo);
                p.transform.SetParent(BlueTeamChampPos);
                p.GetComponent<PlayerInfo>().Initialize(GameDataSettings.BLUE_TEAM, player.ActorNumber, player.NickName);
            }
        }       
        //:<<

        //Champ Data Instantiate
        for (int i = 0; i < DataContainer.Instance.champDataContainer.championDatasContainer.Capacity; i++)
        {
            GameObject champ = Instantiate(pfDeclareChampSlot, ChampSlotsPos.transform.position, ChampSlotsPos.transform.rotation);
            champ.GetComponent<DeclareChampSlot>().Initialize(DataContainer.Instance.champDataContainer.championDatasContainer[i]);
            champ.GetComponent<DeclareChampSlot>().slotId = i;
            champ.transform.SetParent(ChampSlotsPos);
        }

        //Spell Data Instantiate
        for (int i = 0; i < DataContainer.Instance.spellDataContainer.spellDatasContainer.Capacity; i++)
        {
            GameObject spells = Instantiate(pfDeclareSpellSlot, SpellSlotsPos.transform.position, SpellSlotsPos.transform.rotation);
            spells.GetComponent<DeclareSpellSlot>().Initialize(DataContainer.Instance.spellDataContainer.spellDatasContainer[i]);
            spells.GetComponent<DeclareSpellSlot>().slotId = i;
            spells.transform.SetParent(SpellSlotsPos);
        }        
    }

    public void OnGameExitButton()
    {
        LoginPanel.SetActive(false);
        LobbyPanel.SetActive(true);
        RoomPanel.SetActive(false);
        ChampSelectPanel.SetActive(false);

        PhotonNetwork.LeaveRoom();
    }
    public void OnGameStartButton()
    {
        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.CurrentRoom.IsVisible = false;

        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;
            //PhotonNetwork.LoadLevel(1);
        }

        LoginPanel.SetActive(false);
        LobbyPanel.SetActive(false);
        RoomPanel.SetActive(false);
        ChampSelectPanel.SetActive(false);
        LoadingScreenPanel.SetActive(true);

        //>>: To Do -> Update Loading Screen
        GameObject slots = Instantiate(pfPlayerLoadingSlot);
        slots.transform.SetParent(slotLayerPos);
        //:<<

        StartCoroutine(BeginLoad());
    }
    private IEnumerator BeginLoad()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(1);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            loadingFillImage.fillAmount = progress;
            loadingPercentText.text = Mathf.Round(progress * 100) + "%";

            yield return new WaitForSeconds(3f);
        }
        operation = null;
    }
    #endregion

    #region CHATTING
    public void OnChatSendButton()
    {
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

