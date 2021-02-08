using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Experimental.VFX;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.VFX;

public class PointMeterScript : MonoBehaviour
{
    [Header ("Serialized References")]

    [SerializeField] CenterGuysSphereScript cgsSC;

    [SerializeField] MyGuyCenterController myGuyccSC;

    [SerializeField] CameraFollowScript cameraSC;

    [SerializeField] ParticleSystem[] confettiArray = new ParticleSystem[4];

    [SerializeField] VisualEffect[] fireworksArray = new VisualEffect[2];

    private void OnTriggerEnter(Collider other)
    {
        myGuyccSC.fallSpeed = 0;

        myGuyccSC.glideSpeed = 0;

        // cameraSC.isLanded = true;

        foreach (GameObject item in cgsSC.guysParachute)
        {
            item.SetActive(false);

        }

        foreach (ParticleSystem confetti in confettiArray)
        {
            confetti.Play();

        }

        foreach (VisualEffect fire in fireworksArray)
        {
            fire.Play();

        }

    }

}
