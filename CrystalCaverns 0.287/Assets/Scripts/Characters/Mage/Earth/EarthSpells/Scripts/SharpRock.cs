using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharpRock : Spell
{

    [SerializeField] private GameObject explosionPrefab;
    private float explosionEffectDmgMultiplier = 40.0f / 100.0f;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject colObj = collision.gameObject;
        if (colObj.CompareTag("Enemy"))
        {
            Character enemy = colObj.GetComponent<Character>();
            enemy.Health = -damage;
            
            GameObject explosionEffect = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            explosionEffect.GetComponent<Spell>().Damage = damage * explosionEffectDmgMultiplier;

        }
    
        Destroy(gameObject);

    }
}
