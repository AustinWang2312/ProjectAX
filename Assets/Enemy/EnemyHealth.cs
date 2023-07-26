using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class EnemyHealth : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth;
    public EnemyHealthBar healthBar;
    public float maxArmor;
    public float currentArmor;

    private bool isBurning = false; // Whether the enemy is currently burning
    private int burnZoneCount = 0; // The number of burn zones the enemy is in
    public float burnDuration; // The duration of the burn after leaving the zone
    public float totalBurnDPS = 0; // The amount of damage to apply each second

    private float burnTickRate = 1f;  // The time between each damage tick
    private float burnTickCounter = 0;  // A counter to keep track of time since the last damage tick

    private void Start()
    {
        currentHealth = maxHealth;
        currentArmor = maxArmor;
    }

    public void TakeFlatDamage(float amount)
    {
        float final_amount = amount * (1 - Mathf.Max(0, currentArmor));
        currentHealth -= final_amount;
        ShowDamage(final_amount);
        currentHealth = Mathf.Min(maxHealth, currentHealth);

        healthBar.UpdateHealthBar(currentHealth);
        SoundManager.instance.PlaySound("Enemy Impact", false);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void TakeHpPercentDamage(float percent)
    {
        float flatDamage = percent * maxHealth;
        TakeFlatDamage(flatDamage);
    }



    public void TakeBurnDamage(float amount)
    {
        float final_amount = amount;
        currentHealth -= final_amount;
        ShowDamage(final_amount);
        healthBar.UpdateHealthBar(currentHealth);
        SoundManager.instance.PlaySound("Enemy Burn Damage", false);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void ShowDamage(float amount)
    {
        DamageTextPool.Instance.ShowDamage(amount, this.transform);
    }


    public void BreakArmor(float armor_reduction, float duration)
    {
        StartCoroutine(BreakArmorEffect(armor_reduction, duration));
    }

    private IEnumerator BreakArmorEffect(float armor_reduction, float duration)
    {
        currentArmor -= armor_reduction;
        yield return new WaitForSeconds(duration);
        currentArmor += armor_reduction;
    }

    public void BurnOnce(float burningDPS, float duration)
    {
        StartCoroutine(StopBurningAfterDelay(burningDPS, duration));
    }

    public void EnterBurnZone(float burningDPS, float duration)
    {
        isBurning = true;
        totalBurnDPS += burningDPS;


        // Increment the count of burn zones
        burnZoneCount++;
    }

    public void ExitBurnZone(float burningDPS, float duration)
    {
        burnZoneCount--;
        totalBurnDPS -= burningDPS;
        // Start a coroutine to stop burning the enemy after a while
        StartCoroutine(StopBurningAfterDelay(burningDPS, duration));


    }

    IEnumerator StopBurningAfterDelay(float burningDPS, float delay)
    {
        isBurning = true;
        burnZoneCount += 1;
        totalBurnDPS += burningDPS;
        
        // Wait for the duration of the burn
        yield return new WaitForSeconds(delay);

        // Stop burning
        burnZoneCount -= 1;
        totalBurnDPS -= burningDPS;

        if (burnZoneCount < 1)
        {
            isBurning = false;
        }
        
    }

    void Die()
    {
        SoundManager.instance.PlaySound("Enemy Death");

        Destroy(gameObject);
    }

    private void Update()
    {
        if (isBurning)
        {
            burnTickCounter += Time.deltaTime;

            if (burnTickCounter >= burnTickRate)
            {
                // It's time for a damage tick
                float damageThisTick = totalBurnDPS * burnTickRate;
                TakeBurnDamage(damageThisTick);
                ShowDamage(damageThisTick);

                // Reset the tick counter
                burnTickCounter = 0;
            }
        }
    }

}
