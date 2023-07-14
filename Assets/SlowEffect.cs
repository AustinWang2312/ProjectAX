using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowEffect : MonoBehaviour
{
    public float slowAmount;
    public float duration;
    private List<EnemyController> enemiesInZone = new List<EnemyController>();


    // When collides with a slow effect object
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collided1");
        EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
        if (enemy)
        {
            Debug.Log("collided slow");
            enemy.SlowOnce(slowAmount, duration);
        }
    }

    // When an emey enters a slow zone
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger collided");
        EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();

        if (enemy)
        {
            Debug.Log("collided slow");
            enemy.SlowContinuous(slowAmount);
            enemiesInZone.Add(enemy);

        }
    }

    // When leaving a slow zone
    void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("exited");
        EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();

        if (enemy)
        {
            Debug.Log("exited slow");
            enemy.RemoveSlowContinuous(slowAmount);
        }
    }


    void OnDestroy()
    {
        foreach (var enemy in enemiesInZone)
        {
            enemy.RemoveSlowContinuous(slowAmount);
        }
    }




}
