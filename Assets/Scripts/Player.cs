using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rgdb;
    BoxCollider2D bodyCollider;
    CapsuleCollider2D feetCollider;
    Animator animator;
    SpriteRenderer spriteRenderer;

    bool isInvencible = false;

    [SerializeField] public int healthPoints = 3;
    [SerializeField] float iFramesDelay, knockbackStrength;

    // Start is called before the first frame update
    void Start()
    {
        rgdb = GetComponent<Rigidbody2D>();
        bodyCollider = GetComponent<BoxCollider2D>();
        feetCollider = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (bodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies")))
        {
            OnDamage();

            Vector2 direction = new Vector2(Mathf.Abs(transform.position.x + other.transform.position.x), Mathf.Abs(transform.position.y + other.transform.position.y));
            Vector2 knockbackDirection = direction.normalized;
            rgdb.velocity = Vector2.zero;
            rgdb.AddForce(knockbackDirection * knockbackStrength, ForceMode2D.Impulse);
        }
    }

    private void OnDamage()
    {
        if (isInvencible) { return; }

        isInvencible = true;
        Physics2D.IgnoreLayerCollision(0, 3, true);
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.8f);
    
        healthPoints -= 1;
        animator.SetTrigger("isTakingDamage");
        StartCoroutine(DamageDelay());
    }

    private IEnumerator DamageDelay()
    {
        yield return new WaitForSeconds(iFramesDelay);
        Physics2D.IgnoreLayerCollision(0, 3, false);
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
        isInvencible = false;
    }
}
