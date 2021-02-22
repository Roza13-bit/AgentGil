using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGuyCenterController : MonoBehaviour
{
    [Header("Cached References")]

    [SerializeField] FloatingJoystick variableJoystick;

    [SerializeField] MyGuysAttachController myGuyAttach;

    [SerializeField] AllyAgentScript allyAgentSC;

    [Header("Movement Controlls")]

    public float turnSpeed = 10f;

    public float fallSpeed = -10f;

    public float glideSpeed = 65f;

    public float sphereLandingSpeed;

    public float myGuyBorder;

    public float rotDiverSpeed = 10f;

    public Vector3 direction;

    private Quaternion endSphereRot;

    private void Start()
    {
        endSphereRot.eulerAngles = Vector3.zero;
    }

    public void FixedUpdate()
    {
        Vector3 downVector = transform.up * fallSpeed;

        Vector3 forwardVector = transform.forward * glideSpeed;

        direction = Vector3.right * variableJoystick.Horizontal * turnSpeed;

        // Debug.Log("Direction: " + direction);

        Vector3 viewPos = transform.position;

        viewPos.x = Mathf.Clamp(viewPos.x, -myGuyBorder, myGuyBorder);

        transform.Translate(direction + downVector + forwardVector * Time.fixedDeltaTime);

        if (transform.localPosition.z >= 2756)
        {
            glideSpeed = 0;

            transform.position = new Vector3(transform.position.x, transform.position.y, 2756f);

        }

    }

    public void RotateSphereToZero()
    {
        StartCoroutine(LerpSphereRotation());

    }

    private IEnumerator LerpSphereRotation()
    {
        var timeSinceStarted = 0.0f;

        while (true)
        {
            timeSinceStarted += Time.deltaTime;

            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, endSphereRot, timeSinceStarted * sphereLandingSpeed);

            if (transform.localRotation == endSphereRot)
            {
                yield break;

            }

            yield return null;

        }

    }
}
