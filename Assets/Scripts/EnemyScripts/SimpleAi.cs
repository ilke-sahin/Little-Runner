using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SimpleAi : MonoBehaviour
{
    private Transform target;
    private NavMeshAgent agent; 

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        target = player.transform;
    }

    void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);
        }
    }
}
