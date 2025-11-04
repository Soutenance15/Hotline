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

    private Animator animator;
    private Vector3 lastPosition;

    void Start()
    {
        animator = enemy.GetComponent<Animator>();

        if (patrolPoints.Count > 0)
        {
            enemy.transform.position = patrolPoints[0].position;
            targetPoint = patrolPoints[0];
        }

        lastPosition = enemy.transform.position;
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

        if (targetPoint == patrolPoints[1])
        {
            enemy.transform.localScale = new Vector3(1, 1, 1); // Face gauche
        }
        else if (targetPoint == patrolPoints[0])
        {
            enemy.transform.localScale = new Vector3(-1, 1, 1); // Face droite
        }

        float moveDistance = (enemy.transform.position - lastPosition).magnitude;
        if (animator != null)
        {
            animator.SetBool("IsWalking", moveDistance > 0.01f);
        }

        lastPosition = enemy.transform.position;
    }

    public void StopMovement()
    {
        speed = 0f;
        looping = false;
        if (animator != null)
        {
            animator.SetBool("IsWalking", false);
        }
    }

    public void ResumeMovement()
    {
        speed = 2f;
        looping = true;
    }
}
