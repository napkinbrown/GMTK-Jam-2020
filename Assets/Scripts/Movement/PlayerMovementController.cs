﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MovementController
{
    void FixedUpdate() {
        ApplySidewaysMovement();
    }

    void Update() {
        CheckForJump(); // Unity is a bastard and doesn't handle jump inputs well in FixedInput
    }
    
    protected override void ApplySidewaysMovement() {
        MoveSideways(rateOfAcceleration);
        base.ApplySidewaysMovement();
    }

    protected override void CheckForJump() {
        if (Input.GetButtonDown("Jump")) {
            base.CheckForJump();
        }
    }

}
