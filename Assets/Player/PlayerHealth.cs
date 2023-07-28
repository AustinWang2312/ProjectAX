using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private float maxHealth = 100;
    private float currentHealth;
    public PlayerHealthBar healthBar;

    public float currentResistance = 0;
    public float defaultResistance = 0;

    [SerializeField] float maxShield = 0;
    [SerializeField] float currentShield = 0;


    private float damageTakenThisTick;
    private float tickCounter;
    private float tickRate = 0.5f;
  

    // Start is called before the first frame update
    void Start()
    {
        //Initialize Health and Shield
        currentHealth = maxHealth;
        currentShield = maxShield;
        currentResistance = defaultResistance;
        if(healthBar)
        {
            healthBar.UpdateHealthBar();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        tickCounter += Time.deltaTime;
        float roundedDamage = 0;
        if (damageTakenThisTick != 0)
        {
            roundedDamage = Mathf.Round(damageTakenThisTick * 10f) / 10f;
        }
        
        if (roundedDamage != 0 && tickCounter >= tickRate)
        {
            ApplyTickDamage(roundedDamage);
            ShowDamage(roundedDamage);

            tickCounter = 0;
            damageTakenThisTick = 0;
        }
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

    public void ShowDamage(float amount)
    {
        DamageTextPool.Instance.ShowDamage(amount, this.transform);
    }

    // Function to handle damage taken by the player
    public void TakeFlatDamage(float damageAmount)
    {
        float totalDamage = damageAmount * (1 - currentResistance);
        damageTakenThisTick += totalDamage;
    
    }

    private void ApplyTickDamage(float totalDamage)
    {
        if (currentShield > 0)
        {
            currentShield -= totalDamage;
            if (currentShield < 0)
            {
                currentHealth += currentShield;
                currentShield = 0;
            }
        }
        else
        {
            currentHealth -= totalDamage;
        }

        SoundManager.instance.PlaySound("Player Damage");
        healthBar.UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Min(currentHealth, maxHealth);
        healthBar.UpdateHealthBar();
    }

    public void AddResistance(float amount , float duration)
    {
        StartCoroutine(ResistanceEffect(amount, duration));
    }

    private IEnumerator ResistanceEffect(float resistAmount, float duration)
    {
        currentResistance += resistAmount;
        yield return new WaitForSeconds(duration);
        currentResistance -= resistAmount;
    }

    // Function to handle player's death
    private void Die()
    {
        SceneNavigator deathLoader = GameObject.Find("Death Scene Loader").GetComponent<SceneNavigator>();
        deathLoader.LoadDeathScene();
        // Perform actions when the player dies
    }


}
