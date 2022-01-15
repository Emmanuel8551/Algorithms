using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveScript : MonoBehaviour
{
    [SerializeField]
    internal PlayerMainScript mainScript;

    public Vector3 curDir;
    private Vector3 lastDir;
    public float maxSpeed, acceleration, brake;
    
    internal Vector2 velocity {
        get {
            return mainScript.rb.velocity;
        }
        set {
            mainScript.rb.velocity = value;
        }
    }

    private void Update()
    {
        manageMoveMouse();
    }

    private void manageMoveKeys () {
        if (mainScript.isAlive) {
            if (curDir.magnitude != 0) manageMoveForward();
            else if (velocity.magnitude > 0) manageBraking();
        }
    }

    internal void stop() {
        velocity = Vector2.zero;
    }

    private void manageMoveForward () {
        if (lastDir != curDir) changeDirection(curDir);
        if (velocity.magnitude <= maxSpeed) {
            accelerate(curDir, acceleration);
            if (velocity.magnitude > maxSpeed)
                velocity = velocity.normalized * maxSpeed;
        }
        lastDir = curDir;
    }

    private void manageBraking () {
        Vector2 lastVel = velocity;
        accelerate(-velocity, brake);
        if (Vector2.Dot(lastVel, velocity) < 0)  
            velocity = Vector2.zero;
    }

    private void accelerate (Vector2 direction, float acceleration) {
        velocity += direction.normalized * acceleration * Time.deltaTime;
    }

    private void changeDirection (Vector2 direction) {
        velocity = direction.normalized * velocity.magnitude;
    }
}
