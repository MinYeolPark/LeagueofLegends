using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
public class PlayerInfo : MonoBehaviourPunCallbacks
{
    public static PlayerInfo Instance;
    private void Awake()
    {
        if (PlayerInfo.Instance == null)
        {
            PlayerInfo.Instance = this;
        }
        else
        {
            if (PlayerInfo.Instance != this)
            {
                Destroy(PlayerInfo.Instance.gameObject);
                PlayerInfo.Instance = this;
            }
        }
        DontDestroyOnLoad(gameObject);
    }

    public int myChamp=0;
    public GameDataSettings.CHAMPIONS myChampion;
    public GameDataSettings.TEAM myTeam;

    public Image portrait;
    public TextMeshProUGUI playerNameText;
    public TextMeshProUGUI playerChampNameText;

    public GameDataSettings.SPELL mySpell1;
    public GameDataSettings.SPELL mySpell2;
    public Image spell1;
    public Image spell2;

    //private void Start()
    //{
    //    SpellSetup();
    //    if (PlayerPrefs.HasKey("MyCharacter"))
    //    {
    //        myChamp = PlayerPrefs.GetInt("MyCharacter");
    //    }
    //    else
    //    {
    //        //mySelectedChampion = 0;
    //        //PlayerPrefs.SetInt("MyCharacter", mySelectedChampion);
    //        //PlayerPrefs.
    //    }
    //}

    public void Initialize(int myTeamId, string myNickName)
    {
        myTeam = (GameDataSettings.TEAM)myTeamId;
        playerNameText.text = myNickName;
    }
    public void SetChampion(int selectedChamp)
    {        
        myChampion = (GameDataSettings.CHAMPIONS)selectedChamp;
        portrait.sprite = DataContainer.Instance.champDataContainer.championDatasContainer[selectedChamp].portraitCircle;
    }
    //Temp variable value
    public void SpellSetup()
    {
      
    }

    public void SetFlash()
    {

    }

    public void SetTeleport()
    {

    }
    
}
