using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float jumpStrength;
    public float rateOfAcceleration;
    public float topHorizontalSpeed;
    public bool facingLeft;
    
    protected bool hasGroundJump;
    protected bool hasDoubleJump;
    protected bool inAir;
    public Rigidbody2D rb;
    
    void Start() {
        rb = this.GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Ground") {
            hasGroundJump = true;
            hasDoubleJump = true;
            inAir = false;
        }
    }

    void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.tag == "Ground") {
            hasGroundJump = false;
            inAir = true;
        }
    }

    protected virtual void ApplySidewaysMovement() {
        MoveSideways(rateOfAcceleration);
        CapHorizontalSpeed();
    }

    // Move from input
    protected void MoveSideways(float accelerationDelta) {
        float displacement = Input.GetAxisRaw("Horizontal") * accelerationDelta;
        rb.AddForce(Vector2.right * displacement);
    }

    // Move toward a target
    protected void MoveSideways(float accelerationDelta, GameObject target) {

        float dist = GetDistance(target); 

        Debug.Log("Moving " + dist + " to " + target.gameObject.tag);

        if (dist > 3)
            rb.AddForce(new Vector2(dist, 0) * accelerationDelta);
    }

    protected void CapHorizontalSpeed() {
        if (Mathf.Abs(rb.velocity.x) > topHorizontalSpeed * Time.deltaTime) {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * topHorizontalSpeed * Time.deltaTime, rb.velocity.y);
        }
    }

    protected virtual void CheckForJump() {
        if(hasGroundJump) {
            AddJumpForce();
            hasGroundJump = false;
        } else if(hasDoubleJump) {
            AddJumpForce();
            hasDoubleJump = false;
        }
    }

    protected virtual GameObject CheckForPlatform() {
        // Will need to check direction facing at an angle
        Vector2 angle = facingLeft ? 
            this.transform.up - this.transform.right : this.transform.up + transform.right;

        // Cast a ray up and into the facing direction
        RaycastHit2D ray = Physics2D.Raycast(transform.position, angle);

        return ray.collider ? ray.collider.gameObject : null;
    }

    protected void AddJumpForce() {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * jumpStrength, ForceMode2D.Impulse); 
    }

    protected float GetDistance(GameObject thatObj) {
        return Mathf.Abs(thatObj.transform.position.x - this.transform.position.x);
    }
}
