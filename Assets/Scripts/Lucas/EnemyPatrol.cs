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

    public Vector3 spawnPosition;

    // Effets
    public GameObject prefabDieText;
    public GameObject prefabDieBlood;
    public GameObject dieBloodObject;
    public AudioClip shootClip;
    public AudioClip dieClip;

    void OnDisable()
    {
        if (null != health)
        {
            health.OnDie -= Die;
        }
    }

    void Start()
    {
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

        enemyAttack.isAlive = false;
    }

    public void StopMovement()
    {
        speed = 0f;
        looping = false;
    }

    public void ResumeMovement()
    {
        speed = 2f;
        looping = true;
    }

    // Update is called once per frame
    void Update() { }
}
