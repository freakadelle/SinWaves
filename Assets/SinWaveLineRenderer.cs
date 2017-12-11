using UnityEngine;
using System.Collections;

public class SinWaveLineRenderer : MonoBehaviour
{
    public Gradient lineColorGradient;
    public int interpolationDetail = 20;

    [Range(0.0001f,1)]
    public float waveLength;
    [Range(0,10)]
    public float frequency;

    public Material lineMaterial;

    public AnimationCurve amplitudeOverTime;
    public AnimationCurve transformPathY;
    public AnimationCurve transformPathX;

    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = lineMaterial;
        lineRenderer.colorGradient = lineColorGradient;
        lineRenderer.startWidth = 0.2f;
        lineRenderer.endWidth = 0.2f;
        lineRenderer.positionCount = interpolationDetail;
    }

    void Update()
    {

        lineRenderer.positionCount = interpolationDetail;

        int i = 0;
        while (i < lineRenderer.positionCount)
        {
            float interpolation = (i / (float)interpolationDetail);

            float amplitude = transform.localScale.y * amplitudeOverTime.Evaluate(interpolation);
            float followPathY = transformPathY.Evaluate(interpolation);
            float followPathX = transformPathX.Evaluate(interpolation);

            //float sinusX = (Time.time / periodTime) - (interpolation * Mathf.PI * 2 / waveLength);
            float sinusX = (2 * Mathf.PI * frequency * Time.time) + ((interpolation * Mathf.PI * 2) / (waveLength));
            float xPos = (interpolation * transform.localScale.x) + transform.position.x + followPathX;
            float yPos = transform.position.y + followPathY;

            Vector3 pos = new Vector3(xPos, amplitude * Mathf.Sin(sinusX) + yPos, transform.position.z);
            lineRenderer.SetPosition(i, pos);
            i++;
        }
    }
}
