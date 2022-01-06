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
                StartCoroutine(SpawnUnit(castMinion.gameObject, spawnLoc: midSpawnPoint));
                StartCoroutine(SpawnUnit(castMinion.gameObject, spawnLoc: topSpawnPoint));
                StartCoroutine(SpawnUnit(castMinion.gameObject, spawnLoc: botSpawnPoint));

                yield return new WaitForSeconds(GameDataSettings.MINION_SPAWNINTERVAL);      //Spawn Interval
            }
        }

        IEnumerator SpawnUnit(GameObject prefab, Transform spawnLoc)
        {
            TeamManager teamManager = GetComponentInParent<TeamManager>();           

            Minion minion = Instantiate(prefab, spawnLoc.transform.position, Quaternion.identity).GetComponent<Minion>();
            
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

            minions.Add(minion);
            minion.OnDestroy += () => minions.Remove(minion);
            //minion.onDeath += () => Destroy(minion.gameObject, 1f);     //사망한 미니언 1초뒤에 파괴

            yield return new WaitForSeconds(0.5f);      //0.2초마다 생성
        }

    }
}

