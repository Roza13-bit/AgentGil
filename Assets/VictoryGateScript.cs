using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryGateScript : MonoBehaviour
{
    [SerializeField] private CenterGuysSphereScript cgsSC;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MyGuy"))
        {
            // cgsSC.UnparentActiveGuys();

            other.GetComponentInChildren<Animator>().SetTrigger("victoryAnimationTrigger");

        }

    }

}
