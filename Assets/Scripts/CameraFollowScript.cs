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
    public float cameraEndY;

    public float cameraEndZ;

    Vector3 newPos;

    void LateUpdate()
    {
        if (!pointMeterSC.isLanded)
        {
            newPos = new Vector3(transform.position.x, myGuyTransform.position.y + followDownRange, myGuyTransform.position.z - followBackRange);

            newPos.y = Mathf.Clamp(newPos.y, cameraEndY, 5000f);

            newPos.z = Mathf.Clamp(newPos.z, -2000f, cameraEndZ);

            transform.position = Vector3.Lerp(transform.position, newPos, 1f);

        }

    }

}
