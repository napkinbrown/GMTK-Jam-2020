using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : EntityHealthController
{
    public int meltingDamage;
    public float meltingDelayInSecs;
    public HealthBar healthBar;

    public Sprite phase1Sprite;
    public Sprite phase2Sprite;
    public Sprite phase3Sprite;

    private SpriteRenderer spriteRenderer;
    private FreezeController freezeController;
    private bool isFreezing;
    private bool doMeltingDamage;

    public enum HealthPhase {
        Solid,
        Melty,
        Melted
    }
    
    
    void OnEnable() {
        EventManager.StartListening(EventNames.FREEZE_START, SetIsFreezingToTrue);
        EventManager.StartListening(EventNames.FREEZE_STOP, SetIsFreezingToFalse);
        EventManager.StartListening(EventNames.FREEZE_TICK, FreezeHeal);
    }

    void OnDisable() {
        EventManager.StopListening(EventNames.FREEZE_START, SetIsFreezingToTrue);
        EventManager.StopListening(EventNames.FREEZE_STOP, SetIsFreezingToFalse);
        EventManager.StopListening(EventNames.FREEZE_TICK, FreezeHeal);
    }

    void SetIsFreezingToTrue() {
        isFreezing = true;
    }

    void SetIsFreezingToFalse() {
        isFreezing = false;
    }

    protected new void Start()
    {
        base.Start();
        healthBar.SetMaxHealth(maxhealth);
        freezeController = this.GetComponentInParent<FreezeController>();
        doMeltingDamage = true;
        spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    void Update() {
        ApplyMeltingDamage();
        CheckForSpriteSwap();
    }

    void ApplyMeltingDamage() {
        if(isAlive && !isFreezing && doMeltingDamage) {
            DamageCharacter(meltingDamage);
            StartCoroutine(CoolDownMeltingDamage());
        }
    }

    void CheckForSpriteSwap()
    {
        
        switch(GetHealthPhase())
        {
            case HealthPhase.Solid:
                spriteRenderer.sprite = phase1Sprite;
                break;
            case HealthPhase.Melty:
                spriteRenderer.sprite = phase2Sprite;
                break;
            case HealthPhase.Melted:
                spriteRenderer.sprite = phase3Sprite;
                break;
        }
    }

    IEnumerator CoolDownMeltingDamage() {
        doMeltingDamage = false;
        yield return new WaitForSeconds(meltingDelayInSecs);
        doMeltingDamage = true;
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
        EventManager.TriggerEvent(EventNames.PLAYER_DIED);
        Destroy(this.gameObject);
    }
}
