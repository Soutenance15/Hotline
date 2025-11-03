using System.Collections;
using UnityEngine;

public class LifeGain : MonoBehaviour
{
    public AudioClip healthPlus;
    public ParticleSystem particleSystemToPlay;
    public float duration = 2f; // durée d’activation

    private bool isPlaying = false;

    void Start()
    {
        if (particleSystemToPlay != null)
            particleSystemToPlay.gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Si une coroutine est déjà en cours, on la stoppe
            if (!isPlaying && particleSystemToPlay != null)
                StartCoroutine(PlayParticleOnce());

            PlayerController playerController = collision.GetComponent<PlayerController>();
            GameSoundEffect.PlaySound(healthPlus);
            playerController.health.currentHealth = playerController.health.maxHealth;
            playerController.health.UpdateHealthBar();
        }
    }

    IEnumerator PlayParticleOnce()
    {
        isPlaying = true;

        // Active la particule
        particleSystemToPlay.gameObject.SetActive(true);
        particleSystemToPlay.Play();

        // Attends la durée spécifiée
        yield return new WaitForSeconds(duration);

        // Stoppe et désactive la particule
        particleSystemToPlay.Stop();
        particleSystemToPlay.gameObject.SetActive(false);

        isPlaying = false;
    }
}
