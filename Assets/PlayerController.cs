using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public float speed = 2.5f;
    public Rigidbody2D rb;
    private Vector2 moveDirection;
    private Vector2 mousePosition;

    private Transform mainCameraTransform;
    private Vector3 cameraOffset;
    public float smoothSpeed = 0.125f;

    public float maxHealth = 100;
    public float currentHealth;
    public PlayerHealthBar healthBar;

    private void Start()
    {
        //Initialize Camera
        mainCameraTransform = Camera.main.transform;
        cameraOffset = mainCameraTransform.position - transform.position;

        //Initialize Health
        currentHealth = maxHealth;
        healthBar.UpdateHealthBar();
    }

    

    private void Update()
    {
        // Get input for movement
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        // Calculate movement vector
        moveDirection = new Vector2(moveX, moveY).normalized;

    }

    private void FixedUpdate()
    {
        // Face the mouse position
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDirection = mousePosition - rb.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;

        // Apply movement
        rb.MovePosition(rb.position + moveDirection * speed * Time.fixedDeltaTime);

        // Update camera position smoothly
        Vector3 desiredCameraPosition = transform.position + cameraOffset;
        Vector3 smoothedCameraPosition = Vector3.Lerp(mainCameraTransform.position, desiredCameraPosition, smoothSpeed);
        mainCameraTransform.position = smoothedCameraPosition;
    }

    // Function to handle damage taken by the player
    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        healthBar.UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Function to handle player's death
    private void Die()
    {
        // Perform actions when the player dies
    }


}
