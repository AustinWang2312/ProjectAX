using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasteEffect : MonoBehaviour, ISpellComponent
{
    public float hasteAmount;
    public float hasteDuration;

    public void ApplyStats(SpellStats spellStats)
    {
        hasteAmount = spellStats.HasteAmount;
        hasteDuration = spellStats.HasteDuration;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("haste collision");
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if (player)
        {
            Debug.Log("player hasted");
            player.SpeedUp(hasteAmount, hasteDuration);
        }
    }

    
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("haste zone entry");
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();

        if (player)
        {
            Debug.Log("zone player hasted");
            player.SpeedUp(hasteAmount, hasteDuration);
        }
    }
}
