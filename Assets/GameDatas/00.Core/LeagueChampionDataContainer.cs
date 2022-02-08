using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "NewObject", menuName = "League of Legends/LeagueChampion Data Container", order =0)]
public class LeagueChampionDataContainer : ScriptableObject,ISerializationCallbackReceiver
{
    public List<LeagueChampionData> championDatasContainer;

    public void OnAfterDeserialize()
    {

    }

    public void OnBeforeSerialize()
    {
        List<LeagueChampionData> temp = championDatasContainer.OrderBy(x=>x.name).ToList();

        championDatasContainer = temp;
    }
}
