using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour
{
    private float amplitude = 2f;
    private float minAmplitude = 1f;
    private float maxAmplitude = 2.5f;
    private float frequency = 5f;
    private float minFrequency = 0.5f;
    private float maxFrequency = 2f;
    private float time = 0f;

    private void Start()
    {
        frequency = Random.Range(minFrequency, maxFrequency);
        amplitude = Random.Range(minAmplitude, maxAmplitude);
    }

    // Update is called once per frame
    void Update ()
    {
        time += frequency * Time.deltaTime;
        transform.localPosition = new Vector3(0, Mathf.Abs(amplitude * Mathf.Cos(time)), 0);
    }
}
