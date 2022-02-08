using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerStatsPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI attackDamageText;
    [SerializeField] private TextMeshProUGUI abilityPowerText;
    [SerializeField] private TextMeshProUGUI armorText;
    [SerializeField] private TextMeshProUGUI magicResistText;
    [SerializeField] private TextMeshProUGUI attackSpeedText;
    [SerializeField] private TextMeshProUGUI abilityHasteText;
    [SerializeField] private TextMeshProUGUI criticalStrikeText;
    [SerializeField] private TextMeshProUGUI moveSpeedText;

    [SerializeField] private Image health2D;
    [SerializeField] private Image resource2D;
    [SerializeField] private TextMeshProUGUI health2DText;
    [SerializeField] private TextMeshProUGUI resource2DText;

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

        InvokeRepeating("UpdateStats", 0f, Time.deltaTime);
    }
    void UpdateStats()
    {
        if (Photon.Pun.PhotonNetwork.IsConnected)
        {
            attackDamageText.text = stats.attackDamage.ToString();
            abilityPowerText.text = stats.abilityPower.ToString();
            armorText.text = stats.armor.ToString();
            magicResistText.text = stats.magicResist.ToString();
            attackSpeedText.text = stats.attackSpeed.ToString();
            abilityHasteText.text = stats.abilityHaste.ToString();
            criticalStrikeText.text = stats.criticalStrike.ToString();
            moveSpeedText.text = stats.moveSpeed.ToString();

            health2D.fillAmount = stats.health / stats.maxHealth;
            health2DText.text = string.Format($"{stats.health}/{stats.maxHealth}");

            resource2D.fillAmount = stats.resource / stats.maxResource;
            resource2DText.text = string.Format($"{stats.resourceRegen}/{stats.maxResource}");
        }            
    }
}
