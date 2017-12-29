using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{

    [SerializeField] private GameObject burnPrefab;

    private float damage;

    public float Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    private void Start()
    {
        Destroy(gameObject, 0.5f);
    }
    



    private void OnTriggerEnter(Collider other)
    {
        
        GameObject colObj = other.gameObject;
        string colTag = colObj.tag;
        if (colTag == "Enemy")
        {   
            Character enemy = colObj.GetComponent<Character>();
            enemy.Health = -damage;
            if (!enemy.IsBurning)
                Instantiate(burnPrefab, colObj.transform);
            else enemy.transform.Find("Burn(Clone)").GetComponent<Burn>().addStack();
        }


    }
}
