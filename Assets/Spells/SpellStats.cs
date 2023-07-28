using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class SpellStats
{
    private static readonly Dictionary<string, string> PropertyDescriptions = new Dictionary<string, string>
{
    { "Area", "- AOE" },
    { "ProjectileSpeed", "- Projectile speed: {0}" },
    { "HpPercentDmg", "- Damage: {0}% of enemy health" },
    { "FlatDmg", "- Damage: {0} HP" },
    { "ObjectDuration", "- Lasts: {0} seconds" },
    { "WeakenAmount", "- Weaken Amount {0}%" },
    { "WeakenDuration", "- Weaken Duration: {0} seconds" },
    { "BurningDPS", "- Burn Damage: {0} HP per second" },
    { "BurningDuration", "- Burn Duration: {0} seconds" },
    { "BreakArmorAmount", "- Armor Break Amount: {0}%" },
    { "BreakDuration", "- Armor Break Duration: {0} seconds" },
    { "KnockbackForce", "- Knockback Force: {0}" },
    { "Healing", "- Heals: {0} HP" },
    { "HasteAmount", "- Haste Amount: {0}%" },
    { "HasteDuration", "- Haste Duration: {0} seconds" },
    { "ShieldAmount", "- Shield Amount: {0} HP" },
    { "ResistanceAmount", "- Resistance Increase: {0}" },
    { "ResistanceDuration", "- Resistance Duration: {0} seconds" },
    { "SlowAmount", "- Slow Amount: {0}%" },
    { "SlowDuration", "- Slow Duration: {0} seconds" },
    { "StunDuration", "- Stun Duration: {0} seconds" },
};


    public float Area { get; set; }
    public float ProjectileSpeed { get; set; }
    public float HpPercentDmg { get; set; }
    public float FlatDmg { get; set; }
    public float ObjectDuration { get; set; }

    public float WeakenAmount { get; set; }
    public float WeakenDuration { get; set; }
    public float BurningDPS { get; set; }
    public float BurningDuration { get; set; }
    public float BreakArmorAmount { get; set; }
    public float BreakDuration { get; set; }
    public float KnockbackForce { get; set; }
    public float Healing { get; set; }
    public float HasteAmount { get; set; }
    public float HasteDuration { get; set; }
    public float ShieldAmount { get; set; }
    public float ResistanceAmount { get; set; }
    public float ResistanceDuration { get; set; }
    public float SlowAmount { get; set; }
    public float SlowDuration { get; set; }
    public float StunDuration { get; set; }

    public SpellStats()
    {

    }

    public SpellStats(SpellStats source)
    {
        foreach (var property in typeof(SpellStats).GetProperties())
        {
            if (property.PropertyType == typeof(float))
            {
                property.SetValue(this, property.GetValue(source));
            }
        }
    }

    public class Builder
    {
        private SpellStats _spellStats;

        public Builder()
        {
            _spellStats = new SpellStats();
        }

        public Builder WithArea(float area)
        {
            _spellStats.Area = area;
            return this;
        }

        public Builder WithProjectileSpeed(float projectileSpeed)
        {
            _spellStats.ProjectileSpeed = projectileSpeed;
            return this;
        }

        public Builder WithHpPercentDmg(float hpPercentDmg)
        {
            _spellStats.HpPercentDmg = hpPercentDmg;
            return this;
        }

        public Builder WithFlatDmg(float flatDmg)
        {
            _spellStats.FlatDmg = flatDmg;
            return this;
        }

        public Builder WithObjectDuration(float objectDuration)
        {
            _spellStats.ObjectDuration = objectDuration;
            return this;
        }

        public Builder WithWeakenAmount(float weakenAmount)
        {
            _spellStats.WeakenAmount = weakenAmount;
            return this;
        }

        public Builder WithWeakenDuration(float weakenDuration)
        {
            _spellStats.WeakenDuration = weakenDuration;
            return this;
        }

        public Builder WithBurningDPS(float burningDPS)
        {
            _spellStats.BurningDPS = burningDPS;
            return this;
        }

        public Builder WithBurningDuration(float burningDuration)
        {
            _spellStats.BurningDuration = burningDuration;
            return this;
        }

        public Builder WithBreakArmorAmount(float breakArmorAmount)
        {
            _spellStats.BreakArmorAmount = breakArmorAmount;
            return this;
        }

        public Builder WithBreakDuration(float breakDuration)
        {
            _spellStats.BreakDuration = breakDuration;
            return this;
        }

        public Builder WithKnockbackForce(float knockbackForce)
        {
            _spellStats.KnockbackForce = knockbackForce;
            return this;
        }

        public Builder WithHealing(float healing)
        {
            _spellStats.Healing = healing;
            return this;
        }

        public Builder WithHasteAmount(float hasteAmount)
        {
            _spellStats.HasteAmount = hasteAmount;
            return this;
        }

        public Builder WithHasteDuration(float hasteDuration)
        {
            _spellStats.HasteDuration = hasteDuration;
            return this;
        }

        public Builder WithShieldAmount(float shieldAmount)
        {
            _spellStats.ShieldAmount = shieldAmount;
            return this;
        }

        public Builder WithResistanceAmount(float resistanceAmount)
        {
            _spellStats.ResistanceAmount = resistanceAmount;
            return this;
        }

        public Builder WithResistanceDuration(float resistanceDuration)
        {
            _spellStats.ResistanceDuration = resistanceDuration;
            return this;
        }

        public Builder WithSlowAmount(float slowAmount)
        {
            _spellStats.SlowAmount = slowAmount;
            return this;
        }

        public Builder WithSlowDuration(float slowDuration)
        {
            _spellStats.SlowDuration = slowDuration;
            return this;
        }

        public Builder WithStunDuration(float stunDuration)
        {
            _spellStats.StunDuration = stunDuration;
            return this;
        }

        public SpellStats Build()
        {
            return _spellStats;
        }

        
    }

    public SpellStats ApplyPlayerStats(PlayerStats playerStats)
    {

        SpellStats copy = new SpellStats();

        // Use reflection to apply player's multipliers
        foreach (var property in typeof(SpellStats).GetProperties())
        {
            if (property.PropertyType == typeof(float))
            {
                float spellStatValue = (float)property.GetValue(this);
                float playerStatMultiplier = (float)typeof(PlayerStats).GetProperty(property.Name).GetValue(playerStats);
                property.SetValue(copy, spellStatValue * playerStatMultiplier);
            }
        }

        return copy;
    }

    public string GenerateDescription()
    {
        StringBuilder description = new StringBuilder();
        foreach (var property in typeof(SpellStats).GetProperties())
        {
            string propertyName = property.Name;
            float value = (float)property.GetValue(this);

            if (propertyName != "ObjectDuration" && value < 1.0f)
            {
                value *= 100;  // Convert the decimal to a percentage for display
            }

            if (value != 0 && PropertyDescriptions.ContainsKey(propertyName))
            {
                string propertyDescription = PropertyDescriptions[propertyName];
                description.AppendLine(string.Format(propertyDescription, value));
            }
        }
        Debug.Log(description.ToString());
        return description.ToString();
    }






}
