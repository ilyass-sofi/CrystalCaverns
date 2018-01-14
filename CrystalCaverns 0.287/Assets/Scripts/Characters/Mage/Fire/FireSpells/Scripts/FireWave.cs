using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWave : Spell
{

    [SerializeField] private float speed = 10;

    private void OnTriggerEnter(Collider other)
    {
        GameObject colObj = other.gameObject;
        if (colObj.CompareTag("Enemy"))
        {
            Character enemy = colObj.GetComponent<Character>();
            if(!enemy.IsBurning)
                enemy.Health = -damage;
            else enemy.Health = -damage * 2;
        }
    }

    void Update ()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * -speed);
    }
}
