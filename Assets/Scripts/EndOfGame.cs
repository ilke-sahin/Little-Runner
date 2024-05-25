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
    private CoinCounter coinCounter;

    private void Start()
    {
        gameOver = false;
        Time.timeScale = 1;
        gameOverPanel.SetActive(false);
        coinCounter = GameObject.FindObjectOfType<CoinCounter>();
    }

    private void Update()
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

                // Coin sayýsýný göster
                if (coinCounter != null)
                {
                    coinCounter.ShowFinalCoinCount();
                }
            }
        }
    }

    public static void TriggerGameOver()
    {
        gameOver = true;
    }
}
