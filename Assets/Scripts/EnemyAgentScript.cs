using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAgentScript : MonoBehaviour
{


    //public float fightingDistanceEnemy;

    //public Transform fightSpot;

    //private void Update()
    //{
    //    if (transform.GetComponent<NavMeshAgent>().hasPath)
    //    {
    //        EnemyReachedDestination();

    //    }

    //}

    //private void EnemyReachedDestination()
    //{
    //    var reached = false;

    //    if (transform.GetComponent<NavMeshAgent>().remainingDistance <= fightingDistanceEnemy && !reached)
    //    {
    //        reached = true;

    //        transform.GetComponent<NavMeshAgent>().isStopped = true;

    //        Vector3 DeltaVec = (fightSpot.position - transform.position);
    //        DeltaVec.y = 0.0f;

    //        transform.rotation = Quaternion.LookRotation(DeltaVec);

    //        transform.GetComponent<Animator>().SetTrigger("startPunchAnime");

    //    }

    //}
}
