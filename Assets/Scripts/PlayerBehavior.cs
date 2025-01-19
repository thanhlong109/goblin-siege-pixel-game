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
    [SerializeField] bool isAttacking = false;

    private GatherInput gI;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private int direction = 1;
   
    void Start()
    {
        gI = GetComponent<GatherInput>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        Attack();
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

    // Flip player follow player direction
    private void Flip()
    {
        if(direction * gI.valueX < 0) // if currect direction difference with new direction then 
        {
            spriteRenderer.flipX = direction > 0;
            direction *= -1;
        }
    }

    private void Attack()
    {
        if (gI.tryAttack && !isAttacking)
        {
            isAttacking = true;
            animator.SetTrigger("attack");
        }
       
    }

    public void ResetAttack()
    {
        gI.tryAttack = false;
        isAttacking = false;
    }

    private void SetAnimatorValues()
    {
        animator.SetFloat("moveSpeed", Mathf.Abs(rb.velocity.x));   
        animator.SetFloat("ySpeed", rb.velocity.y);
        animator.SetBool("grounded", grounded);
    }

    private void CheckStatus()
    {
        RaycastHit2D leftCheckHit = Physics2D.Raycast(leftPoint.position, Vector2.down, rayLength, groundLayer);
        RaycastHit2D rightCheckHit = Physics2D.Raycast(rightPoint.position, Vector2.down, rayLength, groundLayer);
        grounded = leftCheckHit || rightCheckHit;
        SeeRay(leftCheckHit, leftPoint);
        SeeRay(rightCheckHit, rightPoint);
    }

    private void SeeRay(RaycastHit2D checkHit, Transform pos)
    {
        Color color = checkHit ? Color.red : Color.green;
        Debug.DrawRay(pos.position, Vector2.down * rayLength, color);
    }
}
