using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadLaser : MonoBehaviour {

    UnityEngine.AI.NavMeshAgent agent;

    [HideInInspector] public GameObject currentTarget;
    [HideInInspector] public GameObject shop;

    private void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    void Update ()
    {
        if (currentTarget)
        {
            if (Vector3.Distance(currentTarget.transform.position, transform.position) < 10) agent.SetDestination(shop.transform.position);
        }
    }
}
