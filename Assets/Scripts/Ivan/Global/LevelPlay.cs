using UnityEngine;

public class LevelPlay : MonoBehaviour
{
    public GameObject enemies;
    public GameObject playerObject;

    private PlayerController playerController;

    void OnEnable()
    {
        if (null != GameManager.instance)
        {
            GameManager.instance.OnResumeFromGameOver += RestartLevel;
        }
    }

    void OnDisable()
    {
        if (null != GameManager.instance)
        {
            GameManager.instance.OnResumeFromGameOver -= RestartLevel;
        }
    }

    void Awake()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.gameState = GameManager.GameState.Play;
            GameManager.instance.ResumeGame();
        }

        if (null == playerObject)
        {
            playerObject = GameObject.Find("PlayerController");
        }
        if (null != playerObject)
        {
            playerController = playerObject.GetComponent<PlayerController>();
            Debug.Log("playerObject Non Null");
        }
        if (null != playerController)
        {
            Debug.Log("playerController Non Null");
        }

        if (null == enemies)
        {
            enemies = GameObject.Find("Enemies");
        }
    }

    void RestartLevel()
    {
        RespawnEnemies();
        RespawnPlayer();
        Debug.Log("Restart Level");
    }

    void RespawnPlayer()
    {
        if (null != playerController)
        {
            playerController.transform.position = playerController.spawnTransform.position;
            // playerController.health.currentHealth = playerController.health.maxHealth;
            playerController.health.currentHealth = 50;
            Debug.Log("player non null");
        }
    }

    void RespawnEnemies()
    {
        if (null != enemies)
        {
            Debug.Log("enemies non null");
            foreach (Transform child in enemies.transform)
            {
                GameObject enemyObject = child.gameObject;
                EnemyPatrol enemyPatrol = enemyObject.GetComponent<EnemyPatrol>();
                enemyPatrol.transform.position = enemyPatrol.spawnTransform.position;
                enemyPatrol.health.currentHealth = enemyPatrol.health.maxHealth;
            }
        }
    }
}
