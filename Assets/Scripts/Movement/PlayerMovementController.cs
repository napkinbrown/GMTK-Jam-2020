using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MovementController
{
    public PlayerHealthController healthController;
    public float phase2JumpStrength;
    public float phase2RateOfAcceleration;
    public float phase2TopHorizontalSpeed;
    public float phase3JumpStrength;
    public float phase3RateOfAcceleration;
    public float phase3TopHorizontalSpeed;

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

    protected override float GetTopHorizontalSpeed() {
        switch(healthController.GetHealthPhase())
        {
            case PlayerHealthController.HealthPhase.Solid:
               return topHorizontalSpeed;
         
            case PlayerHealthController.HealthPhase.Melty:
                return phase2TopHorizontalSpeed;
            
            case PlayerHealthController.HealthPhase.Melted:
                return phase3TopHorizontalSpeed;
        }
        return base.GetTopHorizontalSpeed();
    }

    protected override float GetJumpStrength() {
        switch(healthController.GetHealthPhase())
        {
            case PlayerHealthController.HealthPhase.Solid:
                return jumpStrength;
            case PlayerHealthController.HealthPhase.Melty:
                return phase2JumpStrength;
            case PlayerHealthController.HealthPhase.Melted:
                return phase3JumpStrength;
        }
        return base.GetJumpStrength();
    }

    protected override float GetRateOfAccelaration() {
        switch(healthController.GetHealthPhase())
        {
            case PlayerHealthController.HealthPhase.Solid:
                return rateOfAcceleration;
            case PlayerHealthController.HealthPhase.Melty:
                return phase2RateOfAcceleration;
            case PlayerHealthController.HealthPhase.Melted:
                return phase3RateOfAcceleration;
        }
        return base.GetRateOfAccelaration();
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
