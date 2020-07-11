using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    public Rigidbody2D rb;
    
    public float jumpStrength;
    public float rateOfAcceleration;
    public float topHorizontalSpeed;
    public float airAcceleration;
    public float airDrag;
    
    private bool hasGroundJump;
    private bool hasDoubleJump;
    private bool inAir;

    void Start() {
        rb = this.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {
        ApplySidewaysMovement();
    }

    void Update() {
        CheckForJump(); // Unity is a bastard and doesn't handle jump inputs well in FixedInput
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

    void ApplySidewaysMovement() {
        MoveSideways(rateOfAcceleration);
        CapHorizontalSpeed();
    }

    void MoveSideways(float accelerationDelta) {
        float displacement = Input.GetAxisRaw("Horizontal") * accelerationDelta;
        rb.AddForce(Vector2.right * displacement);
    }

    void CapHorizontalSpeed() {
        if (Mathf.Abs(rb.velocity.x) > topHorizontalSpeed * Time.deltaTime) {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * topHorizontalSpeed * Time.deltaTime, rb.velocity.y);
        }
    }

    void CheckForJump() {
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

    void AddJumpForce() {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * jumpStrength, ForceMode2D.Impulse); 
    }
}
