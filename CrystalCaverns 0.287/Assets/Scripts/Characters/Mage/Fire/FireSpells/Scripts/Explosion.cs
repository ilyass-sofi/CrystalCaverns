using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{


    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private float timeDestroy = 5;
    private float explosionEffectDmgMultiplier = 40.0f / 100.0f;

    private float damage;
    public float Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    private void Start()
    {
        Destroy(gameObject, timeDestroy);
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject colObj = collision.gameObject;
        string colTag = colObj.tag;
        if (colTag == "Enemy")
        {
            Character enemy = colObj.GetComponent<Character>();
            enemy.Health = -damage;
            GameObject explosionEffect = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            explosionEffect.GetComponent<ExplosionEffectFire>().Damage = damage * explosionEffectDmgMultiplier;

        }

        if(colTag != "Friendly")
        {
            Destroy(gameObject);
        }
        
        
        
    }

}
