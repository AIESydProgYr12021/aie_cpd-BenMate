using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static Score instance;

    public Text scoreText;

    public int playerScore = 0;


    private void Awake()
    {
        instance = this;
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

}
