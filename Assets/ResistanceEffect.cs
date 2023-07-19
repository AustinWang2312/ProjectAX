using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResistanceEffect : MonoBehaviour, ISpellComponent
{
    public float resistAmount;
    public float resistDuration;

    public void ApplyStats(SpellStats spellStats)
    {
        resistAmount = spellStats.ResistanceAmount;
        resistDuration = spellStats.ResistanceDuration;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("resist collision");
        PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();
        if (player)
        {
            Debug.Log("player resist up");
            player.AddResistance(resistAmount, resistDuration);
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("resist zone entry");
        PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();

        if (player)
        {
            Debug.Log("zone player resist up");
            player.AddResistance(resistAmount, resistDuration);
        }
    }
}
