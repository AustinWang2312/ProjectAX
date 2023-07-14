using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackEffect : MonoBehaviour
{
    [SerializeField] float knockbackForce;

    public void SetKnockBack(float force)
    {
        knockbackForce = force;
    }


    //must have collider and isTrigger Checked
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collided");
        EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
        Debug.Log(enemy);
        if (enemy)
        {
            Debug.Log("Enemy knocked back");
            enemy.ApplyKnockback(transform.position, knockbackForce);
        }
    }
}
