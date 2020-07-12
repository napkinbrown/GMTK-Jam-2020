using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageController : MonoBehaviour
{
    public int damageDealtPerTic;
    public float secondPerTic;

    private bool damageOnCooldown;

    void Start() {
        damageOnCooldown = false;
    }
    
    void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.tag == "Player") {
            if (!damageOnCooldown) {
                other.gameObject.GetComponent<EntityHealthController>().DamageCharacter(damageDealtPerTic);
                StartCoroutine(DamageCooldown());
            }
            
        }
    }

    IEnumerator DamageCooldown() {
        damageOnCooldown = true;
        yield return new WaitForSeconds(secondPerTic);
        damageOnCooldown = false;
    }
}
