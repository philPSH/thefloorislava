using System;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    public class PlatformerCharacter2D : MonoBehaviour
    {
        // Serialized Fields
        [SerializeField] private LayerMask whatIsGround;    // a mask determining what is ground to the character

        // Non Serialized Fields
        private float moveSpeed = 1f;                  // amount of speed added to horizontal velocity when the player moves
        private float maxVelocity = 20f;               // maximum amount of horizontal velocity the player can attain
        private float horizontalFriction = 0.5f;       // amount of horizontal friction applied to player's velocity
        private float verticalFriction = 0.5f;         // amount of vertical friction applied to player's velocity
        private float jumpSpeed = 20f;                 // amount of speed added to vertical velocity when the player jumps
        private float airSpeed = 0.3f;                 // amount of speed added to horizontal velocity when in the air
        private float velocityScalar = 0.01f;          // scalar value used to diminish the final velocity

        // #1 /1/15/0.75/0.5/20/0.3/0.01
        // #2 /1/20/0.5/0.5/20/0.3/0.01

        private Vector2 playerVelocity;                 // the player's velocity
        private Transform groundCheck;                  // position marking where to check if the player is grounded
        const float groundedRadius = .2f;               // radius of the overlap circle to determine if grounded
        private bool grounded;                          // whether or not the player is grounded
        private Animator anim;                          // reference to the player's animator component
        private bool facingRight = true;                // for determining which way the player is currently facing
        private SpriteRenderer spriteRenderer;          // reference to player's sprite renderer component

        private void Awake()
        {
            // Setting up references.
            groundCheck = transform.Find("GroundCheck");
            anim = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void FixedUpdate()
        {
            grounded = false;

            // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
            // This can be done using layers instead but Sample Assets will not overwrite your project settings.
            Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, whatIsGround);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                    grounded = true;
            }
            anim.SetBool("Ground", grounded);

            // Set the vertical animation
            anim.SetFloat("vSpeed", playerVelocity.y);
        }

        public void Move(float move, bool jump)
        {
            // speed animator parameter is set to the absolute value of the horizontal input
            anim.SetFloat("Speed", Mathf.Abs(move));

            // if the player is moving in the direction opposite to facing, flip facing
            Flip(move);

            ApplyMovement(move);

            ApplyJump(jump);

            ApplyFriction();

            // Apply movements found to player's transformation
            ApplyTransformation();
        }

        private void ApplyMovement(float move)
        {
            // only apply movement if not yet reached max velocity
            if (move * playerVelocity.x < maxVelocity)
            {
                // apply movement depending on whether or not the player is grounded
                if (grounded)
                {
                    playerVelocity = new Vector2(playerVelocity.x + (move * moveSpeed), playerVelocity.y);
                }
                else
                {
                    playerVelocity = new Vector2(playerVelocity.x + (move * airSpeed), playerVelocity.y);
                }
            }
        }

        private void ApplyJump(bool jump)
        {
            // only apply jump if player is grouned and jump has been pressed
            if (grounded && jump)
            {
                grounded = false;
                anim.SetBool("Ground", false);
                playerVelocity.y += jumpSpeed;
            }
        }

        private void ApplyFriction()
        {
            // only apply horizontal friction if grounded
            if(grounded)
            {
                // if grounded there will be no vertical velocity
                playerVelocity.y = 0;

                // only apply friction in the direction needed
                // if velocity tends to zero then set to zero
                if (playerVelocity.x > horizontalFriction)
                {
                    playerVelocity.x -= horizontalFriction;
                }
                else if (playerVelocity.x < -horizontalFriction)
                {
                    playerVelocity.x += horizontalFriction;
                }
                else
                {
                    playerVelocity.x = 0;
                }
            }
            else
            {
                // if not grounded then apply vertical friction / gravity
                playerVelocity.y -= verticalFriction;
            }
        }

        private void ApplyTransformation()
        {
            transform.position = new Vector3(transform.position.x + (playerVelocity.x * velocityScalar), transform.position.y + (playerVelocity.y * velocityScalar), 0);
        }

        private void Flip(float move)
        {
            if(move > 0 && !facingRight || move < 0 && facingRight)
            {
                facingRight = !facingRight;
                spriteRenderer.flipX = !facingRight;
            }
        }
    }
}
