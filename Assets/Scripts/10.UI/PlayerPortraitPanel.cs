using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerPortraitPanel : MonoBehaviour
{
    [SerializeField] private Image playerEXPImg;
    [SerializeField] private Image playerPortraitImg;
    [SerializeField] private TextMeshProUGUI levelText;

    BaseStats stats;
    GameObject localChamp;
    private void Start()
    {
        localChamp = FindObjectOfType<BaseChampController>().gameObject;
        stats = localChamp.GetComponent<BaseStats>();

        playerEXPImg.fillAmount = 0;

        InvokeRepeating("UpdatePortrait", 0f, Time.deltaTime);
    }

    void UpdatePortrait()
    {
        playerPortraitImg.sprite = stats.localData.portraitCircle;
        playerEXPImg.fillAmount = stats.curExp / 100;
        levelText.text = stats.level.ToString();
    }
}
