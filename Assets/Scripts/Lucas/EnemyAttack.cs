using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public GameObject projectile;
    public GameObject player;
    public Transform shootPos;
    public float timer;
    public float shootDistance;
    EnemyPatrol patrol;

    public bool isAlive = true;

    // Effet
    public AudioClip shootClip;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (null != player && isAlive)
        {
            shootDistance = Vector2.Distance(transform.position, player.transform.position);
        }
    }

    public void ForRespawnAttack()
    {
        isAlive = true;
    }

    void FixedUpdate()
    {
        if (isAlive)
        {
            if (shootDistance < 5)
            {
                patrol = GetComponent<EnemyPatrol>();
                patrol.StopMovement();
                timer += Time.deltaTime;
                if (timer > 1)
                {
                    timer = 0;
                    Shoot();
                }
            }
            else if (shootDistance > 5)
            {
                patrol = GetComponent<EnemyPatrol>();
                patrol.ResumeMovement();
            }
        }
    }

    public void Shoot()
    {
        Instantiate(projectile, shootPos.position, Quaternion.identity);
        GameSoundEffect.PlaySound(shootClip);
    }
}
