using System;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    public class PlatformerCharacter2D : MonoBehaviour
    {
        [SerializeField]
        public float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
        [SerializeField]
        private float m_JumpForce = 400f;                  // Amount of force added when the player jumps.
        [Range(0, 1)]
        [SerializeField]
        private float m_CrouchSpeed = .36f;  // Amount of maxSpeed applied to crouching movement. 1 = 100%
        [SerializeField]
        private bool m_AirControl = false;                 // Whether or not a player can steer while jumping;
        [SerializeField]
        private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character
        [SerializeField]
        private float m_PullbackForce = 0f; 

        private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
        const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
        private bool m_Grounded;            // Whether or not the player is grounded.
        private Transform m_CeilingCheck;   // A position marking where to check for ceilings
        const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
        private Animator m_Anim;            // Reference to the player's animator component.
        private Rigidbody2D m_Rigidbody2D;
        private bool m_FacingRight = true;  // For determining which way the player is currently facing.


        public bool isHoldingCable = false; // Is the player holding a cable?
        public bool isNearCable = false;    // Is the player near a cable?
        public bool isNearLight = false;    // Is the player near a light?
        public bool stretched = false;      // Is the player stretching the cord?
        public GameObject cableObject;      // The cable the player is currently interacting with.
        public GameObject lightObject;      // The light the player is currently interacting with.
        public Animator animator;



        private void Awake()
        {
            // Setting up references.
            m_GroundCheck = transform.Find("GroundCheck");
            m_CeilingCheck = transform.Find("CeilingCheck");
            m_Anim = GetComponent<Animator>();
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
        }


        private void FixedUpdate()
        {
            m_Grounded = false;

            // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
            // This can be done using layers instead but Sample Assets will not overwrite your project settings.
            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                    m_Grounded = true;
            }
            m_Anim.SetBool("Ground", m_Grounded);

            // Set the vertical animation
            m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);

            
            //Object Interactions
            //Grab Cable
            if (Input.GetButtonDown("Select") && isNearCable && !isHoldingCable && cableObject.GetComponent<Wire>().isPlugged == false)
                GrabCable();

            //Release Cable
            if (Input.GetButtonDown("Release") && isHoldingCable)
                ReleaseCable();

            //Attach Cable to Light
            if(Input.GetButtonDown("Select") && isHoldingCable && isNearLight)
                AttachCable();
        }


        public void Move(float move, bool crouch, bool jump)
        {
            // If crouching, check to see if the character can stand up
            if (!crouch && m_Anim.GetBool("Crouch"))
            {
                // If the character has a ceiling preventing them from standing up, keep them crouching
                if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
                {
                    crouch = true;
                }
            }

            // Set whether or not the character is crouching in the animator
            m_Anim.SetBool("Crouch", crouch);

            //only control the player if grounded or airControl is turned on
            if (m_Grounded || m_AirControl)
            {
                // Reduce the speed if crouching by the crouchSpeed multiplier
                move = (crouch ? move * m_CrouchSpeed : move);

                // The Speed animator parameter is set to the absolute value of the horizontal input.
                m_Anim.SetFloat("Speed", Mathf.Abs(move));

                // Move the character
                m_Rigidbody2D.velocity = new Vector2(move * m_MaxSpeed, m_Rigidbody2D.velocity.y);

                // If the input is moving the player right and the player is facing left...
                if (move > 0 && !m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
                // Otherwise if the input is moving the player left and the player is facing right...
                else if (move < 0 && m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
            }
            // If the player should jump...
            if (m_Grounded && jump && m_Anim.GetBool("Ground"))
            {
                // Add a vertical force to the player.
                m_Grounded = false;
                m_Anim.SetBool("Ground", false);
                m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
            }

            //If the player is stretching the cable...
            if (stretched)
            {
                // ...Pull back towards cable !!!modify PullbackForce to represent vector between Player and last anchor point
                var pullBackForce = m_PullbackForce;
                if (cableObject.GetComponent<Wire>().CableOriginPos.x < transform.position.x)
                {
                    pullBackForce = -pullBackForce;
                }
                m_Rigidbody2D.AddForce(new Vector2(pullBackForce, 0f));
            }
        }


        private void Flip()
        {
            // Switch the way the player is labelled as facing.
            m_FacingRight = !m_FacingRight;

            // Multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }

        public void ReleaseCable()
        {
            // Release the cable
            Debug.Log("Releasing Cable");
            // !!!Release Animation
            animator.SetTrigger("Startled");
            // !!!Cable state
            cableObject.GetComponent<Wire>().IsHeld = false;
            isHoldingCable = false;
            cableObject.GetComponent<Renderer>().material.color = Color.red;
            cableObject.GetComponent<Wire>().DestroyWireParts();
        }

        public void GrabCable()
        {
            //Grab the cable
            Debug.Log("Grabbing Cable");
            cableObject.GetComponent<Wire>().IsHeld = true;
            cableObject.GetComponent<Renderer>().material.color = Color.green;
            isHoldingCable = true;
        }

        public void AttachCable()
        {
            Debug.Log("Light Activated");
            lightObject.GetComponent<LightBulbe>().plugged = true;
            cableObject.GetComponent<Wire>().PlugCableToLight(lightObject.GetComponent<LightBulbe>());
            cableObject.GetComponent<Wire>().IsHeld = false;
            cableObject.GetComponent<Wire>().isPlugged = true;
            cableObject.GetComponent<Wire>().targetPosition = lightObject.transform.position;
            cableObject.GetComponent<Renderer>().material.color = Color.blue;
            isHoldingCable = false;
            m_MaxSpeed = 10f;  // Reset to standard
            stretched = false; // Reset to standard
        }

        public void OnTriggerEnter2D(Collider2D col)
        {
            //Generator
            if (col.gameObject.tag == "Generator")
            {
                //Debug.Log("Generator");
                isNearCable = true;
                if (!isHoldingCable)
                {
                    cableObject = col.gameObject;
                }
            }

            //Light
            else if (col.gameObject.tag == "Light")
            {
                isNearLight = true;
                lightObject = col.gameObject;
            }

            //Danger
            else if (col.gameObject.tag == "Danger")
            {
                //!!! Damage animation
                ReleaseCable();
            }
        }

        public void OnTriggerExit2D(Collider2D col)
        {
            //Generator
            if (col.gameObject.tag == "Generator")
            {
                isNearCable = false;
            }

            //Light
            else if (col.gameObject.tag == "Light")
            {
                isNearLight = false;
            }
        }
    }
}

