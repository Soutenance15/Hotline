using UnityEngine;

public class GameVisualEffect : MonoBehaviour
{
    public static bool showVFX = true;

    public static void DieEffectTextEnemy(Transform transform, GameObject prefabDieText)
    {
        if (showVFX)
        {
            // Instancier le prefab
            Instantiate(prefabDieText, transform.position, Quaternion.identity);
        }
    }

    public static GameObject DieEffectBlood(Transform transform, GameObject prefabDieBlood)
    {
        if (showVFX)
        {
            // Instancier le prefab
            return Instantiate(prefabDieBlood, transform.position, Quaternion.identity);
        }
        return null;
    }

    public static void ShowVFX(bool show)
    {
        showVFX = show;
    }
}
