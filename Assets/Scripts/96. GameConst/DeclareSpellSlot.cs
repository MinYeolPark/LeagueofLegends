using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DeclareSpellSlot : MonoBehaviour
{
    public LeagueAbilityData spellData;
    public Image icon;
    public int slotId;
    private void Awake()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(() => PlayerInfo.Instance.SetSpell(slotId));
    }
    public void Initialize(LeagueAbilityData whichSpell)
    {
        spellData = whichSpell;
        icon.sprite = whichSpell.icon;
    }
}
