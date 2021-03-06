using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject WinMenu;
    public GameObject pauseButton;

    public Score score;

    bool onAndroid = false;



    void Start()
    {
#if UNITY_ANDROID
        onAndroid = true;
#endif

        if (onAndroid)
        {
            pauseButton.SetActive(true);
        }
    }


    void Update()
    {
        if (WinMenu.activeInHierarchy == false)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (gameIsPaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }

    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
        gameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0.0f;
        gameIsPaused = true;
    }


    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");

        if (score.playerScore > PlayerPrefs.GetInt("HScore"))
        {
            PlayerPrefs.SetInt("HScore", score.playerScore);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
