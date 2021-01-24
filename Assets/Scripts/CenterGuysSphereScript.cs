using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CenterGuysSphereScript : MonoBehaviour
{
    [Header("Cached References")]
    [SerializeField] GameObject MyGuyPrefab;

    [SerializeField] MyGuyCenterController myGuyCenterSC;

    [SerializeField] Transform CenterSphere;

    [Header("Circle Controll")]
    public List<GameObject> myGuysList;

    public float numberOfGuys;

    public float sizeMultiply;

    public float radius;

    public float sphereXRotation;

    public float waitForCombineFloat = 0;

    [Header("Landing Sequance Control")]
    public float rotationSpeedMyGuy;

    public float rotationSpeedSphere;

    private Quaternion endRotSphere;
    private Quaternion endRotMyGuy;

    // Start is called before the first frame update
    void Start()
    {
        myGuysList = new List<GameObject>();
        this.InitCircleFormation();

        endRotSphere.eulerAngles = new Vector3(90f, 0f, 0f);
        endRotMyGuy.eulerAngles = new Vector3(-90f, 0f, 0f);

    }

    public void InitCircleFormation()
    {
        // Loop through the number of points in the circle.
        for (int i = 0; i < numberOfGuys; i++)
        {
            // Instantiate the prefab.
            GameObject go = Instantiate(MyGuyPrefab, transform);

            myGuysList.Add(go);

            //// Get the angle of the current index being instantiated
            //// from the center of the circle.
            //float angle = i * (2 * 3.14159f / numberOfGuys);

            //// Get the X Position of the angle times 1.5f. 1.5f is the radius of the circle.
            //float x = Mathf.Cos(angle) * 9f;
            //// Get the Y Position of the angle times 1.5f. 1.5f is the radius of the circle.
            //float y = Mathf.Sin(angle) * 9f;

            //// Set the position of the instantiated object to the targetPosition.
            //instance.transform.position = new Vector3(CenterSphere.position.x + x, CenterSphere.position.y + y, CenterSphere.position.z);

        }

        UpdateCircle();

        transform.rotation = Quaternion.Euler(sphereXRotation, 0f, 0f);

    }

    private void UpdateCircle()
    {
        for (var i = 0; i < myGuysList.Count; i++)
        {
            // Get the angle of the current index being instantiated
            // from the center of the circle.
            float angle = i * (2 * 3.14159f / myGuysList.Count);

            // Get the X Position of the angle times 1.5f. 1.5f is the radius of the circle.
            float x = Mathf.Cos(angle) * radius;

            // Get the Y Position of the angle times 1.5f. 1.5f is the radius of the circle.
            float y = Mathf.Sin(angle) * radius;

            // Set the position of the instantiated object to the targetPosition.
            myGuysList[i].transform.localPosition = new Vector3(x, y, 0f);

            // myGuysList[i].transform.LookAt(new Vector3(0f,0f,0f));

        }

    }

    public void StartClansFight()
    {


    }

    public void StartMyGuyRotation()
    {
        foreach (Transform child in transform)
        {
            StartCoroutine(StartRotCoroutine(child));

        }

    }

    public IEnumerator StartSphereRotation()
    {
        var timeSinceStartedRotation = 0.0f;

        while (true)
        {
            timeSinceStartedRotation += Time.deltaTime;

            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, endRotSphere, timeSinceStartedRotation * rotationSpeedSphere);

            if (transform.localRotation == endRotSphere)
            {
                myGuyCenterSC.glideSpeed = 0;

                myGuyCenterSC.fallSpeed = 0;

                myGuyCenterSC.turnSpeed = 0;

                foreach (Transform child in this.transform)
                {
                    // child.GetComponent<NavMeshAgent>().enabled = true;
                }

                CenterSphere.gameObject.GetComponent<Rigidbody>().isKinematic = false;

                yield break;
            }

            yield return null;

        }

    }

    public IEnumerator StartRotCoroutine(Transform child)
    {
        var timeSinceStartedMyGuyRotation = 0.0f;

        while (true)
        {
            timeSinceStartedMyGuyRotation += Time.deltaTime;

            child.localRotation = Quaternion.RotateTowards(child.localRotation, endRotMyGuy, timeSinceStartedMyGuyRotation * rotationSpeedMyGuy);

            if (transform.localRotation == endRotSphere)
            {
                yield break;
            }

            yield return null;

        }

    }

}
