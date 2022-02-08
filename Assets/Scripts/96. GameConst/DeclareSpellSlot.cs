using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class DeclareSpellSlot : MonoBehaviourPunCallbacks
{
    public LeagueAbilityData spellData;
    public Image icon;
    public int slotId;

    private void Awake()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(PlayerSetSpell);
    }

    public void Initialize(LeagueAbilityData whichSpell)
    {
        spellData = whichSpell;
        icon.sprite = whichSpell.icon;
    }

    void PlayerSetSpell()
    {
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            PlayerInfo[] players = FindObjectsOfType<PlayerInfo>();

            if (PhotonNetwork.LocalPlayer.ActorNumber == players[i].ownerId)
            {
                players[i].SetSpell(slotId);
            }
        }

        RoomManager.Instance.SpellSelectPanel.SetActive(false);
        //Hashtable props = new Hashtable()
        //{
        //    { GameDataSettings.PLAYER_CHAMPION, (GameDataSettings.CHAMPIONS)slotId }
        //};
    }
}
