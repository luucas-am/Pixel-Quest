using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : MonoBehaviour
{
    [SerializeField] int score = 0;
    [SerializeField] int pointsToLifeUp = 3000;
    [SerializeField] int playerHealth = 3;

    [SerializeField] TMP_Text healthText;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] Animator transitionAnimator;

    void Awake()
    {
        int numGameSession = FindObjectsOfType<GameSession>().Length;
        if (numGameSession > 1)
            Destroy(gameObject);
        else
            DontDestroyOnLoad(gameObject);
    }

    void Start() 
    {
        healthText.text = $"life: {playerHealth}";
        scoreText.text = $"score: {score}";
    }

    public void ProcessPlayerDeath(bool pitFall)
    {
        if (playerHealth > 1 && !pitFall)
        {
            playerHealth--;
            healthText.text = $"life: {playerHealth}";

            score -= 200;
            pointsToLifeUp += 200;
            scoreText.text = $"score: {score}";
        }
        else
            ResetLevel();
    }

    public void ProcessFruitPickup(int fruitPoints)
    {
        score += fruitPoints;
        scoreText.text = $"score: {score}";

        pointsToLifeUp -= fruitPoints;
        if(pointsToLifeUp <= 0)
        {
            playerHealth++;
            healthText.text = $"life: {playerHealth}";
            pointsToLifeUp = 5000;
        }
    }

    void ResetLevel()
    {
        Destroy(gameObject);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void LoadNextLevel(float timeDelay)
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(LoadLevel(timeDelay, currentSceneIndex));
    }

    IEnumerator LoadLevel(float timeDelay, int currentSceneIndex)
    {
        transitionAnimator.SetTrigger("Start");
        Debug.Log(currentSceneIndex > 0 && currentSceneIndex < SceneManager.sceneCountInBuildSettings - 1);

        yield return new WaitForSeconds(timeDelay);

        if(currentSceneIndex+1 < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(currentSceneIndex+1);
        else if (currentSceneIndex == SceneManager.sceneCountInBuildSettings - 1)
            SceneManager.LoadScene(0);

        if (currentSceneIndex+1 > 0 && currentSceneIndex+1 < SceneManager.sceneCountInBuildSettings - 1)
        {
            healthText.gameObject.SetActive(true);
            scoreText.gameObject.SetActive(true);
        }
        else
        {
            score = 0;
            playerHealth = 3;
            healthText.text = $"life: {playerHealth}";
            scoreText.text = $"score: {score}";
            
            healthText.gameObject.SetActive(false);
            scoreText.gameObject.SetActive(false);
        }
    }
}