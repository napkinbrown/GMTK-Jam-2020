using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroccoliMovementController : EnemyMovementController
{
    
    // MovementController's jumpStrength will be attack speed
    // Accel will be hover speed
    public bool canAttack;
    public float bounceAngle;
    public float minBounceAngle;
    public float diveRate;
    public Vector2 hoverDirection;

    void OnStart() {
        hoverDirection = RandomVector(bounceAngle, minBounceAngle);
        StartCoroutine(DiveCooldown());
        MoveSideways(rateOfAcceleration, hoverDirection);
    }

    void OnTriggerEnter2D(Collider2D target) {
        if (target.tag == "Boundary")
            hoverDirection = RandomVector(bounceAngle, minBounceAngle);
    }

    protected override void ApplySidewaysMovement() {

        if (canAttack) {
            base.ApplySidewaysMovement();
            canAttack = false;
            StartCoroutine(DiveCooldown());
        }
    }

    IEnumerator DiveCooldown() {
         yield return new WaitForSeconds(diveRate);
         canAttack = true;
    }

    public Vector2 RandomVector(float angle, float angleMin){
        float random = Random.value * angle + angleMin;
        return new Vector2(Mathf.Cos(random), Mathf.Sin(random));
    }

}