using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private float maxHealth = 100;
    private float currentHealth;
    public PlayerHealthBar healthBar;

    [SerializeField] float maxShield = 0;
    [SerializeField] float currentShield = 0;
  

    // Start is called before the first frame update
    void Start()
    {
        //Initialize Health and Shield
        currentHealth = maxHealth;
        currentShield = maxShield;
        healthBar.UpdateHealthBar();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public void SetMaxHealth(float hp)
    {
        maxHealth = hp;

    }

    public float GetMaxShield()
    {
        return maxShield;
    }

    public float GetCurrentShield()
    {
        return currentShield;
    }


    public void SetShield(float shieldAmount)
    {
        if (shieldAmount > currentShield)
        {
            maxShield = shieldAmount;
            currentShield = shieldAmount;
        }
        
        healthBar.UpdateHealthBar();

    }

    // Function to handle damage taken by the player
    public void TakeFlatDamage(float damageAmount)
    {
        if (currentShield > 0)
        {
            currentShield -= damageAmount;
            if (currentShield < 0)
            {
                currentHealth += currentShield;
                currentShield = 0;
            }
        }
        else
        {
            currentHealth -= damageAmount;
        }
        
        healthBar.UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Function to handle player's death
    private void Die()
    {
        // Perform actions when the player dies
    }
}
