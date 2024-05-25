using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics.Internal;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class EnemyController : MonoBehaviour
{


    private Transform player;

    private float range = 7.5f;

    private NavMeshAgent agent;

    public bool targettingPlayer = false;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

 
    void Update()
    {
        if(Vector3.Distance(transform.position, player.position) <= range)
        {
            transform.LookAt(player);
            agent.SetDestination(player.position);
            targettingPlayer = true;
        }
        else
            targettingPlayer = false;
    }
}
