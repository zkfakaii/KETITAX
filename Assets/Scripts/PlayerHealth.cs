using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100; // Vida máxima del jugador
    private int currentHealth; // Vida actual del jugador

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Reiniciar la escena al morir
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

