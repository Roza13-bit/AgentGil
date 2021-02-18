using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGuysAttachController : MonoBehaviour
{
    [SerializeField] CenterGuysSphereScript cgsSC;

    [SerializeField] CanvasController canvasControl;

    private bool hasCollidedAttach = false;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Attach Trigger");

        // attachParticlesArray.Play();

        if (!hasCollidedAttach)
        {
            hasCollidedAttach = true;

            canvasControl.NiceTextStartSequance();

            Debug.Log("called nice text pop");

            cgsSC.GuyPickupSequance();

            Destroy(gameObject);

        }

    }

}
