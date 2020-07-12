using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : EntityHealthController
{
    public HealthBar healthBar;

    private FreezeController freezeController;

    public enum HealthPhase {
        Solid,
        Melty,
        Melted
    }

    
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

    /**
    Returns the "health phase" we're in 
    */
    public HealthPhase GetHealthPhase()
    {
        float twoThirds = .66f * maxhealth;
        float oneThird = .33f * maxhealth;

        if(currentHealth >= twoThirds)
        {
            return HealthPhase.Solid;
        } else if (currentHealth > oneThird) {
            return HealthPhase.Melty; 
        } else {
            return HealthPhase.Melted;
        }
    }

    void FreezeHeal() 
    {
        HealCharacter((int) freezeController.healRate);
    }

    protected override void Die() {
        Debug.LogError("Not yet implemented");
    }
}
