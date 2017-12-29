using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    private float damage;
    [SerializeField] private GameObject burnPrefab;
    [SerializeField] private float timeDestroy = 5;


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
            if (!enemy.IsBurning) Instantiate(burnPrefab, colObj.transform);
            else enemy.transform.Find("Burn(Clone)").GetComponent<Burn>().addStack();
        }

       
        Destroy(gameObject);
        
    }



}
