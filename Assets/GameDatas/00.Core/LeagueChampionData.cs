using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Serialization;
using System;
[CreateAssetMenu(fileName = "NewObject", menuName = "League of Legends/LeagueChampion Data")]
public class LeagueChampionData : LeagueObjectData
{
    public Category category;
    public enum Category
    {
        Tank,
        Bruser,
        ADCarry,
        Mage,
        Assasin
    }    

    public ResourceType resourceType;
    public enum ResourceType
    {
        None,
        Mana,
    }
}
