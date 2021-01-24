using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AllyAgentScript : MonoBehaviour
{
    public float fightingDistance;

    private void Update()
    {
        if (transform.GetComponent<NavMeshAgent>().hasPath)
        {
            AllyReachedDestination();

        }

    }

    private void AllyReachedDestination()
    {
        var reached = false;

        if (transform.GetComponent<NavMeshAgent>().remainingDistance <= fightingDistance && !reached)
        {
            reached = true;

            transform.GetComponent<NavMeshAgent>().isStopped = true;

            transform.GetComponent<Animator>().SetTrigger("allyPunchAnime");

        }

    }

}
