using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldEffect : MonoBehaviour, ISpellComponent
{
    public float shieldAmount;

    public void ApplyStats(SpellStats spellStats)
    {
        shieldAmount = spellStats.ShieldAmount;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("shield collision");
        PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();
        if (player)
        {
            Debug.Log("player shielded");
            player.SetShield(shieldAmount);
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("shield zone entry");
        PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();

        if (player)
        {
            Debug.Log("zone player shielded");
            player.SetShield(shieldAmount);
        }
    }
}
