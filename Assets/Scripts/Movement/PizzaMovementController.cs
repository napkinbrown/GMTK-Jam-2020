using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaMovementController : MovementController
{

    public float lookDist;

    void FixedUpdate() {
        ApplySidewaysMovement();
    }

    void Update() {
        CheckForJump(); // Unity is a bastard and doesn't handle jump inputs well in FixedInput
    }

    protected override void MoveSideways(float accelerationDelta) {

    }

    protected override void CheckForJump() {
        
        GameObject obj = CheckForPlatform();

        // If object detected at angle + dist and not straight up, jump
        // Also need to check player position is higher/lower
        if (obj && obj.tag == "Ground") {
            float dist = Mathf.Abs(obj.transform.position.x - this.transform.position.x);
            
            Debug.Log("Pizza sees " + obj.tag + " at distance " + dist);

            if (dist <= lookDist) {
                base.CheckForJump();
            }
        }
    }

}