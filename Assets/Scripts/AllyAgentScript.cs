using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AllyAgentScript : MonoBehaviour
{
    public MyGuyCenterController myGuyccSC;

    public float tiltLevel;

    private Quaternion newRot;

    private void Start()
    {
        newRot.eulerAngles = Vector3.zero;

    }

    private void FixedUpdate()
    {
        RotateDiverOnMove(myGuyccSC.direction.x);

    }

    public void RotateDiverOnMove(float tilt)
    {
        newRot.eulerAngles = new Vector3(0f, 0f, tilt * tiltLevel);

        transform.localRotation = Quaternion.Lerp(transform.localRotation, newRot, 1);

    }

}
