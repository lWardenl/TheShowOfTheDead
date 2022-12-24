using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashTest : MonoBehaviour
{
    // Dash distance
    [SerializeField] private float dashDistance = 5f;

    // Dash speed
    [SerializeField] private float dashSpeed = 20f;

    // Dash duration
    [SerializeField] private float dashDuration = 0.5f;

    // Dash cool down
    [SerializeField] private float dashCoolDown = 1f;

    // Timer for dash duration
    private float dashTimer;

    // Timer for dash cool down
    private float coolDownTimer;

    // Dash direction
    private Vector3 dashDirection;

    // Dash rigidbody
    private Rigidbody rb;

    // Player's current velocity
    private Vector3 currentVelocity;

    // Player's current angular velocity
    private Vector3 currentAngularVelocity;

    // Dash state
    private bool isDashing;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isDashing && coolDownTimer <= 0)
        {
            // Start dashing
            isDashing = true;

            // Reset dash timer
            dashTimer = dashDuration;

            // Get dash direction
            dashDirection = transform.forward;

            // Save current velocity and angular velocity
            currentVelocity = rb.velocity;
            currentAngularVelocity = rb.angularVelocity;

            // Set dash velocity
            rb.velocity = dashDirection * dashSpeed;
            rb.angularVelocity = Vector3.zero;
        }

        if (isDashing)
        {
            // Decrement dash timer
            dashTimer -= Time.deltaTime;

            // If dash timer has expired, stop dashing
            if (dashTimer <= 0)
            {
                isDashing = false;

                // Reset velocity and angular velocity
                rb.velocity = currentVelocity;
                rb.angularVelocity = currentAngularVelocity;

                // Start cool down timer
                coolDownTimer = dashCoolDown;
            }
        }

        // Decrement cool down timer
        coolDownTimer -= Time.deltaTime;
    }
}
