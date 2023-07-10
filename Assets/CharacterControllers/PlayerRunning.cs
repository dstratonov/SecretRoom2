using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunning : MonoBehaviour
{
    private Status status;
    private Rigidbody rb;
    private Animator animator;
    public float speed = 5.0f;
    public float rotationSpeed = 700.0f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        status = GetComponent<Status>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        float mouseInput = Input.GetAxis("Mouse X");

        Vector3 direction = new Vector3(moveHorizontal, 0, moveVertical).normalized;
        direction = transform.TransformDirection(direction);
        
        // Move the character
        if (direction != Vector3.zero && status.IsFree())
        {
            rb.velocity = new Vector3(direction.x * speed, rb.velocity.y, direction.z * speed);
        }
        
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
