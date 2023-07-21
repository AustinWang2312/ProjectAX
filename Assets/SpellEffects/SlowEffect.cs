using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// which are handled by OnCollisionEnter2D
// Zones
// - are handled by OnTriggerEnter2D / OnTriggerExit2D
// - slowAmount: applied until zone is left (duration optional)
// - Have Colliders, with IsTrigger marked
// - No Rigidbody
// - enemiesInZone to track all enemies inside

//Instances
// - Slows by slowAmount for Duration
// - handled by OnCollisionEnter2D
// - Rigidbody
// - 2d collider IsTrigger UNmarked

public class SlowEffect : MonoBehaviour, ISpellComponent
{
    public float slowAmount;
    public float duration;
    private List<EnemyController> enemiesInZone = new List<EnemyController>();

    public void ApplyStats(SpellStats spellStats)
    {
        this.slowAmount = spellStats.SlowAmount;
        this.duration = spellStats.SlowDuration;
    }

    // When collides with a slow effect object
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collided1");
        EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
        if (enemy)
        {
            Debug.Log("collided slow");
            enemy.SlowOnce(slowAmount, duration);
        }
    }

    // When an enemy enters a slow zone
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger collided");
        EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();

        if (enemy)
        {
            Debug.Log("collided slow");
            enemy.SlowContinuous(slowAmount);
            enemiesInZone.Add(enemy);

        }
    }

    // When leaving a slow zone
    void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("exited");
        EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();

        if (enemy)
        {
            Debug.Log("exited slow");
            enemiesInZone.Remove(enemy);
            enemy.RemoveSlowContinuous(slowAmount);
            
        }
    }


    void OnDestroy()
    {
        foreach (var enemy in enemiesInZone)
        {
            enemy.RemoveSlowContinuous(slowAmount);
            Debug.Log("Slow removed");
        }
    }




}
