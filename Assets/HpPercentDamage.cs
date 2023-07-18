using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpPercentDamage : MonoBehaviour, ISpellComponent
{
    public float baseDamage;

    public void ApplyStats(SpellStats spellStats)
    {
        baseDamage = spellStats.HpPercentDmg;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("flat damage collision");
        EnemyHealth enemy = collision.gameObject.GetComponent<EnemyHealth>();
        if (enemy)
        {
            Debug.Log("hp percent damage dealt");
            enemy.TakeHpPercentDamage(baseDamage);
        }
    }

    // When an enemy enters a damage zone
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("flat damage zone entry");
        EnemyHealth enemy = collision.gameObject.GetComponent<EnemyHealth>();

        if (enemy)
        {
            Debug.Log("zone hp percent damage dealt");
            enemy.TakeHpPercentDamage(baseDamage);

        }
    }
}
