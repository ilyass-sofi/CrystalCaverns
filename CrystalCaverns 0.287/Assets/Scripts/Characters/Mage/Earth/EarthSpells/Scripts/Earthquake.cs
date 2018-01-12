using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earthquake : Spell
{
    private List<GameObject> enemyList = new List<GameObject>();

    private float nextBlastCd;
    [SerializeField] private float blastCd;


    public override void Start()
    {
        base.Start();

    }

    private void Update()
    {
        if (enemyList.Count > 0)
        {
            if (Time.time >= nextBlastCd)
            {
                nextBlastCd = Time.time + blastCd;

                for (int i = 0; i < enemyList.Count; i++)
                {
                    if (!enemyList[i])
                    {
                        enemyList.Remove(enemyList[i]);
                    }
                    else
                    {
                        enemyList[i].GetComponent<Enemy>().Health = -damage;
                    }
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)  
    {
        if (other.CompareTag("Enemy"))
        {
            if (!enemyList.Contains(other.gameObject))
            {
                enemyList.Add(other.gameObject);

                other.GetComponent<Enemy>().Slow(0.5f, 10);
               
            }
        }
    }


    void OnTriggerExit(Collider other)  
    {
        if (other.CompareTag("Enemy"))
        {
            if (enemyList.Contains(other.gameObject))
            {
                enemyList.Remove(other.gameObject);
            }
        }
    }


}
