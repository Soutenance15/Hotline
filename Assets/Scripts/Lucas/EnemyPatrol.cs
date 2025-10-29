using UnityEngine;
using System.Collections.Generic;

public class EnemyPatrol : MonoBehaviour
{
    public List<Transform> patrolPoints;
    public float speed = 2f;
    private int currentPointIndex = 0;
    public GameObject enemy;
    private Transform targetPoint;
    private bool looping = true;


    void Start()
    {
        if (patrolPoints.Count > 0)
        {
            enemy.transform.position = patrolPoints[0].position;
            targetPoint = patrolPoints[0];
        }
    }

    void FixedUpdate()
    {
        if (patrolPoints.Count == 0) return;

        Vector3 direction = (targetPoint.position - enemy.transform.position).normalized;
        Vector3 movement = direction * speed * Time.fixedDeltaTime;
        enemy.transform.position += movement;

        if (Vector3.Distance(enemy.transform.position, targetPoint.position) < 0.1f && looping)
        {
            currentPointIndex = (currentPointIndex + 1) % patrolPoints.Count;
            targetPoint = patrolPoints[currentPointIndex];
        }
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
    void Update()
    {
        
    }
}
