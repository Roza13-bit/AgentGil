using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGuyDetachController : MonoBehaviour
{
    [SerializeField] CenterGuysSphereScript cgsSC;

    [SerializeField] Camera cameraFOV;

    [SerializeField] float cameraFOVSpeed;

    [SerializeField] float fovToAdd;

    private GameObject greyGuy;

    private bool hasCollidedDetach = false;

    private void Start()
    {
        greyGuy = transform.Find("MyGuyDiving").gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Detach Trigger");

        if (!hasCollidedDetach)
        {
            hasCollidedDetach = true;

            StartCoroutine(LerpCameraFOV());

            cgsSC.GuyRemoveSequance();

            greyGuy.SetActive(true);

        }

    }

    private IEnumerator LerpCameraFOV()
    {
        var endFOV = cameraFOV.fieldOfView - fovToAdd;

        var timeSinceStartedFOV = 0.0f;

        while (true)
        {
            timeSinceStartedFOV += Time.deltaTime;

            cameraFOV.fieldOfView = Mathf.Lerp(cameraFOV.fieldOfView, endFOV, timeSinceStartedFOV * cameraFOVSpeed);

            if (cameraFOV.fieldOfView == endFOV)
            {
                yield break;

            }

            yield return null;

        }

    }

}
