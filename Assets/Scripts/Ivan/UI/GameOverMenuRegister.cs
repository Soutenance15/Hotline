using UnityEngine;

public class GameOverMenuRegister : MonoBehaviour
{
    void Awake()
    {
        if (GameManager.instance == null)
        {
            GameManager.InstantiateIfNeeded();
        }

        if (GameManager.instance != null)
        {
            GameManager.instance.RegisterGameOverMenu(gameObject);
        }
    }
}
