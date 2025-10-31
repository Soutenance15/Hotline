using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [Header("-----------------Audio Source-----------------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("-----------------Audio Source-----------------")]
    public AudioClip musicMenu;
    public AudioClip musicLevel1;
    public AudioClip musicLevel2;
    public AudioClip musicLevel3;
    public AudioClip musicNarratif;

    [Header("---------------------- SFX -------------------")]
    public AudioClip shoot1;
    public AudioClip shoot2;
    public AudioClip door;
    public AudioClip table;
    public AudioClip die;
    public AudioClip click;
    public AudioClip ammo;
    public AudioClip typewriter;

   
    private static AudioManager instance = null;
    public static AudioManager Instance => instance;

    void Awake()
    {
        instance = this;

        if (Object.FindObjectsByType<AudioManager>(FindObjectsSortMode.None).Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        if (musicSource.clip != musicMenu)
        {
            musicSource.clip = musicMenu;
            musicSource.Play();
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void PlayTableSound()
    {
        if (table != null && SFXSource != null)
        {
            SFXSource.PlayOneShot(table);
        }
    }

    public void PlayTypewriterSoundLoop()
    {
        if (typewriter != null && SFXSource != null)
        {
            SFXSource.loop = true;
            SFXSource.clip = typewriter;
            SFXSource.Play();
        }
    }

    public void StopTypewriterSound()
    {
        if (SFXSource != null && SFXSource.isPlaying && SFXSource.clip == typewriter)
        {
            SFXSource.Stop();
            SFXSource.loop = false;
            SFXSource.clip = null;
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Start()
    {
        PlayMusicForScene(SceneManager.GetActiveScene().name);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayMusicForScene(scene.name);
    }

    void PlayMusicForScene(string sceneName)
    {
        if (sceneName == "TitleMenu" && musicSource.clip != musicMenu)
        {
            musicSource.clip = musicMenu;
            musicSource.Play();
        }
        else if ((sceneName == "Story1" || sceneName == "Story2" || sceneName == "Story3" || sceneName == "Story4" || sceneName == "Story5" || sceneName == "Story6") && musicSource.clip != musicNarratif)
        {
            musicSource.clip = musicNarratif;
            musicSource.Play();
        }
        else if (sceneName == "Level1" && musicSource.clip != musicLevel1)
        {
            musicSource.clip = musicLevel1;
            musicSource.Play();
        }
        else if (sceneName == "Level2" && musicSource.clip != musicLevel2)
        {
            musicSource.clip = musicLevel2;
            musicSource.Play();
        }
        else if (sceneName == "Level3" && musicSource.clip != musicLevel3)
        {
            musicSource.clip = musicLevel3;
            musicSource.Play();
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}