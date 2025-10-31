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

    public Vector3 spawnPosition;

    public Health health;

    public GameObject prefabDieText;
    public EnemyAttack enemyAttack;

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

    void Die()
    {
        Debug.Log("oui die");
        if (null != prefabDieText)
        {
            GameVisualEffect.DieEffectEnemy(transform, prefabDieText);
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
