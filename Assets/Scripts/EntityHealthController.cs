using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityHealthController : MonoBehaviour
{
    public int maxhealth;
    public int currentHealth;

    protected void Start() {
        currentHealth = maxhealth;
    }

    public void DamageCharacter(int damage) 
    {
        if(currentHealth - damage < 0) {
            currentHealth = 0;
        } else {
            currentHealth -= damage;
        }

        if(currentHealth <= 0) {
            Die();
        }
    }

    protected abstract void Die();
}
