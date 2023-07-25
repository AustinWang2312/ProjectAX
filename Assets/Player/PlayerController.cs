using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public float currentSpeed = 5.0f;
    public float defaultSpeed = 5.0f;
    public Rigidbody2D rb;
    private Vector2 moveDirection;
    private Vector3 mousePosition;

    private Transform mainCameraTransform;
    private Vector3 cameraOffset;
    public float smoothSpeed = 0.125f;
    private Coroutine footstepCoroutine;



    private void Start()
    {
        //Initialize Camera
        mainCameraTransform = Camera.main.transform;
        cameraOffset = mainCameraTransform.position - transform.position;

        
    }

    

    private void Update()
    {
       

    }

    private void FixedUpdate()
    {

        // Get input for movement
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        // Calculate movement vector
        moveDirection = new Vector2(moveX, moveY).normalized;

        // Apply movement
        rb.MovePosition(rb.position + moveDirection * currentSpeed * Time.fixedDeltaTime);

        // Update camera position smoothly
        Vector3 desiredCameraPosition = transform.position + cameraOffset;
        Vector3 smoothedCameraPosition = Vector3.Lerp(mainCameraTransform.position, desiredCameraPosition, smoothSpeed);
        mainCameraTransform.position = smoothedCameraPosition;



        checkFootSteps();
    }



    private void LateUpdate()
    {
        // Face the mouse position
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDirection = (Vector2)mousePosition - rb.position;
        
        transform.up = lookDirection;


    }

    private void checkFootSteps()
    {
        bool isMoving = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);

        if (isMoving && footstepCoroutine == null)
        {
            // Start footstep sound loop
            footstepCoroutine = StartCoroutine(FootstepSoundLoop());
        }
        else if (!isMoving && footstepCoroutine != null)
        {
            // Stop footstep sound loop
            StopCoroutine(footstepCoroutine);
            footstepCoroutine = null;
        }
    }

    IEnumerator FootstepSoundLoop()
    {
        while (true)
        {
            SoundManager.instance.PlayFootStepSound();
            yield return new WaitForSeconds(0.4f); // Adjust the delay to match the character's walking speed
        }
    }

    public void SpeedUp(float hasteAmount, float duration)
    {
        StartCoroutine(HasteEffect(hasteAmount, duration));
    }

    private IEnumerator HasteEffect(float hasteAmount, float duration)
    {
        currentSpeed *= (1 + hasteAmount);
        Debug.Log(currentSpeed);
        yield return new WaitForSeconds(duration);
        currentSpeed /= (1 + hasteAmount);
        currentSpeed = Mathf.Min(currentSpeed, defaultSpeed);
    }



}
