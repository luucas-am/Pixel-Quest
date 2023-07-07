using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteveMovement : MonoBehaviour
{

    Animator animator;
    Vector3 originalPosition;
    Rigidbody2D rgdb;

    bool isIdling = true;
    bool canBlink = true;
    
    [SerializeField] float fallingSpeed = 10f;
    [SerializeField] float recoverySpeed = 2f;
    [SerializeField] float blinkDelay = 5f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rgdb = GetComponent<Rigidbody2D>();
        originalPosition = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position == originalPosition)
            isIdling = true;
        Blink();
    }

    void Blink()
    {
        if (isIdling && canBlink)
        {
            animator.SetTrigger("Blink");
            canBlink = false;
            StartCoroutine(BlinkRate(blinkDelay));
        }
    }

    private IEnumerator BlinkRate(float blinkDelay)
    {
        yield return new WaitForSeconds(blinkDelay);;
        canBlink = true;
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Player")
        {
            isIdling = false;
            animator.SetTrigger("isFalling");
            rgdb.velocity = new Vector2 (0f, -fallingSpeed);
        }
    }

    void OnCollisionEnter2D(Collision2D other) 
    {
        if (rgdb.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            animator.SetTrigger("isRecharging");
            while (gameObject.transform.position != originalPosition)
            {
                rgdb.velocity = new Vector2(0f, recoverySpeed);
            }
        }
    }
}
