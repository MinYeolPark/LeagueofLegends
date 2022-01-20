using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DeclareChampSlot : MonoBehaviour
{
    public LeagueChampionData champData;    

    public TextMeshProUGUI champNameText;
    public Image portrait;
    public int slotId;

    private void Awake()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(() => PlayerInfo.Instance.SetChampion(slotId));        
    }
    public void Initialize(LeagueChampionData whichChamp)
    {
        champData = whichChamp;
        portrait.sprite = whichChamp.portraitSquare;
        champNameText.text = whichChamp.name;
    }
}
