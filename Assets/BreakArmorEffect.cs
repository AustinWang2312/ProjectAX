using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakArmorEffect : MonoBehaviour
{
    //should be between 0+ where 1 is 100% of damage
    public float breakAmount;
    public float duration;

    // When colliding with a stun effect object
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("break armor collided");
        EnemyHealth enemy = collision.gameObject.GetComponent<EnemyHealth>();
        if (enemy)
        {
            Debug.Log("armor broken");
            enemy.BreakArmor(breakAmount, duration);

        }
    }

    // When an enemy enters a stun zone
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("break armor zone collided");
        EnemyHealth enemy = collision.gameObject.GetComponent<EnemyHealth>();

        if (enemy)
        {
            Debug.Log("zone armor broken");
            enemy.BreakArmor(breakAmount, duration);

        }
    }
}
