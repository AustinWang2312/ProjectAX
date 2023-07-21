using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnEffect : MonoBehaviour, ISpellComponent
{
    //Only used for zone
    private List<EnemyHealth> burningEnemies = new List<EnemyHealth>();
    public float burningDPS;
    public float duration;

    public void ApplyStats(SpellStats spellStats)
    {
        this.burningDPS = spellStats.BurningDPS;
        this.duration = spellStats.BurningDuration;
    }


    // When colliding with a burn effect object
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("burning object collided");
        EnemyHealth enemy = collision.gameObject.GetComponent<EnemyHealth>();
        if (enemy)
        {
            Debug.Log("projectile burning enemy");
            enemy.BurnOnce(burningDPS, duration);

        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("burning zone entered");

        EnemyHealth enemy = collision.gameObject.GetComponent<EnemyHealth>();
        if (enemy)
        {
            Debug.Log("burning zone start burn");

            // Start the burn effect
            burningEnemies.Add(enemy);
            enemy.EnterBurnZone(burningDPS, duration);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("burning zone entered");


        EnemyHealth enemy = collision.gameObject.GetComponent<EnemyHealth>();
        if (enemy)
        {
            Debug.Log("burning zone end burn");
            // Begin ending the burn effect
            enemy.ExitBurnZone(burningDPS, duration);
            burningEnemies.Remove(enemy);
        }
    }

    void OnDestroy()
    {
        // When the burn zone is destroyed, end burn for all enemies
        foreach (EnemyHealth enemy in burningEnemies)
        {
            enemy.ExitBurnZone(burningDPS, duration);
        }
    }
}
