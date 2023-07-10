using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharrController : MonoBehaviour
{
    public Rigidbody rb;
    public Animator animator;
    public float groundCheckDistance = 0.2f;
    public float jumpForce = 2.0f;
    public LayerMask groundLayer;
    
    private bool isJumping = false;
    private bool jumpInProcess = false;
    private bool groundCheckStart = false;
    public float speed = 3.0f;
    public float rotationSpeed = 700.0f;

    private void Start()
    {
        // Hide and lock the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && IsGrounded() && !jumpInProcess)
        {
            groundCheckStart = false;
            isJumping = true;
            jumpInProcess = true;
            animator.SetTrigger("IsJump");
        }

        if (IsGrounded() && groundCheckStart)
        {
            animator.SetBool("IsFalling", false);
        }
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        float mouseInput = Input.GetAxis("Mouse X");

        Vector3 direction = new Vector3(moveHorizontal, 0, moveVertical).normalized;
        direction = transform.TransformDirection(direction);
        
        // Move the character
        if (direction != Vector3.zero && !isJumping && !jumpInProcess)
        {
            rb.velocity = new Vector3(direction.x * speed, rb.velocity.y, direction.z * speed);
        }

        // Rotate the character
        Vector3 rotation = new Vector3(0, mouseInput, 0) * rotationSpeed;
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation * Time.fixedDeltaTime));

        // Calculate velocity components
        float velocityHorizontal = Vector3.Dot(rb.velocity, transform.right) / speed;
        float velocityVertical = Vector3.Dot(rb.velocity, transform.forward) / speed;

        // Set the Animator parameters
        animator.SetFloat("MoveHorizontal", velocityHorizontal, 1.0f, Time.deltaTime * 10f);
        animator.SetFloat("MoveVertical", velocityVertical, 1.0f, Time.deltaTime * 10f);
        animator.SetBool("IsRunning", velocityHorizontal != 0 || velocityVertical != 0);
    }
    
    public void JumpStart()
    {
        rb.velocity += new Vector3(0.0f, jumpForce, 0.0f) + rb.velocity;
        isJumping = false;
        animator.SetBool("IsFalling", true);
        animator.SetBool("IsJump", false);
    }

    public void GoGroundCheck()
    {
        groundCheckStart = true;
    }

    public void JumpEnd()
    {
        groundCheckStart = false;
        jumpInProcess = false;
    }
    
    bool IsGrounded()
    {
        RaycastHit hit;
        float radius = 0.3f;  // This radius should be less than the half size of your character to avoid collision with walls

        // For SphereCast, you need to define a starting point and radius, along with direction, distance and layer mask
        if (Physics.SphereCast(transform.position + Vector3.up * 0.3f, radius, -Vector3.up, out hit, groundCheckDistance, groundLayer))
        {
            if (Vector3.Distance(hit.point, transform.position) < groundCheckDistance)  return true;
        }
        return false;
    }
}
