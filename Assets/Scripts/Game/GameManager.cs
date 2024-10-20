using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private bool pauseMenuOpen;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject gameOverMenu;


    private void OnEnable()
    {
        PlayerInventory._gameOver += EndGame;
        InputMonitor._pause += PauseGame;
        PauseMenu._resume += Resume;
        PauseMenu._exit += ExitGame;
        GameOver._resume += Resume;
        GameOver._exit += ExitGame;
    }

    private void OnDisable()
    {
        PlayerInventory._gameOver -= EndGame;
        InputMonitor._pause -= PauseGame;
        PauseMenu._resume -= Resume;
        PauseMenu._exit -= ExitGame;
        GameOver._resume -= Resume;
        GameOver._exit -= ExitGame;
    }

    private void EndGame()
    {
        Debug.Log("Game Over Man");
    }

    private void PauseGame()
    {
        if (!pauseMenuOpen)
        {
            Debug.Log("Pause for the cause");
            Pause();

        }
        else if(pauseMenuOpen)
        {
            Debug.Log("Read my resume");
            Resume();
        }
    }

    private void Pause()
    {
        pauseMenuOpen = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    private void Resume()
    {
        pauseMenuOpen = false;
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }

    private void ExitGame()
    {
        SceneManager.LoadSceneAsync("MainMenu");
        Resume();
    }
}
