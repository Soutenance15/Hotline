using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // UIMenuEvent uIMenuManager;
    // UITitleMenu uITitleMenu;

    // void Awake()
    // {
    //     uIMenuManager = GetComponent<UIMenuEvent>();
    //     uITitleMenu = GetComponent<UITitleMenu>();
    // }

    void OnEnable()
    {
        UIMenuEvent.OnMenuChange += HandleMenuChange;
    }

    void HandleMenuChange(UIMenuEvent.MenuState menuState)
    {
        switch (menuState)
        {
            case UIMenuEvent.MenuState.PlayGame:
                SceneManager.LoadScene("PlayerScene");
                break;
            case UIMenuEvent.MenuState.ResumeGame:
                ResumeGame();
                break;
            case UIMenuEvent.MenuState.BackGame:
                SceneManager.LoadScene("TitleMenu");
                break;
            case UIMenuEvent.MenuState.Quit:
                QuitApplication();
                break;
            // case UIMenuEvent.MenuState.None:
            //     break;
        }

        // Functions
        void ResumeGame()
        {
            Debug.Log("Resume Game");
        }
        void QuitApplication()
        {
            Debug.Log("Quitter Application");
        }
    }
}
