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
        transform.position = Vector3.zero;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
