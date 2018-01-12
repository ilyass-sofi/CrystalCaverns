using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// el goblin te sigue siempre desde que te ve
/// </summary>
public class Goblin : Enemy {

    private bool following;
    private GameObject followed;

	// Use this for initialization
	void Awake ()
    {

        player = GameObject.FindGameObjectWithTag("Friendly");

        HealthMax = 100;
        Health = HealthMax;
        attackSpeed = 3f;

        Damage = 10;

        agent = GetComponent<NavMeshAgent>();
        BaseSpeed = 5f;
        CurrentSpeed = BaseSpeed;

        following = false;

        goldDrop = 10;
        goldDropPercent = 100;
    }
	
	// Update is called once per frame
	void Update () {

        if (!following)
        {
            if (Vector3.Distance(currentTarget.transform.position, transform.position) < 10)
            {
                agent.SetDestination(shop.transform.position);
            }

                
            if (Vector3.Distance(player.transform.position, transform.position) < visionRange)
            {
                followed = player;
                following = true;
                defaultTarget = false;
            }
        }
        else
        {
            if (Vector3.Distance(new Vector3(player.transform.position.x, 0, player.transform.position.z), new Vector3(transform.position.x, 0, transform.position.z)) > 2.6)//rango minimo mele por definir
            {
                agent.isStopped = false;
                SetTarget(followed);
            }
            else
            {
                agent.isStopped = true;
                transform.LookAt(new Vector3(player.transform.position.x,transform.position.y, player.transform.position.z));
            }
        }

    }
}
