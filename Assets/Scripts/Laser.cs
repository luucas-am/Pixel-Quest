using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    Rigidbody2D rgdb;
    PlayerMovement player;

    float xSpeed;
    [SerializeField] float laserSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rgdb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
        xSpeed = laserSpeed*player.transform.localScale.x;
        transform.localScale = player.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        rgdb.velocity = new Vector2(xSpeed, 0f);
    }

    void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.tag == "Enemy")
            Destroy(other.gameObject, 0.1f);

        Destroy(gameObject);
    }
}