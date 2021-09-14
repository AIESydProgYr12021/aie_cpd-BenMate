using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static Score instance;

    public Text scoreText;
    
    public int playerScore = 0;
    public int playerPowerUps = 0;


    private void Awake()
    {
        if (instance == null)       
            instance = this;    
        else      
            Destroy(this);
        
    }

    private void Start()
    {
        scoreText.text = "Score : " + playerScore.ToString();       
    }

    public void UpdateScore(int amountToAdd)
    {
        playerScore += amountToAdd;
        scoreText.text = "Score : " + playerScore.ToString(); 
    }

    public void UpdatePowerUpCount()
    {
        playerPowerUps++;
    }

}
