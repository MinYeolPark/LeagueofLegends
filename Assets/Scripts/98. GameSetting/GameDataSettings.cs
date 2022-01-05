public static class GameDataSettings
{
    public static TEAM team;
    public enum TEAM
    {
        NEAUTRAL,
        RED_TEAM,
        BLUE_TEAM
    }

    public static LANE lane;
    public enum LANE
    {
        MID,
        TOP,
        BOT
    }

    public static int MELEE_COUNT = 3;
    public static int RANGE_COUNT = 3;
    public static int CANNON_COUNT = 1;
    public static int SUPER_COUNT = 1;
    public static int SUPER_ALL_COUNT = 2;

    public static int RED_TEAM = 1;
    public static int BLUE_TEAM = 2;
    public static int NEAUTRAL = 3;

    public static int SPAWN_MID = 0;
    public static int SPAWN_TOP = 1;
    public static int SPAWN_BOT = 2;

    public static float MINION_WAVESPAWNINTERVAL_TIME = 5f;
    public static float MINION_WAVESTART_TIME = 3;
    public static float MINION_SPAWNINTERVAL = 0.5f;
    public static float CANNON_FIRST_WAVE = 1200;
    public static float CANNON_SECOND_WAVE = 2100;
}
