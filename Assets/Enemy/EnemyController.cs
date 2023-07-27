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
    private bool isStunned = false;
    private int stunInstances = 0;

    public float baseDamage = 10f;
    public float currentDamage = 10f;


    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        currentSpeed = defaultSpeed;
        currentDamage = baseDamage;

    }

    

    private void OnCollisionStay2D(Collision2D collision)
    {
        // Check if we are colliding with the player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Get the player controller
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

            // Deal damage to the player
            if (playerHealth != null)
            {
                playerHealth.TakeFlatDamage(currentDamage * Time.deltaTime);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!target)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
        if (target && !isStunned && !isKnockedBack)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            rb.rotation = angle;
            moveDirection = direction;            
        }
    }

    private void FixedUpdate()
    {
        if (target && !isKnockedBack &&!isStunned)
        {
            rb.velocity = new Vector2(moveDirection.x, moveDirection.y) * currentSpeed;
        }
    }

    public void ApplyKnockback(Vector2 circleCenter, float knockbackForce)
    {
        Vector2 knockbackDirection = (Vector2)transform.position - circleCenter;

        //if stunned
        if (isStunned)
        {
            // Calculate the new position
            Vector2 newPosition = (Vector2)transform.position + knockbackDirection.normalized * knockbackForce * 0.25f / rb.mass;

            // Coroutine to smoothly move to the new position
            StartCoroutine(MoveToPosition(newPosition, 0.25f));
        }
        else
        {
            rb.AddForce(knockbackDirection.normalized * knockbackForce, ForceMode2D.Impulse);
        }
        

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

    private IEnumerator MoveToPosition(Vector2 newPosition, float time)
    {
        Vector2 startingPos = transform.position;
        float elapsedTime = 0;

        while (elapsedTime < time)
        {
            transform.position = Vector2.Lerp(startingPos, newPosition, (elapsedTime / time));
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        transform.position = newPosition;
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
        currentSpeed *= (1 - slowAmount);
    }

    //function to slow the enemy continuously
    public void RemoveSlowContinuous(float slowAmount)
    {
        currentSpeed /= (1 - slowAmount);
        Debug.Log("Slow successfully removed");
    }

    private IEnumerator SlowEffect(float slowAmount, float duration)
    {
        currentSpeed *= (1 - slowAmount);
        yield return new WaitForSeconds(duration);
        currentSpeed /= (1 - slowAmount);
    }

    public void Weaken(float weakenAmount, float duration)
    {
        StartCoroutine(WeakenEffect(weakenAmount, duration));
    }

    private IEnumerator WeakenEffect(float weakenAmount, float duration)
    {
        currentDamage *= (1 - weakenAmount);
        yield return new WaitForSeconds(duration);
        currentDamage /= (1 - weakenAmount);
        currentDamage = Mathf.Min(currentDamage, baseDamage);
    }



    public void Stun(float duration)
    {
        StartCoroutine(StunEffect(duration));
    }

    private IEnumerator StunEffect(float duration)
    {
        isStunned = true;
        stunInstances++;
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.velocity = new Vector2(0, 0);

        yield return new WaitForSeconds(duration);
        stunInstances--;
        if (stunInstances <= 0)
        {
            isStunned = false;
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
        
    }
}
