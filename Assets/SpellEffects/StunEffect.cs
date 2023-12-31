using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// which are handled by OnCollisionEnter2D
// Zones
// - are handled by OnTriggerEnter2D 
// - stun duration applied until zone is left
// - Have Colliders, with IsTrigger marked
// - No Rigidbody

//Instances
// - stuns for Duration
// - handled by OnCollisionEnter2D
// - Rigidbody
// - 2d collider IsTrigger UNmarked

public class StunEffect : MonoBehaviour, ISpellComponent
{
    public float duration;

    public void ApplyStats(SpellStats spellStats)
    {
        this.duration = spellStats.StunDuration;
    }

    // When colliding with a stun effect object
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("stun collided");
        EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
        if (enemy)
        {
            Debug.Log("stunned");
            enemy.Stun(duration);
        }
    }

    // When an enemy enters a stun zone
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("stun zone collided");
        EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();

        if (enemy)
        {
            Debug.Log("zone stunned");
            enemy.Stun(duration);

        }
    }
}
