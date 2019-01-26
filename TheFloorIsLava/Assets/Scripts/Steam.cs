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
        private MeshRenderer meshRenderer = null;
        private bool state = true;
        private float elapsedTime = 0f;
        private float switchTime = 1f;
        private float boostSpeed = 30f;

        private void Awake()
        {
            boxCollider = GetComponent<BoxCollider2D>();
            meshRenderer = GetComponent<MeshRenderer>();
        }

        // Update is called once per frame
        void Update()
        {
            // switch between on or off every 'switchTime' seconds
            elapsedTime += Time.deltaTime;
            if (elapsedTime > switchTime)
            {
                elapsedTime = 0;
                Switch();
            }
        }
        private void Switch()
        {
            // invert current state
            state = !state;

            // activate/inactive components depending on state
            if (state)
            {
                boxCollider.enabled = true;
                meshRenderer.enabled = true;
            }
            else
            {
                boxCollider.enabled = false;
                meshRenderer.enabled = false;
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
