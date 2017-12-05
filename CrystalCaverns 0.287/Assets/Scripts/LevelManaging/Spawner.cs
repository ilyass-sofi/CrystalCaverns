using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    [Tooltip("Spawn Id")]
    [SerializeField] private int spawnNumber = 1;
   
    [Tooltip("Enable Spawn (FOR TESTING)")]
    [SerializeField] private bool spawn = true;

    private bool waveSpawn = true;
    private bool unitSpawn = false;

    private int enemiesAlive = 0;

    [Tooltip("Enemies Per Wave")]
    [SerializeField] private int enemiesPerWave = 5;
    private int numEnemies = 0;

    [Tooltip("Number of total Waves")]
    [SerializeField] private int totalWaves = 3;
    private int currentWave = 0;

    [Tooltip("Time between Waves (Consider time between units)")]
    [SerializeField] private float waveTime = 10.0f;
    private float timeForWave = 0;

    [Tooltip("Time between units")]
    [SerializeField] private float spawnTime = 1.0f;
    private float timeForSpawn = 0;


    [Tooltip("Final Target")]
    [SerializeField] private GameObject target;

    public enum EnemyTypes
    {
        Basic,
        Medium
    }

    private EnemyTypes enemyLevel = EnemyTypes.Basic;
    private Dictionary<EnemyTypes, GameObject> Enemies = new Dictionary<EnemyTypes, GameObject>(2);

    [Tooltip("Enemy n1")]
    [SerializeField] private GameObject enemy1;
    [Tooltip("Enemy n2")]
    [SerializeField] private GameObject enemy2;



    private Vector3 targetPos;

    void Start()
    {
        targetPos = target.transform.position;
        Enemies.Add(EnemyTypes.Basic, enemy1);
        Enemies.Add(EnemyTypes.Medium, enemy2);


        print("test");



    }

    void Update()
    {
        //Able to spawn
        if (spawn)
        {
            //Not reached max waves
            if (currentWave <= totalWaves)
            {
                //Update time for next wave
                timeForWave += Time.deltaTime;

                //Able to spawn a wave
                if (waveSpawn)
                {
                    //Update time for next enemy
                    timeForSpawn += Time.deltaTime;

                    //Able to spawn a unit
                    if (unitSpawn)
                    {
                        //Spawns Enemy
                        SpawnEnemy();
                    }
                    //Checks if is able to spawn another unit (time has passed)
                    if (timeForSpawn >= spawnTime)
                    {
                        unitSpawn = true;
                        timeForSpawn = 0;
                    }
                    //If time has not been reached then do not spawn the unit
                    else
                    {
                        unitSpawn = false;
                    }

                }

                //Checks if is able to spawn another wave (time has passed)
                if (timeForWave >= waveTime)
                {
                    //ChangeWaveType();
                    waveSpawn = true;
                    timeForWave = 0;
                    numEnemies = 0;
                    currentWave++;

                }
                //If enemies limit has been reached then do not spawn wave
                if (numEnemies >= enemiesPerWave)
                {
                    waveSpawn = false;
                }

            }
            else
            {
                spawn = false;
            }

        }
    }

    /// <summary>
    /// Spawn enemy and sends their target
    /// </summary>
    public void SpawnEnemy()
    {
        GameObject tempEnemy = Instantiate(Enemies[enemyLevel], transform.position, Quaternion.identity);
        //tempEnemy.GetComponent<Enemy>().SetFinalTarget(target);
        tempEnemy.GetComponent<Enemy>().SetSpawner(gameObject);
        numEnemies++;
        enemiesAlive++;
        LevelManager.Instance.EnemiesAlive += 1;
    }



    public void ChangeWaveType()
    {
        enemyLevel = EnemyTypes.Medium;
        //Set type here
    }

    public void KillEnemy()
    {
        LevelManager.Instance.EnemiesAlive -= 1;
    }

    public void ResetWaves()
    {
        numEnemies = 0;
        currentWave = 0;
    }

    public bool Spawn
    {
        get { return spawn; }
        set { spawn = value; }
    }
}


