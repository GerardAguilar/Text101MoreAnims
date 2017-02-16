using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestController : MonoBehaviour {

    public Text text;

    public enum States { cell, mirror, sheets_0, lock_0, cell_mirror, sheets_1, lock_1, corridor_0, yard, hole };
    public States myState; // stores a state from the above


	// Use this for initialization of the current state
	void Start () {
        myState = States.cell;
	}

	// Update is used to sense the state and then call the corresponding method (repeatedly though)
	void Update () {
        print(myState);
        //initial
        if      (myState == States.cell) { cell(); }
        //States enum are just triggers, the actual methods are called here in the update function
        else if (myState == States.sheets_0) { sheets_0(); }
        else if (myState == States.sheets_1) { sheets_1(); }
        else if (myState == States.lock_0) { lock_0(); }
        else if (myState == States.lock_1) { lock_1(); }
        else if (myState == States.mirror) { mirror(); }
        else if (myState == States.cell_mirror) { cell_mirror(); }
        else if (myState == States.corridor_0) { corridor_0(); }
        else if (myState == States.yard) { yard(); }
        else if (myState == States.hole) { hole(); }
	}

    #region State Handler methods
    void cell() {//hold text and handle key presses
        text.text = "You are in a prison cell, and you want to escape. There are " +
                "some dirty sheets on the bed, a mirror on the wall, and the door " +
                "is locked from the outside.\n\n" +
                "Press S to view Sheets, M to view Mirror, and L to view Lock";

        //what are all the keys you can press while you're in the cell? S, M, L
        //changes the state that the Update() prints out regularly
        if      (Input.GetKeyDown(KeyCode.S)) { myState = States.sheets_0; }
        else if (Input.GetKeyDown(KeyCode.M)) { myState = States.mirror; }
        else if (Input.GetKeyDown(KeyCode.L)) { myState = States.lock_0; }
    }

    void sheets_0() {
        text.text = "You can't believe you sleep in these things. Surely it's " +
            "time somebady changed them. The pleasures of prison life " +
            "I guess!\n\n" +
            "Press R to Return to roaming your cell";

        if      (Input.GetKeyDown(KeyCode.R)) {myState = States.cell;}
    }

    void sheets_1() {
        text.text = "Holding a mirror in your hand doesn't make the sheets look " +
            "any better.\n\n" +
            "Press R to Return to roaming your cell";
        if      (Input.GetKeyDown(KeyCode.R)) { myState = States.cell_mirror; }

    }

    void lock_0() {
        text.text = "This is one of those button locks. You have no idea what the " +
            "combination is. You wish you could somehow see where the dirty " +
            "fingerprints were, maybe that would help.\n\n" +
            "Press R to Return to roaming your cell";
        if      (Input.GetKeyDown(KeyCode.R)){myState = States.cell;}

    }
    void lock_1() {
        text.text = "You carefully put the mirror through the bars, and turn it round " +
            "so you can see the lock. You can just make out the fingerprints around " +
            "the buttons. You press the dirty buttons, and hear a click.\n\n" +
            "Press O to Open, or R to Return to your cell";
        if      (Input.GetKeyDown(KeyCode.O)) { myState = States.corridor_0; }
        else if (Input.GetKeyDown(KeyCode.R)) { myState = States.cell_mirror; }
    }
    void mirror() {
        text.text = "The dirty old mirror on the wall seems loose.\n\n" +
            "Press T to Take the mirror, or R to Return to cell";
        if      (Input.GetKeyDown(KeyCode.T)) { myState = States.cell_mirror; }
        else if (Input.GetKeyDown(KeyCode.R)) { myState = States.cell; }
    }
    void cell_mirror() {
        text.text = "You are still in your cell, and you STILL want to escape! There are " +
            "some dirty sheets on the bed, a mark where the mirror was, " +
            "and that pesky door is still there, and firmly locked!\n\n" +
            "Press S to view Sheets, or L to view Lock";
        if      (Input.GetKeyDown(KeyCode.S)) { myState = States.sheets_1; }
        else if (Input.GetKeyDown(KeyCode.L)) { myState = States.lock_1; }

    }
    void corridor_0() {
        text.text = "You are in the corridor! The door to the yard is right there!\n\n" +
            "Press Y to go to the yard.";
        if      (Input.GetKeyDown(KeyCode.Y)) { myState = States.yard; }
    }
    void yard() {
        text.text = "You are now in the yard. There's a big hole on the wall.\n\n " +
            "Press H to go through the Hole, or C to go back to the Corridor.";
        if      (Input.GetKeyDown(KeyCode.H)) { myState = States.hole; }
        else if (Input.GetKeyDown(KeyCode.C)) { myState = States.corridor_0; }
    }
    void hole() {
        text.text = "You're free!\n\n" +
            "Press P to Play again.";
        if      (Input.GetKeyDown(KeyCode.P)) { myState = States.cell; }
    }

    #endregion

    /*
    I need to move the body to the different game object transforms
    I may just make myState public and have a different script access it. Based on certain scripts, that's when we change the lerping position.
    */

    public bool SameStateAs(string comp) {
        if (comp.Equals(myState.ToString())) {
            return true;
        }
        return false;
    }

}
