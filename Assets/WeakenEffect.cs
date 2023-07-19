using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakenEffect : MonoBehaviour, ISpellComponent
{
    //should be between 0+ where 1 is 100% of damage
    public float weakenAmount;
    public float duration;

    public void ApplyStats(SpellStats spellStats)
    {

        this.weakenAmount = spellStats.WeakenAmount;
        this.duration = spellStats.WeakenDuration;
    }

    // When colliding with a stun effect object
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("break armor collided");
        EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
        if (enemy)
        {
            Debug.Log("armor broken");
            enemy.Weaken(weakenAmount, duration);
        }
    }

    // When an enemy enters a stun zone
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("break armor zone collided");
        EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
        if (enemy)
        {
            Debug.Log("zone armor broken");
            enemy.Weaken(weakenAmount, duration);

        }
    }
}
