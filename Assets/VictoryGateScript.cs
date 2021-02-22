using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryGateScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MyGuy"))
        {
            other.GetComponentInChildren<Animator>().SetTrigger("victoryAnimationTrigger");

        }

    }

}
