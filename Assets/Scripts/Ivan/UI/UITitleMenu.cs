using System;
using UnityEngine;
using UnityEngine.UI;

public class UITitleMenu : MonoBehaviour
{
    Button playButton;
    Button quitButton;

    public static Action OnPlay;
    public static Action OnQuit;

    void Awake()
    {
        if (null == playButton)
        {
            playButton = GameObject.Find("PlayButton").GetComponent<Button>();
        }
        if (null == quitButton)
        {
            quitButton = GameObject.Find("QuitButton").GetComponent<Button>();
        }

        // Init All Buttons

        if (null != playButton)
        {
            playButton.onClick.AddListener(() => OnClickPlay());
        }
        if (null != quitButton)
        {
            quitButton.onClick.AddListener(() => OnClickQuit());
        }
    }

    void OnClickPlay()
    {
        OnPlay?.Invoke();
    }

    void OnClickQuit()
    {
        OnQuit?.Invoke();
    }
}
