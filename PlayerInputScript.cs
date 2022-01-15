using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputScript : MonoBehaviour
{
    [SerializeField]
    internal PlayerMainScript mainScript;
    internal PlayerMoveScript moveScript {
        get {
           return mainScript.moveScript;
        }
    }
    
    [SerializeField]
    internal string[] pad;

    private void Update()
    {
        checkMovement();
    }

    private void checkMovement () {
        listenAxisInput(pad[1], pad[2], pad[0].ToCharArray()[0]);
        listenAxisInput(pad[4], pad[5], pad[3].ToCharArray()[0]);
    }

    private void listenAxisInput (string pad1, string pad2, char axis) {
        // Listen for input
        if (GetKeyDown(pad1)) setMoveDirection(1, axis);
        if (GetKeyDown(pad2)) setMoveDirection(-1, axis);
        if (!(GetKey(pad1) || GetKey(pad2))) setMoveDirection(0, axis);
        // Check if key is axis is still used after releasing one side
        if (GetKeyUp(pad1) || GetKeyUp(pad2)) {
            if (GetKey(pad1)) setMoveDirection(1, axis);
            if (GetKey(pad2)) setMoveDirection(-1, axis);
        }
    }
    
    private void setMoveDirection (int value, char axis) {
        Vector2 direction = moveScript.curDir;
        if (axis == 'x') direction.x = value;
        if (axis == 'y') direction.y = value;
        moveScript.curDir = direction;
    }

    private bool GetKeyUp (string key) {
        return Input.GetKeyUp(key);
    }

    private bool GetKey (string key) {
        return Input.GetKey(key);
    }

    private bool GetKeyDown (string key) {
        return Input.GetKeyDown(key);
    }
}
