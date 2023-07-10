using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Status status;
    private Rigidbody rb;
    private Animator animator;
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
        if (Input.GetMouseButtonDown(0) && status.SetLock())
        {
            animator.SetTrigger("isAttack");
        }
    }
    
    public void AttackEnd()
    {
        status.Release();
    }
}
