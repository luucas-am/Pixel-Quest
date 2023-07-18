using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] float timeDelay;
    public void PlayGame()
    {
        FindObjectOfType<GameSession>().LoadNextLevel(timeDelay);
    }
}