using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollactibleScript : MonoBehaviour
{
    [SerializeField] CanvasController canvasControlSC;

    [SerializeField] float rotationCoinSpeed;

    private bool collectedGO = false;

    private void FixedUpdate()
    {
        transform.Rotate(Vector3.up, rotationCoinSpeed * Time.fixedDeltaTime);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (this.gameObject.CompareTag("Diamond") && !collectedGO)
        {
            collectedGO = true;

            canvasControlSC.diamondsGained++;

            Destroy(this.gameObject);

        }

    }

}
