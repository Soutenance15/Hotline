using UnityEngine;

public class LifeGain : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController playerController = collision.GetComponent<PlayerController>();

            playerController.health.currentHealth = playerController.health.maxHealth;
        }
    }
}
