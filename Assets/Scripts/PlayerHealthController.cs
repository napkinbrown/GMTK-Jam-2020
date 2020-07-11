using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public int maxhealth = 30;
    public int currentHealth;
    public HealthBar healthBar;

    
    void OnEnable() {
        EventManager.StartListening(EventNames.FREEZE_TICK, FreezeHeal);
    }

    void OnDisable() {
        EventManager.StopListening(EventNames.FREEZE_TICK, FreezeHeal);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxhealth;
        healthBar.SetMaxHealth(maxhealth);
    }

    // Update is called once per frame
    void Update()
    {
        //TODO track damage better, but for now this tests health
        if(Input.GetKeyDown(KeyCode.D)) {
            DamageCharacter(1);
        }
    }

    void DamageCharacter(int damage) 
    {
        if(currentHealth - damage < 0) {
            currentHealth = 0;
        } else {
            currentHealth -= damage;
        }
        
        healthBar.SetHealth(currentHealth);
    }

    void HealCharacter(int amount)
    {
        if(currentHealth + amount >= maxhealth) {
            currentHealth = maxhealth;
        } else {
            currentHealth += amount;
        }

        healthBar.SetHealth(currentHealth);
    }

    void FreezeHeal() 
    {
        HealCharacter(1);
    }
}
