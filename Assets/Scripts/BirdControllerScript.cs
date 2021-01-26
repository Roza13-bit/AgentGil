using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdControllerScript : MonoBehaviour
{
    public CenterGuysSphereScript cgsSC;
    public Transform instanceCenterSphere;
    public Material greyMat;

    private bool hasCollidedObject = false;

    private void OnTriggerEnter(Collider other)
    {
        if (hasCollidedObject)
        {
            return;
        }
        else if (!hasCollidedObject)
        {
            hasCollidedObject = true;

            Debug.Log("TRIGER");

            Debug.Log("Object name : " + other.name + "Object Pos : " + other.transform.position);

            MyGuyDeathSequance(other);

            StartCoroutine(WaitBeforeRemoving(other));

        }

    }

    private IEnumerator WaitBeforeRemoving(Collider other)
    {
        var numOfGuysReduced = cgsSC.numberOfGuys;

        cgsSC.numberOfGuys--;
        Debug.Log("number of guys " + cgsSC.numberOfGuys);

        cgsSC.InitFormation(other.gameObject);

        yield return null;

    }

    private void MyGuyDeathSequance(Collider other)
    {
        this.gameObject.GetComponent<Collider>().enabled = false;

        other.transform.SetParent(null);

        other.GetComponentInChildren<SkinnedMeshRenderer>().sharedMaterial = greyMat;

    }

}
