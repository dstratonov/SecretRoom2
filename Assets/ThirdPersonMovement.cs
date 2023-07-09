using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float speed = 3.0f;
    public float rotationSpeed = 700.0f;
    private Rigidbody rb;
    public Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Hide and lock the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        float mouseInput = Input.GetAxis("Mouse X");

        Vector3 direction = new Vector3(moveHorizontal, 0, moveVertical).normalized;
        direction = transform.TransformDirection(direction);

        bool isJump = animator.GetBool("IsJump");
        
        // Move the character
        if (direction != Vector3.zero && !isJump)
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
}
