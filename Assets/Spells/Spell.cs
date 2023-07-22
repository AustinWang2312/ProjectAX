using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell
{
    protected SpellStats stats;

    public Spell()
    {
    }

    // An abstract method that must be implemented by subclasses.
    public abstract void Cast(OrbManager player);

    // A non-abstract method that already has an implementation.
    public void DefaultMethod()
    {
        Debug.Log("test");
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


public class EarthShield : Spell
{
    public EarthShield()
    {
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
    }

}


public class Geyser : Spell
{
    public Geyser()
    {
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
    }

}

public class Icicle : Spell
{
    public Icicle()
    {
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
    }

}

public class Tarpit : Spell
{
    public Tarpit()
    {
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
    }

}

public class Fireball : Spell
{
    public Fireball()
    {
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
    }

}

public class Boulder : Spell
{
    public Boulder()
    {
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
    }

}

public class Flamebreath : Spell
{
    public Flamebreath()
    {
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
    }

}

public class GreekFire : Spell
{
    public GreekFire()
    {
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

        DelegatedGreekFireSpawner.Instance.WaitAndExplode(1f, indicator, playerStats, player, spellLocation, stats);
        
    }

    private IEnumerator WaitFor(float duration)
    {
        yield return new WaitForSeconds(duration);
    }


}

public class DelegatedGreekFireSpawner: MonoBehaviour
{
    public static DelegatedGreekFireSpawner Instance { get; private set; }

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
            GameObject singletonObject = new GameObject(nameof(DelegatedGreekFireSpawner));
            singletonObject.AddComponent<DelegatedGreekFireSpawner>();
        }
    }

    public void WaitAndExplode(float duration, GameObject indicator, PlayerStats playerStats, OrbManager player, Vector3 spellLocation, SpellStats spellStats)
    {
        StartCoroutine(WaitFor(duration, indicator, playerStats, player, spellLocation, spellStats));
    }

    private IEnumerator WaitFor(float duration, GameObject indicator, PlayerStats playerStats, OrbManager player, Vector3 spellLocation, SpellStats spellStats)
    {
        yield return new WaitForSeconds(duration);
        Destroy(indicator);
        GameObject greekFire = (GameObject)GameObject.Instantiate(Resources.Load<GameObject>("GreekFire"), spellLocation, Quaternion.identity);
        SpellStats finalStats = spellStats.ApplyPlayerStats(playerStats);

        GreekFire spell = new GreekFire();
        spell.ApplySpellStatsToGameObject(finalStats, greekFire, player);


    }

}



