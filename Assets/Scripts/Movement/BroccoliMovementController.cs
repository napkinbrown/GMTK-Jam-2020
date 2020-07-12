using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroccoliMovementController : EnemyMovementController
{
    
    // MovementController's jumpStrength will be attack speed
    // Accel will be hover speed
    private bool canAttack;
    public float attackRate;
    private bool canBounce;
    public float bounceRate;
    public float bounceAngle;
    public float minBounceAngle;
    private Vector2 hoverDirection;

    void Start() {
        hoverDirection = GetRandomVector(bounceAngle, minBounceAngle);
        StartCoroutine(DiveCooldown());
        MoveSideways(rateOfAcceleration, hoverDirection);
        canAttack = true;
        canBounce = true;
    }

    void OnTriggerEnter2D(Collider2D target) {
        Bounce(target);
        canBounce = false;
    }

    void OnTriggerStay2D(Collider2D target) {
        Bounce(target);
    }

    private void Bounce(Collider2D target) {
        if (target.tag == "Boundary" && canBounce) {
            hoverDirection = GetRandomVector(bounceAngle, minBounceAngle);
            MoveSideways(rateOfAcceleration, hoverDirection);
            StartCoroutine(BounceCooldown());
        }
    }

    protected override void ApplySidewaysMovement() {
        if (canAttack) Attack();
    }

    private void Attack() {
        MoveSideways(jumpStrength, GetVectorDistance(player));
        canAttack = false;
        StartCoroutine(DiveCooldown());
    }

    IEnumerator DiveCooldown() {
         yield return new WaitForSeconds(attackRate);
         canAttack = true;
    }

    // To chill out broccoli's wacky bouncing
    IEnumerator BounceCooldown() {
         yield return new WaitForSeconds(bounceRate);
         canBounce = true;
    }

}