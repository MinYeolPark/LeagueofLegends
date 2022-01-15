using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
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
    public GameObject LobbyPanel;
    public Button JoinRoomButton;

    [Space(5)]
    [Header("Room Panel Components")]
    public GameObject RoomPanel;
    public Button ChampSelectButton;
    public TMP_InputField ChatInputField;
    public TextMeshProUGUI[] ChatTexts; 

    [Space(5)]
    [Header("ChampSelect Panel Components")]
    public GameObject ChampSelectPanel;
    public Button GameStartButton;

    [Space(5)]
    /// <summary>
    /// Room Panel Components
    /// </summary>
    public GameObject pfPlayerEntry;
    public GameObject RedTeamPos;
    public GameObject BlueTeamPos;
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
    public override void OnConnectedToMaster()
    {
        LoginButton.gameObject.SetActive(true);
    }

    public override void OnJoinedLobby()
    {
        Debug.Log($"Joined Lobby=" + PhotonNetwork.CurrentLobby);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log($"PhotonNetwork.InRoom={PhotonNetwork.InRoom}");
        Debug.Log($"Player Count={PhotonNetwork.CurrentRoom.PlayerCount}");

        GameObject list = Instantiate(pfPlayerEntry, RedTeamPos.gameObject.transform.position, RedTeamPos.gameObject.transform.rotation);
        list.transform.SetParent(RedTeamPos.transform);
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
        PhotonNetwork.CreateRoom($"{userId}'s Room ", ro);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log($"Room Name={PhotonNetwork.CurrentRoom.Name}");
    }

    #region UI BUTTON'S CALLBACK
    public void OnLoginButton()
    {
        LoginPanel.SetActive(false);
        LobbyPanel.SetActive(true);
        RoomPanel.SetActive(false);
        ChampSelectPanel.SetActive(false);

        if (IdInputField.text != null)
        {
            userId = IdInputField.text;
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

    public void OnReadyButton()
    {
        LoginPanel.SetActive(false);
        LobbyPanel.SetActive(false);
        RoomPanel.SetActive(false);
        ChampSelectPanel.SetActive(true);
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

