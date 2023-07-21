using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats
{
    //Each of these represent the player multipliers for the current stat
    public float Area { get; set; } = 1.0f;
    public float ProjectileSpeed { get; set; } = 1.0f;
    public float HpPercentDmg { get; set; } = 1.0f;
    public float FlatDmg { get; set; } = 1.0f;
    public float ObjectDuration { get; set; } = 1.0f;

    public float WeakenAmount { get; set; } = 1.0f;
    public float WeakenDuration { get; set; } = 1.0f;
    public float BurningDPS { get; set; } = 1.0f;
    public float BurningDuration { get; set; } = 1.0f;
    public float BreakArmorAmount { get; set; } = 1.0f;
    public float BreakDuration { get; set; } = 1.0f;
    public float KnockbackForce { get; set; } = 1.0f;
    public float Healing { get; set; } = 1.0f;
    public float HasteAmount { get; set; } = 1.0f;
    public float HasteDuration { get; set; } = 1.0f;
    public float ShieldAmount { get; set; } = 1.0f;
    public float ResistanceAmount { get; set; } = 1.0f;
    public float ResistanceDuration { get; set; } = 1.0f;
    public float SlowAmount { get; set; } = 1.0f;
    public float SlowDuration { get; set; } = 1.0f;
    public float StunDuration { get; set; } = 1.0f;
}
