using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public PlayerController playerController;

    private void Start()
    {
        healthSlider.maxValue = playerController.maxHealth;
        healthSlider.value = playerController.maxHealth;
    }

    public void UpdateHealthBar()
    {
        healthSlider.value = playerController.currentHealth;
    }
}
