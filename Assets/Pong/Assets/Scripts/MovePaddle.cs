using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePaddle : MonoBehaviour {

    public KeyCode UpKey = KeyCode.UpArrow;
    public KeyCode DownKey = KeyCode.DownArrow;
    public float speed = 5.0f;
    private Rigidbody rb;
    public bool AIEnable = false;
    private GameObject ball;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        ball = GameObject.FindGameObjectWithTag("Ball");
	}
	
	// Update is called once per frame
	void Update () {
        rb.velocity = Vector3.zero;
        if (!AIEnable) {
            if (Input.GetKey(UpKey)) {
                moveUp();
            }
            if (Input.GetKey(DownKey)) {
                moveDown();
            }
        } else {
            if(rb.position.y > ball.transform.position.y) {
                moveDown();
            } else {
                moveUp();
            }
        }

    }

    public void enableAI() {
        AIEnable = true;
    }

    public void disableAI() {
        AIEnable = false;
    }

    public void toggleAI() {
        AIEnable = !AIEnable;
    }

    void moveUp() {
        rb.velocity = new Vector3(0.0f, speed / 10 / Time.deltaTime, 0.0f);
    }
    void moveDown() {
        if (rb.velocity == Vector3.zero) {
            rb.velocity = new Vector3(0.0f, -speed / 10 / Time.deltaTime, 0.0f);
        } else {
            rb.velocity = Vector3.zero;
        }
    }
}
