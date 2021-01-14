using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterGuysSphereScript : MonoBehaviour
{
    [Header("Cached References")]
    [SerializeField] GameObject MyGuyPrefab;

    [SerializeField] Transform CenterSphere;

    [Header("Circle Controll")]
    public List<GameObject> myGuysList;

    public float numberOfGuys;

    public float radius;

    public float sphereXRotation;

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
            GameObject go = Instantiate(MyGuyPrefab, CenterSphere);

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

    public void CombineForLandingSequance()
    {
        var sizeMultiply = numberOfGuys;

        foreach (Transform child in CenterSphere)
        {
            Destroy(child.gameObject);
        }

        myGuysList.Clear();

        numberOfGuys = 1;

        InitCircleFormation();

        CenterSphere.GetComponentInChildren<Transform>().localScale = new Vector3(1f, 1f, 1f) * sizeMultiply;

    }

}
