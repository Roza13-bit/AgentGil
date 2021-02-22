using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public IEnumerator CameraShakeCR(float duration, float magnitude)
    {
        Vector3 originalPos = Vector3.zero;

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = UnityEngine.Random.Range(-1, 1) * magnitude;
            float y = UnityEngine.Random.Range(-1, 1) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;

        }

        transform.localPosition = Vector3.zero;

    }

}
