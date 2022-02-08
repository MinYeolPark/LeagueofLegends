using UnityEngine;
using TMPro;

public class GameScorePanel : MonoBehaviour
{
    public TextMeshProUGUI redKillText;
    public TextMeshProUGUI blueKillText;
    public TextMeshProUGUI playerKDAText;
    public TextMeshProUGUI minionScoreText;
    public TextMeshProUGUI gameTimeText;

    GameObject localChamp;
    BaseStats stats;

    public void Initialize()
    {
        if(localChamp==null)
        {
            if (Photon.Pun.PhotonNetwork.IsConnected)
            {
                localChamp = UIManager.Instance.localChamp.gameObject;
                stats = localChamp.gameObject.GetComponent<BaseStats>();
            }
        }


        InvokeRepeating("UpdateStatePanel", 0f, Time.deltaTime);
    }

    void UpdateStatePanel()
    {
        if (Photon.Pun.PhotonNetwork.IsConnected)
        {
            playerKDAText.text = $"{stats.kills}/{stats.deaths}/{stats.assists}";
            minionScoreText.text = stats.minionScore.ToString("D0");
            gameTimeText.text = GameManager.Instance.GameTime.ToString("00:00");
        }            
    }
}
