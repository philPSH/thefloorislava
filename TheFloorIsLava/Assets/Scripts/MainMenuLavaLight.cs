using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuLavaLight : MonoBehaviour
{

    private Light dirLight;
    public float speed = 1.0f;
    public float distance = 2.0f;

    // Use this for initialization
    void Start ()
    {
        dirLight = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update ()
    {

        // Calculating a value between 0 and 1 that oscillates to give a nice turn
        float frac = 0.5f + 0.5f * Mathf.Sin(speed * Mathf.PI * Time.time);
 
        // Lerp along the x axis to move the cookie creating the effect of lava below
        dirLight.transform.position = Vector3.Lerp(new Vector3(-distance, 0.0f, 0.0f), new Vector3(distance, 0.0f, 0.0f), frac);

    }
}
