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
        Debug.Log("Collided");

        if (other.CompareTag("MyGuy"))
        {
            foreach (Transform child in instanceCenterSphere)
            {
                Destroy(child.gameObject);

            }

            cgsSC.myGuysList.Clear();

            cgsSC.numberOfGuys++;

            cgsSC.InitCircleFormation();

            Destroy(this.gameObject);

        }

    }

}
