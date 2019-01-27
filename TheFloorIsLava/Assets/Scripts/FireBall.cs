using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof(HandleInfluences))]
    public class FireBall : MonoBehaviour
    {
        private HandleInfluences handleInfluences = null;
        private float jumpVelocity = 0f;
        private float jumpSpeed = 34f;
        private float verticalFriction = 0.6f;
        private float velocityScalar = 0.01f;
        private float restMin = 0f;
        private float restMax = 2.5f;
        private float restTime = 2f;
        private float elapsedTime = 0f;
        private float boostSpeed = 10f;
        private float baseHeight = -5f;

        //public AudioSource fireball_sound;

        void FixedUpdate()
        {
            ApplyFriction();

            ApplyJump();

            ApplyTransformation();
        }

        private void ApplyJump()
        {
            // if the fireball has fallen below the base marker then time rest and apply jump
            if (transform.localPosition.y < baseHeight)
            {
                elapsedTime += Time.deltaTime;
                jumpVelocity = 0;

                if (elapsedTime > restTime)
                {
                    elapsedTime = 0;
                    restTime = Random.Range(restMin, restMax);
                    jumpVelocity = jumpSpeed;
                }
            }
        }

        private void ApplyFriction()
        {
            // always apply verticla friction
            jumpVelocity -= verticalFriction;
            
        }

        private void ApplyTransformation()
        {
            // apply transformation
            transform.localPosition = new Vector3(0, transform.localPosition.y + (jumpVelocity * velocityScalar), 0);
            
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            // find direction used to boost player away from fireball
            Vector3 colliderPosition = collider.transform.position;
            Vector3 direction = colliderPosition - transform.position;

            // get reference to player's public script and apply boost 
            handleInfluences = collider.gameObject.GetComponent<HandleInfluences>();
            handleInfluences.FireBallBoost(new Vector2(direction.x*boostSpeed, direction.y*boostSpeed));
          
        }
    }
}
