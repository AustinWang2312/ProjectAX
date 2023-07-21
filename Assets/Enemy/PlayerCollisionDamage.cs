using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionDamage : MonoBehaviour
{
    public float baseDamage;

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
