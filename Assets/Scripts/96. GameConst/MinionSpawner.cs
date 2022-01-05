using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionSpawner : MonoBehaviour
{
    private List<Minion> minions = new List<Minion>();

    public GameDataSettings.TEAM teamId;

    public int waveNumber = 0;
    float waveTimer;

    public Transform midSpawnPoint;
    public Transform topSpawnPoint;
    public Transform botSpawnPoint;

    public bool midInhibitor = true;
    public bool topInhibitor = true;
    public bool botInhibitor = true;

    public Minion meleeMinion;
    public Minion castMinion;
    public Minion cannonMinion;
    public Minion superMinion;

    private void Update()
    {
        if (GameManager.Instance.GameTime < GameDataSettings.MINION_WAVESTART_TIME)
        {
            return;
        }

        else
        {
            //Instantiate Minion After WAVESTART_TIME and Spawn every MINION_SPAWNINTERVAL_TIME
            if (waveTimer <= 0f)
            {
                StartCoroutine(SpawnWave());
                waveTimer = GameDataSettings.MINION_WAVESPAWNINTERVAL_TIME;       //Reset Timer
                waveNumber++;
            }
            else
            {
                waveTimer -= Time.deltaTime;
            }
        }


        IEnumerator SpawnWave()
        {
            //if (TeamID == 0)
            //{
            for (int i = 0; i < GameDataSettings.RANGE_COUNT; i++)
            {
                SpawnUnit(castMinion.gameObject, spawnLoc: midSpawnPoint);
                SpawnUnit(castMinion.gameObject, spawnLoc: topSpawnPoint);
                SpawnUnit(castMinion.gameObject, spawnLoc: botSpawnPoint);
                yield return new WaitForSeconds(GameDataSettings.MINION_SPAWNINTERVAL);      //Spawn Interval
            }
            //}
            //else
            //{
            //    for (int i = 0; i < LoLGameSetting.MELEE_COUNT; i++)
            //    {
            //        SpawnUnit(GameDataSources.Instance.Minions[0].bluePrefab, LoLGameSetting.SPAWN_MID);
            //        SpawnUnit(GameDataSources.Instance.Minions[0].bluePrefab, LoLGameSetting.SPAWN_TOP);
            //        SpawnUnit(GameDataSources.Instance.Minions[0].bluePrefab, LoLGameSetting.SPAWN_BOT);
            //        yield return new WaitForSeconds(LoLGameSetting.MINION_SPAWNINTERVAL);      //Spawn Interval
            //    }
            //}
        }

        void SpawnUnit(GameObject prefab, Transform spawnLoc)
        {
            Minion minion = Instantiate(prefab, spawnLoc.transform.position, transform.rotation).GetComponent<Minion>();
            TeamManager teamManager = GetComponentInParent<TeamManager>();

            minions.Add(minion);
            
            if(spawnLoc==midSpawnPoint)
            {
                minion.path = teamManager.midWayPoints;
            }
            else if(spawnLoc==topSpawnPoint)
            {
                minion.path = teamManager.topWayPoints;
            }
            else
            {
                minion.path = teamManager.botWayPoints;
            }

            minion.OnDestroy += () => minions.Remove(minion);
            //minion.onDeath += () => Destroy(minion.gameObject, 1f);     //사망한 미니언 1초뒤에 파괴
        }

    }
}

