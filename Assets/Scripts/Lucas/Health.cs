using System;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public int minHealth = 0;
    public Slider healthBar;
    public bool isAlive = true;

    public Action OnDie;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
        healthBar.minValue = minHealth;
    }

    public void UpdateHealthBar()
    {
        healthBar.value = currentHealth;
        Debug.Log("Update Health Bar");
    }

    public void UpdateDamage(int damage)
    {
        currentHealth += damage;
        UpdateHealthBar();
        if (currentHealth <= 0 && isAlive)
        {
            isAlive = false;
            // Destroy(gameObject);
            OnDie?.Invoke();
        }
    }

    public void ForRespawnHealth()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
        isAlive = true;
        healthBar.gameObject.SetActive(true);
    }
}
