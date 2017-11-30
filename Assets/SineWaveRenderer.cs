using UnityEngine;
using System.Collections;

public class SineWaveRenderer : MonoBehaviour
{

    public GameObject plotPointObject;
    public int numberOfPoints = 100;
    public float animSpeed = 1.0f;
    public float scaleInputRange = 2 * Mathf.PI; // scale number from [0 to 99] to [0 to 2Pi]
    public float scaleResult = 10.0f;
    public bool animate = true;

    GameObject[] plotPoints;

    // Use this for initialization
    void Start()
    {

        if (plotPointObject == null) //if user did not fill in a game object to use for the plot points
            plotPointObject = GameObject.CreatePrimitive(PrimitiveType.Sphere); //create a sphere

        plotPoints = new GameObject[numberOfPoints]; //creat an array of 100 points.

        for (int i = 0; i < numberOfPoints; i++)
        {
            plotPoints[i] = (GameObject)GameObject.Instantiate(plotPointObject, new Vector3(i - (numberOfPoints / 2), 0, 0), Quaternion.identity); //this specifies what object to create, where to place it and how to orient it
        }
        //we now have an array of 100 points- your should see them in the hierarchy when you hit play
        plotPointObject.SetActive(false); //hide the original

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < numberOfPoints; i++)
        {
            float functionXvalue = i * scaleInputRange / numberOfPoints; // scale number from [0 to 99] to [0 to 2Pi]
            if (animate)
            {
                functionXvalue += Time.time * animSpeed;
            }
            plotPoints[i].transform.position = new Vector3(i - (numberOfPoints / 2), ComputeFunction(functionXvalue) * scaleResult, 0);
        }
    }
    float ComputeFunction(float x)
    {
        return Mathf.Sin(x);
    }
}