using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndOfGame : MonoBehaviour
{
    private float gameOverTimer = 0f;
    public static bool gameOver;
    public GameObject gameOverPanel;
    public float delay = 0.5f;

    void Start()
    {
        gameOver = false;
        Time.timeScale = 1;
        gameOverPanel.SetActive(false); // Ensure the panel is initially hidden
    }

    void Update()
    {
        if (gameOver)
        {
            gameOverTimer += Time.deltaTime;
            if (gameOverTimer >= delay)
            {
                Time.timeScale = 0;
                gameOverPanel.SetActive(true);
                gameOver = false;
                gameOverTimer = 0f;
            }
        }
        
    }

    public static void TriggerGameOver()
    {
        gameOver = true;
    }

}
