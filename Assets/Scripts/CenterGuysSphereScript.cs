using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class CenterGuysSphereScript : MonoBehaviour
{
    [Header("Serialized References")]

    [SerializeField] MyGuyCenterController myGuyccSC;

    [SerializeField] GameObject[] myGuysArray = new GameObject[12];

    public List<GameObject> activeGuys = new List<GameObject>();

    [SerializeField] List<Transform> guysTransform = new List<Transform>();

    public List<GameObject> guysParachute = new List<GameObject>();

    [SerializeField] GameObject centerSphereGO;

    [SerializeField] GameObject losePopup;

    [SerializeField] GameObject TopUI;

    [Header("Speed Controls")]

    [SerializeField] float gameTime;

    [SerializeField] float formationRotSpeed;

    [SerializeField] float posSpeed;

    [SerializeField] float rotSpeed;

    [Header("Landing Speed Controls")]

    [SerializeField] float landRotSpeed;

    [SerializeField] float glideSpeedLanding;

    [SerializeField] float fallSpeedLanding;

    [SerializeField] float waitBeforeParachuteOpen;

    private Quaternion guyLandingQuat;

    private Quaternion newRot;

    private Quaternion sphereLandingQuat;

    private void Start()
    {
        Time.timeScale = gameTime;

        activeGuys.Insert(0, myGuysArray[0]);

        guyLandingQuat.eulerAngles = new Vector3(0f, 0f, 0f);

        sphereLandingQuat.eulerAngles = new Vector3(0f, 0f, 0f);

        newRot.eulerAngles = Vector3.zero;

    }

    private void FixedUpdate()
    {
        transform.Rotate(Vector3.up, formationRotSpeed * Time.deltaTime);

    }

    public void GuyLandingSequance()
    {
        myGuyccSC.glideSpeed = glideSpeedLanding;

        myGuyccSC.fallSpeed = fallSpeedLanding * 1.5f;

        formationRotSpeed = 0.0f;

        foreach (GameObject guy in activeGuys)
        {
            StartCoroutine(LerpGuysForLanding(guy));

        }

    }

    private IEnumerator LerpGuysForLanding(GameObject guy)
    {
        var timeSinceStartedLandingSequance = 0.0f;

        while (true)
        {
            timeSinceStartedLandingSequance += Time.deltaTime;

            guy.transform.localRotation = Quaternion.RotateTowards(guy.transform.localRotation, guyLandingQuat, timeSinceStartedLandingSequance * landRotSpeed * 10f);

            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, sphereLandingQuat, timeSinceStartedLandingSequance * landRotSpeed * 2);

            if (transform.localRotation == sphereLandingQuat && guy.transform.localRotation == guyLandingQuat)
            {
                break;

            }

            yield return null;

        }

        Debug.Log("Reached animator call");

        guy.GetComponentInChildren<Animator>().SetTrigger("parachuteOpenTrigger");

        yield return new WaitForSeconds(waitBeforeParachuteOpen);

        foreach (GameObject item in guysParachute)
        {
            item.transform.localPosition = new Vector3(0f, 6.6f, 2.3f);

            item.SetActive(true);

            if (item.GetComponent<Animator>().isActiveAndEnabled)
            {
                item.GetComponent<Animator>().SetTrigger("openParachute");

            }

        }

    }

    public void GuyPickupSequance()
    {
        for (int x = 0; x < myGuysArray.Length; x++)
        {
            if (myGuysArray[x].activeSelf)
            {

            }
            else if (!myGuysArray[x].activeSelf)
            {
                myGuysArray[x].SetActive(true);

                activeGuys.Insert(0, myGuysArray[x]);

                break;

            }

        }

        MoveGuysToPositions();

    }

    public void GuyRemoveSequance()
    {
        Debug.Log("Active Guys Length : " + activeGuys.Count);

        activeGuys[activeGuys.Count - 1].SetActive(false);

        Debug.Log("Active Guys Length : " + activeGuys.Count);

        activeGuys.RemoveAt(activeGuys.Count - 1);

        Debug.Log("Active Guys Length : " + activeGuys.Count);

        if (activeGuys.Count == 0)
        {
            TopUI.SetActive(false);

            losePopup.SetActive(true);

            // GameLostCoroutine();

            Destroy(gameObject);

        }

    }

    public void OnClickRetryButton()
    {
        SceneManager.LoadScene(0);

    }

    public void UnparentActiveGuys()
    {
        for (int x = 0; x < activeGuys.Count; x++)
        {
            activeGuys[x].transform.SetParent(null);

        }

    }

    private void GameLostCoroutine()
    {
        Time.timeScale = 0.1f;

    }

    private void MoveGuysToPositions()
    {
        for (int y = 0; y < activeGuys.Count; y++)
        {
            StartCoroutine(LerpGuysPos(y));

            StartCoroutine(LerpGuysRot(y));

        }

    }

    private IEnumerator LerpGuysPos(int y)
    {
        var timeSinceStartedPos = 0.0f;

        while (true)
        {
            activeGuys[y].transform.localPosition = Vector3.MoveTowards(activeGuys[y].transform.localPosition, guysTransform[y].localPosition, timeSinceStartedPos * posSpeed);
            timeSinceStartedPos += Time.deltaTime;

            if (activeGuys[y].transform.localPosition == guysTransform[y].localPosition)
            {
                yield break;
            }

            yield return null;

        }

    }

    private IEnumerator LerpGuysRot(int y)
    {
        var timeSinceStartedRot = 0.0f;

        while (true && activeGuys[y] != null)
        {
            activeGuys[y].transform.localRotation = Quaternion.RotateTowards(activeGuys[y].transform.localRotation, guysTransform[y].localRotation, timeSinceStartedRot * rotSpeed);
            timeSinceStartedRot += Time.deltaTime;

            if (activeGuys[y].transform.localRotation == guysTransform[y].localRotation)
            {
                yield break;
            }

            yield return null;

        }

    }

    //[Header("Cached References")]
    //[SerializeField] GameObject MyGuyPrefab;

    //[SerializeField] MyGuyCenterController myGuyCenterSC;

    //[SerializeField] Transform CenterSphere;

    //[SerializeField] GameObject emptyHolderGO;

    //[Header("Formations Control")]
    //public List<GameObject> myGuysList;

    //public float numberOfGuys;

    //[Header("Circle")]
    //public float sizeMultiply;

    //public float radius;

    //public float sphereXRotation;

    //[Header("Square")]
    //public int xSquareOffset;

    //public int ySquareOffset;

    //[Header("Triangle")]
    //public float rowOffsetTri = 0f;

    //public float yOffsetTri = 0f;

    //public float xOffsetTri = 0f;

    //[Header("Line")]
    //public float xOffsetLine;

    //[Header("Landing Sequance Control")]
    //public float waitForCombineFloat = 0;

    //public float rotationSpeedMyGuy;

    //public float rotationSpeedSphere;

    //private Quaternion endRotSphere;
    //private Quaternion endRotMyGuy;

    //public float formationSpeed;

    //// Start is called before the first frame update
    //void Start()
    //{
    //    myGuysList = new List<GameObject>();
    //    this.InitFormation();

    //    endRotSphere.eulerAngles = new Vector3(90f, 0f, 0f);
    //    endRotMyGuy.eulerAngles = new Vector3(-90f, 0f, 0f);

    //    transform.rotation = Quaternion.Euler(sphereXRotation, 0f, 0f);

    //}

    //public void InitFormation()
    //{
    //    // Loop through the number of points in the circle.
    //    for (int i = 0; i < numberOfGuys; i++)
    //    {
    //        if (numberOfGuys > myGuysList.Count)
    //        {
    //            // Instantiate the prefab.
    //            GameObject myGuy = Instantiate(MyGuyPrefab, transform);

    //            myGuy.transform.localPosition = Vector3.zero;

    //            myGuysList.Add(myGuy);

    //        }

    //        //// Get the angle of the current index being instantiated
    //        //// from the center of the circle.
    //        //float angle = i * (2 * 3.14159f / numberOfGuys);

    //        //// Get the X Position of the angle times 1.5f. 1.5f is the radius of the circle.
    //        //float x = Mathf.Cos(angle) * 9f;
    //        //// Get the Y Position of the angle times 1.5f. 1.5f is the radius of the circle.
    //        //float y = Mathf.Sin(angle) * 9f;

    //        //// Set the position of the instantiated object to the targetPosition.
    //        //instance.transform.position = new Vector3(CenterSphere.position.x + x, CenterSphere.position.y + y, CenterSphere.position.z);

    //    }

    //    //switch (numberOfGuys)
    //    //{
    //    //    case 1:
    //    //        UpdateLine1();
    //    //        break;

    //    //    case 2:
    //    //        UpdateLine2();
    //    //        break;

    //    //    case 3:
    //    //        UpdateTriangle3();
    //    //        break;

    //    //    case 4:
    //    //        UpdateSquare4();
    //    //        break;

    //    //    case 5:
    //    //        UpdateCircle();
    //    //        break;

    //    //    case 6:
    //    //        UpdateTriangle6();
    //    //        break;

    //    //    case 7:
    //    //        UpdateCircle();
    //    //        break;

    //    //    case 8:
    //    //        UpdateSquare8();
    //    //        break;

    //    //    case 9:
    //    //        UpdateTriangle9();
    //    //        break;

    //    //    case 10:
    //    //        UpdateCircle();
    //    //        break;

    //    //    case 11:
    //    //        UpdateCircle();
    //    //        break;

    //    //    case 12:
    //    //        UpdateSquare12();
    //    //        break;

    //    //}

    //     UpdateCircle();

    //}

    //// Formation Functions:

    ////  Line-
    //private void UpdateLine1()
    //{
    //    Vector3 targetPosition = transform.localPosition;

    //    GameObject myGuyLine = myGuysList[0];

    //    myGuyLine.transform.localPosition = targetPosition;

    //}

    //private void UpdateLine2()
    //{
    //    List<Vector3> targetPositions = new List<Vector3>();

    //    Vector3 targetPosition = transform.localPosition;

    //    for (int i = 0; i < 2; i++)
    //    {
    //        GameObject myGuyLine = myGuysList[i];

    //        if (i == 0)
    //        {
    //            targetPosition = new Vector3(targetPosition.x - xOffsetLine, 0f, 0f);

    //        }
    //        else if (i == 1)
    //        {
    //            targetPosition = new Vector3(targetPosition.x + xOffsetLine, 0f, 0f);

    //        }

    //        targetPositions.Add(targetPosition);
    //        // StartCoroutine(LineLerp2(targetPosition, myGuyLine));

    //    }

    //    SetGuyPositionFromList(targetPositions);

    //}

    ////  Triangles -
    //private void UpdateTriangle3()
    //{
    //    List<Vector3> targetPositions = new List<Vector3>();

    //    Vector3 targetPosition = Vector3.left;

    //    int rows = 2;

    //    int counter = 0;

    //    for (int i = 1; i <= rows; i++)
    //    {
    //        for (int x = 0; x < i; x++)
    //        {
    //            GameObject myGuyTriangle = myGuysList[counter];
    //            if (counter == 0)
    //            {
    //                targetPosition = new Vector3(0f, targetPosition.y + 6.15f, 0);

    //            }
    //            else { targetPosition = new Vector3(targetPosition.x + xOffsetTri, targetPosition.y, 0); }

    //            counter++;

    //            targetPositions.Add(targetPosition);
    //            // StartCoroutine(TriangleLerp3(targetPosition, myGuyTriangle));

    //        }

    //        targetPosition = new Vector3((rowOffsetTri * i) - xOffsetTri, targetPosition.y + yOffsetTri, 0);

    //    }

    //    SetGuyPositionFromList(targetPositions);

    //}

    //private void UpdateTriangle6()
    //{
    //    List<Vector3> targetPositions = new List<Vector3>();

    //    Vector3 targetPosition = Vector3.left;

    //    int rows = 3;

    //    int counter = 0;

    //    for (int i = 1; i <= rows; i++)
    //    {
    //        for (int x = 0; x < i; x++)
    //        {
    //            GameObject myGuyTriangle = myGuysList[counter];
    //            if (counter == 0)
    //            {
    //                targetPosition = new Vector3(0f, targetPosition.y + 12.3f, 0f);

    //            }
    //            else { targetPosition = new Vector3(targetPosition.x + xOffsetTri, targetPosition.y, 0f); }

    //            counter++;

    //            targetPositions.Add(targetPosition);
    //            // StartCoroutine(TriangleLerp6(targetPosition, myGuyTriangle));

    //        }

    //        targetPosition = new Vector3((rowOffsetTri * i) - xOffsetTri, targetPosition.y + yOffsetTri, 0f);

    //    }

    //    SetGuyPositionFromList(targetPositions);

    //}

    //private void UpdateTriangle9()
    //{
    //    List<Vector3> targetPositions = new List<Vector3>();

    //    Vector3 targetPosition = Vector3.left;

    //    int rows = 4;

    //    int counter = 0;

    //    for (int i = 1; i <= rows; i++)
    //    {
    //        for (int x = 0; x < i; x++)
    //        {
    //            if (i == 3 && x == 1)
    //            {
    //                GameObject myGuyTriangle = Instantiate(emptyHolderGO);

    //                targetPosition = new Vector3(targetPosition.x + xOffsetTri, targetPosition.y, 0f);

    //                myGuyTriangle.transform.localPosition = targetPosition;

    //            }
    //            else
    //            {
    //                GameObject myGuyTriangle = myGuysList[counter];

    //                if (counter == 0)
    //                {
    //                    targetPosition = new Vector3(0f, targetPosition.y + 34.6f, 0f);

    //                }

    //                else { targetPosition = new Vector3(targetPosition.x + xOffsetTri, targetPosition.y, 0f); }

    //                counter++;

    //                targetPositions.Add(targetPosition);
    //                // StartCoroutine(TriangleLerp9(targetPosition, myGuyTriangle));

    //            }


    //        }

    //        targetPosition = new Vector3((rowOffsetTri * i) - xOffsetTri, targetPosition.y + yOffsetTri, 0f);

    //    }

    //    SetGuyPositionFromList(targetPositions);

    //}

    ////   Squares-
    //private void UpdateSquare4()
    //{
    //    List<Vector3> targetPositions = new List<Vector3>();

    //    // Set a targetposition variable of where to spawn objects.
    //    Vector3 targetpostion = transform.localPosition;
    //    targetpostion.x = -xSquareOffset / 2;
    //    targetpostion.y = -xSquareOffset / 2;

    //    // Counter used for indexing when to start a new row.
    //    int counter = -1;
    //    // The offset of each object from one another on the X axis.
    //    int xoffset = -1;

    //    // Get the square root
    //    float sqrt = Mathf.Sqrt(4);

    //    // Get the reference to the starting target positions x.
    //    float startx = targetpostion.x;

    //    for (int i = 0; i < numberOfGuys; i++)
    //    {
    //        GameObject myGuySquare = myGuysList[i];

    //        // Increment the counter by 1.
    //        counter++;
    //        // Increment the xoffset by 1.
    //        xoffset++;

    //        /// We do this check because we do not want the offset being 1 on the 
    //        /// first iteration of the loop. We want the first index to be placed at 0.
    //        // If the xoffset > 1.
    //        if (xoffset > 1)
    //        {
    //            // Set the xoffset to 1.
    //            xoffset = 1;

    //        }

    //        // Set the targetposition to a new Vector 3 with the new variables and offset applied.

    //        targetpostion = new Vector3(targetpostion.x + (xoffset * xSquareOffset), targetpostion.y, 0f);

    //        // If the counter is equal to the sqrt variable rounded down.
    //        if (counter == Mathf.Floor(sqrt))
    //        {
    //            // Reset counter to 0.
    //            counter = 0;
    //            // Set the targetposition x to the referenced start x.
    //            targetpostion.x = startx;
    //            // Set the targetposition y to 1 + 0.25f.
    //            // The 1 is to increment in the y axis, giving another row.
    //            // The 0.25f is to offset each sphere is the y axis so they do not overlap.
    //            targetpostion.y += 1 + ySquareOffset;

    //        }

    //        targetPositions.Add(targetpostion);
    //        // StartCoroutine(SquareLerp4(targetpostion, myGuySquare));

    //    }

    //    SetGuyPositionFromList(targetPositions);

    //}

    //private void UpdateSquare8()
    //{
    //    List<Vector3> targetPositions = new List<Vector3>();

    //    // Set a targetposition variable of where to spawn objects.
    //    Vector3 targetpostion = transform.localPosition;
    //    targetpostion.x = -xSquareOffset / 2;
    //    targetpostion.y = -xSquareOffset / 2;

    //    // Counter used for indexing when to start a new row.
    //    int counter = -1;
    //    // The offset of each object from one another on the X axis.
    //    int xoffset = -1;

    //    // Get the square root
    //    float sqrt = Mathf.Sqrt(12);

    //    // Get the reference to the starting target positions x.
    //    float startx = targetpostion.x;

    //    for (int i = 0; i < numberOfGuys; i++)
    //    {
    //        GameObject myGuySquare = myGuysList[i];

    //        if (i == 4)
    //        {
    //            // Increment the counter by 1.
    //            counter += 2;
    //            // Increment the xoffset by 1.
    //            xoffset++;
    //        }
    //        else
    //        {
    //            // Increment the counter by 1.
    //            counter++;
    //            // Increment the xoffset by 1.
    //            xoffset++;
    //        }

    //        /// We do this check because we do not want the offset being 1 on the 
    //        /// first iteration of the loop. We want the first index to be placed at 0.
    //        // If the xoffset > 1.
    //        if (xoffset > 1)
    //        {
    //            // Set the xoffset to 1.
    //            xoffset = 1;

    //        }

    //        // Set the targetposition to a new Vector 3 with the new variables and offset applied.
    //        if (i == 4)
    //        {
    //            targetpostion = new Vector3(targetpostion.x + (xoffset * xSquareOffset * 2), targetpostion.y, 0f);
    //        }
    //        else { targetpostion = new Vector3(targetpostion.x + (xoffset * xSquareOffset), targetpostion.y, 0f); }


    //        // If the counter is equal to the sqrt variable rounded down.
    //        if (counter == Mathf.Floor(sqrt))
    //        {
    //            // Reset counter to 0.
    //            counter = 0;
    //            // Set the targetposition x to the referenced start x.
    //            targetpostion.x = startx;
    //            // Set the targetposition y to 1 + 0.25f.
    //            // The 1 is to increment in the y axis, giving another row.
    //            // The 0.25f is to offset each sphere is the y axis so they do not overlap.
    //            targetpostion.y += 1 + ySquareOffset;

    //        }

    //        targetPositions.Add(targetpostion);
    //        // StartCoroutine(SquareLerp8(targetpostion, myGuySquare));

    //    }

    //    SetGuyPositionFromList(targetPositions);

    //}


    //private void UpdateSquare12()
    //{
    //    List<Vector3> targetPositions = new List<Vector3>();

    //    // Set a targetposition variable of where to spawn objects.
    //    Vector3 targetpostion = transform.localPosition;
    //    targetpostion.x = -xSquareOffset / 2;
    //    targetpostion.y = -xSquareOffset / 2;

    //    // Counter used for indexing when to start a new row.
    //    int counter = -1;
    //    // The offset of each object from one another on the X axis.
    //    int xoffset = -1;

    //    // Get the square root
    //    float sqrt = Mathf.Sqrt(16);

    //    // Get the reference to the starting target positions x.
    //    float startx = targetpostion.x;

    //    for (int i = 0; i < numberOfGuys; i++)
    //    {
    //        GameObject myGuySquare = myGuysList[i];

    //        if (i == 5 || i == 7)
    //        {
    //            // Increment the counter by 1.
    //            counter += 3;
    //            // Increment the xoffset by 1.
    //            xoffset++;
    //        }
    //        else
    //        {
    //            // Increment the counter by 1.
    //            counter++;
    //            // Increment the xoffset by 1.
    //            xoffset++;
    //        }

    //        /// We do this check because we do not want the offset being 1 on the 
    //        /// first iteration of the loop. We want the first index to be placed at 0.
    //        // If the xoffset > 1.
    //        if (xoffset > 1)
    //        {
    //            // Set the xoffset to 1.
    //            xoffset = 1;

    //        }

    //        // Set the targetposition to a new Vector 3 with the new variables and offset applied.
    //        if (i == 5 || i == 7)
    //        {
    //            targetpostion = new Vector3(targetpostion.x + (xoffset * xSquareOffset * 3), targetpostion.y, 0f);
    //        }
    //        else { targetpostion = new Vector3(targetpostion.x + (xoffset * xSquareOffset), targetpostion.y, 0f); }


    //        // If the counter is equal to the sqrt variable rounded down.
    //        if (counter == Mathf.Floor(sqrt))
    //        {
    //            // Reset counter to 0.
    //            counter = 0;
    //            // Set the targetposition x to the referenced start x.
    //            targetpostion.x = startx;
    //            // Set the targetposition y to 1 + 0.25f.
    //            // The 1 is to increment in the y axis, giving another row.
    //            // The 0.25f is to offset each sphere is the y axis so they do not overlap.
    //            targetpostion.y += 1 + ySquareOffset;

    //        }

    //        targetPositions.Add(targetpostion);
    //        // StartCoroutine(SquareLerp12(targetpostion, myGuySquare));

    //    }

    //    SetGuyPositionFromList(targetPositions);

    //}

    ////   Circle-
    //private void UpdateCircle()
    //{
    //    List<Vector3> targetPositions = new List<Vector3>();

    //    for (var i = 0; i < myGuysList.Count; i++)
    //    {
    //        // Get the angle of the current index being instantiated
    //        // from the center of the circle.
    //        float angle = i * (2 * 3.14159f / myGuysList.Count);

    //        // Get the X Position of the angle times 1.5f. 1.5f is the radius of the circle.
    //        float x = Mathf.Cos(angle) * (radius * numberOfGuys);

    //        // Get the Y Position of the angle times 1.5f. 1.5f is the radius of the circle.
    //        float y = Mathf.Sin(angle) * (radius * numberOfGuys);

    //        targetPositions.Add(new Vector3(x, y, 0f));

    //        //StartCoroutine(CircleLerp(i, x, y));

    //    }

    //    SetGuyPositionFromList(targetPositions);

    //}

    //// Set The Positions From The List:
    //private void SetGuyPositionFromList(List<Vector3> targetPositions)
    //{
    //    float currentClosest = 100f;

    //    Vector3 currentTarget = Vector3.zero;

    //    foreach (GameObject guy in myGuysList)
    //    {
    //        if (guy.activeSelf)
    //        {
    //            for (int z = 0; z < targetPositions.Count; z++)
    //            {
    //                if (Vector3.Distance(guy.transform.localPosition, targetPositions[z]) <= currentClosest)
    //                {
    //                    currentClosest = Vector3.Distance(guy.transform.localPosition, targetPositions[z]);

    //                    currentTarget = targetPositions[z];

    //                }

    //            }

    //            StartCoroutine(guy.GetComponent<AllyAgentScript>().GuySendToClosestLocation(currentTarget, numberOfGuys));

    //            targetPositions.Remove(currentTarget);

    //            currentClosest = 100f;
    //        }
    //    }
    //}

    //// Rotation Coroutines:

    ////  Start Sequance-
    //public void StartMyGuyRotation()
    //{
    //    foreach (Transform child in transform)
    //    {
    //        StartCoroutine(StartRotCoroutine(child));

    //    }

    //}

    //public IEnumerator StartSphereRotation()
    //{
    //    var timeSinceStartedRotation = 0.0f;

    //    while (true)
    //    {
    //        timeSinceStartedRotation += Time.deltaTime;

    //        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, endRotSphere, timeSinceStartedRotation * rotationSpeedSphere);

    //        if (transform.localRotation == endRotSphere)
    //        {
    //            myGuyCenterSC.glideSpeed = 0;

    //            myGuyCenterSC.fallSpeed = 0;

    //            myGuyCenterSC.turnSpeed = 0;

    //            foreach (Transform child in this.transform)
    //            {
    //                // child.GetComponent<NavMeshAgent>().enabled = true;
    //            }

    //            CenterSphere.gameObject.GetComponent<Rigidbody>().isKinematic = false;

    //            yield break;
    //        }

    //        yield return null;

    //    }

    //}

    //public IEnumerator StartRotCoroutine(Transform child)
    //{
    //    var timeSinceStartedMyGuyRotation = 0.0f;

    //    while (true)
    //    {
    //        timeSinceStartedMyGuyRotation += Time.deltaTime;

    //        child.localRotation = Quaternion.RotateTowards(child.localRotation, endRotMyGuy, timeSinceStartedMyGuyRotation * rotationSpeedMyGuy);

    //        if (transform.localRotation == endRotSphere)
    //        {
    //            yield break;
    //        }

    //        yield return null;

    //    }

    //}

}
