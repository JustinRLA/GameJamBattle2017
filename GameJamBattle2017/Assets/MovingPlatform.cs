using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

    public bool horizontalMove = false;
    public float horizontalSpeed = 1.0F;
    public int horizontalLength = 1;

    public bool verticalMove = false;
    public float verticalSpeed = 1.0F;
    public int verticalLength = 1;


    // Use this for initialization
    void Start ()
    {

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
        }
        else if (verticalMove)
        {
            transform.position = new Vector3(transform.position.x, Mathf.PingPong(verticalSpeed*Time.time, verticalLength), transform.position.z);
        }
    }
}
