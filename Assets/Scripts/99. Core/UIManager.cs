using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : Singleton<UIManager>
{
    public BaseChampController localChamp;
    [SerializeField] private List<LeagueAbilityData> abilities;
    [SerializeField] private List<LeagueAbilityData> spells;
    [SerializeField] private List<Transform> itemSlots;

    private void Start()
    {
        localChamp = FindObjectOfType<BaseChampController>();

        UIInit();
    }

    public void UIInit()
    {
        abilities = localChamp.localData.localChampionAbilities;

    }
}
