using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharpRock : MonoBehaviour
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
        if (colObj.CompareTag("Enemy"))
        {
            Character enemy = colObj.GetComponent<Character>();
            enemy.Health = -damage;
            
            GameObject explosionEffect = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            explosionEffect.GetComponent<ExplosionEffectEarth>().Damage = damage * explosionEffectDmgMultiplier;

        }

        if (!colObj.CompareTag("Friendly"))
        {
            Destroy(gameObject);
        }



    }
}
