using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AllyAgentScript : MonoBehaviour
{
    public float fightingDistance;

    public Transform fightSpot;

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

            Vector3 DeltaVec = (fightSpot.position - transform.position);
            DeltaVec.y = 0.0f;

            transform.rotation = Quaternion.LookRotation(DeltaVec);

            transform.GetComponent<Animator>().SetTrigger("allyPunchAnime");

        }

    }

}
