using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public Slider shieldSlider;
    public PlayerHealth playerHealth;

    private void Start()
    {
        healthSlider.maxValue = playerHealth.GetMaxHealth();
        healthSlider.value = playerHealth.GetCurrentHealth();
        shieldSlider.maxValue = playerHealth.GetMaxShield();
        shieldSlider.value = playerHealth.GetCurrentShield();

    }

    public void UpdateHealthBar()
    {
        healthSlider.value = playerHealth.GetCurrentHealth();
        shieldSlider.maxValue = playerHealth.GetMaxShield();
        shieldSlider.value = playerHealth.GetCurrentShield();
    }
}
