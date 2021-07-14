using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;

    public GameObject pauseMenu;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                ResumeGame();
            }
            else 
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void LoasMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }
}
