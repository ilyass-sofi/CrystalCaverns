using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {

    #region enemy objecives
    [SerializeField] private GameObject[] objectives;
    [SerializeField] private GameObject shop;
    #endregion
    

    [System.Serializable]
    struct enemiescant
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
        public enemiescant[] enemiesInWave;
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
            for(int i = 0; i < waves[waveIndex].enemiesInWave.Length; i++)
            {
                waves[waveIndex].enemiesInWave[i].enemies += waves[waveIndex].enemiesInWave[i].enemiesModifier;
            }
        }
        StartCoroutine(SpawnEnemies());        
    }

    private IEnumerator SpawnEnemies()
    {
        GameObject enemy;

        int waveDificultyValue=0;
        for(int i = 0; i < waves[waveIndex].enemiesInWave.Length; i++)
        {
            waveDificultyValue += waves[waveIndex].enemiesInWave[i].enemyPrefab.GetComponent<Enemy>().GetValue() * waves[waveIndex].enemiesInWave[i].enemies;
        }

        int[] enemiesLeftToSpawn = new int[waves[waveIndex].enemiesInWave.Length];
        for(int i = 0; i < enemiesLeftToSpawn.Length; i++)
        {
            enemiesLeftTosSpawn[i] = waves[waveIndex].enemiesInWave[i].enemies;
        }

        int actualEnemyValue;

        spawning = true;
        for (int i = 0; i < waves[waveIndex].waveSplit; i++)
        {
            actualEnemyValue = 0;
            for (; actualEnemyValue < waveDificultyValue / waves[waveIndex].waveSplit;)
            {
                int enemyIndex=0;
                do
                {
                    enemyIndex = Random.Range(0, waves[waveIndex].enemiesInWave.Length);
                } while (enemiesLeftToSpawn[enemyIndex] < 1);

                enemiesLeftToSpawn[enemyIndex]--;
                enemy = waves[waveIndex].enemiesInWave[enemyIndex].enemyPrefab;

                actualEnemyValue += enemy.GetComponent<Enemy>().GetValue();
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
        tempEnemy.GetComponent<Enemy>().SetTarget(objectives[Random.Range(0, objectives.Length)]);
        tempEnemy.GetComponent<Enemy>().SetSpawner(gameObject);
        tempEnemy.GetComponent<Enemy>().SetShop(shop);
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
