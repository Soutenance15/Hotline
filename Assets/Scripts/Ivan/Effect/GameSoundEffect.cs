using Unity.VisualScripting;
using UnityEditor.MPE;
using UnityEngine;

public class GameSoundEffect : MonoBehaviour
{
    private static AudioSource audioSource;
    private static AudioSource audioSourceMusic;

    private static void UpdateAudioSourceMusic()
    {
        // Recupere l'adioSource du LevelPlay s'il existe
        GameObject levelPlayObject = GameObject.Find("LevelPlay");
        if (null != levelPlayObject)
        {
            LevelPlay levelPlay = levelPlayObject.GetComponent<LevelPlay>();
            if (null != levelPlay)
            {
                if (null != levelPlay.audioSourceMusic)
                {
                    audioSourceMusic = levelPlay.audioSourceMusic;
                }
            }
        }
    }

    private static void UpdateAudioSource()
    {
        // Recupere l'adioSource du LevelPlay s'il existe
        GameObject levelPlayObject = GameObject.Find("LevelPlay");
        if (null != levelPlayObject)
        {
            LevelPlay levelPlay = levelPlayObject.GetComponent<LevelPlay>();
            if (null != levelPlay)
            {
                if (null != levelPlay.audioSource)
                {
                    audioSource = levelPlay.audioSource;
                }
            }
        }
    }

    public static void SetAudioSource(AudioSource audioSource)
    {
        GameSoundEffect.audioSource = audioSource;
    }

    public static void SetAudioSourceMusic(AudioSource audioSourceMusic)
    {
        GameSoundEffect.audioSourceMusic = audioSourceMusic;
    }

    public static void PlaySound(AudioClip clip, float volume = 1f)
    {
        UpdateAudioSource();
        if (null != clip)
        {
            audioSource.PlayOneShot(clip, volume);
        }
        else
        {
            Debug.LogWarning("Attention Clip audio manquant.");
        }
    }

    public static void PlaySoundMusic(AudioClip clip, float volume = 1f)
    {
        UpdateAudioSourceMusic();
        if (null != clip)
        {
            audioSourceMusic.PlayOneShot(clip, volume);
        }
    }

    public static void StopAudioSource()
    {
        audioSource.Stop();
    }

    public static void StopAudioSourceMusic()
    {
        audioSourceMusic.Stop();
    }
}
