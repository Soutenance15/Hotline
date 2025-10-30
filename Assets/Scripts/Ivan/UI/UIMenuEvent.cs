using System;
using UnityEngine;

public class UIMenuEvent : MonoBehaviour
{
    public static Action<MenuState> OnMenuChange;
    public GameObject menuTitle;
    public GameObject menuPause;

    public enum MenuState
    {
        None,
        PlayGame,
        ResumeGame,
        BackGame,
        Quit,
    }

    public MenuState menuState;

    // void Awake()
    // {
    //     if (null != menuTitle)
    //     {
    //         menuTitle = GameObject.Find("MenuTitle");
    //     }
    //     if (null != menuPause)
    //     {
    //         menuPause = GameObject.Find("MenuPause");
    //     }
    // }

    void OnEnable()
    {
        UITitleMenu.OnPlay += PlayGame;
        UITitleMenu.OnQuit += Quit;

        UIPauseMenu.OnResume_Pause += ResumeGame;
        UIPauseMenu.OnBack_Pause += BackTitle;
        UIPauseMenu.OnQuit_Pause += Quit;
    }

    void OnDisable()
    {
        UITitleMenu.OnPlay -= PlayGame;
        UITitleMenu.OnQuit -= Quit;

        UIPauseMenu.OnResume_Pause += ResumeGame;
        UIPauseMenu.OnBack_Pause += BackTitle;
        UIPauseMenu.OnQuit_Pause += Quit;
    }

    void PlayGame()
    {
        menuState = MenuState.PlayGame;
        OnMenuChange?.Invoke(menuState);
    }

    void Quit()
    {
        menuState = MenuState.Quit;
        OnMenuChange?.Invoke(menuState);
    }

    void ResumeGame()
    {
        menuState = MenuState.ResumeGame;
        OnMenuChange?.Invoke(menuState);
    }

    void BackTitle()
    {
        menuState = MenuState.BackGame;
        OnMenuChange?.Invoke(menuState);
    }
}
