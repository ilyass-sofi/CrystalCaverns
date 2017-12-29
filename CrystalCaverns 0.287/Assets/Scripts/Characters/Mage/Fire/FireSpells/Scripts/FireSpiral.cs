using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpiral : MonoBehaviour
{
    [SerializeField] private float timeDestroy;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private float explosionEffectDmgMultiplier = 35.0f / 100.0f;

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

    private void OnTriggerEnter(Collider other)
    {   
        GameObject colObj = other.gameObject;
        string colTag = colObj.tag;
        if (colTag == "Enemy")
        {

            Character enemy = colObj.GetComponent<Character>();
            enemy.Health = -damage;
            if (enemy.IsBurning)
            {
                GameObject explosionEffect = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                explosionEffect.GetComponent<ExplosionEffect>().Damage = damage * explosionEffectDmgMultiplier;
            }
              


        }


    }
}
