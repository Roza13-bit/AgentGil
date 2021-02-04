using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGuysAttachController : MonoBehaviour
{
    //[Header("Cached References")]
    //public CenterGuysSphereScript cgsSC;
    //public Transform instanceCenterSphere;

    ////  [Header("Local Variables")]
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

        //gameObject.GetComponent<Collider>().enabled = false;

        //if (other.CompareTag("MyGuy"))
        //{
        //    cgsSC.numberOfGuys++;
        //    Debug.Log("number of guys " + cgsSC.numberOfGuys);

        //    cgsSC.InitFormation();

        //    Destroy(this.gameObject);

        //}
    }

}
