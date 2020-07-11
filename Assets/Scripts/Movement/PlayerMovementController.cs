using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MovementController
{

    private bool isFreezing = false;

    void OnEnable() {
        EventManager.StartListening(EventNames.FREEZE_START, FreezeStart);
        EventManager.StartListening(EventNames.FREEZE_STOP, FreezeStop);
    }

    void OnDisable() {
        EventManager.StopListening(EventNames.FREEZE_START, FreezeStart);
        EventManager.StopListening(EventNames.FREEZE_STOP, FreezeStop);
    }
    
    void FixedUpdate() {
        ApplySidewaysMovement();
    }

    void Update() {
        CheckForJump(); // Unity is a bastard and doesn't handle jump inputs well in FixedInput
    }
    
    protected override void ApplySidewaysMovement() {
        if(!isFreezing)
        {
            MoveSideways(rateOfAcceleration);
        }
        base.ApplySidewaysMovement();
    }

    protected override void CheckForJump() {
        if(!isFreezing){
            if (Input.GetButtonDown("Jump")) {
                base.CheckForJump();
            }
        }
    }

    void FreezeStart() 
    {
        isFreezing = true;
    }

    void FreezeStop() 
    {
        isFreezing = false;
    }

}
