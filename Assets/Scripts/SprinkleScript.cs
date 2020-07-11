using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprinkleScript : MonoBehaviour
{
    public int speed = 50;          // The speed our bullet travels
    public Vector3 targetVector;    // the direction it travels
    public float lifetime = 10f;     // how long it lives before destroying itself
    public float damage = 10;       // how much damage this projectile causes

    private SpriteRenderer spriteRenderer;
    public Sprite sprinkle1;
    public Sprite sprinkle2;
    public Sprite sprinkle3;
    public Sprite sprinkle4;

 
    
    // Start is called before the first frame update
    void Start()
    {

        PickStartColor();
 
        
         // find our RigidBody
        Rigidbody2D rb = gameObject.GetComponentInChildren<Rigidbody2D>();
        // add force 
        rb.AddForce(targetVector.normalized * speed);
    }

    void PickStartColor()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        switch (Random.Range(1, 5))
        {
            case 1:
                spriteRenderer.sprite = sprinkle2;
                break;
            case 2:
                spriteRenderer.sprite = sprinkle3;
                break;
            case 3:
                spriteRenderer.sprite = sprinkle4;
                break;
            case 4:
                //Default is already set in the prefab
                spriteRenderer.sprite = sprinkle1;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // decrease our life timer
        lifetime -= Time.deltaTime;
        if (lifetime <= 0f) {
            // we have ran out of life
            Destroy(gameObject);    // kill me
        }
    }
}


