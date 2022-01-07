using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ObjectStatusPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI attackDamageText;
    [SerializeField] private TextMeshProUGUI abilityPowerText;
    [SerializeField] private TextMeshProUGUI armorText;
    [SerializeField] private TextMeshProUGUI magicResistText;
    [SerializeField] private TextMeshProUGUI attackSpeedText;
    [SerializeField] private TextMeshProUGUI abilityHasteText;
    [SerializeField] private TextMeshProUGUI criticalStrikeText;
    [SerializeField] private TextMeshProUGUI moveSpeedText;
    [SerializeField] private Image objPortrait;
    [SerializeField] private Transform[] objItemSlots;

    public void ObjStatusUpdate(BaseStats clickedObj)
    {
        attackDamageText.text = clickedObj.attackDamage.ToString();
        abilityPowerText.text = clickedObj.abilityPower.ToString(); ;
        armorText.text = clickedObj.attackDamage.ToString(); ;
        magicResistText.text = clickedObj.attackDamage.ToString(); ;
        attackSpeedText.text = clickedObj.attackDamage.ToString(); ;
        abilityHasteText.text = clickedObj.attackDamage.ToString(); ;
        criticalStrikeText.text = clickedObj.attackDamage.ToString(); ;
        moveSpeedText.text = clickedObj.attackDamage.ToString(); ;
        objPortrait.sprite = clickedObj.localData.portraitImage;
        //objItemSlots = stats.attackDamage.ToString(); ;
    }
}
