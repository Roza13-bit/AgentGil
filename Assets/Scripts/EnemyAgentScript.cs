using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAgentScript : MonoBehaviour
{
    public float fightingDistanceEnemy;

    private void Update()
    {
        if (transform.GetComponent<NavMeshAgent>().hasPath)
        {
            EnemyReachedDestination();

        }

    }

    private void EnemyReachedDestination()
    {
        var reached = false;

        if (transform.GetComponent<NavMeshAgent>().remainingDistance <= fightingDistanceEnemy && !reached)
        {
            reached = true;

            transform.GetComponent<NavMeshAgent>().isStopped = true;

            transform.GetComponent<Animator>().SetTrigger("startPunchAnime");

        }

    }

}
