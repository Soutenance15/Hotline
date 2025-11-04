using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelPlay : MonoBehaviour
{
    public GameObject enemies;
    public GameObject playerObject;
    private PlayerController playerController;
    public AudioSource audioSource;
    public AudioSource audioSourceMusic;
    public AudioClip musicLevel;
    public GameObject panelNextLevel;

    public string nextSceneName;
    public int enemiesTotal;
    public int nbKill;

    void OnEnable()
    {
        if (null != GameManager.instance)
        {
            GameManager.instance.OnResumeFromGameOver += RestartLevel;
        }
        EnemyPatrol.enemyDie += ManageDying;
    }

    void ManageDying()
    {
        nbKill += 1;
        if (null != playerController)
        {
            playerController.playerUI.UpdateNbEnemyKilledText(nbKill.ToString());
        }
        if (nbKill == enemiesTotal)
        {
            SuccessLevel();
        }
    }

    void SuccessLevel()
    {
        GameManager.instance.JustPauseTime();
        StartCoroutine(WaitAndLoadNextScene());
    }

    IEnumerator WaitAndLoadNextScene()
    {
        if (null != panelNextLevel)
        {
            panelNextLevel.SetActive(true);
        }
        yield return new WaitForSeconds(3f);
        if (null != nextSceneName && nextSceneName != "")
        {
            SceneManager.LoadScene(nextSceneName);
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
        if (null != panelNextLevel)
        {
            panelNextLevel.SetActive(false);
        }

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
        if (null == enemies)
        {
            enemies = GameObject.Find("Enemies");
        }
        enemiesTotal = enemies.transform.childCount;

        if (null != playerObject)
        {
            playerController = playerObject.GetComponent<PlayerController>();
            playerController.playerUI.UpdateNbEnemyTotalText(enemiesTotal.ToString());
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
            nbKill = 0;
            playerController.playerUI.UpdateNbEnemyKilledText(nbKill.ToString());
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
