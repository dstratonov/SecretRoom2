using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    private Status status;
    private Rigidbody rb;
    private Animator animator;

    public float groundCheckDistance = 0.2f;
    public float jumpForce = 5.0f;
    public LayerMask groundLayer;

    private bool groundCheckStart = false;

    // Start is called before the first frame update
    void Start()
    {
        status = GetComponent<Status>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && IsGrounded() && status.SetLock())
        {
            groundCheckStart = false;
            animator.SetTrigger("IsJump");
        }

        if (IsGrounded() && groundCheckStart)
        {
            animator.SetBool("IsFalling", false);
        }
    }
    
    public void JumpStart()
    {
        rb.velocity += new Vector3(0.0f, jumpForce, 0.0f) + rb.velocity * 2.0f;
        animator.SetBool("IsFalling", true);
        animator.SetBool("IsJump", false);
    }

    public void GoGroundCheck()
    {
        groundCheckStart = true;
    }

    public void JumpEnd()
    {
        status.Release();
        groundCheckStart = false;
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
