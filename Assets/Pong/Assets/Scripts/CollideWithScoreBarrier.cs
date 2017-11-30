using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideWithScoreBarrier : MonoBehaviour {
    public GameController gameController;
    public Object ball;

    public enum Side { right, left };
    public Side side;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.tag == "Ball") {
            gameController.scorePlayer((int)side);
        }
    }
}
