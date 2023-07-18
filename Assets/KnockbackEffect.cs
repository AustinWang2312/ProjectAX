using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackEffect : MonoBehaviour, ISpellComponent
{
    [SerializeField] float knockbackForce;

    public void ApplyStats(SpellStats spellStats)
    {
        knockbackForce = spellStats.KnockbackForce;
    }


    //must have collider and isTrigger Checked
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collided");
        EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
        Debug.Log(enemy);
        if (enemy)
        {
            Debug.Log("Enemy knocked back");
            enemy.ApplyKnockback(transform.position, knockbackForce);
        }
    }
}
