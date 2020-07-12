using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroccoliMovementController : EnemyMovementController
{
    
    // MovementController's jumpStrength will be attack speed
    // Accel will be hover speed
    public bool canAttack;
    public float attackRate;
    public float bounceAngle;
    public float minBounceAngle;
    public Vector2 hoverDirection;

    void Start() {
        hoverDirection = RandomVector(bounceAngle, minBounceAngle);
        StartCoroutine(DiveCooldown());
        MoveSideways(rateOfAcceleration, hoverDirection);
    }

    void OnTriggerStay2D(Collider2D target) {
        if (target.tag == "Boundary") {
            hoverDirection = RandomVector(bounceAngle, minBounceAngle);
            MoveSideways(rateOfAcceleration, hoverDirection);
        }
    }

    protected override void ApplySidewaysMovement() {
        if (canAttack) Attack();
    }

    private void Attack() {
        Debug.Log("Broccoli attacking");
        MoveSideways(jumpStrength, GetVectorDistance(player));
        canAttack = false;
        StartCoroutine(DiveCooldown());
    }

    IEnumerator DiveCooldown() {
         yield return new WaitForSeconds(attackRate);
         canAttack = true;
    }

    public Vector2 RandomVector(float angle, float angleMin){
        float random = Random.value * angle + angleMin;
        return new Vector2(Mathf.Cos(random), Mathf.Sin(random));
    }

}