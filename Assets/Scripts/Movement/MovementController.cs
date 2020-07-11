using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float jumpStrength;
    public float rateOfAcceleration;
    public float topHorizontalSpeed;
    
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

    protected void ApplySidewaysMovement() {
        MoveSideways(rateOfAcceleration);
        CapHorizontalSpeed();
    }

    protected void MoveSideways(float accelerationDelta) {
        float displacement = Input.GetAxisRaw("Horizontal") * accelerationDelta;
        rb.AddForce(Vector2.right * displacement);
    }

    protected void CapHorizontalSpeed() {
        if (Mathf.Abs(rb.velocity.x) > topHorizontalSpeed * Time.deltaTime) {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * topHorizontalSpeed * Time.deltaTime, rb.velocity.y);
        }
    }

    protected void CheckForJump() {
        if (Input.GetButtonDown("Jump")) {
            if(hasGroundJump) {
                AddJumpForce();
                hasGroundJump = false;
            } else if(hasDoubleJump) {
                AddJumpForce();
                hasDoubleJump = false;
            }
        }
    }

    protected void AddJumpForce() {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * jumpStrength, ForceMode2D.Impulse); 
    }
}
