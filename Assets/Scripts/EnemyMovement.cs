using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Rigidbody2D enemyRigidbody;
    Animator enemyAnimator;
    CapsuleCollider2D enemyWalkCollider;

    bool isWalking = true;
    float stopChance = 0.2f; // Adjust this value to control the likelihood of stopping
    float stopDuration = 1f; // Adjust this value to set the duration of the stop
    float timer = 0f;
    [SerializeField] float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();
        enemyAnimator = GetComponent<Animator>();
        enemyWalkCollider = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        if (isWalking)
        {
            enemyRigidbody.velocity = new Vector2(moveSpeed, 0f);
            timer += Time.deltaTime;

            if (timer >= stopDuration)
            {
                float randomValue = UnityEngine.Random.value;

                if (randomValue <= stopChance)
                {
                    // Stop the enemy
                    enemyRigidbody.velocity = Vector2.zero;
                    isWalking = false;
                    timer = 0f;
                }
                else
                {
                    // Continue walking
                    timer = 0f;
                }
            }
        }
        
        else
        {
            // Enemy is currently stopped
            timer += Time.deltaTime;

            if (timer >= stopDuration)
            {
                // Resume walking
                isWalking = true;
                timer = 0f;
            }
        }
        enemyAnimator.SetBool("isRunning", isWalking);
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (enemyWalkCollider.IsTouchingLayers(LayerMask.GetMask("Ground", "Enemies")))
        {
            moveSpeed = -moveSpeed;
            FlipSprite();
        }
    }

    void FlipSprite()
    {
        transform.localScale = new Vector2 (-(Mathf.Sign(enemyRigidbody.velocity.x)), 1f);
    }
}
