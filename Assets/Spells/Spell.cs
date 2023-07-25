using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Spell
{
    protected SpellStats stats;
    protected List<OrbManager.OrbType> combo;
    protected string spellName;
    public AudioClip spellSound;

    public Spell()
    {
    }

    // An abstract method that must be implemented by subclasses.
    public abstract void Cast(OrbManager player);

    public List<OrbManager.OrbType> GetComboString()
    {
        return combo;
    }

    public string getName()
    {
        return spellName;
    }

    public void playSFX()
    {
        SoundManager.instance.PlaySound(spellName);

    }

    public void playSFX(string spellName)
    {
        SoundManager.instance.PlaySound(spellName);

    }



    public void ApplySpellStatsToGameObject(SpellStats spellStats, GameObject gameObject, OrbManager player)
    {

        //Set Velocity
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        Projectile projectile = gameObject.GetComponent<Projectile>();
        if (rb != null && projectile != null)
        {
            rb.velocity = player.firePoint.up * spellStats.ProjectileSpeed;
        }

        //adjust area
        if(spellStats.Area != 0)
        {
            Vector2 scaledUp = new Vector2(gameObject.transform.localScale.x * spellStats.Area, gameObject.transform.localScale.y * spellStats.Area);
            gameObject.transform.localScale = scaledUp;
        }
        


        var spellComponents = gameObject.GetComponents<ISpellComponent>();
        foreach (var component in spellComponents)
        {
            component.ApplyStats(spellStats);
        }
    }
}

public class BaseSpell: Spell
{
    //For DelegatedSpawner
    public override void Cast(OrbManager player)
    {
        PlayerStats playerStats = player.playerStats;
        GameObject shield = (GameObject)GameObject.Instantiate(Resources.Load<GameObject>("EarthShield"), player.transform.position, Quaternion.identity);
        SpellStats finalStats = stats.ApplyPlayerStats(playerStats);

        ApplySpellStatsToGameObject(finalStats, shield, player);
    }


}


public class EarthShield : Spell
{
    public EarthShield()
    {
        combo = new List<OrbManager.OrbType> { OrbManager.OrbType.Earth, OrbManager.OrbType.Earth, OrbManager.OrbType.Earth };

        spellName = "Earth Shield";

        // Initial values for this specific spell
        float area = 1f;
        float objectDuration = 0.01f;
        float shield_base = 40;

        stats = new SpellStats.Builder()
            .WithArea(area)
            .WithObjectDuration(objectDuration)
            .WithShieldAmount(shield_base)
            .Build();
    }

    public override void Cast(OrbManager player)
    {
        PlayerStats playerStats = player.playerStats;
        GameObject shield = (GameObject)GameObject.Instantiate(Resources.Load<GameObject>("EarthShield"), player.transform.position, Quaternion.identity);
        SpellStats finalStats = stats.ApplyPlayerStats(playerStats);

        ApplySpellStatsToGameObject(finalStats, shield, player);
        playSFX();
    }

}


public class Geyser : Spell
{
    public Geyser()
    {
        combo = new List<OrbManager.OrbType> { OrbManager.OrbType.Water, OrbManager.OrbType.Earth, OrbManager.OrbType.Water };

        spellName = "Geyser";

        // Initial values for this specific spell
        float area = 1f;
        float objectDuration = 0.01f;
        float knockbackForce = 25f;
        float hpPercentDmg = 0.1f;

        stats = new SpellStats.Builder()
            .WithArea(area)
            .WithKnockbackForce(knockbackForce)
            .WithObjectDuration(objectDuration)
            .WithHpPercentDmg(hpPercentDmg)
            .Build();
    }

    public override void Cast(OrbManager player)
    {
        PlayerStats playerStats = player.playerStats;
        GameObject geyser = (GameObject)GameObject.Instantiate(Resources.Load<GameObject>("Geyser"), player.cursorPoint.position, Quaternion.identity);
        SpellStats finalStats = stats.ApplyPlayerStats(playerStats);

        ApplySpellStatsToGameObject(finalStats, geyser, player);
        playSFX();
    }

}

public class Icicle : Spell
{
    public Icicle()
    {
        combo = new List<OrbManager.OrbType> { OrbManager.OrbType.Water, OrbManager.OrbType.Fire, OrbManager.OrbType.Water };

        spellName = "Icicle";

        // Initial values for this specific spell
        float baseDamage = 25f;
        float projectileSpeed = 15f;
        float baseStun = 3f;
        float baseBreak = 0.25f;
        float baseBreakDuration = 10f;

        stats = new SpellStats.Builder()
            .WithProjectileSpeed(projectileSpeed)
            .WithFlatDmg(baseDamage)
            .WithStunDuration(baseStun)
            .WithBreakArmorAmount(baseBreak)
            .WithBreakDuration(baseBreakDuration)
            .Build();
    }

    public override void Cast(OrbManager player)
    {
        PlayerStats playerStats = player.playerStats;
        GameObject icicle = (GameObject)GameObject.Instantiate(Resources.Load<GameObject>("Icicle"), player.firePoint.position, player.transform.rotation);
        SpellStats finalStats = stats.ApplyPlayerStats(playerStats);

        ApplySpellStatsToGameObject(finalStats, icicle, player);
        playSFX();
    }

}

public class Tarpit : Spell
{
    public Tarpit()
    {
        combo = new List<OrbManager.OrbType> { OrbManager.OrbType.Fire, OrbManager.OrbType.Earth, OrbManager.OrbType.Earth };

        spellName = "Tarpit";

        // Initial values for this specific spell
        float area = 1f;
        float objectDuration = 7f;
        float burningDPS = 5f;
        float burningDuration = 1f;
        float slowAmount = 0.3f;

        stats = new SpellStats.Builder()
            .WithArea(area)
            .WithBurningDPS(burningDPS)
            .WithBurningDuration(burningDuration)
            .WithObjectDuration(objectDuration)
            .WithSlowAmount(slowAmount)
            .Build();
    }

    public override void Cast(OrbManager player)
    {
        PlayerStats playerStats = player.playerStats;
        GameObject tarpit = (GameObject)GameObject.Instantiate(Resources.Load<GameObject>("Tarpit"), player.cursorPoint.position, Quaternion.identity);
        SpellStats finalStats = stats.ApplyPlayerStats(playerStats);

        ApplySpellStatsToGameObject(finalStats, tarpit, player);
        playSFX();
    }

}

public class Fireball : Spell
{
    public Fireball()
    {
        combo = new List<OrbManager.OrbType> { OrbManager.OrbType.Fire, OrbManager.OrbType.Fire, OrbManager.OrbType.Fire };

        spellName = "Fireball";
        // Initial values for this specific spell
        float baseDamage = 100f;
        float projectileSpeed = 10f;
        float burningDPS = 20f;
        float burningDuration = 3f;

        stats = new SpellStats.Builder()
            .WithProjectileSpeed(projectileSpeed)
            .WithFlatDmg(baseDamage)
            .WithBurningDPS(burningDPS)
            .WithBurningDuration(burningDuration)
            .Build();
    }

    public override void Cast(OrbManager player)
    {
        PlayerStats playerStats = player.playerStats;
        GameObject fireball = (GameObject)GameObject.Instantiate(Resources.Load<GameObject>("Fireball"), player.firePoint.position, Quaternion.identity);
        SpellStats finalStats = stats.ApplyPlayerStats(playerStats);

        ApplySpellStatsToGameObject(finalStats, fireball, player);
        playSFX();
    }

}

public class Boulder : Spell
{
    public Boulder()
    {
        combo = new List<OrbManager.OrbType> { OrbManager.OrbType.Earth, OrbManager.OrbType.Earth, OrbManager.OrbType.Fire };

        spellName = "Boulder";

        // Initial values for this specific spell
        float baseHPPercentDamage = 0.25f;
        float projectileSpeed = 10f;
        float slowAmount = 0.8f;
        float slowDuration = 3.0f;
        float weakenAmount = 0.5f;
        float weakenDuration = 10.0f;
        float stunDuration = 0.5f;

        stats = new SpellStats.Builder()
            .WithProjectileSpeed(projectileSpeed)
            .WithHpPercentDmg(baseHPPercentDamage)
            .WithSlowAmount(slowAmount)
            .WithSlowDuration(slowDuration)
            .WithWeakenAmount(weakenAmount)
            .WithWeakenDuration(weakenDuration)
            .WithStunDuration(stunDuration)
            .Build();
    }

    public override void Cast(OrbManager player)
    {
        PlayerStats playerStats = player.playerStats;
        GameObject boulder = (GameObject)GameObject.Instantiate(Resources.Load<GameObject>("Boulder"), player.firePoint.position, Quaternion.identity);
        SpellStats finalStats = stats.ApplyPlayerStats(playerStats);

        ApplySpellStatsToGameObject(finalStats, boulder, player);
        playSFX();
    }

}

public class Flamebreath : Spell
{
    public Flamebreath()
    {
        combo = new List<OrbManager.OrbType> { OrbManager.OrbType.Earth, OrbManager.OrbType.Fire, OrbManager.OrbType.Fire };

        spellName = "Flame Breath";

        // Initial values for this specific spell
        float area = 1f;
        float baseDamage = 25f;
        float slowAmount = 0.9f;
        float weakenAmount = 0.4f;
        float weakenDuration = 4.0f;
        float breakAmount = 0.1f;
        float breakDuration = 6.0f;
        float burningDps = 4f;
        float burningDuration = 2.0f;
        float objectDuration = 0.4f;

        stats = new SpellStats.Builder()
            .WithArea(area)
            .WithFlatDmg(baseDamage)
            .WithSlowAmount(slowAmount)
            .WithWeakenAmount(weakenAmount)
            .WithWeakenDuration(weakenDuration)
            .WithBreakArmorAmount(breakAmount)
            .WithBreakDuration(breakDuration)
            .WithBurningDPS(burningDps)
            .WithBurningDuration(burningDuration)
            .WithObjectDuration(objectDuration)
            .Build();
    }

    public override void Cast(OrbManager player)
    {
        PlayerStats playerStats = player.playerStats;
        Vector2 offset = player.firePoint.up * 1;
        Vector3 newPosition = player.firePoint.position + new Vector3(offset.x, offset.y, 0);
        GameObject flameBreath = (GameObject)GameObject.Instantiate(Resources.Load<GameObject>("FlameBreath"), newPosition, player.transform.rotation);
        SpellStats finalStats = stats.ApplyPlayerStats(playerStats);

        ApplySpellStatsToGameObject(finalStats, flameBreath, player);
        playSFX();
    }

}

public class GreekFire : Spell
{
    public GreekFire()
    {
        combo = new List<OrbManager.OrbType> { OrbManager.OrbType.Water, OrbManager.OrbType.Fire, OrbManager.OrbType.Fire };

        spellName = "Greek Fire";

        // Initial values for this specific spell
        float area = 1f;
        float breakAmount = 0.1f;
        float breakDuration = 10.0f;
        float burningDps = 5f;
        float burningDuration = 10.0f;
        float objectDuration = 0.1f;

        stats = new SpellStats.Builder()
            .WithArea(area)
            .WithBreakArmorAmount(breakAmount)
            .WithBreakDuration(breakDuration)
            .WithBurningDPS(burningDps)
            .WithBurningDuration(burningDuration)
            .WithObjectDuration(objectDuration)
            .Build();
    }

    public override void Cast(OrbManager player)
    {
        PlayerStats playerStats = player.playerStats;
        Vector3 spellLocation = player.cursorPoint.position;
        GameObject indicator = (GameObject)GameObject.Instantiate(Resources.Load<GameObject>("greekFireIndicator"), spellLocation, Quaternion.identity);

        DelegatedSpawner.Instance.WaitAndTrigger("GreekFire", 1f, indicator, playerStats, player, spellLocation, stats, spellName);
        playSFX("Greek Fire Indicator");
    }
}

public class Sunstrike : Spell
{
    public Sunstrike()
    {
        combo = new List<OrbManager.OrbType> { OrbManager.OrbType.Fire, OrbManager.OrbType.Earth, OrbManager.OrbType.Fire };

        spellName = "Sun Strike";


        // Initial values for this specific spell
        float area = 1f;
        float objectDuration = 0.1f;
        float flatDamage = 250f;

        stats = new SpellStats.Builder()
            .WithArea(area)
            .WithFlatDmg(flatDamage)
            .WithObjectDuration(objectDuration)
            .Build();
    }

    public override void Cast(OrbManager player)
    {
        PlayerStats playerStats = player.playerStats;
        Vector3 spellLocation = player.cursorPoint.position;
        GameObject indicator = (GameObject)GameObject.Instantiate(Resources.Load<GameObject>("sunstrikeIndicator"), spellLocation, Quaternion.identity);
        playSFX("Sun Strike Indicator");
        DelegatedSpawner.Instance.WaitAndTrigger("Sunstrike", 2f, indicator, playerStats, player, spellLocation, stats, spellName);

    }


}



//Helper Class for Delayed Instantiation (Any spells that require coroutines
public class DelegatedSpawner: MonoBehaviour
{
    public static DelegatedSpawner Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void InitializeSingleton()
    {
        if (Instance == null)
        {
            GameObject singletonObject = new GameObject(nameof(DelegatedSpawner));
            singletonObject.AddComponent<DelegatedSpawner>();
        }
    }

    public void WaitAndTrigger(string spellName, float duration, GameObject indicator, PlayerStats playerStats, OrbManager player, Vector3 spellLocation, SpellStats spellStats, string spellSFXName)
    {
        StartCoroutine(WaitFor(spellName, duration, indicator, playerStats, player, spellLocation, spellStats, spellSFXName));
    }

    private IEnumerator WaitFor(string spellName, float duration, GameObject indicator, PlayerStats playerStats, OrbManager player, Vector3 spellLocation, SpellStats spellStats, string spellSFXName)
    {
        yield return new WaitForSeconds(duration);
        Destroy(indicator);
        GameObject spell = (GameObject)GameObject.Instantiate(Resources.Load<GameObject>(spellName), spellLocation, Quaternion.identity);
        SpellStats finalStats = spellStats.ApplyPlayerStats(playerStats);

        Spell baseSpell = new BaseSpell();
        baseSpell.ApplySpellStatsToGameObject(finalStats, spell, player);
        baseSpell.playSFX(spellSFXName);


    }

}




