using UnityEditor.MPE;
using UnityEngine;

public class GameSoundEffect : MonoBehaviour
{
    private static AudioSource audioSource;

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

    public static void PlaySound(AudioClip clip, float volume = 1f)
    {
        UpdateAudioSource();
        if (null != clip)
        {
            // if(null != OptionGame.volume)
            // {
            //     volume = OptionGame.volume;
            // }
            audioSource.PlayOneShot(clip, volume);
        }
        else
        {
            Debug.LogWarning("Attention Clip audio manquant.");
        }
    }

}
