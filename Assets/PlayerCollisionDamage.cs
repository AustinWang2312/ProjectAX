using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionDamage : MonoBehaviour
{
    public float baseDamage;
    public bool takingDamage = false;
    PlayerHealth playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // Check if we are colliding with the player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Get the player controller
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

            // Deal damage to the player
            if (playerHealth != null)
            {
                playerHealth.TakeFlatDamage(baseDamage * Time.deltaTime);
            }
        }
    }
}
