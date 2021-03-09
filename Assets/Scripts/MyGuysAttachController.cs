using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGuysAttachController : MonoBehaviour
{
    [SerializeField] CenterGuysSphereScript cgsSC;

    [SerializeField] CanvasController canvasControl;

    [SerializeField] Camera cameraFOV;

    [SerializeField] float cameraFOVSpeed;

    [SerializeField] float fovToAdd;

    private bool hasCollidedAttach = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!hasCollidedAttach)
        {
            hasCollidedAttach = true;

            // gameObject.GetComponentInChildren<ParticleSystem>().Play();

            // StartCoroutine(cameraShakeSC.CameraShakeCR(.8f, 15f));

            StartCoroutine(LerpCameraFOV());

            canvasControl.NiceTextStartSequance();

            cgsSC.GuyPickupSequance();

            gameObject.SetActive(false);

        }

    }

    private IEnumerator LerpCameraFOV()
    {
        Debug.Log("Attach Trigger");

        var endFOV = cameraFOV.fieldOfView + fovToAdd;

        var timeSinceStartedFOV = 0.0f;

        while (true)
        {
            Debug.Log("While...");

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
