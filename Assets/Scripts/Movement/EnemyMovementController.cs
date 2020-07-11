using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementController : MovementController
{
    public float enemyPlayerDist;
    public float lookDist;

    void FixedUpdate() {
        ApplySidewaysMovement();
    }

    protected override void ApplySidewaysMovement() {
        CheckForMove(GameObject.FindWithTag("Player"));
        base.ApplySidewaysMovement();
    }

    protected void CheckForMove(GameObject target) {
        
        Vector2 dist = GetVectorDistance(target); 
        
        if (Mathf.Abs(dist.x) > enemyPlayerDist)
            MoveSideways(rateOfAcceleration, dist);
    }
    
    protected virtual GameObject CheckForPlatform() {
        // Will need to check direction facing at an angle
        Vector2 angle = facingLeft ? 
            this.transform.up - this.transform.right : this.transform.up + transform.right;

        // Cast a ray up and into the facing direction
        RaycastHit2D ray = Physics2D.Raycast(transform.position, angle);

        return ray.collider ? ray.collider.gameObject : null;
    }

}