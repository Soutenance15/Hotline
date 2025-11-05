using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public List<Transform> patrolPoints;
    public float speed = 2f;
    private int currentPointIndex = 0;
    public GameObject enemy;
    private Transform targetPoint;
    private bool looping = true;

    // Composants
    public EnemyAttack enemyAttack;
    public Health health;

    // Animator animator;

    public Vector3 spawnPosition;
    private Vector3 lastPosition;

    // Effets
    public GameObject prefabDieText;
    public GameObject prefabDieBlood;
    public GameObject dieBloodObject;
    public AudioClip shootClip;
    public AudioClip dieClip;
    public static Action enemyDie;

    void OnDisable()
    {
        if (null != health)
        {
            health.OnDie -= Die;
        }
    }

    void Start()
    {
        // animator = enemy.GetComponent<Animator>();

        // Sauvegarde des positions mondiales des points
        Vector3[] worldPositions = new Vector3[patrolPoints.Count];
        for (int i = 0; i < patrolPoints.Count; i++)
        {
            worldPositions[i] = patrolPoints[i].position;
        }

        // Détache les points de leur parent
        foreach (Transform point in patrolPoints)
            point.SetParent(null);

        // Replace les points aux positions sauvegardées (au cas où)
        for (int i = 0; i < patrolPoints.Count; i++)
        {
            patrolPoints[i].position = worldPositions[i];
        }

        health = GetComponent<Health>();
        enemyAttack = GetComponent<EnemyAttack>();
        enemyAttack.shootClip = shootClip;
        if (patrolPoints.Count > 0)
        {
            enemy.transform.position = patrolPoints[0].position;
            targetPoint = patrolPoints[0];
        }
        if (null != health)
        {
            health.OnDie += Die;
        }

        spawnPosition = transform.position;
        lastPosition = enemy.transform.position;
    }

    void FixedUpdate()
    {
        if (null != health && health.isAlive)
        {
            if (patrolPoints.Count == 0)
                return;

            Vector3 direction = (targetPoint.position - enemy.transform.position).normalized;
            Vector3 movement = direction * speed * Time.fixedDeltaTime;
            enemy.transform.position += movement;

            if (Vector3.Distance(enemy.transform.position, targetPoint.position) < 0.1f && looping)
            {
                currentPointIndex = (currentPointIndex + 1) % patrolPoints.Count;
                targetPoint = patrolPoints[currentPointIndex];
            }
            float moveDistance = (enemy.transform.position - lastPosition).magnitude;
            // if (animator != null)
            // {
            //     animator.SetBool("IsWalking", moveDistance > 0.01f);
            // }

            lastPosition = enemy.transform.position;
        }
    }

    public void ForRespawn()
    {
        transform.position = spawnPosition;
        Destroy(dieBloodObject);
        health.ForRespawnHealth();
        enemyAttack.ForRespawnAttack();
    }

    void Die()
    {
        if (null != prefabDieText)
        {
            GameVisualEffect.DieEffectTextEnemy(transform, prefabDieText);
            GameSoundEffect.PlaySound(dieClip);
        }
        if (null != prefabDieBlood)
        {
            dieBloodObject = GameVisualEffect.DieEffectBlood(transform, prefabDieBlood);
        }
        health.healthBar.gameObject.SetActive(false);
        enemyAttack.isAlive = false;
        EnemyPatrol.enemyDie?.Invoke();
    }

    public void StopMovement()
    {
        speed = 0f;
        looping = false;
        // if (animator != null)
        // {
        //     animator.SetBool("IsWalking", false);
        // }
    }

    public void ResumeMovement()
    {
        speed = 2f;
        looping = true;
    }

    // Update is called once per frame
    void Update() { }
}
