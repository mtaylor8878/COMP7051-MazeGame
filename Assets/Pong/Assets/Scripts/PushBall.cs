using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBall : MonoBehaviour {

    private Rigidbody ball;
    public float xThrust = 1.0f;
    public float yThrust = 1.0f;

    // Use this for initialization
    void Start () {
        ball = GetComponent<Rigidbody>();
        ball.AddForce(xThrust, yThrust, 0.0f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
