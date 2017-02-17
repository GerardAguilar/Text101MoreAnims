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
    public Vector3 sheetPosition;
    public Vector3 doorPosition;
    public Vector3 corridorPosition;
    public TestController controller;
    public bool isLerping;

    private float totalTimeToLerp;
    public float startTime;
    public string currentState;

	// Use this for initialization
	void Start () {
        origin = transform.position;
        target = origin;

        controller = GameObject.Find("Text").GetComponent<TestController>();
        mirrorPosition = GameObject.Find("Mirror").transform.position;
        sheetPosition = GameObject.Find("Bed").transform.position;
        doorPosition = GameObject.Find("Door").transform.position;
        corridorPosition = GameObject.Find("Corridor").transform.position;

        totalTimeToLerp = 20f;
        startTime = 0f;

        isLerping = false;
        currentState = controller.CurrentState();        
	}

    // Update is called once per frame
    
    void Update () {
        #region Move to sections on screen
        if (controller.SameStateAs("mirror")| controller.SameStateAs("cell_mirror"))
        {
            target = new Vector3(mirrorPosition.x, -3.55f, 0f);
        }
        else if (controller.SameStateAs("sheets_0") | controller.SameStateAs("sheets_1") | controller.SameStateAs("cell")) {
            target = new Vector3(sheetPosition.x, -3.55f, 0f);
        }
        else if (controller.SameStateAs("lock_0") | controller.SameStateAs("lock_1"))
        {
            target = new Vector3(doorPosition.x, -3.55f, 0f);
        }
        else if (controller.SameStateAs("corridor") | controller.SameStateAs("yard") | controller.SameStateAs("hole"))
        {
            target = new Vector3(corridorPosition.x, -3.55f, 0f);
        }
        #endregion
        #region Triggers Lerp Movement
        if (!controller.SameStateAs(currentState)) {//acts as a trigger
            currentState = controller.CurrentState();
            isLerping = true;
            startTime = Time.time;                
        }
        #endregion

    }
    

    void FixedUpdate() {        
        if (isLerping) {
            //startTime is static during Lerping. This gives us a baseline for percentage complete
            float timeSinceStarted = Time.time - startTime;
            float percentageComplete = timeSinceStarted / totalTimeToLerp;
            transform.position = Vector3.Lerp(transform.position, target, percentageComplete);

            if (percentageComplete >= 1.0f)
            {
                isLerping = false;
            }
        }
    }
}
