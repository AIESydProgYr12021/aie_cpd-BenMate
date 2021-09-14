using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HighScore : MonoBehaviour
{
    public TMP_Text powerUpText;
    public TMP_Text scoreText;
    public Score score;
    
    void OnEnable()
    {
        powerUpText.text = $"{score.playerPowerUps}";
        scoreText.text = $"{score.playerScore}";

        //save the highscore if its a better score
        if (score.playerScore > PlayerPrefs.GetInt("HScore")) PlayerPrefs.SetInt("HScore", score.playerScore);
            
    }
}
