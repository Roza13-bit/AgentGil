using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdControllerScript : MonoBehaviour
{
    public CenterGuysSphereScript cgsSC;
    public Transform instanceCenterSphere;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MyGuy"))
        {
            other.transform.SetParent(null);

            this.gameObject.GetComponent<Collider>().enabled = false;

            other.attachedRigidbody.useGravity = true;

            this.gameObject.GetComponent<Rigidbody>().useGravity = true;

            foreach (Transform child in instanceCenterSphere)
            {
                Destroy(child.gameObject);

            }

            cgsSC.myGuysList.Clear();

            cgsSC.numberOfGuys--;

            cgsSC.InitCircleFormation();

        }    

    }

}
