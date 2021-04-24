using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseChecker : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;

    void Start()
    {
        pauseMenuUI.SetActive(false);
    }
    void Update()
    {   
        // Access pause menu when user presses escape key
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (GameIsPaused) {
                Resume();
            } else {
                Pause();
            }
        }
    }

    // Resume game
    public void Resume() 
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;    // Resumes game, sets time scale to normal
        GameIsPaused = false;
    }

    // Pause game
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;    // Pauses game, stops time scale
        GameIsPaused = true;
    }
}
