using System;
using UnityEngine;

public class UIMenuEvent : MonoBehaviour
{
    public static Action<MenuState> OnMenuChange;
    public GameObject menuTitle;
    public GameObject menuPause;
    public GameObject menuOption;

    public enum MenuState
    {
        None,
        PlayGame,
        Pause,
        ResumeGame,
        ResumeGameFromGameOver,
        BackGame,
        Option,
        Quit,
    }

    public MenuState menuState;

    void OnEnable()
    {
        UITitleMenu.OnPlay += PlayGame;
        UITitleMenu.OnQuit += Quit;

        UIPauseMenu.OnResume_Pause += ResumeGame;
        UIPauseMenu.OnBack_Pause += BackTitle;
        UIPauseMenu.OnQuit_Pause += Quit;
        UIPauseMenu.OnOption_Pause += OptionOpen;

        UIGameOverMenu.OnResumeFromGameOver_Pause += ResumeGameFromGameOver;
        UIGameOverMenu.OnBack_Pause += BackTitle;
        UIGameOverMenu.OnQuit_Pause += Quit;

        UIOptionMenu.OnMusicSlider_Option += VolumeMusic;
        UIOptionMenu.OnEffectSlider_Option += VolumeEffect;
        UIOptionMenu.OnVFXToggleChanged_Option += VFXChanged;
        UIOptionMenu.OnResume_Option += ResumeGame;
    }

    void OnDisable()
    {
        UITitleMenu.OnPlay -= PlayGame;
        UITitleMenu.OnQuit -= Quit;

        UIPauseMenu.OnResume_Pause += ResumeGame;
        UIPauseMenu.OnBack_Pause -= BackTitle;
        UIPauseMenu.OnQuit_Pause -= Quit;
        UIPauseMenu.OnOption_Pause -= OptionOpen;

        UIGameOverMenu.OnResumeFromGameOver_Pause -= ResumeGameFromGameOver;
        UIGameOverMenu.OnBack_Pause -= BackTitle;
        UIGameOverMenu.OnQuit_Pause -= Quit;

        UIOptionMenu.OnMusicSlider_Option -= VolumeMusic;
        UIOptionMenu.OnEffectSlider_Option -= VolumeEffect;
        UIOptionMenu.OnVFXToggleChanged_Option -= VFXChanged;
        UIOptionMenu.OnResume_Option -= ResumeGame;
    }

    void OptionOpen()
    {
        menuState = MenuState.Option;
        OnMenuChange?.Invoke(menuState);
    }

    void VFXChanged(bool toogle)
    {
        GameVisualEffect.ShowVFX(toogle);
    }

    void VolumeMusic(float volume)
    {
        OptionManager.VolumeMusic(volume);
        GameSoundEffect.UpdateAudioSourceMusic();
    }

    void VolumeEffect(float volume)
    {
        OptionManager.VolumeEffect(volume);
        GameSoundEffect.UpdateAudioSource();
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

    void ResumeGameFromGameOver()
    {
        menuState = MenuState.ResumeGameFromGameOver;
        OnMenuChange?.Invoke(menuState);
    }

    void BackTitle()
    {
        menuState = MenuState.BackGame;
        OnMenuChange?.Invoke(menuState);
    }
}