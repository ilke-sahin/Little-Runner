using UnityEngine;
using TMPro;

public class CoinCounter : MonoBehaviour
{
    public TMP_Text numOfCoinsText;
    public TMP_Text gameOverCoinsText;
    public TMP_Text highestCoinText; 

    private int numOfCoins = 0;
    private int highestCoin = 0; 

    void Start()
    {
        numOfCoinsText.text = "Coins: " + numOfCoins.ToString();
        highestCoin = PlayerPrefs.GetInt("HighestCoin", 0); 
        highestCoinText.text = "Highest Coin: " + highestCoin.ToString(); 
    }

    public void AddCoin()
    {
        numOfCoins++;
        numOfCoinsText.text = "Coins: " + numOfCoins.ToString();
        if (numOfCoins > highestCoin) 
        {
            highestCoin = numOfCoins;
            PlayerPrefs.SetInt("HighestCoin", highestCoin);
            highestCoinText.text = "Highest Coin: " + highestCoin.ToString(); 
        }
    }

    public void ShowFinalCoinCount()
    {
        gameOverCoinsText.text = "Coins: " + numOfCoins.ToString();
    }
}
