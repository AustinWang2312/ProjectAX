using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellStats
{
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


    public class Builder
    {
        private SpellStats _spellStats;
        private PlayerStats _playerStats;

        public Builder(PlayerStats playerStats)
        {
            _spellStats = new SpellStats();
            _playerStats = playerStats;
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

            // Use reflection to apply player's multipliers
            foreach (var property in typeof(SpellStats).GetProperties())
            {
                if (property.PropertyType == typeof(float))
                {
                    float spellStatValue = (float)property.GetValue(_spellStats);
                    float playerStatMultiplier = (float)typeof(PlayerStats).GetProperty(property.Name).GetValue(_playerStats);
                    property.SetValue(_spellStats, spellStatValue * playerStatMultiplier);
                }
            }

            return _spellStats;
        }
    }
        
}
