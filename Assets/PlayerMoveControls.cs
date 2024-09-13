using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveControl : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 5f;
    private float direction = 1f;
    public GatherInput gatherInput;
    public Rigidbody2D rigidbody2D;

    public Animator animator;

    public float rayLength = 1f;
    public LayerMask groundLayer;
    public Transform leftPoint;
    private bool grounded = false;

    // Start is called before the first frame update
    void Start()
    {
        gatherInput = GetComponent<GatherInput>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    private void SetAnimatorValue() {
        animator.SetFloat("Speed", Mathf.Abs(rigidbody2D.velocity.x));
        animator.SetFloat("vSpeed", rigidbody2D.velocity.y);
        animator.SetBool("Grounded", grounded); 
    }
    private void CheckStatus() {
        grounded = Physics2D.Raycast(leftPoint.position,
            Vector2.down, rayLength, groundLayer); 
    }
    // Update is called once per frame
    void Update()
    {
        SetAnimatorValue();
        rigidbody2D.velocity = new Vector2(
            speed * gatherInput.valueX,
            rigidbody2D.velocity.y
        );
    }
    private void FixedUpdate()
    { 
        CheckStatus(); 
        Move();
        JumpPlayer();
    }
    private void Move(){
        Flip();
        rigidbody2D.velocity = new Vector2(
                    speed * gatherInput.valueX, rigidbody2D.velocity.y
                );
    }
    private void Flip(){
        if(gatherInput.valueX * direction < 0){
            transform.localScale = new Vector3(
                -transform.localScale.x,1,1
            );
            direction *= -1;
        }
    }
    private void JumpPlayer(){
        if(gatherInput.jumpInput && grounded){
            rigidbody2D.velocity = new Vector2(
                gatherInput.valueX * speed, jumpForce
            );
        }
         gatherInput.jumpInput = false;
    }
}