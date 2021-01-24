using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdControllerScript : MonoBehaviour
{
    public CenterGuysSphereScript cgsSC;
    public Transform instanceCenterSphere;
    public Material greyMat;

    private float touchedCounter = 0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MyGuy"))
        {
            MyGuyDeathSequance(other);

            foreach (Transform child in instanceCenterSphere)
            {
                Destroy(child.gameObject);

            }

            cgsSC.myGuysList.Clear();

            cgsSC.numberOfGuys--;

            cgsSC.InitCircleFormation();

        }

    }

    private void MyGuyDeathSequance(Collider other)
    {
        this.gameObject.GetComponent<Collider>().enabled = false;

        other.transform.SetParent(null);

        other.GetComponentInChildren<SkinnedMeshRenderer>().sharedMaterial = greyMat;

    }

}
