using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : Character
{
    /// <summary>
    /// Spawner that spawned this enemy
    /// </summary>
    protected GameObject spawner;
    /// <summary>
    /// Reference to the agent
    /// </summary>    
    protected NavMeshAgent agent;
    protected GameObject player;
    protected GameObject currentTarget;
    protected GameObject shop;
    protected bool defaultTarget;
    protected float visionRange = 10;
    [SerializeField] protected int value;

    [SerializeField] protected int goldDrop;
    [SerializeField] protected int dropPercent;
    [SerializeField] private GameObject damageTextPrefab;

    void Awake()
    {
        defaultTarget = true;
    }

    protected override void SetSpeed(float speedValue)
    {
        currentSpeed = speedValue;
        agent.speed = currentSpeed;
    }

    protected override void SetHealth(float healthValue)
    {
        SpawnDamageNumber(healthValue);
        base.SetHealth(healthValue);
    }

    public void SetTarget(GameObject _target)
    {
        currentTarget = _target;
        agent.SetDestination(currentTarget.transform.position);
    }

    public void SetSpawner(GameObject _spawner)
    {
        spawner = _spawner;
    }

    public void SetShop(GameObject _shop)
    {
        shop = _shop;
    }

    protected override void GameOver()
    {
        Loot();
        Kill();
    }

    private void Kill()
    {
        spawner.GetComponent<Spawn>().KillEnemy();
        Destroy(gameObject);
    }


    public void SpawnDamageNumber(float damage)
    {
        GameObject damageText = Instantiate(damageTextPrefab, transform.position + new Vector3(Random.Range(-1f, 1f), transform.lossyScale.y, Random.Range(-1f, 1f)), Quaternion.identity);
        damageText.GetComponent<TextMesh>().text = System.Math.Abs(damage) + "";
        if (damage < 0)
        {
            damageText.GetComponent<TextMesh>().color = Color.red;
            if (damage > -10) damageText.GetComponent<TextMesh>().color = new Color32(239, 127, 26, 255);
        }
        else
        {
            damageText.GetComponent<TextMesh>().color = new Color32(0, 255, 23, 255);
        }

    }

    protected abstract void Loot();

    public abstract int GetValue();
}
