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

    //sound effect
    SoundEffectsPLayer soundEffectsplayer;

    private void Awake()
    {
        soundEffectsplayer = GameObject.FindGameObjectWithTag("Audio").GetComponent<SoundEffectsPLayer>();
    }

    void Start()
    {
        gameOver = false;
        Time.timeScale = 1;
        gameOverPanel.SetActive(false); // panel is hidden at first
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

                soundEffectsplayer.PlaySFX(soundEffectsplayer.endgame);//sfx
                soundEffectsplayer.StopBackgroundMusic(); //end music
            }
        }
        
    }

    public static void TriggerGameOver()
    {
        gameOver = true;
         
    }

}
