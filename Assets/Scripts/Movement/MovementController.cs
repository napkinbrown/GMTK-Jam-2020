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
        CapHorizontalSpeed();
    }

    // Move from input
    protected void MoveSideways(float accelerationDelta) {
        float displacement = Input.GetAxisRaw("Horizontal") * accelerationDelta;
        rb.AddForce(Vector2.right * displacement);
    }

    // Move a specified distance
    protected void MoveSideways(float accelerationDelta, Vector2 dist) {
        rb.AddForce(dist * accelerationDelta);
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

    protected void AddJumpForce() {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * jumpStrength, ForceMode2D.Impulse); 
    }

    protected float GetDistance(GameObject target) {
        return target.transform.position.x - this.transform.position.x;
    }
    
    protected Vector2 GetVectorDistance(GameObject target) {
        Vector2 thisPos = new Vector2(this.transform.position.x, this.transform.position.y);
        Vector2 thatPos = new Vector2(target.transform.position.x, target.transform.position.y);
        return thatPos - thisPos;
    }
}
