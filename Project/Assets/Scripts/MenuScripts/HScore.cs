using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HScore : MonoBehaviour
{
    // Start is called before the first frame update
    public Text HStext;

    private void Start()
    {
        //load highscore
        HStext.text = "HighScore : " + PlayerPrefs.GetInt("HScore");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
