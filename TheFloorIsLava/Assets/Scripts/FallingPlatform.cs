﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    private Vector3 endPosition;
    private float fallDistance = 20f;
    private float fallSpeed = 1f;
    private float fallVelocity = 0f;
    private float elapsedTime = 0f;
    private float stallTime = 1f;
    private float velocityScalar = 0.01f;
    private bool falling = false;

    private void Awake ()
    {
        endPosition = new Vector3(0, -fallDistance, 0);
	}
	
	void FixedUpdate ()
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
                if (transform.localPosition.y > endPosition.y)
                {
                    // increase velocity and apply effect
                    fallVelocity += fallSpeed;
                    transform.localPosition = new Vector3(0, (transform.localPosition.y - (fallVelocity * velocityScalar)), 0);
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
