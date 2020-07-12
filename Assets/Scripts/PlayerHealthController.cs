using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : EntityHealthController
{
    public HealthBar healthBar;

    private FreezeController freezeController;

    
    void OnEnable() {
        EventManager.StartListening(EventNames.FREEZE_TICK, FreezeHeal);
    }

    void OnDisable() {
        EventManager.StopListening(EventNames.FREEZE_TICK, FreezeHeal);
    }
    
    // Start is called before the first frame update
    protected new void Start()
    {
        base.Start();
        healthBar.SetMaxHealth(maxhealth);
        freezeController = this.GetComponentInParent<FreezeController>();
    }

    protected new void DamageCharacter(int damage) {
        base.DamageCharacter(damage);
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
        HealCharacter((int) freezeController.healRate);
    }

    protected override void Die() {
        Debug.LogError("Not yet implemented");
    }
}
