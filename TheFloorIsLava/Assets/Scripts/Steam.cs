using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof(HandleInfluences))]
    public class Steam : MonoBehaviour
    {
        private HandleInfluences handleInfluences = null;
        private BoxCollider2D boxCollider = null;
        private ParticleSystem steamParticles = null;
        private bool state = true;
        private float boostSpeed = 20f;
        private float elapsedSwitchTime = 0f;
        private float switchTime = 1f;
        private float minSwitchTime = 2f;
        private float maxSwitchTime = 3f;
        private float elapsedParticleTime = 0f;
        private float particleTime = 0.5f;

        private void Awake()
        {
            boxCollider = GetComponent<BoxCollider2D>();
            steamParticles = gameObject.GetComponentInChildren<ParticleSystem>();
        }

        // Update is called once per frame
        void Update()
        {
            elapsedSwitchTime += Time.deltaTime;

            // switch between on or off every 'switchTime' seconds
            if (elapsedSwitchTime > switchTime)
            {
                elapsedSwitchTime = 0;
                switchTime = Random.Range(minSwitchTime, maxSwitchTime);
                Switch();
            }

            if(state)
            {
                // timer to allow particles to rise/disperese before enabling or diabling the trigger box
                if(elapsedParticleTime > particleTime)
                {
                    boxCollider.enabled = true;
                }
                else
                {
                    elapsedParticleTime += Time.deltaTime;
                }
            }
            else
            {
                if(elapsedParticleTime > particleTime)
                {
                    boxCollider.enabled = false;
                }
                else
                {
                    elapsedParticleTime += Time.deltaTime;
                }
            }
        }

        private void Switch()
        {
            // invert current state
            state = !state;
            elapsedParticleTime = 0;

            // activate/inactive particle system depending on state
            if (state)
            {
                steamParticles.Play();
            }
            else
            {
                steamParticles.Stop();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // get reference to player's public script and apply boost
            handleInfluences = collision.gameObject.GetComponent<HandleInfluences>();
            handleInfluences.SteamBoost(boostSpeed);
        }
    }
}
