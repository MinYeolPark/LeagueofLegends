using UnityEngine;
using TMPro;

public class UILevelSettings : MonoBehaviour
{
    public TextMeshProUGUI levelText;

    //temp
    private void Start()
    {
        BaseStats champStats = FindObjectOfType<BaseChampController>().GetComponent<BaseStats>();

        SetUpLevelText(champStats.level);
    }
    public void SetUpLevelText(float level)
    {        
        levelText.text = level.ToString();
    }

    public void UpdateLevelText(float level)
    {
        levelText.text = level.ToString();
    }
}
