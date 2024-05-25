using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{


    private Transform player;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float range = 7.5f;

    public bool targettingPlayer = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("player").transform;
    }

 
    void Update()
    {
        if(Vector3.Distance(transform.position, player.position) <= range)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
            transform.LookAt(player);
            targettingPlayer = true;
        }
        else
            targettingPlayer = false;
    }
}
