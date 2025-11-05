using System;
using UnityEngine;
using UnityEngine.UI;

public class UIGameOverMenu : MonoBehaviour
{
    public Button resumeButton;
    public Button backButton;
    public Button quitButton;

    public static Action OnResumeFromGameOver_Pause;
    public static Action OnBack_Pause;
    public static Action OnQuit_Pause;

    void Awake()
    {
        if (!resumeButton)
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

        // Init All Buttons

        if (!resumeButton)
        {
            resumeButton.onClick.AddListener(() => OnClickResumeFromGameOver());
        }
        if (null != backButton)
        {
            backButton.onClick.AddListener(() => OnClickBack());
        }
        if (null != quitButton)
        {
            quitButton.onClick.AddListener(() => OnClickQuit());
        }
    }

    void OnClickResumeFromGameOver()
    {
        OnResumeFromGameOver_Pause?.Invoke();
        // gameObject.transform.Find("LevelPlay").GetComponent<LevelPlay>().RestartLevel();
    }

    void OnClickBack()
    {
        OnBack_Pause?.Invoke();
    }

    void OnClickQuit()
    {
        OnQuit_Pause?.Invoke();
    }
}
