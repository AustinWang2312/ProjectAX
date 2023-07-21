using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealEffect : MonoBehaviour, ISpellComponent
{
    public float healAmount;

    public void ApplyStats(SpellStats spellStats)
    {
        healAmount = spellStats.Healing;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("heal collision");
        PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();
        if (player)
        {
            Debug.Log("player healed");
            player.Heal(healAmount);
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("heal zone entry");
        PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();

        if (player)
        {
            Debug.Log("zone player healed");
            player.Heal(healAmount);
        }
    }
}
