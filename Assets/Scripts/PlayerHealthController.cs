using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public int maxhealth = 30;
    public int currentHealth;
    public HealthBar healthBar;

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
            currentHealth -= 1;
            healthBar.SetHealth(currentHealth);
        }
    }
}
