using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "NewObject", menuName = "League of Legends/LeagueSpell Data Container", order = 0)]
public class LeagueSpellDataContainer : ScriptableObject, ISerializationCallbackReceiver
{ 
    public List<LeagueAbilityData> spellDatasContainer;

    public void OnAfterDeserialize()
    {

    }

    public void OnBeforeSerialize()
    {
        List<LeagueAbilityData> temp = spellDatasContainer.OrderBy(x => x.name).ToList();

        foreach (var item in temp)
        {
            Debug.Log(item);
        }
        spellDatasContainer = temp;
    }
}
