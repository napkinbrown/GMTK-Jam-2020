using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityHealthController : MonoBehaviour
{
    public int maxhealth;
    public int currentHealth;
    [HideInInspector]
    public bool isAlive;

    protected void Start() {
        isAlive = true;
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
            isAlive = false;
            Die();
        }
    }

    protected abstract void Die();
}
