using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour, IDamageable
{
    // Movement speed
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private Animator anim;
    [SerializeField] private float health = 100;

    // Rigidbody component
    private Rigidbody rb;

    private bool isAttacking => !(anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") || anim.GetCurrentAnimatorStateInfo(0).IsName("Run"));

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(Time.timeScale != 0)
        {
            HandleMovement();
            HandleAnimation();
        }
    }

    private void HandleMovement()
    {
        // Get input axes
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // Get camera's forward direction
        Vector3 cameraForward = Camera.main.transform.forward;
        cameraForward.y = 0;

        // Get camera's right direction
        Vector3 cameraRight = Camera.main.transform.right;
        cameraRight.y = 0;

        // Calculate movement direction
        Vector3 movementDirection = (cameraForward * verticalInput) + (cameraRight * horizontalInput);

        // Normalize movement direction
        movementDirection = movementDirection.normalized;

        // Calculate movement velocity
        Vector3 movementVelocity = movementDirection * movementSpeed * Time.deltaTime;

        if (!isAttacking)
        {
            rb.velocity = movementVelocity;
        } else
        {
            rb.velocity = Vector3.zero;
        }

        // Rotate character towards movement direction
        if (movementDirection.magnitude > 0.01f && !isAttacking)
        {
            transform.rotation = Quaternion.LookRotation(movementDirection, Vector3.up);
        }
    }

    private void HandleAnimation()
    {
        anim.SetFloat("Speed", rb.velocity.sqrMagnitude);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if(health <= 0)
        {
            health = 0;
            Destroy(gameObject);
        }
    }
}
