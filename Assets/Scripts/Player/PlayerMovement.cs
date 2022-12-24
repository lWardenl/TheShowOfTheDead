using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Movement speed
    [SerializeField] private float movementSpeed = 10f;

    // Rigidbody component
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(!PauseMenu.isPaused)
            HandleMovement();
    }

    private void HandleMovement()
    {
        // Get input axes
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // Get camera's forward direction
        Vector3 cameraForward = Camera.main.transform.forward;
        cameraForward.y = 0;
        cameraForward = cameraForward.normalized;

        // Get camera's right direction
        Vector3 cameraRight = Camera.main.transform.right;
        cameraRight.y = 0;
        cameraRight = cameraRight.normalized;

        // Calculate movement direction
        Vector3 movementDirection = (cameraForward * verticalInput) + (cameraRight * horizontalInput);

        // Normalize movement direction
        movementDirection = movementDirection.normalized;

        // Calculate movement velocity
        Vector3 movementVelocity = movementDirection * movementSpeed;

        // Set rigidbody velocity
        if (movementDirection.magnitude > 0)
        {
            rb.velocity = movementVelocity;
        }
        else
        {
            rb.velocity = Vector3.zero;
        }

        // Rotate character towards movement direction
        if (movementDirection.magnitude > 0)
        {
            transform.rotation = Quaternion.LookRotation(movementDirection, Vector3.up);
        }
    }

}
