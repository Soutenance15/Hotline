using TMPro;
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
        GameObject dieText = Instantiate(prefabDieText, transform.position, Quaternion.identity);

        // // Récupérer le composant TextMeshPro
        // TextMeshPro tmp = dieText.GetComponent<TextMeshPro>();

        // if (tmp != null)
        // {
        //     tmp.text = "T'es mort Batard";
        // }
    }
}
