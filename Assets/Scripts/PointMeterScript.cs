using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointMeterScript : MonoBehaviour
{
    public GameObject splatterQuadGO;
    public CenterGuysSphereScript cgsSC;

    public bool isLanded = false;

    private void OnTriggerEnter(Collider other)
    {
        isLanded = true;

        splatterQuadGO.SetActive(true);

        splatterQuadGO.transform.localScale = new Vector3(75f * (cgsSC.sizeMultiply/1.5f), 75f * cgsSC.sizeMultiply * 2f, 1f);

        Destroy(other.gameObject);
        
    }

}
