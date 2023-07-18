using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disappear : MonoBehaviour,ISpellComponent
{
    public float duration;

    public void ApplyStats(SpellStats spellStats)
    {
        this.duration = spellStats.ObjectDuration;
        Destroy(gameObject, duration);
    }
}
