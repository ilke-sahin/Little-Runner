using UnityEngine;
using TMPro;

public class CoinCounter : MonoBehaviour
{
    public TMP_Text numOfCoinsText;
    public TMP_Text gameOverCoinsText;

    private int numOfCoins = 0;

    void Start()
    {
        numOfCoinsText.text = "Coins: " + numOfCoins.ToString();
    }

    public void AddCoin()
    {
        numOfCoins++;
        numOfCoinsText.text = "Coins: " + numOfCoins.ToString();
    }

    public void ShowFinalCoinCount()
    {
        gameOverCoinsText.text = "Coins: " + numOfCoins.ToString();
    }
}
