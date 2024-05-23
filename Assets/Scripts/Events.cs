using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Events : MonoBehaviour
{
    public void ReplayGame()
    {
        // Ensure the game is not paused when restarting
        Time.timeScale = 1;
        SceneManager.LoadScene("Level");
        Debug.Log("ReplayGame function called.");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
