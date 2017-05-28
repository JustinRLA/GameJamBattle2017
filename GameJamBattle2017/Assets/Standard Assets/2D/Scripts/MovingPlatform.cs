using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

    private Vector2 startingPosition;
    public bool horizontalMove = false;
    public float horizontalSpeed = 1.0F;
    public int horizontalLength = 1;

    public bool verticalMove = false;
    public float verticalSpeed = 1.0F;
    public int verticalLength = 1;

    public bool useWaypoint = false;
    public float waypointSpeed = 1.0F;

    public bool isBird = false;
    public Animator animator;
    private bool m_FacingRight = true;  // For determining which way the player is currently facing.
    public Transform waypoint;
    private Transform startPos;
    private float lerpRate = 0.01f;
    private float lerpPercent = 0.0f;
    private bool waypointBack = false;

    // Use this for initialization
    void Start ()
    {
        if(isBird)
        {
            animator = GetComponent<Animator>();
        }
        startPos = transform;

        startingPosition = new Vector2(transform.position.x, transform.position.y);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (horizontalMove && verticalMove)
        {
            transform.position = new Vector3(startingPosition.x + Mathf.PingPong(horizontalSpeed*Time.time, horizontalLength), startingPosition.y + Mathf.PingPong(verticalSpeed*Time.time, verticalLength), transform.position.z);
            if (Mathf.PingPong(horizontalSpeed * Time.time, horizontalLength) >= horizontalLength || Mathf.PingPong(horizontalSpeed * Time.time, horizontalLength) <= 0)
            {
                Flip();
                //Debug.Log("Flip");
            }
        }
        else if (horizontalMove)
        {
            transform.position = new Vector3(startingPosition.x + Mathf.PingPong(horizontalSpeed*Time.time, horizontalLength), transform.position.y, transform.position.z);
            //Debug.Log("Speed*Time " + horizontalSpeed * Time.time);
            //Debug.Log("PingPong " + Mathf.PingPong(horizontalSpeed * Time.time, horizontalLength));
            if (Mathf.PingPong(horizontalSpeed * Time.time, horizontalLength) >= horizontalLength || Mathf.PingPong(horizontalSpeed * Time.time, horizontalLength) <= 0)
            {
                Flip();
                //Debug.Log("Flip");
            }
        }
        else if (verticalMove)
        {
            transform.position = new Vector3(transform.position.x, startingPosition.y + Mathf.PingPong(verticalSpeed*Time.time, verticalLength), transform.position.z);
        }


        else if (useWaypoint && waypoint != null)
        {
            if (waypointBack)
            {
                lerpPercent -= lerpRate;
                if(lerpPercent <= 0.0f)
                {
                    lerpPercent = 0.0f;
                    waypointBack = false;
                }
            }
            else
            {
                lerpPercent += lerpRate;
                if (lerpPercent >= 1.0f)
                {
                    lerpPercent = 1.0f;
                    waypointBack = true;
                }
            }


            transform.position = Vector3.Lerp(startPos.position, waypoint.position, lerpPercent);
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
