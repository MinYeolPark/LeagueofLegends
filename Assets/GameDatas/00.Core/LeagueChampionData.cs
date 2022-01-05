using UnityEngine;
using System.Collections.Generic;

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
}
