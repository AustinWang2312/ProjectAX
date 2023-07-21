using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlatDamage : MonoBehaviour, ISpellComponent
{
    //This "base" stat refers to post modifier, pre enemy reduction damage
    public float baseDamage;

    public void ApplyStats(SpellStats spellStats)
    {
        baseDamage = spellStats.FlatDmg;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("flat damage collision");
        EnemyHealth enemy = collision.gameObject.GetComponent<EnemyHealth>();
        if (enemy)
        {
            Debug.Log("flat damage dealt");
            enemy.TakeFlatDamage(baseDamage);
        }
    }

    // When an enemy enters a damage zone
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("flat damage zone entry");
        EnemyHealth enemy = collision.gameObject.GetComponent<EnemyHealth>();

        if (enemy)
        {
            Debug.Log("zone flat damage dealt");
            enemy.TakeFlatDamage(baseDamage);

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
