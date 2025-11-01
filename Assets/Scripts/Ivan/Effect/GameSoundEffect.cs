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

    // Joue un son "global" (2D)
    public static void PlaySound(AudioClip clip, float volume = 1f)
    {
        UpdateAudioSource();
        if (null != clip)
        {
            audioSource.PlayOneShot(clip, volume);
            Debug.Log("Play Sound");
        }
        else
        {
            Debug.Log("CLIP IS NULL****");
        }
    }

    public static void DieSoundEffectEnemy(AudioClip clip, float volume = 1f)
    {
        PlaySound(clip, volume);
    }

    // // Joue un son à une position donnée (3D)
    // public static void PlaySoundAt(AudioClip clip, Vector3 position, float volume = 1f)
    // {
    //     if (clip != null)
    //     {
    //         AudioSource.PlayClipAtPoint(clip, position, volume);
    //     }
    // }
}
