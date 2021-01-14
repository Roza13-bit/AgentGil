using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGuyCenterController : MonoBehaviour
{
    [Header("Cached References")]
    [SerializeField] FloatingJoystick variableJoystick;

    [SerializeField] CharacterController myGuyCC;

    [SerializeField] MyGuysAttachController myGuyAttach;

    [Header("Movement Controlls")]
    [SerializeField] float turnSpeed = 10f;

    [SerializeField] float fallSpeed = -10f;

    [SerializeField] float glideSpeed = 65f;

    private void Start()
    {
    }

    public void FixedUpdate()
    {
        Vector3 downVector = transform.up * fallSpeed;

        Vector3 forwardVector = transform.forward * glideSpeed;

        Vector3 direction = Vector3.right * variableJoystick.Horizontal * turnSpeed;

        Vector3 viewPos = transform.position;

        viewPos.x = Mathf.Clamp(viewPos.x, -30, 30);

        transform.Translate(direction + downVector + forwardVector * Time.fixedDeltaTime);

    }

}
