using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eyes : MonoBehaviour {

    PlatformerCharacter2D avatar;

	// Use this for initialization
	void Start () {
		avatar = FindObjectOfType<PlatformerCharacter2D>();
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 diff = avatar.transform.position - transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }
}
