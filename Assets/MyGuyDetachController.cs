using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGuyDetachController : MonoBehaviour
{
    [SerializeField] CenterGuysSphereScript cgsSC;

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

            cgsSC.GuyRemoveSequance();

            greyGuy.SetActive(true);

        }

    }

}
