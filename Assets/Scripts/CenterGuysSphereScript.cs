using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterGuysSphereScript : MonoBehaviour
{
    [Header("Cached References")]
    [SerializeField] GameObject MyGuyPrefab;

    [SerializeField] Transform CenterSphere;

    public MyGuyDivingMatScript mgdmSC;

    public GameObject smokeHolderGO;

    [Header("Circle Controll")]
    public List<GameObject> myGuysList;

    public float numberOfGuys;

    public float sizeMultiply;

    public float radius;

    public float sphereXRotation;

    public float waitForCombineFloat = 0;

    [Header("Landing Sequance Control")]
    public float xRotMyGuys;

    public float yRotMyGuys;

    public float zRotMyGuys;

    public float lerpTime;

    public float moveMyGuysTime;

    public float timeForRotateAndCombine;

    [Header("Circle Controll")]
    public Material[] myGuyBurningMaterialsArray = new Material[4];

    public float changeSpeed;

    private Quaternion startRot;
    private Quaternion endRotPlus;
    private Quaternion endRotMinus;
    private Vector3 endRotMinusVector;
    private Vector3 endRotPlusVector;


    // Start is called before the first frame update
    void Start()
    {
        myGuysList = new List<GameObject>();
        this.InitCircleFormation();

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
        for(var i = 0; i < myGuysList.Count; i++)
        {
            // Get the angle of the current index being instantiated
            // from the center of the circle.
            float angle = i * (2 * 3.14159f / myGuysList.Count);

            // Get the X Position of the angle times 1.5f. 1.5f is the radius of the circle.
            float x = Mathf.Cos(angle) * radius;

            // Get the Y Position of the angle times 1.5f. 1.5f is the radius of the circle.
            float y = Mathf.Sin(angle) * radius;

            // Set the position of the instantiated object to the targetPosition.
            myGuysList[i].transform.localPosition = new Vector3( x, y, 0f);

            // myGuysList[i].transform.LookAt(new Vector3(0f,0f,0f));

        }

    }

    public IEnumerator CombineForLandingSequance()
    {
        sizeMultiply = numberOfGuys;

        endRotPlusVector = new Vector3(-xRotMyGuys, -yRotMyGuys, -zRotMyGuys);
        endRotMinusVector = new Vector3(-xRotMyGuys, yRotMyGuys, zRotMyGuys);
        endRotPlus.eulerAngles = endRotPlusVector;
        endRotMinus.eulerAngles = endRotMinusVector;

        foreach (Transform child in transform)
        {
            StartCoroutine(QuaternionRotateTowardsMyGuys(child));

        }

        yield return new WaitForSeconds(timeForRotateAndCombine);

        DestroyAllInstantiateBigMyGuy();

    }
    public void DestroyAllInstantiateBigMyGuy()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        myGuysList.Clear();

        numberOfGuys = 1;

        radius = 0;

        InitCircleFormation();

        transform.GetComponentInChildren<Transform>().localScale = new Vector3(1f, 1f, 1f) * sizeMultiply;

        smokeHolderGO.SetActive(true);

        StartCoroutine(mgdmSC.ChangeMyGuyDivingColor());

    }

    public IEnumerator QuaternionRotateTowardsMyGuys(Transform child)
    {
        if (child.localPosition.x >= 0)
        {
            float timeSinceStartedPlus = 0f;
            float timeSinceStartedMovingPlus = 0f;

            while (true)
            {
                timeSinceStartedPlus += Time.deltaTime;
                timeSinceStartedMovingPlus += Time.deltaTime;

                Debug.Log("Entered Vector != 0 + " + timeSinceStartedMovingPlus);

                startRot = child.localRotation;
                //child.transform.localRotation = Quaternion.Slerp(startRot, Quaternion.Euler(endRotPlus.eulerAngles), lerpTime);
                child.localRotation = Quaternion.RotateTowards(startRot, Quaternion.Euler(endRotPlus.eulerAngles), timeSinceStartedPlus * lerpTime);

                child.localPosition = Vector3.MoveTowards(child.localPosition, Vector3.zero, timeSinceStartedMovingPlus * moveMyGuysTime);

                // If the object has arrived, stop the coroutine
                if (child.rotation == endRotPlus || child.localPosition == Vector3.zero)
                {
                    yield break;
                }

                // Otherwise, continue next frame
                yield return null;

            }

        }

        else if (child.localPosition.x < 0)
        {
            float timeSinceStartedMinus = 0f;
            float timeSinceStartedMovingMinus = 0f;

            while (true)
            {
                timeSinceStartedMinus += Time.deltaTime;
                timeSinceStartedMovingMinus += Time.deltaTime;

                Debug.Log("Entered Vector != 0 + " + timeSinceStartedMovingMinus);

                startRot = child.transform.localRotation;
                //child.transform.localRotation = Quaternion.Slerp(startRot, Quaternion.Euler(endRotMinus.eulerAngles), lerpTime);
                child.transform.localRotation = Quaternion.RotateTowards(startRot, Quaternion.Euler(endRotMinus.eulerAngles), timeSinceStartedMinus * lerpTime);

                child.localPosition = Vector3.MoveTowards(child.localPosition, Vector3.zero, timeSinceStartedMovingMinus * moveMyGuysTime);

                // If the object has arrived, stop the coroutine
                if (child.rotation == endRotMinus || child.localPosition == Vector3.zero)
                {
                    yield break;
                }

                // Otherwise, continue next frame
                yield return null;

            }

        }

    }

}
