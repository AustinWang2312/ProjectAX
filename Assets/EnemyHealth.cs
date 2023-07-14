using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth;
    public EnemyHealthBar healthBar;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeFlatDamage(float amount)
    {
        currentHealth -= amount;
        healthBar.UpdateHealthBar(currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    private void Update()
    {
    }

}
