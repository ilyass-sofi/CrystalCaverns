using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {

    #region enemy objecives
    [SerializeField] private GameObject[] objectives;
    [SerializeField] private GameObject shop;
    #endregion

    [SerializeField] private Transform[] spawners;

    private bool spawning;
    
    [SerializeField] private int defaultTimeBetEnemies;
    private int newTimeBetEnemies;

    [SerializeField] private float timeBetWaves;
    private int wavesInWave;

    private int actualEnemyValue;
    [SerializeField] private int waveDificultyValue;
    [SerializeField] private int waveDificultyModifier;

    [SerializeField] private GameObject[] enemies;




    // Use this for initialization
    void Start () {
        newTimeBetEnemies = defaultTimeBetEnemies;
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    public void Manager(int actualWave)
    {
        if(actualWave == 1)
        {
            timeBetWaves = 0;
            wavesInWave = 1;
            waveDificultyValue = 2;
        }
        else if (actualWave <= 5)
        {
            timeBetWaves = 0;
            wavesInWave = 1;
            waveDificultyValue += 4;
        }
        else if (actualWave <= 10)
        {
            timeBetWaves = 5;
            wavesInWave = 2;
            waveDificultyValue += 6;
        }
        else
        {
            timeBetWaves = 5;
            wavesInWave = 3;
            waveDificultyValue *= 2;
        }
        StartCoroutine(SpawnEnemies());        
    }

    private IEnumerator SpawnEnemies()
    {
        GameObject enemy;
        int enemyValue;

        newTimeBetEnemies = defaultTimeBetEnemies;
        spawning = true;
        for(int i = 0; i < wavesInWave;i++)
        {
            actualEnemyValue = 0;
            for (; actualEnemyValue < waveDificultyValue / wavesInWave;)
            {
                enemy = enemies[Random.Range(0, enemies.Length)];
                enemyValue = enemy.GetComponent<Enemy>().GetValue();
                while (actualEnemyValue + enemyValue > waveDificultyValue/wavesInWave)//loop while the enemy value is higher than the lvl dificulty
                {
                    enemy = enemies[Random.Range(0, enemies.Length - 1)];
                    enemyValue = enemy.GetComponent<Enemy>().GetValue();
                }
                actualEnemyValue += enemyValue;
                spawn(enemy);
                yield return new WaitForSeconds(newTimeBetEnemies);
            }
            yield return new WaitForSeconds(timeBetWaves);
            newTimeBetEnemies /= 2;
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

}
