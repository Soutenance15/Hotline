using System;
using UnityEngine;
using UnityEngine.UI;

public class UIOptionMenu : MonoBehaviour
{
    public Button backButton;
    public Slider volumeMusicSlider;
    public Slider volumeEffectSlider;
    public Button resumeButton;
    public Toggle VFXToogle;

    public static Action OnBack_Option;
    public static Action<float> OnMusicSlider_Option;
    public static Action<float> OnEffectSlider_Option;
    public static Action<bool> OnVFXToggleChanged_Option;
    public static Action OnResume_Option;

    void Awake()
    {
        if (null == backButton)
        {
            backButton = GameObject.Find("BackButton").GetComponent<Button>();
        }
        if (null == resumeButton)
        {
            resumeButton = GameObject.Find("ResumeButton").GetComponent<Button>();
        }
        if (null == volumeMusicSlider)
        {
            volumeMusicSlider = GameObject.Find("VolumeMusicSlider").GetComponent<Slider>();
        }
        if (null == volumeEffectSlider)
        {
            volumeEffectSlider = GameObject.Find("VolumeEffectSlider").GetComponent<Slider>();
        }
        if (null == VFXToogle)
        {
            VFXToogle = GameObject.Find("VFXToogle").GetComponent<Toggle>();
        }

        // Init All Buttons

        if (null != backButton)
        {
            backButton.onClick.AddListener(() => OnClickBack());
        }
        if (null != resumeButton)
        {
            resumeButton.onClick.AddListener(() => OnClickResume());
        }
        if (null != volumeMusicSlider)
        {
            volumeMusicSlider.onValueChanged.AddListener(OnMusicSliderChanged);
        }
        if (null != volumeEffectSlider)
        {
            volumeEffectSlider.onValueChanged.AddListener(OnEffectSliderChanged);
        }
        if (null != volumeEffectSlider)
        {
            VFXToogle.onValueChanged.AddListener(OnVFXToggleChanged);
        }
    }

    void OnVFXToggleChanged(bool toogle)
    {
        OnVFXToggleChanged_Option?.Invoke(toogle);
    }

    void OnMusicSliderChanged(float volume)
    {
        OnMusicSlider_Option?.Invoke(volume);
    }

    void OnEffectSliderChanged(float volume)
    {
        OnEffectSlider_Option?.Invoke(volume);
    }

    void OnClickBack()
    {
        OnBack_Option?.Invoke();
    }

    void OnClickResume()
    {
        OnResume_Option?.Invoke();
    }
}
