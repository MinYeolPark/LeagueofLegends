using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerLoadingSlot : MonoBehaviour
{
    [SerializeField] private Image champSquare;
    [SerializeField] private Image playerIcon;
    [SerializeField] private Image spell1;
    [SerializeField] private Image spell2;
    [SerializeField] private TextMeshProUGUI playerName;
    [SerializeField] private TextMeshProUGUI championName;
       

    private void Awake()
    {
        champSquare.sprite = null;
        playerIcon = null;
        spell1 = null;
        spell2 = null;
        playerName.text = null;
        championName.text = null;
    }
    private void OnEnable()
    {
        
    }

    private void Start()
    {
        
    }
}
