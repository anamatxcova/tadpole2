using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private audioManager audioManager;

    void Start()
    {
        audioManager = FindObjectOfType<audioManager>();
        FindObjectOfType<audioManager>().Play("Theme");
    }

    public void PlayGame()
    {
        audioManager.Play("Click");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

        public void QuitGame ()
    {
        audioManager.Play("Click");
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
