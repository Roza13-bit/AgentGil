using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGuyDivingMatScript : MonoBehaviour
{
    public CenterGuysSphereScript cgsSC;

    public IEnumerator ChangeMyGuyDivingColor()
    {
        var meshRend = transform.GetComponent<SkinnedMeshRenderer>();

        var timeSinceStartedChangingColor = 0.0f;

        while (true)
        {
            timeSinceStartedChangingColor += Time.deltaTime;

            Debug.Log("Time since started changing color : " + timeSinceStartedChangingColor.ToString());

            meshRend.sharedMaterial.Lerp(meshRend.sharedMaterial, cgsSC.myGuyBurningMaterialsArray[1], timeSinceStartedChangingColor * cgsSC.changeSpeed);

            // If the object has arrived, stop the coroutine
            if (timeSinceStartedChangingColor >= 0.9f)
            {
                Debug.Log("Color matched");
                break;
            
            }

            // Otherwise, continue next frame
            yield return null;

        }

        timeSinceStartedChangingColor = 0.0f;

        while (true)
        {
            timeSinceStartedChangingColor += Time.deltaTime;

            Debug.Log("Time since started changing color : " + timeSinceStartedChangingColor.ToString());

            meshRend.sharedMaterial.Lerp(meshRend.sharedMaterial, cgsSC.myGuyBurningMaterialsArray[2], timeSinceStartedChangingColor * cgsSC.changeSpeed);

            // If the object has arrived, stop the coroutine
            if (timeSinceStartedChangingColor >= 0.9f)
            {
                Debug.Log("Color matched");
                break;

            }

            // Otherwise, continue next frame
            yield return null;

        }

        timeSinceStartedChangingColor = 0.0f;

        while (true)
        {
            timeSinceStartedChangingColor += Time.deltaTime;

            Debug.Log("Time since started changing color : " + timeSinceStartedChangingColor.ToString());

            meshRend.sharedMaterial.Lerp(meshRend.sharedMaterial, cgsSC.myGuyBurningMaterialsArray[3], timeSinceStartedChangingColor * cgsSC.changeSpeed);

            // If the object has arrived, stop the coroutine
            if (timeSinceStartedChangingColor >= 0.9f)
            {
                Debug.Log("Color matched");
                yield break;

            }

            // Otherwise, continue next frame
            yield return null;

        }

    }

}
