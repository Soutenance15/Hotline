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

    public void OnEnable()
    {
        UITitleMenu.OnPlay += PlayGame;
        UITitleMenu.OnQuit += Quit;

        UIPauseMenu.OnResume_Pause += ResumeGame;
        UIPauseMenu.OnBack_Pause += BackTitle;
        UIPauseMenu.OnQuit_Pause += Quit;
    }

    public void OnDisable()
    {
        UITitleMenu.OnPlay -= PlayGame;
        UITitleMenu.OnQuit -= Quit;

        UIPauseMenu.OnResume_Pause += ResumeGame;
        UIPauseMenu.OnBack_Pause += BackTitle;
        UIPauseMenu.OnQuit_Pause += Quit;
    }

    public void PlayGame()
    {
        menuState = MenuState.PlayGame;
        OnMenuChange?.Invoke(menuState);
    }

    public void Quit()
    {
        menuState = MenuState.Quit;
        OnMenuChange?.Invoke(menuState);
    }

    public void ResumeGame()
    {
        menuState = MenuState.ResumeGame;
        OnMenuChange?.Invoke(menuState);
    }

    public void BackTitle()
    {
        menuState = MenuState.BackGame;
        OnMenuChange?.Invoke(menuState);
    }
}
