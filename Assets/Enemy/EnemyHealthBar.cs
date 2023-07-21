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

    // When collides with a slow effect object
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collided");
        SlowEffect slowEffect = collision.gameObject.GetComponent<SlowEffect>();
        if (slowEffect)
        {
            Debug.Log("collided slow");
        }
    }

    // When entering a slow zone
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collided");
        SlowEffect slowEffect = collision.gameObject.GetComponent<SlowEffect>();

        if (slowEffect)
        {
            Debug.Log("collided slow");

        }
    }

    // When leaving a slow zone
    void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("exited");
        if (collision.gameObject.CompareTag("SlowEffect"))
        {
            Debug.Log("exited slow");
        }
    }
}
