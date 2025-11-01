using UnityEngine;

public class PauseMenuRegister : MonoBehaviour
{
    void Awake()
    {
        if (GameManager.instance == null)
        {
            GameManager.InstantiateIfNeeded();
        }

        if (GameManager.instance != null)
        {
            GameManager.instance.RegisterMenuPause(gameObject);
        }
    }
}
