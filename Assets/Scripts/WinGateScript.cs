using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinGateScript : MonoBehaviour
{
    public CenterGuysSphereScript cgsSC;

    public bool isLandingStarted = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!isLandingStarted)
        {
            cgsSC.CombineForLandingSequance();
            isLandingStarted = true;

        }
        
    }

}
