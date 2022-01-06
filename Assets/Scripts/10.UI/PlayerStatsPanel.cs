using UnityEngine;
using TMPro;

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

    BaseStats stats;

    private void Start()
    {
        GameObject localChamp = FindObjectOfType<BaseChampController>().gameObject;
        stats = localChamp.GetComponent<BaseStats>();

        InvokeRepeating("UpdateStats", 0f, Time.deltaTime);
    }

    void UpdateStats()
    {
        attackDamageText.text=stats.attackDamage.ToString();
        abilityPowerText.text=stats.abilityPower.ToString();
        armorText.text=stats.armor.ToString();
        magicResistText.text=stats.magicResist.ToString();
        attackSpeedText.text=stats.attackSpeed.ToString();
        abilityHasteText.text = stats.abilityHaste.ToString();
        criticalStrikeText.text= stats.criticalStrike.ToString();
        moveSpeedText.text=stats.moveSpeed.ToString();
    }
}
