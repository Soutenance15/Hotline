using UnityEngine;

public class GameSoundEffect : MonoBehaviour
{
    private static AudioSource audioSource;
    private static AudioSource audioSourceMusic;

    public static void UpdateAudioSourceMusic()
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
                    audioSourceMusic.volume = OptionManager.GetVolumeMusic();
                }
            }
        }
    }

    public static void UpdateAudioSource()
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
                    audioSource.volume = OptionManager.GetVolumeEffect();
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

    public static void PlaySound(AudioClip clip)
    {
        UpdateAudioSource();
        if (null != clip)
        {
            float volumeOtion = OptionManager.GetVolumeEffect();
            audioSource.PlayOneShot(clip, volumeOtion);
        }
        else
        {
            Debug.LogWarning("Attention Clip audio manquant.");
        }
    }

    public static void PlaySoundMusic(AudioClip clip)
    {
        UpdateAudioSourceMusic();
        if (null != clip)
        {
            float volumeOtion = OptionManager.GetVolumeMusic();
            audioSourceMusic.PlayOneShot(clip, volumeOtion);
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
