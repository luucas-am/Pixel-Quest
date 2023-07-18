using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    Rigidbody2D rgdb;
    BoxCollider2D bodyCollider;
    CapsuleCollider2D feetCollider;
    Animator animator;
    SpriteRenderer spriteRenderer;
    PlayerInput input;

    bool isInvencible = false;
    bool canFire = true;

    [SerializeField] GameObject laser;
    [SerializeField] Transform laserSpawner;
    [SerializeField] float iFramesDelay, knockbackStrength;
    [SerializeField] float fireRate;

    // Start is called before the first frame update
    void Start()
    {
        rgdb = GetComponent<Rigidbody2D>();
        bodyCollider = GetComponent<BoxCollider2D>();
        feetCollider = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        input = GetComponent<PlayerInput>();

        Physics2D.IgnoreLayerCollision(0, 3, false);
        Physics2D.IgnoreLayerCollision(0, 10, false);

        input.DeactivateInput();
        StartCoroutine(WaitTransition());
    }

    private void OnFire(InputValue value)
    {
        if (!canFire) { return; }

        canFire = false;
        Instantiate(laser, laserSpawner.position, transform.rotation);
        StartCoroutine(FireDelay());
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (bodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies", "Traps")))
        {
            OnDamage();

            Vector2 direction = new Vector2(Mathf.Abs(transform.position.x + other.transform.position.x), Mathf.Abs(transform.position.y + other.transform.position.y));
            Vector2 knockbackDirection = direction.normalized;
            rgdb.velocity = Vector2.zero;
            rgdb.AddForce(knockbackDirection * knockbackStrength, ForceMode2D.Impulse);
        }

        else if (bodyCollider.IsTouchingLayers(LayerMask.GetMask("Pit")))
            FindObjectOfType<GameSession>().ProcessPlayerDeath(true);
    }

    private void OnDamage()
    {
        if (isInvencible) { return; }

        isInvencible = true;
        Physics2D.IgnoreLayerCollision(0, 3, true);
        Physics2D.IgnoreLayerCollision(0, 10, true);
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.8f);

        GetComponents<AudioSource>()[1].Play();
        FindObjectOfType<GameSession>().ProcessPlayerDeath(false);
        animator.SetTrigger("isTakingDamage");
        StartCoroutine(DamageDelay());
    }

    private IEnumerator DamageDelay()
    {
        yield return new WaitForSeconds(iFramesDelay);
        
        Physics2D.IgnoreLayerCollision(0, 3, false);
        Physics2D.IgnoreLayerCollision(0, 10, false);

        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
        isInvencible = false;
    }

    private IEnumerator FireDelay()
    {
        yield return new WaitForSeconds(fireRate);
        canFire = true;
    }

    IEnumerator WaitTransition()
    {
        yield return new WaitForSeconds(0.3f);
        animator.SetTrigger("Start");
        if (SceneManager.GetActiveScene().buildIndex > 0 && SceneManager.GetActiveScene().buildIndex  < 8)
            input.ActivateInput();
    }
}