using UnityEngine;

public class LevelPlay : MonoBehaviour
{
    void Awake()
    {
        if (GameManager.instance != null)
            GameManager.instance.gameState = GameManager.GameState.Play;
    }
}
