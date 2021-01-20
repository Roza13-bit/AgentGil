using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointMeterScript : MonoBehaviour
{
    [Header("Public Cached References")]
    public CenterGuysSphereScript cgsSC;

    public bool isLanded = false;

    private void OnTriggerEnter(Collider other)
    {
        isLanded = true;

        //Destroy(other.gameObject);

    }

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
