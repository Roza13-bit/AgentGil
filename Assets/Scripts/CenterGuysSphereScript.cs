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

    [SerializeField] GameObject emptyHolderGO;

    [Header("Formations Control")]
    public List<GameObject> myGuysList;

    public float numberOfGuys;

    [Header("Circle")]
    public float sizeMultiply;

    public float radius;

    public float sphereXRotation;

    [Header("Square")]
    public int xSquareOffset;

    public int ySquareOffset;

    [Header("Triangle")]
    public float rowOffsetTri = 0f;

    public float yOffsetTri = 0f;

    public float xOffsetTri = 0f;

    [Header("Line")]
    public float xOffsetLine;

    [Header("Landing Sequance Control")]
    public float waitForCombineFloat = 0;

    public float rotationSpeedMyGuy;

    public float rotationSpeedSphere;

    private Quaternion endRotSphere;
    private Quaternion endRotMyGuy;

    public float formationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        myGuysList = new List<GameObject>();
        this.InitFormation(MyGuyPrefab);

        endRotSphere.eulerAngles = new Vector3(90f, 0f, 0f);
        endRotMyGuy.eulerAngles = new Vector3(-90f, 0f, 0f);

        transform.rotation = Quaternion.Euler(sphereXRotation, 0f, 0f);

    }

    public void InitFormation(GameObject go)
    {
        // Loop through the number of points in the circle.
        for (int i = 0; i < numberOfGuys; i++)
        {
            if (numberOfGuys > myGuysList.Count)
            {
                // Instantiate the prefab.
                GameObject myGuy = Instantiate(MyGuyPrefab, transform);

                myGuysList.Add(myGuy);

            }
            else if (numberOfGuys < myGuysList.Count)
            {
                for (var x = 0; x < myGuysList.Count; x++)
                {
                    if (myGuysList[x] == go)
                    {
                        myGuysList.RemoveAt(x);

                        Destroy(go);

                    }

                }

                Debug.Log("Num Of Guys Before Adjust : " + numberOfGuys);
                Debug.Log("Num Of Guys In List Before Adjust : " + myGuysList.Count);

                if (numberOfGuys != myGuysList.Count)
                {
                    numberOfGuys = myGuysList.Count;

                    Debug.Log("Num Of Guys After Adjust : " + numberOfGuys);
                    Debug.Log("Num Of Guys In List Before Adjust : " + myGuysList.Count);

                }

            }

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

        switch (numberOfGuys)
        {
            case 1:
                UpdateLine1();
                break;

            case 2:
                UpdateLine2();
                break;

            case 3:
                UpdateTriangle3();
                break;

            case 4:
                UpdateSquare4();
                break;

            case 5:
                UpdateCircle();
                break;

            case 6:
                UpdateTriangle6();
                break;

            case 7:
                UpdateCircle();
                break;

            case 8:
                UpdateSquare8();
                break;

            case 9:
                UpdateTriangle9();
                break;

            case 10:
                UpdateCircle();
                break;

            case 11:
                UpdateCircle();
                break;

            case 12:
                UpdateSquare12();
                break;

        }

    }

    // Formation Functions:

    //  Line-
    private void UpdateLine1()
    {
        Vector3 targetPosition = transform.localPosition;

        GameObject myGuyLine = myGuysList[0];

        myGuyLine.transform.localPosition = targetPosition;

    }

    private void UpdateLine2()
    {
        Vector3 targetPosition = transform.localPosition;

        for (int i = 0; i < 2; i++)
        {
            GameObject myGuyLine = myGuysList[i];

            if (i == 0)
            {
                targetPosition = new Vector3(targetPosition.x - xOffsetLine, 0f, 0f);

            }
            else if (i == 1)
            {
                targetPosition = new Vector3(targetPosition.x + xOffsetLine, 0f, 0f);

            }

            StartCoroutine(LineLerp2(targetPosition, myGuyLine));

        }

    }

    private IEnumerator LineLerp2(Vector3 targetPosition, GameObject myGuyLine)
    {
        yield return new WaitForSeconds(0.08f);
        if (numberOfGuys == 2 && myGuysList.Count == 2)
        {
            var timeSinceStarted = 0.0f;

            while (true)
            {
                myGuyLine.transform.localPosition = Vector3.Lerp(myGuyLine.transform.localPosition, targetPosition, timeSinceStarted * formationSpeed);
                timeSinceStarted += Time.deltaTime;

                if (myGuyLine.transform.localPosition == targetPosition)
                {
                    yield break;
                }
                yield return null;
            }
        }
    }

    //  Triangles -
    private void UpdateTriangle3()
    {
        Vector3 targetPosition = Vector3.left;

        int rows = 2;

        int counter = 0;

        for (int i = 1; i <= rows; i++)
        {
            for (int x = 0; x < i; x++)
            {
                GameObject myGuyTriangle = myGuysList[counter];
                if (counter == 0)
                {
                    targetPosition = new Vector3(0f, targetPosition.y + 6.15f, 0);

                }
                else { targetPosition = new Vector3(targetPosition.x + xOffsetTri, targetPosition.y, 0); }

                counter++;

                StartCoroutine(TriangleLerp3(targetPosition, myGuyTriangle));

            }

            targetPosition = new Vector3((rowOffsetTri * i) - xOffsetTri, targetPosition.y + yOffsetTri, 0);

        }

    }

    private IEnumerator TriangleLerp3(Vector3 targetPosition, GameObject myGuyTriangle)
    {
        yield return new WaitForSeconds(0.08f);
        if (numberOfGuys == 3 && myGuysList.Count == 3)
        {
            var timeSinceStarted = 0.0f;

            while (true)
            {
                myGuyTriangle.transform.localPosition = Vector3.Lerp(myGuyTriangle.transform.localPosition, targetPosition, timeSinceStarted * formationSpeed);
                timeSinceStarted += Time.deltaTime;

                if (myGuyTriangle.transform.localPosition == targetPosition)
                {
                    yield break;
                }
                yield return null;
            }
        }
    }

    private void UpdateTriangle6()
    {
        Vector3 targetPosition = Vector3.left;

        int rows = 3;

        int counter = 0;

        for (int i = 1; i <= rows; i++)
        {
            for (int x = 0; x < i; x++)
            {
                GameObject myGuyTriangle = myGuysList[counter];
                if (counter == 0)
                {
                    targetPosition = new Vector3(0f, targetPosition.y + 12.3f, 0f);

                }
                else { targetPosition = new Vector3(targetPosition.x + xOffsetTri, targetPosition.y, 0f); }

                counter++;

                StartCoroutine(TriangleLerp6(targetPosition, myGuyTriangle));

            }

            targetPosition = new Vector3((rowOffsetTri * i) - xOffsetTri, targetPosition.y + yOffsetTri, 0f);

        }

    }

    private IEnumerator TriangleLerp6(Vector3 targetPosition, GameObject myGuyTriangle)
    {
        yield return new WaitForSeconds(0.08f);
        if (numberOfGuys == 6 && myGuysList.Count == 6)
        {
            var timeSinceStarted = 0.0f;

            while (true)
            {
                myGuyTriangle.transform.localPosition = Vector3.Lerp(myGuyTriangle.transform.localPosition, targetPosition, timeSinceStarted * formationSpeed);
                timeSinceStarted += Time.deltaTime;

                if (myGuyTriangle.transform.localPosition == targetPosition)
                {
                    yield break;
                }
                yield return null;
            }
        }


    }

    private void UpdateTriangle9()
    {
        Vector3 targetPosition = Vector3.left;

        int rows = 4;

        int counter = 0;

        for (int i = 1; i <= rows; i++)
        {
            for (int x = 0; x < i; x++)
            {
                if (i == 3 && x == 1)
                {
                    GameObject myGuyTriangle = Instantiate(emptyHolderGO);

                    targetPosition = new Vector3(targetPosition.x + xOffsetTri, targetPosition.y, 0f);

                    myGuyTriangle.transform.localPosition = targetPosition;

                }
                else
                {
                    GameObject myGuyTriangle = myGuysList[counter];

                    if (counter == 0)
                    {
                        targetPosition = new Vector3(0f, targetPosition.y + 34.6f, 0f);

                    }

                    else { targetPosition = new Vector3(targetPosition.x + xOffsetTri, targetPosition.y, 0f); }

                    counter++;

                    StartCoroutine(TriangleLerp9(targetPosition, myGuyTriangle));

                }

            }

            targetPosition = new Vector3((rowOffsetTri * i) - xOffsetTri, targetPosition.y + yOffsetTri, 0f);

        }

    }

    private IEnumerator TriangleLerp9(Vector3 targetPosition, GameObject myGuyTriangle)
    {
        yield return new WaitForSeconds(0.08f);
        if (numberOfGuys == 9 && myGuysList.Count == 9)
        {
            var timeSinceStarted = 0.0f;

            while (true)
            {
                myGuyTriangle.transform.localPosition = Vector3.Lerp(myGuyTriangle.transform.localPosition, targetPosition, timeSinceStarted * formationSpeed);
                timeSinceStarted += Time.deltaTime;

                if (myGuyTriangle.transform.localPosition == targetPosition)
                {
                    yield break;
                }
                yield return null;
            }
        }

    }

    //   Squares-
    private void UpdateSquare4()
    {
        // Set a targetposition variable of where to spawn objects.
        Vector3 targetpostion = transform.localPosition;
        targetpostion.x = -xSquareOffset / 2;
        targetpostion.y = -xSquareOffset / 2;

        // Counter used for indexing when to start a new row.
        int counter = -1;
        // The offset of each object from one another on the X axis.
        int xoffset = -1;

        // Get the square root
        float sqrt = Mathf.Sqrt(4);

        // Get the reference to the starting target positions x.
        float startx = targetpostion.x;

        for (int i = 0; i < numberOfGuys; i++)
        {
            GameObject myGuySquare = myGuysList[i];

            // Increment the counter by 1.
            counter++;
            // Increment the xoffset by 1.
            xoffset++;

            /// We do this check because we do not want the offset being 1 on the 
            /// first iteration of the loop. We want the first index to be placed at 0.
            // If the xoffset > 1.
            if (xoffset > 1)
            {
                // Set the xoffset to 1.
                xoffset = 1;

            }

            // Set the targetposition to a new Vector 3 with the new variables and offset applied.

            targetpostion = new Vector3(targetpostion.x + (xoffset * xSquareOffset), targetpostion.y, 0f);

            // If the counter is equal to the sqrt variable rounded down.
            if (counter == Mathf.Floor(sqrt))
            {
                // Reset counter to 0.
                counter = 0;
                // Set the targetposition x to the referenced start x.
                targetpostion.x = startx;
                // Set the targetposition y to 1 + 0.25f.
                // The 1 is to increment in the y axis, giving another row.
                // The 0.25f is to offset each sphere is the y axis so they do not overlap.
                targetpostion.y += 1 + ySquareOffset;

            }

            // x
            // xxx
            // xxx
            // xxx
            ////
            StartCoroutine(SquareLerp4(targetpostion, myGuySquare));

        }

    }

    private IEnumerator SquareLerp4(Vector3 targetpostion, GameObject myGuySquare)
    {
        yield return new WaitForSeconds(0.08f);
        if (numberOfGuys == 4 && myGuysList.Count == 4)
        {
            var timeSinceStarted = 0.0f;

            while (true)
            {
                myGuySquare.transform.localPosition = Vector3.Lerp(myGuySquare.transform.localPosition, targetpostion, timeSinceStarted * formationSpeed);
                timeSinceStarted += Time.deltaTime;

                if (myGuySquare.transform.localPosition == targetpostion)
                {
                    yield break;
                }
                yield return null;
            }
        }

    }

    private void UpdateSquare8()
    {
        // Set a targetposition variable of where to spawn objects.
        Vector3 targetpostion = transform.localPosition;
        targetpostion.x = -xSquareOffset / 2;
        targetpostion.y = -xSquareOffset / 2;

        // Counter used for indexing when to start a new row.
        int counter = -1;
        // The offset of each object from one another on the X axis.
        int xoffset = -1;

        // Get the square root
        float sqrt = Mathf.Sqrt(12);

        // Get the reference to the starting target positions x.
        float startx = targetpostion.x;

        for (int i = 0; i < numberOfGuys; i++)
        {
            GameObject myGuySquare = myGuysList[i];

            if (i == 4)
            {
                // Increment the counter by 1.
                counter += 2;
                // Increment the xoffset by 1.
                xoffset++;
            }
            else
            {
                // Increment the counter by 1.
                counter++;
                // Increment the xoffset by 1.
                xoffset++;
            }

            /// We do this check because we do not want the offset being 1 on the 
            /// first iteration of the loop. We want the first index to be placed at 0.
            // If the xoffset > 1.
            if (xoffset > 1)
            {
                // Set the xoffset to 1.
                xoffset = 1;

            }

            // Set the targetposition to a new Vector 3 with the new variables and offset applied.
            if (i == 4)
            {
                targetpostion = new Vector3(targetpostion.x + (xoffset * xSquareOffset * 2), targetpostion.y, 0f);
            }
            else { targetpostion = new Vector3(targetpostion.x + (xoffset * xSquareOffset), targetpostion.y, 0f); }


            // If the counter is equal to the sqrt variable rounded down.
            if (counter == Mathf.Floor(sqrt))
            {
                // Reset counter to 0.
                counter = 0;
                // Set the targetposition x to the referenced start x.
                targetpostion.x = startx;
                // Set the targetposition y to 1 + 0.25f.
                // The 1 is to increment in the y axis, giving another row.
                // The 0.25f is to offset each sphere is the y axis so they do not overlap.
                targetpostion.y += 1 + ySquareOffset;

            }

            // x
            // xxx
            // xxx
            // xxx
            ////
            StartCoroutine(SquareLerp8(targetpostion, myGuySquare));

        }

    }

    private IEnumerator SquareLerp8(Vector3 targetpostion, GameObject myGuySquare)

    {
        yield return new WaitForSeconds(0.08f);
        if (numberOfGuys == 8 && myGuysList.Count == 8)
        {
            var timeSinceStarted = 0.0f;

            while (true)
            {
                myGuySquare.transform.localPosition = Vector3.Lerp(myGuySquare.transform.localPosition, targetpostion, timeSinceStarted * formationSpeed);
                timeSinceStarted += Time.deltaTime;

                if (myGuySquare.transform.localPosition == targetpostion)
                {
                    yield break;
                }
                yield return null;
            }
        }
    }

    private void UpdateSquare12()
    {
        // Set a targetposition variable of where to spawn objects.
        Vector3 targetpostion = transform.localPosition;
        targetpostion.x = -xSquareOffset / 2;
        targetpostion.y = -xSquareOffset / 2;

        // Counter used for indexing when to start a new row.
        int counter = -1;
        // The offset of each object from one another on the X axis.
        int xoffset = -1;

        // Get the square root
        float sqrt = Mathf.Sqrt(16);

        // Get the reference to the starting target positions x.
        float startx = targetpostion.x;

        for (int i = 0; i < numberOfGuys; i++)
        {
            GameObject myGuySquare = myGuysList[i];

            if (i == 5 || i == 7)
            {
                // Increment the counter by 1.
                counter += 3;
                // Increment the xoffset by 1.
                xoffset++;
            }
            else
            {
                // Increment the counter by 1.
                counter++;
                // Increment the xoffset by 1.
                xoffset++;
            }

            /// We do this check because we do not want the offset being 1 on the 
            /// first iteration of the loop. We want the first index to be placed at 0.
            // If the xoffset > 1.
            if (xoffset > 1)
            {
                // Set the xoffset to 1.
                xoffset = 1;

            }

            // Set the targetposition to a new Vector 3 with the new variables and offset applied.
            if (i == 5 || i == 7)
            {
                targetpostion = new Vector3(targetpostion.x + (xoffset * xSquareOffset * 3), targetpostion.y, 0f);
            }
            else { targetpostion = new Vector3(targetpostion.x + (xoffset * xSquareOffset), targetpostion.y, 0f); }


            // If the counter is equal to the sqrt variable rounded down.
            if (counter == Mathf.Floor(sqrt))
            {
                // Reset counter to 0.
                counter = 0;
                // Set the targetposition x to the referenced start x.
                targetpostion.x = startx;
                // Set the targetposition y to 1 + 0.25f.
                // The 1 is to increment in the y axis, giving another row.
                // The 0.25f is to offset each sphere is the y axis so they do not overlap.
                targetpostion.y += 1 + ySquareOffset;

            }

            StartCoroutine(SquareLerp12(targetpostion, myGuySquare));

        }

    }

    private IEnumerator SquareLerp12(Vector3 targetpostion, GameObject myGuySquare)
    {
        yield return new WaitForSeconds(0.08f);
        if (numberOfGuys == 12 && myGuysList.Count == 12)
        {
            var timeSinceStarted = 0.0f;

            while (true)
            {
                myGuySquare.transform.localPosition = Vector3.Lerp(myGuySquare.transform.localPosition, targetpostion, timeSinceStarted * formationSpeed);
                timeSinceStarted += Time.deltaTime;

                if (myGuySquare.transform.localPosition == targetpostion)
                {
                    yield break;
                }
                yield return null;
            }
        }

    }

    //   Circle-
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

            StartCoroutine(CircleLerp(i, x, y));

        }

    }

    private IEnumerator CircleLerp(int i, float x, float y)
    {
        yield return new WaitForSeconds(0.08f);

        if (numberOfGuys == 5 && myGuysList.Count == 5 || numberOfGuys == 7 && myGuysList.Count == 7 ||
            numberOfGuys == 10 && myGuysList.Count == 10 || numberOfGuys == 11 && myGuysList.Count == 11)
        {
            var timeSinceStarted = 0.0f;

            while (true)
            {
                // Set the position of the instantiated object to the targetPosition.
                myGuysList[i].transform.localPosition = Vector3.Lerp(myGuysList[i].transform.localPosition, new Vector3(x, y, 0f), timeSinceStarted * formationSpeed);
                timeSinceStarted += Time.deltaTime;

                if (myGuysList[i].transform.localPosition == new Vector3(x, y, 0f))
                {
                    yield break;
                }
                yield return null;
            }

        }

    }

    // Rotation Coroutines:

    //  Start Sequance-
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
