using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerPortraitPanel : MonoBehaviour
{
    [SerializeField] private Image playerEXPImg;
    [SerializeField] private Image playerPortraitImg;
    [SerializeField] private TextMeshProUGUI levelText;

    GameObject localChamp;
    BaseStats stats;

    public void Initialize()
    {
        if (localChamp == null)
        {
            if (Photon.Pun.PhotonNetwork.IsConnected)
            {
                localChamp = UIManager.Instance.localChamp.gameObject;
                stats = localChamp.gameObject.GetComponent<BaseStats>();
            }
        }

        playerEXPImg.fillAmount = 0;

        InvokeRepeating("UpdatePortrait", 0f, Time.deltaTime);
    }

    void UpdatePortrait()
    {
        if (Photon.Pun.PhotonNetwork.IsConnected)
        {
            playerPortraitImg.sprite = stats.localData.portraitCircle;
            playerEXPImg.fillAmount = stats.curExp / 100;
            levelText.text = stats.level.ToString();
        }
    }
}
