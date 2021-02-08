using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinGateScript : MonoBehaviour
{
    [Header ("Serialized References")]

    [SerializeField] CenterGuysSphereScript cgsSC;

    [SerializeField] MyGuyCenterController myGuyccSC;

    [SerializeField] CameraFollowScript cameraSC;

    private void OnTriggerEnter(Collider other)
    {
        cgsSC.GuyLandingSequance();

        myGuyccSC.RotateSphereToZero();

        cameraSC.isLanded = true;

    }

}
