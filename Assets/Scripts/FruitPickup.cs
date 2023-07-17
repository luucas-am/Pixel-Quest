using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickFruit : MonoBehaviour
{
    Animator fruitAnimator;

    bool wasCollected = false;
    [SerializeField] int fruitPoints;

    void Start() 
    {
        fruitAnimator = GetComponent<Animator>();    
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Player" && !wasCollected)
        {
            wasCollected = true;
            fruitAnimator.SetTrigger("Collected");
            GetComponent<AudioSource>().Play();
            
            FindObjectOfType<GameSession>().ProcessFruitPickup(fruitPoints);
            StartCoroutine(FruitPickup());
        }
    }

    private IEnumerator FruitPickup()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}