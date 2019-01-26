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
        private float jumpSpeed = 40f;
        private float verticalFriction = 0.75f;
        private float velocityScalar = 0.01f;

        // Update is called once per frame
        void Update()
        {
            ApplyJump();

            ApplyFriction();

            ApplyTransformation();
        }

        private void ApplyJump()
        {
            // if the fireball has fallen below the base marker then apply jump
            if (transform.localPosition.y < 0.0f)
            {
                jumpVelocity = jumpSpeed;
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

        private void OnCollisionEnter2D(Collision2D collision)
        {
            // get reference to player's public script and apply boost
            handleInfluences = collision.gameObject.GetComponent<HandleInfluences>();
            handleInfluences.FireBallBoost(new Vector2(0, 0));
        }
    }
}
