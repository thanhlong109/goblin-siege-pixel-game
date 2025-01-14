using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{

    [SerializeField] float moveSpeed = 3f;
    [SerializeField] float jumpForce = 10f;
    [SerializeField] float rayLength;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform leftPoint;
    [SerializeField] Transform rightPoint;
    [SerializeField] bool grounded = true;

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
        CheckStatus();
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
        if(gI.isJump && grounded) 
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

    private void Attack()
    {
       
    }

    public void ResetAttack()
    {
        gI.tryAttack = false;
        animator.SetBool("attack", false);
    }

    private void SetAnimatorValues()
    {
        animator.SetFloat("moveSpeed", Mathf.Abs(rb.velocity.x));   
        animator.SetFloat("ySpeed", rb.velocity.y);
        animator.SetBool("grounded", grounded);
        animator.SetBool("attack", gI.tryAttack);
    }

    private void CheckStatus()
    {
        RaycastHit2D leftCheckHit = Physics2D.Raycast(leftPoint.position, Vector2.down, rayLength, groundLayer);
        RaycastHit2D rightCheckHit = Physics2D.Raycast(rightPoint.position, Vector2.down, rayLength, groundLayer);
        if (leftCheckHit || rightCheckHit)
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
        SeeRay(leftCheckHit, leftPoint);
        SeeRay(rightCheckHit, rightPoint);
    }

    private void SeeRay(RaycastHit2D checkHit, Transform pos)
    {
        Color color = checkHit ? Color.red : Color.green;
        Debug.DrawRay(pos.position, Vector2.down * rayLength, color);
    }
}
