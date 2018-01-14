using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : Spell
{
    
    [SerializeField] private GameObject burnPrefab;
   
    private void OnCollisionEnter(Collision collision)
    {
        GameObject colObj = collision.gameObject;
        if (colObj.CompareTag("Enemy"))
        {
            Character enemy = colObj.GetComponent<Character>();
            enemy.Health = -damage;
            if (!enemy.IsBurning) Instantiate(burnPrefab, colObj.transform);
            else enemy.transform.Find("Burn").GetComponent<Burn>().ResetEffect();
        }
       
        Destroy(gameObject);
        
    }



}
