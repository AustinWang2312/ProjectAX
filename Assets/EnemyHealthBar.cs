using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public EnemyHealth enemyHealthManager;

    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;

    private void Start()
    {
        
        healthSlider.maxValue = enemyHealthManager.maxHealth;
        healthSlider.value = enemyHealthManager.maxHealth;
    }

    public void UpdateHealthBar(float health)
    {
        healthSlider.value = health;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Camera.main.transform.rotation;
        transform.position = target.position + offset;
        
    }
}
