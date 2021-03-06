using UnityEngine;

public static class GameDataSettings
{
    //All champion have to update
    public static CHAMPIONS champions = CHAMPIONS.NULL;
    public enum CHAMPIONS
    {
        AHRI,
        GAREN,
        NULL,
    }
    public static TEAM team;
    public enum TEAM
    {
        NEAUTRAL,
        RED_TEAM,
        BLUE_TEAM
    }
    public static SPELL spell;
    public enum SPELL
    {
        FLASH,
        TELEPORT,
        //BARRIER,
        //CLARITY,
        //CLEANSE,
        //EXHAUST,
        //GHOST,
        //HEAL,
        //IGNITE,
        //SMITE,
        //MARK_DASH,      //FOR ARAM
        NULL
    }

    public static LANE lane;
    public enum LANE
    {
        MID,
        TOP,
        BOT
    }
    public static TEAM GetTeam(int playerIdx)
    {
        int teamId = playerIdx%2;

        switch (teamId)
        {
            case 0: return TEAM.RED_TEAM;
            case 1: return TEAM.BLUE_TEAM;

            default:
                Debug.LogError($"Get Team team ID={teamId}, Team Devidede Error");
                return TEAM.RED_TEAM;
        }
    }

    public const int MELEE_COUNT = 3;
    public const int RANGE_COUNT = 3;
    public const int CANNON_COUNT = 1;
    public const int SUPER_COUNT = 1;
    public const int SUPER_ALL_COUNT = 2;

    public const int NEAUTRAL = 0;
    public const int RED_TEAM = 1;
    public const int BLUE_TEAM = 2;

    public const int SPAWN_MID = 0;
    public const int SPAWN_TOP = 1;
    public const int SPAWN_BOT = 2;

    public const float MINION_WAVESPAWNINTERVAL_TIME = 5f;
    public const float MINION_WAVESTART_TIME = 3;
    public const float MINION_SPAWNINTERVAL = 0.7f;
    public const float CANNON_FIRST_WAVE = 1200;
    public const float CANNON_SECOND_WAVE = 2100;

    //Player Info Coponents
    public const string PLAYER_CHAMPION = "HasPlayerChampion";
    public const string PLAYER_SPELL1 = "HasPlayerSpell1";
    public const string PLAYER_SPELL2 = "HasPlayerSpell2";
    public const string PLAYER_READY = "IsPlayerLocked";
    public const string PLAYER_LOADED_LEVEL = "PlayerLoadedLevel";
}
