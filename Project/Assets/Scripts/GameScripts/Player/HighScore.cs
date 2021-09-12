using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
    public Text text;
    public Score score;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnEnable()
    {
        text.text = "Score : " + score.playerScore.ToString();

        //save the highscore if its a better score
        if (score.playerScore > PlayerPrefs.GetInt("HScore"))
        {
            PlayerPrefs.SetInt("HScore", score.playerScore);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
