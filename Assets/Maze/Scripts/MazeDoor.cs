using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MazeDoor : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider collide) {
        Debug.Log(collide.tag);
        if(collide.gameObject.tag == "Player") {
            SceneManager.LoadScene(3);
        }
    }
}
