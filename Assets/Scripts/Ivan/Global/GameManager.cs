using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

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
                LoadScene("PlayerScene");
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

    // Functions Menu

    void ResumeGame()
    {
        Debug.Log("Resume Game");
    }

    void QuitApplication()
    {
        Debug.Log("Quitter Application");
    }

    static void LoadScene(string nameScene)
    {
        SceneManager.LoadScene(nameScene);
    }
}
