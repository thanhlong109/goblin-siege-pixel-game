using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{

    [SerializeField] float moveSpeed = 3f;
    [SerializeField] float jumpForce = 10f;

    private GatherInput gI;
    private Rigidbody2D rb;
    private Animator animator;

    private int direction = 1;
   
    void Start()
    {
        gI = GetComponent<GatherInput>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    
    void Update()
    {
        SetAnimatorValues();
    }

    
    private void FixedUpdate()
    {
        Move();
        Jump();
    }

    private void Move()
    {
        Flip();
        rb.velocity = new Vector2(moveSpeed * gI.valueX, rb.velocity.y);
    }

    private void Jump()
    {
        if(gI.isJump) 
        {
            rb.velocity = new Vector2(gI.valueX * moveSpeed, jumpForce);
        }
        gI.isJump = false;
    }

    private void Flip()
    {
        if(direction * gI.valueX < 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x, 1, 1);
            direction *= -1;
        }
    }

    private void SetAnimatorValues()
    {
        animator.SetFloat("moveSpeed", Mathf.Abs(rb.velocity.x));   
    }
}
