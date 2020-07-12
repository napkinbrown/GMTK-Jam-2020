using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageController : MonoBehaviour
{
    public int damageDealtPerTic;
    public float secondPerTic;

    private bool damageOnCooldown;
    private bool hurtOnCooldown;
    EntityHealthController hc;

    void Start() {
        damageOnCooldown = false;
        hurtOnCooldown = false;
    }
    
    void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.tag == "Player") {
            if (!damageOnCooldown) {
                hc = other.gameObject.GetComponent<EntityHealthController>();
                hc.DamageCharacter(damageDealtPerTic);
                StartCoroutine(DamageCooldown());
                if (!hurtOnCooldown) {
                    hc.hurtSoundEffect.Play();
                    StartCoroutine(HurtSoundCooldown());
                }
            }
            
        }
    }

    IEnumerator DamageCooldown() {
        damageOnCooldown = true;
        yield return new WaitForSeconds(secondPerTic);
        damageOnCooldown = false;
    }

    IEnumerator HurtSoundCooldown() {
        hurtOnCooldown = true;
        yield return new WaitForSeconds(1);
        hurtOnCooldown = false;
    }
}
