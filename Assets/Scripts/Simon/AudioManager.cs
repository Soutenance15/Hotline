using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [Header("-----------------Audio Source-----------------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("-----------------Music Clips-----------------")]
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

    [Header("------------------- Settings -----------------")]
    [Range(0f, 5f)] public float fadeDuration = 1.5f;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;

        musicSource.clip = musicMenu;
        musicSource.volume = 1f;
        musicSource.Play();
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
        AudioClip nextClip = null;

        if (sceneName == "TitleMenu") nextClip = musicMenu;
        else if (sceneName.StartsWith("Story")) nextClip = musicNarratif;
        else if (sceneName == "Underground") nextClip = musicLevel1;
        else if (sceneName == "Hall") nextClip = musicLevel2;
        else if (sceneName == "Level3") nextClip = musicLevel3;

        if (nextClip != null && nextClip != musicSource.clip)
        {
            StartCoroutine(FadeToNewMusic(nextClip));
        }
    }

    private IEnumerator FadeToNewMusic(AudioClip newClip)
    {
        if (musicSource.isPlaying)
        {
            float startVolume = musicSource.volume;

            for (float t = 0; t < fadeDuration; t += Time.deltaTime)
            {
                musicSource.volume = Mathf.Lerp(startVolume, 0, t / fadeDuration);
                yield return null;
            }

            musicSource.volume = 0;
            musicSource.Stop();
        }

        musicSource.clip = newClip;
        musicSource.Play();

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            musicSource.volume = Mathf.Lerp(0, 1f, t / fadeDuration);
            yield return null;
        }

        musicSource.volume = 1f;
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip != null)
            SFXSource.PlayOneShot(clip);
    }

    public void PlayTableSound()
    {
        PlaySFX(table);
    }

    public void PlayTypewriterSoundLoop()
    {
        if (typewriter != null)
        {
            SFXSource.loop = true;
            SFXSource.clip = typewriter;
            SFXSource.Play();
        }
    }

    public void StopTypewriterSound()
    {
        if (SFXSource.isPlaying && SFXSource.clip == typewriter)
        {
            SFXSource.Stop();
            SFXSource.loop = false;
            SFXSource.clip = null;
        }
    }
}
