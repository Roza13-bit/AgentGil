using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointMeterScript : MonoBehaviour
{
    [Header("Public Cached References")]
    public CenterGuysSphereScript cgsSC;

    public Transform hitSphereTransform;

    public GameObject[] scoreEmitters = new GameObject[7];

    public ParticleSystem groundHitParticleSys1;

    public ParticleSystem groundHitParticleSys2;

    public ParticleSystem groundHitParticleSys3;

    public bool isLanded = false;

    public float springTime;

    public float waitBetweenEmitters;

    private void OnTriggerEnter(Collider other)
    {
        isLanded = true;

        StartCoroutine(HitSphereSpringLerp());

        groundHitParticleSys1.Play();
        groundHitParticleSys2.Play();
        groundHitParticleSys3.Play();

        Destroy(other.gameObject);

        StartCoroutine(StartEmittersSequance());

    }

    public IEnumerator StartEmittersSequance()
    {
        for (int i = 0; i < cgsSC.sizeMultiply; i++)
        {
            scoreEmitters[i].SetActive(true);

            yield return new WaitForSeconds(waitBetweenEmitters);

        }

    }

    public IEnumerator HitSphereSpringLerp()
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

    }

}
