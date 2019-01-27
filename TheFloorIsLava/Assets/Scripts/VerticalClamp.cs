using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalClamp : MonoBehaviour {

    private float upperLimit = 1.75f;
    private float lowerLimit = -1.75f;

	// Update is called once per frame
	void Update ()
    {
        if(transform.parent.transform.position.y > upperLimit)
        {
            transform.position = new Vector3(transform.parent.transform.position.x, upperLimit, -8f);
        }
        else if(transform.parent.transform.position.y < lowerLimit)
        {
            transform.position = new Vector3(transform.parent.transform.position.x, lowerLimit, -8f);
        }
        else
        {
            transform.position = new Vector3(transform.parent.transform.position.x, transform.parent.transform.position.y, -8f);
        }

        Debug.Log(transform.position);
	}
}
