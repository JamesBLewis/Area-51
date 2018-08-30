using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_controller : MonoBehaviour {

    public int speed = 10;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
    void LateUpdate () {
        if (Input.GetKey("w"))
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime, Camera.main.transform);
        }

        if (Input.GetKey("a"))
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime, Camera.main.transform);
        }

        if (Input.GetKey("d"))
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime, Camera.main.transform);
        }

        if (Input.GetKey("s"))
        {
            transform.Translate(Vector2.down * speed * Time.deltaTime, Camera.main.transform);
        }
	}
}
