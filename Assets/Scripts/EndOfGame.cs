using UnityEngine;
using TMPro;

public class EndOfGame : MonoBehaviour
{
    private float gameOverTimer = 0f;
    public static bool gameOver;
    public GameObject gameOverPanel;
    public float delay = 0.5f;
    private CoinCounter coinCounter;
    public TMP_Text highestCoinText;
    public GameObject highScoreContainer;

    private void Start()
    {
        gameOver = false;
        Time.timeScale = 1;
        gameOverPanel.SetActive(false);
        coinCounter = GameObject.FindObjectOfType<CoinCounter>();
        highestCoinText.text = "Highest Coin: " + PlayerPrefs.GetInt("HighestCoin", 0).ToString(); 
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
                highScoreContainer.SetActive(true);

                gameOver = false;
                gameOverTimer = 0f;

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
