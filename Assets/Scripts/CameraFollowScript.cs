using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    public MyGuyCenterController myGuyCenterC;

    public Transform myGuyTransform;

    public float followDownRange = 10f;

    public float followBackRange = 20f;

    public float landingZ = 10f;

    public float landingY = 20f;

    Vector3 newPos;

    void LateUpdate()
    {
        newPos = new Vector3(transform.position.x, myGuyTransform.position.y + followDownRange, myGuyTransform.position.z - followBackRange);

        transform.position = Vector3.Lerp(transform.position, newPos, 1f);

    }

}
