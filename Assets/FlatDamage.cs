using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlatDamage : MonoBehaviour
{

    public float baseDamage;

    public void SetDamage(float damage)
    {
        baseDamage = damage;
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
