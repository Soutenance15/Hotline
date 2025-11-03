using UnityEngine;

public class OptionMenuRegister : MonoBehaviour
{
    void Awake()
    {
        if (GameManager.instance == null)
        {
            GameManager.InstantiateIfNeeded();
        }

        if (GameManager.instance != null)
        {
            GameManager.instance.OptionMenuPause(gameObject);
        }
    }
}
