using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float defaultSpeed = 2.0f;
    [SerializeField] float currentSpeed;
    private Transform target;
    public Rigidbody2D rb;
    Vector2 moveDirection;

    private bool isKnockedBack = false;



    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        currentSpeed = defaultSpeed;

    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            rb.rotation = angle;
            moveDirection = direction;

            
        }

    }

    private void FixedUpdate()
    {
        if (target & !isKnockedBack)
        {
            rb.velocity = new Vector2(moveDirection.x, moveDirection.y) * currentSpeed;
        }
    }

    public void ApplyKnockback(Vector2 circleCenter, float knockbackForce)
    {
        Vector2 knockbackDirection = (Vector2)transform.position - circleCenter;
        rb.AddForce(knockbackDirection.normalized * knockbackForce, ForceMode2D.Impulse);

        // Set the flag to true when knockback is applied
        isKnockedBack = true;

        // Reset the flag after some time (e.g., 0.5 seconds)
        Invoke("ResetKnockback", 0.25f);
    }

    private void ResetKnockback()
    {
        // Reset the flag
        isKnockedBack = false;
    }



    //function to slow the enemy once per collision
    // reduces speed upon collision, returns speed after duration
    public void SlowOnce(float slowAmount, float duration)
    {
        StartCoroutine(SlowEffect(slowAmount, duration));
    }

    //function to slow the enemy continuously
    public void SlowContinuous(float slowAmount)
    {
        currentSpeed *= slowAmount;
    }

    //function to slow the enemy continuously
    public void RemoveSlowContinuous(float slowAmount)
    {
        currentSpeed /= slowAmount;
    }

    private IEnumerator SlowEffect(float slowAmount, float duration)
    {
        currentSpeed *= slowAmount;
        yield return new WaitForSeconds(duration);
        currentSpeed /= slowAmount;
        currentSpeed = Mathf.Min(currentSpeed, defaultSpeed);
    }


  

 


}
