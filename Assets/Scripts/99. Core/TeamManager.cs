using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun.UtilityScripts;

public class TeamManager : MonoBehaviour
{
    public GameDataSettings.TEAM teamId;
    public List<GameObject> champions;
    public List<Transform> champSpawnPoints;
    public List<Transform> midWayPoints;
    public List<Transform> botWayPoints;
    public List<Transform> topWayPoints;

    [System.Serializable]
    public class Lane
    {
        public List<Transform> wayPoints;
    }
    private void Start()
    {
        //Photon.Realtime.Player[] players;
        //if (PhotonTeamsManager.Instance.TryGetTeamMembers((byte)teamId, out players))
        //{
        //    foreach (var player in players)
        //    {
        //        champions.Add(player.);
        //    }
        //}

    }
    //public List<Transform> MakePath(GameDataSettings.TEAM team, int whichLane)
    //{
    //    //List<Transform> newPath = new List<Transform>();
    //    //GameDataSettings.TEAM whichTeam = teamId == GameDataSettings.TEAM.RED_TEAM ? GameDataSettings.TEAM.BLUE_TEAM : GameDataSettings.TEAM.RED_TEAM;

    //    //foreach (Transform wayPoint in laneSpawnPoints[whichLane].wayPoints)
    //    //{
    //    //    newPath.Add(wayPoint);
    //    //}

    //    //for (int i = laneSpawnPoints[whichLane].wayPoints.Count-1; i >=0; i--)
    //    //{
    //    //    newPath.Add(laneSpawnPoints[whichLane].wayPoints[i]);
    //    //}
    //    //return newPath;
    //}
}
