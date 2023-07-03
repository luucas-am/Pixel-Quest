using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Rigidbody2D kabuuRigidbody;

    [SerializeField] float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        kabuuRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        kabuuRigidbody.velocity = new Vector2(moveSpeed, 0f);
    }

    void OnTriggerExit2D(Collider2D other) 
    {
        moveSpeed = -moveSpeed;
        FlipSprite();
    }

    void FlipSprite()
    {
        transform.localScale = new Vector2 (-(Mathf.Sign(kabuuRigidbody.velocity.x)), 1f);
    }
}
