using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PointMeterScript : MonoBehaviour
{
    [Header("Public Cached References")]
    public CenterGuysSphereScript cgsSC;

    public Camera cam;

    public GameObject decalRed;

    public Rigidbody centerSphereRB;

    public Transform fightingSpot;

    public Transform instanceSphereGO;

    public AllyAgentScript allyAgentSC;

    public EnemyAgentScript enemyAgentSC;

    public List<NavMeshAgent> enemyNavMesh = new List<NavMeshAgent>();

    public List<NavMeshAgent> allyNavMesh = new List<NavMeshAgent>();

    public List<Animator> enemyAnimator = new List<Animator>();

    public List<Animator> allyAnimator = new List<Animator>();

    public bool isLanded = false;

    public float camZoomSpeed;

    public float waitBeforeKillingTime;

    public float minKillTime;

    public float maxKillTime;

    private bool isKillStarted = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("MyGuy"))
        {
            isLanded = true;

            foreach (Transform child in instanceSphereGO)
            {
                child.SetParent(null);
                child.GetComponent<NavMeshAgent>().enabled = true;
                allyNavMesh.Add(child.GetComponent<NavMeshAgent>());
                allyAnimator.Add(child.GetComponent<Animator>());
                child.GetComponent<Animator>().enabled = true;
            }

            StartAllyAttack();

            StartEnemyAttack();

            StartCoroutine(StartCameraCloseup());

            if (!isKillStarted)
            {
                isKillStarted = true;

                StartCoroutine(StartKillingUnits());

            }



        }

    }

    private IEnumerator StartCameraCloseup()
    {
        var timeSinceStarted = 0.0f;
        var endPos = new Vector3(0f, 68f, 1877.4f);

        while (true)
        {
            timeSinceStarted += Time.deltaTime;

            cam.transform.position = Vector3.MoveTowards(cam.transform.position, endPos, timeSinceStarted * camZoomSpeed);

            if (cam.transform.position == endPos)
            {
                yield break;
            }

            yield return null;

        }

    }

    private void StartAllyAttack()
    {
        foreach (NavMeshAgent allyAgent in allyNavMesh)
        {
            allyAgent.SetDestination(fightingSpot.position);

        }

    }

    public void StartEnemyAttack()
    {
        foreach (Animator enemyAnime in enemyAnimator)
        {
            enemyAnime.SetTrigger("startRunAnime");

        }

        foreach (NavMeshAgent enemyAgent in enemyNavMesh)
        {
            enemyAgent.SetDestination(fightingSpot.position);

        }

    }

    public IEnumerator StartKillingUnits()
    {
        Debug.Log("Start killing units wait...");

        yield return new WaitForSeconds(waitBeforeKillingTime);

        while (true)
        {
            KillOneUnit();

            if (enemyNavMesh.Count <= 0)
            {
                Debug.Log("Ally won");

                foreach (Animator allyAnime in allyAnimator)
                {
                    allyAnime.SetTrigger("allyDanceAnime");

                }

                yield break;

            }

            if (allyNavMesh.Count <= 0)
            {
                Debug.Log("Enemy won");

                foreach (Animator enemyAnime in enemyAnimator)
                {
                    enemyAnime.SetTrigger("startDanceAnime");

                }

                yield break;

            }

            yield return new WaitForSeconds(UnityEngine.Random.Range(minKillTime, maxKillTime));

        }

    }

    private void KillOneUnit()
    {
        var randomEnemy = UnityEngine.Random.Range(0, enemyNavMesh.Count);
        var randomAlly = UnityEngine.Random.Range(0, allyNavMesh.Count);

        Debug.Log("Random enemy num : " + randomEnemy);
        Debug.Log("Random ally num : " + randomAlly);

        Quaternion rot = Quaternion.Euler(90f, 0f, 0f);

        var enemydecal = Instantiate(decalRed, enemyNavMesh[randomEnemy].transform.position, rot);
        Destroy(enemyNavMesh[randomEnemy].gameObject);
        enemyNavMesh.RemoveAt(randomEnemy);
        enemyAnimator.RemoveAt(randomEnemy);
        Debug.Log("Enemy list count : " + enemyNavMesh.Count);


        var allydecal = Instantiate(decalRed, allyNavMesh[randomAlly].transform.position, rot);
        Destroy(allyNavMesh[randomAlly].gameObject);
        allyNavMesh.RemoveAt(randomAlly);
        allyAnimator.RemoveAt(randomAlly);
        Debug.Log("Ally list count : " + allyNavMesh.Count);
    }

    /* public void StartFightingEnemy()
     {
         Debug.Log("Entered fighting function");

         foreach (NavMeshAgent enemyAgent in enemyNavMesh)
         {
             Debug.Log("Entered foreach navmesh");
             enemyAgent.isStopped = true;

         }

         foreach (Animator enemyAnime in enemyAnimator)
         {
             Debug.Log("Entered foreach animator");

             enemyAnime.SetTrigger("startPunchAnime");

         }

     } */

    /*public IEnumerator HitSphereSpringLerp()
    {
        var endPos = new Vector3(0f, 0.5f, 0f);
        var startPos = new Vector3(0f, 1.3f, 0f);

        var timeSinceStartedSpring = 0.0f;

        while (true)
        {
            timeSinceStartedSpring += Time.deltaTime;

            hitSphereTransform.localPosition = Vector3.MoveTowards(hitSphereTransform.localPosition, endPos, timeSinceStartedSpring * springTime);

            if (hitSphereTransform.localPosition == endPos)
            {
                break;
            }

            yield return null;

        }

        while (true)
        {
            timeSinceStartedSpring += Time.deltaTime;

            hitSphereTransform.localPosition = Vector3.MoveTowards(hitSphereTransform.localPosition, startPos, timeSinceStartedSpring * springTime);

            if (hitSphereTransform.localPosition == endPos)
            {
                yield break;
            }

            yield return null;

        }

    }*/

}
