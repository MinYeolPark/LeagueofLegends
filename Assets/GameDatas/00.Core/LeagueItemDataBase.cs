using UnityEngine;
using System.Collections.Generic;
using System;

[CreateAssetMenu(fileName = "NewItemDatabase", menuName = "League of Legends/LeagueItem Data/ItemDataBase")]
public class LeagueItemDataBase : ScriptableObject, ISerializationCallbackReceiver
{
    public List<LeagueItemData> leagueItems;
    public Dictionary<int, LeagueItemData> getItem = new Dictionary<int, LeagueItemData>();

    public void OnAfterDeserialize()
    {
        for (int i = 0; i < leagueItems.Count; i++)
        {
            leagueItems[i].Id = i;            
            getItem.Add(i, leagueItems[i]);
        }
    }
    public void OnBeforeSerialize()
    {
        getItem = new Dictionary<int, LeagueItemData>();
    }
}
