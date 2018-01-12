using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Orc : Enemy {

    private bool curandose;

    void Awake()
    {
        curandose = false;

        player = GameObject.FindGameObjectWithTag("Friendly");

        HealthMax = 300;
        Health = HealthMax;
        attackSpeed = 3f;

        Damage = 30;

        agent = GetComponent<NavMeshAgent>();
        BaseSpeed = 5f;
        CurrentSpeed = BaseSpeed;

        goldDrop = 30;
        goldDropPercent = 100;
    }

    // Update is called once per frame
    void Update () {
        if (health < healthMax * 0.8f && !curandose)
        {
            agent.isStopped = true;
            StartCoroutine(healMe());
        }
        else if (health == healthMax)
        {
            if (agent.isStopped)
            {
                StopAllCoroutines();
                agent.isStopped = false;
            }
        }

        if (Vector3.Distance(new Vector3(player.transform.position.x,0, player.transform.position.z), new Vector3(transform.position.x,0, transform.position.z)) < 2.5 && !curandose)//rango minimo mele por definir
        {
            agent.isStopped = true;
            transform.LookAt(new Vector3(player.transform.position.x,transform.position.y,player.transform.position.z));
        }
        else if(agent.isStopped && !curandose)
        {
            agent.isStopped = false;
        }
        if (Vector3.Distance(currentTarget.transform.position, transform.position) < 10) agent.SetDestination(shop.transform.position);
    }

    private IEnumerator healMe()
    {
        curandose = true;
        do
        {
            SetHealth(healthMax * 0.01f);
            yield return new WaitForSeconds(1);

        } while (health < healthMax);
        health = healthMax;
        curandose = false;
    }
}
