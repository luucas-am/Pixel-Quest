using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    Animator animator;

    int currentSceneIndex;

    [SerializeField] float timeDelay;

    void Start() 
    {
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        animator.SetTrigger("isHoisting");
        animator.SetTrigger("isMoving");
        GetComponent<AudioSource>().Play();

        other.GetComponent<PlayerInput>().DeactivateInput();
        other.GetComponent<Animator>().SetTrigger("End");

        FindObjectOfType<GameSession>().LoadNextLevel(timeDelay);
    }
}