using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
Use this to change the position of the Body based on the state of the game
*/
public class Movement : MonoBehaviour {

    public Vector3 origin;
    public Vector3 target;
    public Vector3 mirrorPosition;

    public TestController controller;

	// Use this for initialization
	void Start () {
        origin = transform.position;
        target = origin;

        controller = GameObject.Find("Text").GetComponent<TestController>();
        mirrorPosition = GameObject.Find("Mirror").transform.position;

	}
	
	// Update is called once per frame
	void Update () {
        if (controller.SameStateAs("mirror")) {
            target = mirrorPosition;
        }
        transform.position = target;
	}
}
