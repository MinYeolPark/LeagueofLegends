using UnityEngine;
using TMPro;

public class GameScorePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI redKillText;
    [SerializeField] private TextMeshProUGUI blueKillText;
    [SerializeField] private TextMeshProUGUI playerKDAText;
    [SerializeField] private TextMeshProUGUI minionScoreText;
    [SerializeField] private TextMeshProUGUI gameTimeText;

    BaseStats stats;
    private void Start()
    {
        GameObject localChamp = FindObjectOfType<BaseChampController>().gameObject;
        stats = localChamp.GetComponent<BaseStats>();

        InvokeRepeating("UpdateStatePanel",0f,Time.deltaTime);
    }

    void UpdateStatePanel()
    {
        playerKDAText.text = $"{stats.kills}/{stats.deaths}/{stats.assists}";
        minionScoreText.text = stats.minionScore.ToString("D0");
        gameTimeText.text = GameManager.Instance.GameTime.ToString("00:00");
    }
}
