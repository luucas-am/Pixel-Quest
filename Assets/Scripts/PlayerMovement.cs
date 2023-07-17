using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Vector2 moveInput;
    Rigidbody2D rgdb;
    BoxCollider2D bodyCollider;
    CapsuleCollider2D feetCollider;
    Animator animator;

    [SerializeField] float moveSpeed, jumpForce;

    // Start is called before the first frame update
    void Start()
    {
        rgdb = GetComponent<Rigidbody2D>();
        bodyCollider = GetComponent<BoxCollider2D>();
        feetCollider = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        FlipSprite();
        animator.SetBool("isGrounded", isGrounded());
    }

    private void Move()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * moveSpeed, rgdb.velocity.y); 
        rgdb.velocity = playerVelocity;
    }

    private void FlipSprite()
    {
        bool hasHorizontalSpeed = Mathf.Abs(rgdb.velocity.x) > Mathf.Epsilon;
        animator.SetBool("isRunning", hasHorizontalSpeed);
        if (hasHorizontalSpeed)
            transform.localScale = new Vector2 (Mathf.Sign(rgdb.velocity.x), 1f);
    }

    private void OnMove(InputValue value) 
    {
        moveInput = value.Get<Vector2>();
    }

    private void OnJump(InputValue value)
    {
        if (value.isPressed && isGrounded())
        {
            animator.SetTrigger("isJumping");
            GetComponents<AudioSource>()[0].Play();
            rgdb.velocity += new Vector2 (0f, jumpForce);
        }
    }

    private bool isGrounded()
    {
        bool isGrounded = feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground", "Flying Platform"));
        return isGrounded;
    }
}