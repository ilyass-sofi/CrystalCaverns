using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpiral : Spell
{
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private float explosionEffectDmgMultiplier = 35.0f / 100.0f;

    private void OnTriggerEnter(Collider other)
    {   
        GameObject colObj = other.gameObject;
        if (colObj.CompareTag("Enemy"))
        {
            Character enemy = colObj.GetComponent<Character>();
            enemy.Health = -damage;
            if (enemy.IsBurning)
            {
                GameObject explosionEffect = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                explosionEffect.GetComponent<ExplosionEffectFire>().Damage = damage * explosionEffectDmgMultiplier;
            }
        }
    }
}
