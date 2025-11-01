using System;
using UnityEngine;
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

    public static void InstantiateIfNeeded()
    {
        if (instance == null)
        {
            // Crée un nouvel objet dans la scène
            GameObject gm = new GameObject("GameManager");
            instance = gm.AddComponent<GameManager>();
            DontDestroyOnLoad(gm);
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
        ShowMenu(this.pauseMenu, false);
    }

    public void RegisterGameOverMenu(GameObject gameOverMenu)
    {
        this.gameOverMenu = gameOverMenu;
        ShowMenu(this.gameOverMenu, false);
        Debug.Log("Call Register Game Over Menu");
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
        Debug.Log("GameOver appel");
        gameState = GameState.GameOver;
        // if (null != gameOverMenu)
        // {
        //     gameOverMenu.SetActive(true);
        //     Debug.Log("gameOverMenu existe");
        // }
        // else
        // {
        // gameOverMenu = GameObject.Find("GameOverMenu");
        if (null != gameOverMenu)
        {
            gameOverMenu.SetActive(true);
            Debug.Log("gameOverMenu existe");
        }
        // }
        StopTime();
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
