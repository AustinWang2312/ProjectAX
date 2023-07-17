using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class OrbManager : MonoBehaviour
{
    public int maxOrbs = 3;


    //primary refers to first set of orbs
    //secondary refers to second set of orbs
    private OrbType[] primaryOrbs;
    private OrbType[] secondaryOrbs;

    //currentOrbs and backupOrbs are pointers to primary and secondary
    //These indicate which set is currently in use and which set is
    // the backup set.
    private OrbType[] currentOrbs;
    private OrbType[] backupOrbs;

    
    private bool usingsecondaryOrbs = false;
    public Image[] orbIcons;
    public Image[] backupOrbIcons;

    public Sprite emptyOrb;
    public Sprite earthOrb;
    public Sprite waterOrb;
    public Sprite fireOrb;

    const int FORGE = 1;
    const int CAST = 0;

    public Transform firePoint;

    public PlayerHealth playerHealth;

    private Dictionary<string, System.Action> combinationActions;

    public enum OrbType
    {
        Earth,
        Water,
        Fire,
        Empty
    }

    public class SpellStats
    {
        public float Area { get; set; }
        public float ProjectileSpeed { get; set; }
        public float HpPercentDmg { get; set; }
        public float FlatDmg { get; set; }

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
    }

    public void ApplySpellStatsToGameObject(SpellStats spellStats, GameObject gameObject)
    {
        //var areaComponent = gameObject.GetComponent<YourAreaComponent>();
        //if (areaComponent != null)
        //{
        //    areaComponent.SetArea(spellStats.Area);
        //}


        //Setting projectile velocity
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        if (gameObject != null && rb != null && spellStats.ProjectileSpeed != 0)
        {
            rb.velocity = firePoint.up * spellStats.ProjectileSpeed;
        }

        //var hpPercentDmgComponent = gameObject.GetComponent<YourHpPercentDmgComponent>();
        //if (hpPercentDmgComponent != null)
        //{
        //    hpPercentDmgComponent.SetDamage(spellStats.HpPercentDmg);
        //}

        var flatDmgComponent = gameObject.GetComponent<FlatDamage>();
        if (flatDmgComponent != null)
        {
            flatDmgComponent.SetDamage(spellStats.FlatDmg);
        }

        //var weakenAmountComponent = gameObject.GetComponent<YourWeakenAmountComponent>();
        //if (weakenAmountComponent != null)
        //{
        //    weakenAmountComponent.SetWeakenAmount(spellStats.WeakenAmount);
        //}

        //var weakenDurationComponent = gameObject.GetComponent<YourWeakenDurationComponent>();
        //if (weakenDurationComponent != null)
        //{
        //    weakenDurationComponent.SetWeakenDuration(spellStats.WeakenDuration);
        //}

        var burningComponent = gameObject.GetComponent<BurnEffect>();
        if (burningComponent != null)
        {
            burningComponent.Initialize(spellStats.BurningDPS, spellStats.BurningDuration);
        }

        var breakArmorComponent = gameObject.GetComponent<BreakArmorEffect>();
        if (breakArmorComponent != null)
        {
            breakArmorComponent.Initialize(spellStats.BreakArmorAmount, spellStats.BreakDuration); ;
        }


        var knockbackForceComponent = gameObject.GetComponent<KnockbackEffect>();
        if (knockbackForceComponent != null)
        {
            knockbackForceComponent.SetKnockBack(spellStats.KnockbackForce);
        }

        //var healingComponent = gameObject.GetComponent<YourHealingComponent>();
        //if (healingComponent != null)
        //{
        //    healingComponent.SetHealing(spellStats.Healing);
        //}

        //var hasteAmountComponent = gameObject.GetComponent<YourHasteAmountComponent>();
        //if (hasteAmountComponent != null)
        //{
        //    hasteAmountComponent.SetHasteAmount(spellStats.HasteAmount);
        //}

        //var hasteDurationComponent = gameObject.GetComponent<YourHasteDurationComponent>();
        //if (hasteDurationComponent != null)
        //{
        //    hasteDurationComponent.SetHasteDuration(spellStats.HasteDuration);
        //}

        //var shieldAmountComponent = gameObject.GetComponent<YourShieldAmountComponent>();
        //if (shieldAmountComponent != null)
        //{
        //    shieldAmountComponent.SetShieldAmount(spellStats.ShieldAmount);
        //}

        //var resistanceAmountComponent = gameObject.GetComponent<YourResistanceAmountComponent>();
        //if (resistanceAmountComponent != null)
        //{
        //    resistanceAmountComponent.SetResistanceAmount(spellStats.ResistanceAmount);
        //}

        var slowComponent = gameObject.GetComponent<SlowEffect>();
        if (slowComponent != null)
        {
            slowComponent.Initialize(spellStats.SlowAmount, spellStats.SlowDuration);
        }

        

        var stunComponent = gameObject.GetComponent<StunEffect>();
        if (stunComponent != null)
        {
            stunComponent.Initialize(spellStats.StunDuration);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //Initialize Orbs carried
        primaryOrbs = new OrbType[maxOrbs];
        secondaryOrbs = new OrbType[maxOrbs];
        
        for (int i = 0; i < primaryOrbs.Length; i++)
        {
            primaryOrbs[i] = OrbType.Empty;
        }
        primaryOrbs[0] = OrbType.Earth;
        primaryOrbs[1] = OrbType.Earth;
        primaryOrbs[2] = OrbType.Earth;

        for (int i = 0; i < secondaryOrbs.Length; i++)
        {
            secondaryOrbs[i] = OrbType.Empty;
        }
        secondaryOrbs[0] = OrbType.Fire;
        secondaryOrbs[1] = OrbType.Fire;
        secondaryOrbs[2] = OrbType.Fire;
        
        
        currentOrbs = primaryOrbs;
        backupOrbs = secondaryOrbs;



        //Initialize Combination hash map
        combinationActions = new Dictionary<string, System.Action>();
        for (int orb1 = 0; orb1 < 3; orb1++)
        {
            for (int orb2 = 0; orb2 < 3; orb2++)
            {
                for (int orb3 = 0; orb3 < 3; orb3++)
                {
                    for (int click = 0; click < 2; click++)
                    {
                        string combinationKey = $"{orb1}{orb2}{orb3}{click}";
                        Debug.Log(combinationKey);
    
                        System.Action action = GetCombinationAction(orb1, orb2, orb3, click);
                        combinationActions.Add(combinationKey, action);
                    }
                }
            }
    }

        UpdateOrbHUD();
        
    }

    public int getNumOrbs()
    {
        int count = 0;
        for (int i = 0; i < currentOrbs.Length; i++)
        {
            if (currentOrbs[i] == OrbType.Empty) {
                break;
            }
            count ++;
        }

        return count;

    }


    private string GetOrbTypeName(int orbValue)
    {
        switch (orbValue)
        {
            case 0:
                return "Earth";
            case 1:
                return "Water";
            case 2:
                return "Fire";
            default:
                return "Empty";
        }
    }


    private System.Action GetCombinationAction(int orb1, int orb2, int orb3, int click)
    {
        string stringOrb1 = ((OrbType) orb1).ToString();
        string stringOrb2 = ((OrbType) orb2).ToString();
        string stringOrb3 = ((OrbType) orb3).ToString();
        string methodName = $"CombinationAction_"+stringOrb1+stringOrb2+stringOrb3+click;
        Debug.Log(methodName);
        System.Action action = null;

        // Use reflection to retrieve the method dynamically
        System.Reflection.MethodInfo methodInfo = GetType().GetMethod(methodName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        if (methodInfo != null)
        {
            action = (System.Action)System.Delegate.CreateDelegate(typeof(System.Action), this, methodInfo);
        }

        return action;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(CAST) || Input.GetMouseButtonDown(FORGE)) // Check for left mouse button click
        {
            if (getNumOrbs() == maxOrbs)
            {
                int method = Input.GetMouseButtonDown(CAST) ? CAST : FORGE; // 0 for left click, 1 for right click
                string key = $"{(int)currentOrbs[0]}{(int)currentOrbs[1]}{(int)currentOrbs[2]}{method}";
                Debug.Log(key);
                System.Action action = combinationActions[key];
                Debug.Log(combinationActions[key]);
                action.Invoke();

                // Deplete all orbs back to the empty orb
                for (int i = 0; i < currentOrbs.Length; i++)
                {
                    currentOrbs[i] = OrbType.Empty;
                }

                UpdateOrbHUD();
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            // Switch between currentOrbs and secondaryOrbs
            usingsecondaryOrbs = !usingsecondaryOrbs;

            // Update currentOrbs based on the active set
            currentOrbs = usingsecondaryOrbs ? secondaryOrbs : primaryOrbs;
            backupOrbs = !usingsecondaryOrbs ? secondaryOrbs : primaryOrbs;

            // Update the orb HUD
            UpdateOrbHUD();
        }


        
    }




    public bool AddOrb(OrbType type)
    {
        int numOrbs = getNumOrbs();
        if (numOrbs < currentOrbs.Length) {
            currentOrbs[numOrbs] = type;
            UpdateOrbHUD();
            return true;
        }
        else
        {
            return false;
        }

    }

    private void UpdateOrbHUD()
    {
        for (int i = 0; i < orbIcons.Length; i++)
        {
            switch (currentOrbs[i])
            {
                case OrbType.Empty:
                    orbIcons[i].sprite = emptyOrb;
                    break;

                case OrbType.Earth:
                    orbIcons[i].sprite = earthOrb;
                    break;
                case OrbType.Water:
                    orbIcons[i].sprite = waterOrb;
                    break;
                case OrbType.Fire:
                    orbIcons[i].sprite = fireOrb;
                    break;
            }
        }

        for (int i = 0; i < backupOrbIcons.Length; i++)
        {
            switch (backupOrbs[i])
            {
                case OrbType.Empty:
                    backupOrbIcons[i].sprite = emptyOrb;
                    break;

                case OrbType.Earth:
                    backupOrbIcons[i].sprite = earthOrb;
                    break;
                case OrbType.Water:
                    backupOrbIcons[i].sprite = waterOrb;
                    break;
                case OrbType.Fire:
                    backupOrbIcons[i].sprite = fireOrb;
                    break;
            }
        }




    }

    private void CreatePylon(OrbType orbType, Sprite onSprite, Sprite offSprite)
    {
        GameObject pylon = (GameObject)Instantiate(Resources.Load<GameObject>("Pylon"), transform.position, Quaternion.identity);
        GameObject player = this.gameObject;
        pylon.GetComponent<Pylon>().SetPlayerObject(player);
        pylon.GetComponent<Pylon>().SetOrbType(orbType, onSprite, offSprite);
    }


    // 1 Cast Earth Water Fire
    private void CombinationAction_EarthWaterFire0()
    {
        
        Debug.Log("Performing action for Left Click EarthWaterFire");
    }

    // 2 Forge Earth Water Fire
    private void CombinationAction_EarthWaterFire1()
    {
        Debug.Log("Performing action for Right Click EarthWaterFire");
    }

    // 3 Cast Earth Fire Water
    private void CombinationAction_EarthFireWater0()
    {
        
        Debug.Log("Performing action for Left Click EarthFireWater");
    }

    // 4 Forge Earth Fire Water
    private void CombinationAction_EarthFireWater1()
    {
        Debug.Log("Performing action for Right Click EarthFireWater");
    }

    // 5 Cast Earth Earth Water
    private void CombinationAction_EarthEarthWater0()
    {
        
        Debug.Log("Performing action for Left Click EarthEarthWater");
    }

    // 6 Forge Earth Earth Water
    private void CombinationAction_EarthEarthWater1()
    {
        Debug.Log("Performing action for Right Click EarthEarthWater");
    }

    // 7 Cast Earth Earth Fire
    private void CombinationAction_EarthEarthFire0()
    {
        
        Debug.Log("Performing action for Left Click EarthEarthFire");
    }

    // 8 Forge Earth Earth Fire
    private void CombinationAction_EarthEarthFire1()
    {
        Debug.Log("Performing action for Right Click EarthEarthFire");
    }

    // 9 Cast Earth Earth Earth
    private void CombinationAction_EarthEarthEarth0()
    {
        
        Debug.Log("Performing action for Left Click EarthEarthEarth");
        float shield_base = 40;
        //change with modifiers later
        float final_shield = shield_base;
        playerHealth.SetShield(final_shield);

    }

    // 10 Forge Earth Earth Earth
    private void CombinationAction_EarthEarthEarth1()
    {
        //Animation and sound effect
        Debug.Log("Performing action for Right Click EarthEarthEarth");

        CreatePylon(OrbType.Earth, earthOrb, emptyOrb);
    }



    // 11 Cast Earth Fire Earth
    private void CombinationAction_EarthFireEarth0()
    {
        
        Debug.Log("Performing action for Left Click EarthFireEarth");
        

    }

    // 12 Forge Earth Fire Earth
    private void CombinationAction_EarthFireEarth1()
    {
        Debug.Log("Performing action for Right Click EarthFireEarth");
        
    }

    // 13 Cast Earth Fire Earth
    private void CombinationAction_EarthWaterEarth0()
    {
        
        Debug.Log("Performing action for Left Click EarthWaterEarth");
    }

    // 14 Forge Earth Water Earth
    private void CombinationAction_EarthWaterEarth1()
    {
        Debug.Log("Performing action for Right Click EarthWaterEarth");
    }

    // 15 Cast Earth Water Water
    private void CombinationAction_EarthWaterWater0()
    {
        
        Debug.Log("Performing action for Left Click EarthWaterWater");
    }

    // 16 Forge Earth Water Water
    private void CombinationAction_EarthWaterWater1()
    {
        Debug.Log("Performing action for Right Click EarthWaterWater");
    }

    // 17 Cast Earth Water Water
    private void CombinationAction_EarthFireFire0()
    {
        
        Debug.Log("Performing action for Left Click EarthFireFire");
    }

    // 18 Forge Earth Water Water
    private void CombinationAction_EarthFireFire1()
    {
        Debug.Log("Performing action for Right Click EarthWFireFire");
    }

    // 19 Cast Water Earth Fire
    private void CombinationAction_WaterEarthFire0()
    {
        Debug.Log("Performing action for Left Click WaterEarthFire");
    }

    // 20 Forge Water Earth Fire
    private void CombinationAction_WaterEarthFire1()
    {
        Debug.Log("Performing action for Right Click WaterEarthFire");
    }

    // 21 Cast Water Fire Earth
    private void CombinationAction_WaterFireEarth0()
    {
        Debug.Log("Performing action for Left Click WaterFireEarth");
    }

    // 22 Forge Water Fire Earth
    private void CombinationAction_WaterFireEarth1()
    {
        Debug.Log("Performing action for Right Click WaterFireEarth");
    }

    // 23 Cast Water Earth Water
    private void CombinationAction_WaterEarthWater0()
    {
        Debug.Log("Performing action for Left Click WaterEarthWater");
    }

    // 24 Forge Water Earth Water
    private void CombinationAction_WaterEarthWater1()
    {
        Debug.Log("Performing action for Right Click WaterEarthWater");
    }

    // 25 Cast Water Fire Fire
    private void CombinationAction_WaterFireFire0()
    {
        Debug.Log("Performing action for Left Click WaterFireFire");
    }

    // 26 Forge Water Fire Fire
    private void CombinationAction_WaterFireFire1()
    {
        Debug.Log("Performing action for Right Click WaterFireFire");
    }

    // 27 Cast Water Earth Earth
    private void CombinationAction_WaterEarthEarth0()
    {
        Debug.Log("Performing action for Left Click WaterEarthEarth");
    }

    // 28 Forge Water Earth Earth
    private void CombinationAction_WaterEarthEarth1()
    {
        Debug.Log("Performing action for Right Click WaterEarthEarth");
    }


    // 29 Cast Water Water Earth
    private void CombinationAction_WaterWaterEarth0()
    {
        Debug.Log("Performing action for Left Click WaterWaterEarth");
    }

    // 30 Forge Water Water Earth
    private void CombinationAction_WaterWaterEarth1()
    {
        Debug.Log("Performing action for Right Click WaterWaterEarth");
    }

    // 31 Cast Water Water Fire
    private void CombinationAction_WaterWaterFire0()
    {
        Debug.Log("Performing action for Left Click WaterWaterFire");
    }

    // 32 Forge Water Water Fire
    private void CombinationAction_WaterWaterFire1()
    {
        Debug.Log("Performing action for Right Click WaterWaterFire");
    }

    // 33 Cast Water Water Water
    private void CombinationAction_WaterWaterWater0()
    {
        Debug.Log("Performing action for Left Click WaterWaterWater");
    }

    // 34 Forge Water Water Water
    private void CombinationAction_WaterWaterWater1()
    {
        Debug.Log("Performing action for Right Click WaterWaterWater");
        CreatePylon(OrbType.Water, waterOrb, emptyOrb);

    }

    // 35 Cast Water Fire Water
    private void CombinationAction_WaterFireWater0()
    {
        Debug.Log("Performing action for Left Click WaterFireWater");
        
        float baseDamage = 25f;
        float projectileSpeed = 15f;
        float baseStun = 2f;
        float baseBreak = 0.1f;

        ////add modifiers later in the builder function
        //float totalDamage = baseDamage;
        //float totalProjectileSpeed = projectileSpeed;
        //float totalStun = baseStun;
        //float totalBreak = baseBreak;


        GameObject icicle = (GameObject)Instantiate(Resources.Load<GameObject>("Icicle"), firePoint.position, transform.rotation);
        icicle.GetComponent<FlatDamage>().SetDamage(baseDamage);
        Rigidbody2D rb = icicle.GetComponent<Rigidbody2D>();
        rb.velocity = firePoint.up * projectileSpeed;
    }

    // 36 Forge Water Fire Water
    private void CombinationAction_WaterFireWater1()
    {
        Debug.Log("Performing action for Right Click WaterFireWater");
    }



    // 37 Cast Fire Water Water
    private void CombinationAction_FireWaterWater0()
    {
        Debug.Log("Performing action for Left Click FireWaterWater");
    }

    // 38 Forge Fire Water Water
    private void CombinationAction_FireWaterWater1()
    {
        Debug.Log("Performing action for Right Click FireWaterWater");
    }


    // 39 Cast Fire Earth Water
    private void CombinationAction_FireEarthWater0()
    {
        Debug.Log("Performing action for Left Click FireEarthWater");
    }

    // 40 Forge Fire Earth Water
    private void CombinationAction_FireEarthWater1()
    {
        Debug.Log("Performing action for Right Click FireEarthWater");
    }

    // 41 Cast Fire Earth Fire
    private void CombinationAction_FireEarthFire0()
    {
        Debug.Log("Performing action for Left Click FireEarthFire");
    }

    // 42 Forge Fire Earth Fire
    private void CombinationAction_FireEarthFire1()
    {
        Debug.Log("Performing action for Right Click FireEarthFire");
    }

    // 43 Cast Fire Earth Earth
    private void CombinationAction_FireEarthEarth0()
    {
        Debug.Log("Performing action for Left Click FireEarthEarth");
    }

    // 44 Forge Fire Earth Earth
    private void CombinationAction_FireEarthEarth1()
    {
        Debug.Log("Performing action for Right Click FireEarthEarth");
    }

    // 45 Cast Fire Water Earth
    private void CombinationAction_FireWaterEarth0()
    {
        Debug.Log("Performing action for Left Click FireWaterEarth");
    }

    // 46 Forge Fire Water Earth
    private void CombinationAction_FireWaterEarth1()
    {
        Debug.Log("Performing action for Right Click FireWaterEarth");
    }

    // 47 Cast Fire Water Fire
    private void CombinationAction_FireWaterFire0()
    {
        Debug.Log("Performing action for Left Click FireWaterFire");
    }

    // 48 Forge Fire Water Fire
    private void CombinationAction_FireWaterFire1()
    {
        Debug.Log("Performing action for Right Click FireWaterFire");
    }

    // 49 Cast Fire Fire Fire
    private void CombinationAction_FireFireFire0()
    {
        //add modifiers later
        float baseDamage = 100f;
        Debug.Log("Performing action for Left Click FireFireFire: Fireball");
        int projectileSpeed = 10;
        GameObject fireball = (GameObject)Instantiate(Resources.Load<GameObject>("Fireball"), firePoint.position, Quaternion.identity);
        fireball.GetComponent<FlatDamage>().SetDamage(baseDamage);
        Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();
        rb.velocity = firePoint.up * projectileSpeed;
    }

    // 50 Forge Fire Fire Fire
    private void CombinationAction_FireFireFire1()
    {
        Debug.Log("Performing action for Right Click FireFireFire");

        CreatePylon(OrbType.Fire, fireOrb, emptyOrb);

    }

    // 51 Cast Fire Fire Earth
    private void CombinationAction_FireFireEarth0()
    {
        Debug.Log("Performing action for Left Click FireFireEarth");
    }

    // 52 Forge Fire Fire Earth
    private void CombinationAction_FireFireEarth1()
    {
        Debug.Log("Performing action for Right Click FireFireEarth");
    }

    // 53 Cast Fire Fire Water
    private void CombinationAction_FireFireWater0()
    {
        Debug.Log("Performing action for Left Click FireFireWater");
    }

    // 54 Forge Fire Fire Water
    private void CombinationAction_FireFireWater1()
    {
        Debug.Log("Performing action for Right Click FireFireWater");
    }



    

    
}
