using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    [Header("Cached References")]
    public MyGuyCenterController myGuyCenterC;

    public PointMeterScript pointMeterSC;

    public WinGateScript winGateSC;

    public Transform myGuyTransform;

    [Header("Follow Range Game Time")]
    public float followDownRange = 10f;

    public float followBackRange = 20f;

    [Header("Camera End Positions")]
    public bool isLanded = false;

    public float cameraEndY;

    public float cameraEndZ;

    public float FOVSpeed;

    public float endFOV;

    public float cameraMoveEndSpeed;

    public float cameraRotEndSpeed;

    Vector3 newPos;

    bool flag = false;

    Vector3 endPos;

    Quaternion endRot;

    private void Start()
    {
        endPos = new Vector3(17.4f, cameraEndY, cameraEndZ);

        endRot.eulerAngles = new Vector3(10f, -18.2f, 0f);

    }

    void LateUpdate()
    {
        if (!isLanded && !flag)
        {
            newPos = new Vector3(transform.position.x, myGuyTransform.position.y + followDownRange, myGuyTransform.position.z - followBackRange);

            newPos.y = Mathf.Clamp(newPos.y, cameraEndY, 10000f);

            newPos.z = Mathf.Clamp(newPos.z, -10000f, cameraEndZ);

            transform.position = Vector3.Lerp(transform.position, newPos, 1f);

        }
        else if (isLanded && !flag)
        {
            flag = true;

            StartCoroutine(LerpCameraToEndPos());

        }

    }

    private IEnumerator LerpCameraToEndPos()
    {
        var timeSinceStartedMovement = 0.0f;

        while (true)
        {
            timeSinceStartedMovement += Time.deltaTime;

            gameObject.GetComponent<Camera>().fieldOfView = Mathf.Lerp(gameObject.GetComponent<Camera>().fieldOfView, endFOV, timeSinceStartedMovement * FOVSpeed);

            transform.position = Vector3.MoveTowards(transform.position, endPos, timeSinceStartedMovement * cameraMoveEndSpeed);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, endRot, timeSinceStartedMovement * cameraRotEndSpeed);

            if (gameObject.GetComponent<Camera>().fieldOfView == endFOV && transform.position == endPos && transform.rotation == endRot)
            {
                yield break;
            }

            yield return null;

        }

    }

}
