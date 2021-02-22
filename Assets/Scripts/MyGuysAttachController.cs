using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGuysAttachController : MonoBehaviour
{
    [SerializeField] CenterGuysSphereScript cgsSC;

    [SerializeField] CanvasController canvasControl;

    [SerializeField] CameraShake cameraShakeSC;

    private bool hasCollidedAttach = false;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Attach Trigger");

        if (!hasCollidedAttach)
        {
            hasCollidedAttach = true;

            // gameObject.GetComponentInChildren<ParticleSystem>().Play();

            // StartCoroutine(cameraShakeSC.CameraShakeCR(.8f, 15f));

            canvasControl.NiceTextStartSequance();

            Debug.Log("called nice text pop");

            cgsSC.GuyPickupSequance();

            Destroy(gameObject);

        }

    }

}
