using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Vector2 moveInput;
    Rigidbody2D playerRigidbody;
    Collider2D playerCollider;

    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        FlipSprite();
    }

    private void Move()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * moveSpeed, playerRigidbody.velocity.y); 
        playerRigidbody.velocity = playerVelocity;
    }

    private void FlipSprite()
    {
        bool hasHorizontalSpeed = Mathf.Abs(playerRigidbody.velocity.x) > Mathf.Epsilon;
        if (hasHorizontalSpeed)
            transform.localScale = new Vector2 (Mathf.Sign(playerRigidbody.velocity.x), 1f);
    }

    private void OnMove(InputValue value) 
    {
        moveInput = value.Get<Vector2>();
    }

    private void OnJump(InputValue value)
    {
        if (value.isPressed && playerCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            playerRigidbody.velocity += new Vector2 (0f, jumpForce);
        }
    }
}