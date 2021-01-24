using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGuyCenterController : MonoBehaviour
{
    [Header("Cached References")]
    [SerializeField] FloatingJoystick variableJoystick;

    [SerializeField] MyGuysAttachController myGuyAttach;

    [Header("Movement Controlls")]
    public float turnSpeed = 10f;

    public float fallSpeed = -10f;

    public float glideSpeed = 65f;

    public float myGuyBorder;

    private void Start()
    {
    }

    public void FixedUpdate()
    {
        Vector3 downVector = transform.up * fallSpeed;

        Vector3 forwardVector = transform.forward * glideSpeed;

        Vector3 direction = Vector3.right * variableJoystick.Horizontal * turnSpeed;

        Vector3 viewPos = transform.position;

        viewPos.x = Mathf.Clamp(viewPos.x, -myGuyBorder, myGuyBorder);

        transform.Translate(direction + downVector + forwardVector * Time.fixedDeltaTime);

    }

}
