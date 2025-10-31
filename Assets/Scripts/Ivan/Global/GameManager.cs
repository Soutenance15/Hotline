using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject pauseMenu;
    public GameObject gameOverMenu;

    public Action OnResumeFromGameOver;

    public enum GameState
    {
        Play,
        Pause,
        TitleMenu,
        GameOver,
    }

    public GameState gameState = GameState.TitleMenu;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Menu
    void OnEnable()
    {
        UIMenuEvent.OnMenuChange += HandleMenuChange;
    }

    void HandleMenuChange(UIMenuEvent.MenuState menuState)
    {
        switch (menuState)
        {
            case UIMenuEvent.MenuState.PlayGame:
                LoadScene("FirstScene");
                break;
            case UIMenuEvent.MenuState.ResumeGame:
                ResumeGame();
                break;
            case UIMenuEvent.MenuState.ResumeGameFromGameOver:
                ResumeGameFromGameOver();
                break;
            case UIMenuEvent.MenuState.BackGame:
                LoadScene("TitleMenu");
                break;
            case UIMenuEvent.MenuState.Quit:
                QuitApplication();
                break;
        }
    }

    public void RegisterMenuPause(GameObject pauseMenu)
    {
        this.pauseMenu = pauseMenu;
    }

    public void RegisterGameOverMenu(GameObject gameOverMenu)
    {
        this.gameOverMenu = gameOverMenu;
    }

    // Functions Menu

    void OnPause()
    {
        if (gameState == GameState.Pause)
        {
            ResumeGame();
        }
        else if (gameState == GameState.Play)
        {
            PauseGame();
        }
    }

    public void GameOver()
    {
        gameState = GameState.GameOver;
        if (null != gameOverMenu)
        {
            gameOverMenu.SetActive(true);
        }
    }

    void StopTime()
    {
        Time.timeScale = 0f;
    }

    void PlayTime()
    {
        Time.timeScale = 1f;
    }

    void PauseGame()
    {
        if (null != pauseMenu)
        {
            ShowMenu(pauseMenu, true);
            gameState = GameState.Pause;
            StopTime();
        }
    }

    public void ResumeGameFromGameOver()
    {
        OnResumeFromGameOver?.Invoke();
        ResumeGame();
    }

    public void ResumeGame()
    {
        if (null != pauseMenu)
        {
            ShowMenu(pauseMenu, false);
            gameState = GameState.Play;
        }
        if (null != gameOverMenu)
        {
            ShowMenu(gameOverMenu, false);
        }
        PlayTime();
    }

    void QuitApplication()
    {
        Debug.Log("Quitter Application");
    }

    public void LoadScene(string nameScene)
    {
        SceneManager.LoadScene(nameScene);
    }

    // Display
    public void ShowMenu(GameObject menu, bool show)
    {
        if (null != menu)
        {
            menu.SetActive(show);
        }
    }
}
