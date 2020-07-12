using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprinkleScript : MonoBehaviour
{
    public float lifetime;     // how long it lives before destroying itself
    public int damage;       // how much damage this projectile causes

    private SpriteRenderer spriteRenderer;
    public Sprite sprinkle1;
    public Sprite sprinkle2;
    public Sprite sprinkle3;
    public Sprite sprinkle4;
    
    void Start()
    {
        Rigidbody2D rb = gameObject.GetComponentInChildren<Rigidbody2D>();
        PickStartColor();
    }


    void OnTriggerEnter2D(Collider2D other) {
        EntityHealthController healthController = other.gameObject.GetComponent<EntityHealthController>();
        if (healthController && healthController.GetType() != typeof(PlayerHealthController)) {
            healthController.DamageCharacter(damage);
        }

        if (other.gameObject.tag != "Player") {
            Destroy(this.gameObject);
        }
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


