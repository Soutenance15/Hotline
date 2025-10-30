using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject pauseMenu;

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
                // LoadScene("PlayerScene");
                LoadScene("TestScene");
                break;
            case UIMenuEvent.MenuState.ResumeGame:
                ResumeGame();
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

    void PauseGame()
    {
        if (null != pauseMenu)
        {
            ShowMenu(pauseMenu, true);
            gameState = GameState.Pause;
        }
        Debug.Log("Pause Game");
    }

    public void ResumeGame()
    {
        if (null != pauseMenu)
        {
            ShowMenu(pauseMenu, false);
            gameState = GameState.Play;
        }
        Debug.Log("Resume Game");
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
