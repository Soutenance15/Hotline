using UnityEngine;

public class OptionManager : MonoBehaviour
{
    static float volumeMusic = 10f;
    static float volumeEffect = 10f;

    public static void VolumeMusic(float volume)
    {
        volumeMusic = volume;
    }

    public static void VolumeEffect(float volume)
    {
        volumeEffect = volume;
    }

    public static float GetVolumeMusic()
    {
        return volumeMusic;
    }

    public static float GetVolumeEffect()
    {
        return volumeEffect;
    }
}
