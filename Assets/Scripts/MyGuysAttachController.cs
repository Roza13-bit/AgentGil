using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGuysAttachController : MonoBehaviour
{
    [SerializeField] CenterGuysSphereScript cgsSC;

    private bool hasCollidedAttach = false;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Attach Trigger");

        if (!hasCollidedAttach)
        {
            hasCollidedAttach = true;
            cgsSC.GuyPickupSequance();

            Destroy(gameObject);

        }

    }

}
