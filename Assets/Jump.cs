using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public Rigidbody rb;
    public Animator animator;
    public float groundCheckDistance = 0.2f;
    public float jumpForce = 2.0f;
    public LayerMask groundLayer;
    private bool isJumping = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            isJumping = true;
        }

        animator.SetBool("IsJump", isJumping);
    }

    public void JumpStart()
    {
        rb.AddForce(new Vector3(0, jumpForce, 0) + rb.velocity, ForceMode.Impulse);
    }

    public void JumpEnd()
    {
        isJumping = false;
    }
    
    bool IsGrounded()
    {
        RaycastHit hit;
        float radius = 0.3f;  // This radius should be less than the half size of your character to avoid collision with walls

        // For SphereCast, you need to define a starting point and radius, along with direction, distance and layer mask
        if (Physics.SphereCast(transform.parent.position + Vector3.up * 0.5f, radius, -Vector3.up, out hit, groundCheckDistance, groundLayer))
        {
            return true;
        }
        return false;
    }
}
