using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    private Vector3 startPosition;
    private Vector3 endPosition;
    private float fallDistance = 20f;
    private float fallSpeed = 1f;
    private float fallVelocity = 0f;
    private float elapsedTime = 0f;
    private float stallTime = 1f;
    private float velocityScalar = 0.01f;
    private bool falling = false;

	// Use this for initialization
	private void Awake ()
    {
        startPosition = transform.position;
        endPosition = new Vector3(startPosition.x, startPosition.y - fallDistance, startPosition.z);
	}
	
	// Update is called once per frame
	void Update ()
    {
        // if the platform should be falling
		if(falling)
        {
            // if the stalling period is over
           if(elapsedTime < stallTime)
            {
                elapsedTime += Time.deltaTime;
            }
            else
            {
                // if the pltform has not yet reached terminal position
                if (transform.position.y > endPosition.y)
                {
                    // increase velocity and apply effect
                    fallVelocity += fallSpeed;
                    transform.position = new Vector3(transform.position.x, (transform.position.y - (fallVelocity * velocityScalar)), transform.position.z);
                }
            }
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // on collision trigger falling
        falling = true;
    }
}
