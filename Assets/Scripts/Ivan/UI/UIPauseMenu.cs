using System;
using UnityEngine;
using UnityEngine.UI;

public class UIPauseMenu : MonoBehaviour
{
    public Button resumeButton;
    public Button backButton;
    public Button quitButton;
    public Button optionButton;

    public static Action OnResume_Pause;
    public static Action OnBack_Pause;
    public static Action OnQuit_Pause;
    public static Action OnOption_Pause;

    void Awake()
    {
        if (null == resumeButton)
        {
            resumeButton = GameObject.Find("ResumeButton").GetComponent<Button>();
        }
        if (null == backButton)
        {
            backButton = GameObject.Find("BackButton").GetComponent<Button>();
        }
        if (null == quitButton)
        {
            quitButton = GameObject.Find("QuitButton").GetComponent<Button>();
        }
        if (null == optionButton)
        {
            optionButton = GameObject.Find("OptionButton").GetComponent<Button>();
        }

        // Init All Buttons

        if (null != resumeButton)
        {
            resumeButton.onClick.AddListener(() => OnClickResume());
        }
        if (null != backButton)
        {
            backButton.onClick.AddListener(() => OnClickBack());
        }
        if (null != quitButton)
        {
            quitButton.onClick.AddListener(() => OnClickQuit());
        }
        if (null != optionButton)
        {
            optionButton.onClick.AddListener(() => OnClickOption());
        }
    }

    void OnClickResume()
    {
        OnResume_Pause?.Invoke();
    }

    void OnClickBack()
    {
        OnBack_Pause?.Invoke();
    }

    void OnClickQuit()
    {
        OnQuit_Pause?.Invoke();
    }

    void OnClickOption()
    {
        OnOption_Pause?.Invoke();
    }
}
