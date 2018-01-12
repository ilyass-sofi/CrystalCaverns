using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {

    #region enemy objecives
    [SerializeField] private GameObject[] objectives;
    [SerializeField] private GameObject shop;
    #endregion
    

    [System.Serializable]
    struct EnemyTypes
    {        
        public string name;
        public int enemies;
        public int enemiesModifier;
        public GameObject enemyPrefab;
    }

    private int waveIndex;

    [System.Serializable]
    struct WaveManager
    {
        public string name;
        public int untilWave;
        public int waveSplit;
        public int timeBetMiniWaves;
        public int timeBetEnemies;
        public EnemyTypes[] enemyTypesInWave;
    }

    [SerializeField] WaveManager[] waves;

    [SerializeField] private Transform[] spawners;
    
    private bool spawning;
    [SerializeField] private GameObject wayToBasePrefab;


    void Start ()
    {
        waveIndex = 0;
    }

    public void Manager(int actualWave)
    {
        if (waves[waveIndex].untilWave < actualWave)
            waveIndex++;
        else if(actualWave!=1)
        {
            for(int i = 0; i < waves[waveIndex].enemyTypesInWave.Length; i++)
            {
                waves[waveIndex].enemyTypesInWave[i].enemies += waves[waveIndex].enemyTypesInWave[i].enemiesModifier;
            }
        }
        StartCoroutine(SpawnEnemies());        
    }

    private IEnumerator SpawnEnemies()
    {
        

        int[] enemiesLeftToSpawn = new int[waves[waveIndex].enemyTypesInWave.Length];
        int totalEnemiesInWave=0;
        for (int i = 0; i < enemiesLeftToSpawn.Length; i++)
        {
            enemiesLeftToSpawn[i] = waves[waveIndex].enemyTypesInWave[i].enemies;
            totalEnemiesInWave += waves[waveIndex].enemyTypesInWave[i].enemies;

        }

        spawning = true;

        for (int wavecount = 0; wavecount < waves[waveIndex].waveSplit; wavecount++)
        {
            
            for (int actualEnemies = 0; actualEnemies < totalEnemiesInWave / waves[waveIndex].waveSplit; actualEnemies++)
            {
                int enemyIndex=0;
                do
                {
                    enemyIndex = Random.Range(0, waves[waveIndex].enemyTypesInWave.Length);
                } while (enemiesLeftToSpawn[enemyIndex] < 1);

                enemiesLeftToSpawn[enemyIndex]--;
                GameObject enemy = waves[waveIndex].enemyTypesInWave[enemyIndex].enemyPrefab;

                
                spawn(enemy);
                yield return new WaitForSeconds(waves[waveIndex].timeBetEnemies);
            }
            yield return new WaitForSeconds(waves[waveIndex].timeBetMiniWaves);
        }
        spawning = false;
        checkFin();            
    }

    private void spawn(GameObject enemy)
    {
        GameObject tempEnemy = Instantiate(enemy, spawners[Random.Range(0, spawners.Length)].position, Quaternion.identity);
        tempEnemy.GetComponent<Enemy>().SetSpawner(gameObject);
        tempEnemy.GetComponent<Enemy>().SetShop(shop);
        tempEnemy.GetComponent<Enemy>().SetTarget(objectives[Random.Range(0, objectives.Length)]);
        
        LevelManager.Instance.EnemiesAlive += 1;
    }

    public void KillEnemy()
    {
        LevelManager.Instance.EnemiesAlive -= 1;
        checkFin();
    }

    private void checkFin()
    {
        if (LevelManager.Instance.EnemiesAlive == 0 && !spawning) LevelManager.Instance.SetBuildingPhase();
    }

    public void EnemiesRoad()
    {
        for (int i = 0; i < spawners.Length; i++)
        {
            for (int j = 0; j < objectives.Length; j++)
            {
                GameObject road = Instantiate(wayToBasePrefab, spawners[i].transform.position, Quaternion.identity);
                road.GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(objectives[j].transform.position);
                road.GetComponent<RoadLaser>().shop = shop;
                road.GetComponent<RoadLaser>().currentTarget = objectives[j];
            }
        }
        

    }
}
