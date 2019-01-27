using System;
using UnityEngine;

namespace UnityStandardAssets
{
    public class PlatformerCharacter2D : MonoBehaviour
    {
        // Serialized Fields
        [SerializeField] private LayerMask whatIsGround;    // a mask determining what is ground to the character

        // Non Serialized Fields
        private float moveSpeed = 0.4f;                // amount of speed added to horizontal velocity when the player moves
        private float maxVelocity = 10f;               // maximum amount of horizontal velocity the player can attain
        private float horizontalFriction = 0.25f;      // amount of horizontal friction applied to player's velocity
        private float verticalFriction = 0.5f;         // amount of vertical friction applied to player's velocity
        private float jumpSpeed = 12f;                 // amount of speed added to vertical velocity when the player jumps
        private float doubleJumpSpeed = 12f;           // amount of speed added to vertical velocity upon double jump
        private float airSpeed = 0.35f;                // amount of speed added to horizontal velocity when in the air
        private float velocityScalar = 0.01f;          // scalar value used to diminish the final velocity

        // #1 /1/15/0.75/0.5/20/0.3/0.01
        // #2 /1/20/0.5/0.5/20/0.3/0.01
        // #3 /1.1/25/0.7/0.75/20/0.35/0.01
        // #4

        private Animator anim;                          // reference to the player's animator component
        private Transform model;                        // reference to player's model child
        private bool doubleJump;                        // indicates whether or not a double jump has occured
        private Vector2 playerVelocity;                 // the player's velocity
        private Transform groundCheck;                  // position marking where to check if the player is grounded
        const float groundedRadius = 0.1f;              // radius of the overlap circle to determine if grounded
        private bool grounded;                          // whether or not the player is grounded
        private bool facingRight = true;                // for determining which way the player is currently facing


        public AudioSource jump_sound;


        private void Awake()
        {
            // Setting up references.
            groundCheck = transform.Find("GroundCheck");
            model = transform.Find("Model");
            anim = model.GetComponent<Animator>();
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

            // let animator know player is grounded
            anim.SetBool("Grounded", grounded);
        }

        public void Move(float move, bool jump)
        {
            // let animator know at what speed the player is moving
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
            if(grounded)
            {
                doubleJump = false;
            }

            // only apply jump if player is grouned and jump has been pressed
            if (grounded && jump)
            {
                grounded = false;
                anim.SetBool("Grounded", false);
                playerVelocity.y = jumpSpeed;
                jump_sound = GetComponent<AudioSource>();
                jump_sound.Play(0);                
            }
            else if(!grounded && jump && !doubleJump)
            {
                doubleJump = true;
                playerVelocity.y = doubleJumpSpeed;
                jump_sound = GetComponent<AudioSource>();
                jump_sound.Play(0);
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
                // reflection in the z plane as 3d player models face forward in the z plane
                model.localScale = new Vector3(model.localScale.x, model.localScale.y, -model.localScale.z);
            }
        }

        public void SetVerticalVelocity(float velocity)
        {
            playerVelocity.y = velocity;
        }

        public void SetHorizontalVelocity(float velocity)
        {
            playerVelocity.x = velocity;
        }
    }
}
