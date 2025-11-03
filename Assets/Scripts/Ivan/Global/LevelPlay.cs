using UnityEngine;

public class LevelPlay : MonoBehaviour
{
    public GameObject enemies;
    public GameObject playerObject;
    private PlayerController playerController;
    public AudioSource audioSource;
    public AudioSource audioSourceMusic;
    public AudioClip musicLevel;

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
        else
        {
            GameManager.InstantiateIfNeeded();
            Debug.LogWarning("Attention GameManager instance était NULL. au départ");
        }

        // Créer un AudioSource s'il n'existe pas déjà
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();

            // Configuration du son global 2D
            audioSource.playOnAwake = false;
            audioSource.spatialBlend = 0f; // 0 = 2D, 1 = 3D
            audioSource.volume = 1f;
        }

        if (null == playerObject)
        {
            playerObject = GameObject.Find("PlayerController");
        }
        if (null != playerObject)
        {
            playerController = playerObject.GetComponent<PlayerController>();
        }

        if (null == enemies)
        {
            enemies = GameObject.Find("Enemies");
        }
    }

    void Start()
    {
        GameSoundEffect.SetAudioSource(audioSource);
        GameSoundEffect.SetAudioSourceMusic(audioSourceMusic);
        GameSoundEffect.PlaySoundMusic(musicLevel);
    }

    void RestartLevel()
    {
        GameSoundEffect.StopAudioSource();
        RespawnEnemies();
        RespawnPlayer();
    }

    void RespawnPlayer()
    {
        if (null != playerController)
        {
            playerController.transform.position = playerController.spawnPosition;
            playerController.health.currentHealth = playerController.health.maxHealth;
            playerController.health.isAlive = true;
            playerController.health.UpdateHealthBar();
            playerController.ShowWeaponHUD(false);
        }
    }

    void RespawnEnemies()
    {
        if (null != enemies)
        {
            foreach (Transform child in enemies.transform)
            {
                GameObject enemyObject = child.gameObject;
                EnemyPatrol enemyPatrol = enemyObject.GetComponent<EnemyPatrol>();
                enemyPatrol.ForRespawn();
            }
        }
    }
}
