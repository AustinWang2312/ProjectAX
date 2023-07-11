using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float defaultSpeed = 2.0f;
    private float currentSpeed;
    private Transform target;
    public Rigidbody2D rb;
    Vector2 moveDirection;

    private bool isSlowed;


    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        currentSpeed = defaultSpeed;
        isSlowed = false;

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

            //// Move towards the player
            //transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }

    }

    private void FixedUpdate()
    {
        if (target)
        {
            rb.velocity = new Vector2(moveDirection.x, moveDirection.y) * currentSpeed;
        }
    }

    //function to slow the enemy once per collision
    public void SlowOnce(float slowAmount, float duration)
    {
        StartCoroutine(SlowEffect(slowAmount, duration));
    }

    //function to slow the enemy once per collision
    public void SlowContinuous(float slowAmount, float duration)
    {
        StartCoroutine(SlowZone(slowAmount, duration));
    }



    private IEnumerator SlowEffect(float slowAmount, float duration)
    {
        currentSpeed *= slowAmount;
        yield return new WaitForSeconds(duration);
        currentSpeed = defaultSpeed;
    }


    private IEnumerator SlowZone(float slowAmount, float duration)
    {
        while (isSlowed)
        {
            currentSpeed *= slowAmount;
            yield return new WaitForSeconds(duration);
            currentSpeed = defaultSpeed;
        }
    }

    // When collides with a slow effect object
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collided");
        if (collision.gameObject.CompareTag("SlowEffect"))
        {
            Debug.Log("collided slow");
            SlowEffect slowEffect = collision.gameObject.GetComponent<SlowEffect>();
            if (slowEffect != null)
            {
                SlowOnce(slowEffect.slowAmount, slowEffect.duration);
            }
        }
    }

    // When entering a slow zone
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collided");
        if (collision.gameObject.CompareTag("SlowEffect"))
        {
            Debug.Log("collided slow");
            SlowEffect slowEffect = collision.gameObject.GetComponent<SlowEffect>();
            if (slowEffect != null)
            {
                isSlowed = true;
                SlowContinuous(slowEffect.slowAmount, slowEffect.duration);
            }
        }
    }

    // When leaving a slow zone
    void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("exited");
        if (collision.gameObject.CompareTag("SlowEffect"))
        {
            Debug.Log("exited slow");
            isSlowed = false;
            currentSpeed = defaultSpeed;
        }
    }


}
