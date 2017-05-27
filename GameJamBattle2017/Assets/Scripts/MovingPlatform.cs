﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

    public bool horizontalMove = false;
    public float horizontalSpeed = 1.0F;
    public int horizontalLength = 1;

    public bool verticalMove = false;
    public float verticalSpeed = 1.0F;
    public int verticalLength = 1;

    public bool isBird = false;
    public Animator animator;
    private bool m_FacingRight = true;  // For determining which way the player is currently facing.


    // Use this for initialization
    void Start ()
    {
        if(isBird)
        {
            animator = GetComponent<Animator>();
        }
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (horizontalMove && verticalMove)
        {
            transform.position = new Vector3(Mathf.PingPong(horizontalSpeed*Time.time, horizontalLength), Mathf.PingPong(verticalSpeed*Time.time, verticalLength), transform.position.z);
        }
        else if (horizontalMove)
        {
            transform.position = new Vector3(Mathf.PingPong(horizontalSpeed*Time.time, horizontalLength), transform.position.y, transform.position.z);
            Debug.Log("Speed*Time " + horizontalSpeed * Time.time);
            Debug.Log("PingPong " + Mathf.PingPong(horizontalSpeed * Time.time, horizontalLength));
            if (Mathf.PingPong(horizontalSpeed * Time.time, horizontalLength) >= horizontalLength || Mathf.PingPong(horizontalSpeed * Time.time, horizontalLength) <= 0)
            {
                Flip();
                Debug.Log("Flip");
            }
        }
        else if (verticalMove)
        {
            transform.position = new Vector3(transform.position.x, Mathf.PingPong(verticalSpeed*Time.time, verticalLength), transform.position.z);
        }
        if (isBird)
        {
            animator.SetBool("Flying", true);
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
}
