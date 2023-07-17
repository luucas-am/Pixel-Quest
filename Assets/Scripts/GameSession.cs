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
}