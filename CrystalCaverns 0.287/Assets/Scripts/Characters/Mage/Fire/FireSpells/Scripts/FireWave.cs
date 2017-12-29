using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWave : MonoBehaviour
{

    [SerializeField] private float timeDestroy = 5;
    [SerializeField] private float speed = 10;

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
