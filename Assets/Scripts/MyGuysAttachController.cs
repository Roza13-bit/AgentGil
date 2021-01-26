using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGuysAttachController : MonoBehaviour
{
    [Header("Cached References")]
    public CenterGuysSphereScript cgsSC;
    public Transform instanceCenterSphere;

    //  [Header("Local Variables")]

    private void OnTriggerEnter(Collider other)
    {
        gameObject.GetComponent<Collider>().enabled = false;

        if (other.CompareTag("MyGuy"))
        {
            cgsSC.numberOfGuys++;
            Debug.Log("number of guys " + cgsSC.numberOfGuys);

            cgsSC.InitFormation(other.gameObject);

            Destroy(this.gameObject);

        }

    }

}
