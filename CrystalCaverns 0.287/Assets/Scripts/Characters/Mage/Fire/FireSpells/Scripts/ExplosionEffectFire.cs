using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffectFire : Spell
{

    [SerializeField] private GameObject burnPrefab;

    private void OnTriggerEnter(Collider other)
    {
        GameObject colObj = other.gameObject;
        if (colObj.CompareTag("Enemy"))
        {   
            Character enemy = colObj.GetComponent<Character>();
            enemy.Health = -damage;
            if (!enemy.IsBurning)
                Instantiate(burnPrefab, colObj.transform);
            else enemy.transform.Find("Burn").GetComponent<Burn>().ResetEffect();
        }
    }
}
