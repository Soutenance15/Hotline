using UnityEngine;

public class GameVisualEffect : MonoBehaviour
{
    public static void ShootEffect(Transform transform)
    {
        // Debug.Log("Visual Effect x: " + transform.position.x.ToString());
        // Debug.Log("Visual Effect y: " + transform.position.y.ToString());
    }

    public static void DieEffectEnemy(Transform transform, GameObject prefabDieText)
    {
        // Instancier le prefab
        Instantiate(prefabDieText, transform.position, Quaternion.identity);
    }
}
