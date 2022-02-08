using UnityEngine;
using UnityEngine.UI;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;
using TMPro;

public class PlayerInfo : MonoBehaviourPunCallbacks
{
    public int ownerId;
    private bool isPlayerReady;

    public Image portrait;
    public TextMeshProUGUI playerNameText;
    public TextMeshProUGUI playerChampNameText;

    public GameDataSettings.CHAMPIONS myChampion = GameDataSettings.CHAMPIONS.NULL;
    public GameDataSettings.TEAM myTeam;

    public GameDataSettings.SPELL mySpell1 = GameDataSettings.SPELL.NULL;
    public GameDataSettings.SPELL mySpell2 = GameDataSettings.SPELL.NULL;
    public Image spell1;
    public Image spell2;

    public override void OnEnable()
    {
        base.OnEnable();

        PlayerNumbering.OnPlayerNumberingChanged += OnPlayerNumberingChanged;
    }
    public override void OnDisable()
    {
        base.OnEnable();

        PlayerNumbering.OnPlayerNumberingChanged -= OnPlayerNumberingChanged;
    }
    private void OnPlayerNumberingChanged()
    {
        foreach (Player p in PhotonNetwork.PlayerList)
        {
            if (p.ActorNumber == ownerId)
            {
                Debug.Log("Actor Update");
            }
        }
    }
    private void Start()
    {
        if(PhotonNetwork.LocalPlayer.ActorNumber!=ownerId)
        {
            Debug.Log($"{PhotonNetwork.LocalPlayer.ActorNumber}, ownerId={ownerId}");
        }
        else
        {
            //:<<Set User Properties
            Hashtable initialProps = new Hashtable
            {
                {GameDataSettings.PLAYER_CHAMPION, GameDataSettings.CHAMPIONS.NULL},
                {GameDataSettings.PLAYER_SPELL1, GameDataSettings.SPELL.NULL },
                {GameDataSettings.PLAYER_SPELL2, GameDataSettings.SPELL.NULL },
                {GameDataSettings.PLAYER_READY, false},
                {GameDataSettings.PLAYER_LOADED_LEVEL, false}
            };

            PhotonNetwork.LocalPlayer.SetCustomProperties(initialProps);

            //Check Player's Ready Init
            if(PhotonNetwork.IsMasterClient)
            {
                //FindObjectOfType<NetworkManager>().
            }
            
        }
    }
    public void Initialize(int myTeamId, int playerId, string myNickName)
    {
        myTeam = (GameDataSettings.TEAM)myTeamId;
        ownerId = playerId;
        playerNameText.text = myNickName;
    }

    public void SetChampion(int selectedChamp)
    {
        myChampion = (GameDataSettings.CHAMPIONS)selectedChamp;
        portrait.sprite = DataContainer.Instance.champDataContainer.championDatasContainer[selectedChamp].portraitCircle;
        playerChampNameText.text = DataContainer.Instance.champDataContainer.championDatasContainer[selectedChamp].name;

        Hashtable initialProps = new Hashtable
        {
            { GameDataSettings.PLAYER_CHAMPION, (GameDataSettings.CHAMPIONS)selectedChamp}
        };

        PhotonNetwork.LocalPlayer.SetCustomProperties(initialProps);
    }

    public void SetSpell(int whichSpell)
    {
        switch(RoomManager.Instance.selectedButton)
        {
            case 0:
                mySpell1 = (GameDataSettings.SPELL)whichSpell;
                spell1.sprite = DataContainer.Instance.spellDataContainer.spellDatasContainer[whichSpell].icon;

                Hashtable newSpell1 = new Hashtable
                {
                    {GameDataSettings.PLAYER_SPELL1, (GameDataSettings.SPELL)whichSpell }
                };

                PhotonNetwork.LocalPlayer.SetCustomProperties(newSpell1);
                mySpell1 = (GameDataSettings.SPELL)whichSpell;
                break;
            case 1:
                mySpell2 = (GameDataSettings.SPELL)whichSpell;
                spell2.sprite = DataContainer.Instance.spellDataContainer.spellDatasContainer[whichSpell].icon;

                Hashtable newSpell2 = new Hashtable
                {
                    {GameDataSettings.PLAYER_SPELL2, (GameDataSettings.SPELL)whichSpell }
                };

                PhotonNetwork.LocalPlayer.SetCustomProperties(newSpell2);
                mySpell2 = (GameDataSettings.SPELL)whichSpell;
                break;
        }
    }

}
