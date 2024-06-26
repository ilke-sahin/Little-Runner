using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private float score = 0.0f;
    private float difficultyLevel = 1;
    private int maxDifficultyLevel = 10;
    private int scoreToNextLevel = 10;

    public Text scoreText;

    // Update is called once per frame
    void Update()
    {
        if(score >= scoreToNextLevel)
           LevelUp ();

         score += Time.deltaTime * difficultyLevel;
         scoreText.text = ((int)score).ToString ();
        
    }

     void LevelUp()
    {
        if(difficultyLevel == maxDifficultyLevel)
        return;

        scoreToNextLevel *= 2;
        difficultyLevel+=0.5f;
       GetComponent<PlayerMotor>().Setspeed (difficultyLevel);

       Debug.Log (difficultyLevel);
    }

}
