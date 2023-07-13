using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    Animator animator;

    int currentSceneIndex;

    [SerializeField] float timeDelay;

    void Start() 
    {
        animator = GetComponent<Animator>();

        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        StartCoroutine(LoadNextLevel(timeDelay));
        animator.SetTrigger("isHoisting");
    }

    IEnumerator LoadNextLevel(float timeDelay)
    {
        animator.SetTrigger("isMoving");
        yield return new WaitForSeconds(timeDelay);

        if(currentSceneIndex+1 < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(currentSceneIndex+1);
    }
}