using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthHandler : MonoBehaviour
{
    public int maxHealth = 100;
    private int _currentHealth;

    HealthBar healthBar;

    private void Start()
    {
        //  Initialize current health
        _currentHealth = maxHealth;

        //  Check if character has its own bar
        if (GetComponent<HealthBar>() != null)
        {
            healthBar = GetComponent<HealthBar>();
        }
        //  Character is Enemy. Get Health from their type
        if (GetComponent<Enemy>() != null)
        {
            maxHealth = GetComponent<Enemy>().enemyType.health;
        }

        //  Update health GUI
        HealthChanged();
    }

    /// <summary>
    /// Invoke any listeners to update GUI
    /// </summary>
    void HealthChanged()
    {
        //  Player Hero specific. Should update GUI
        if (CompareTag("Player"))
        {
            GameEvents.Current.SetPlayerHealth(_currentHealth, maxHealth);
        }
        else if (healthBar)
        {
            //  For any other object that has health bar
            healthBar.UpdateHealthBar(_currentHealth, maxHealth);
        }
    }

    /// <summary>
    /// Apply damage to the current character
    /// </summary>
    /// <param name="damage"></param>
    /// <returns>Health after the applied damage</returns>
    public int TakeDamage(int damage)
    {
        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            Die();
        }
        HealthChanged();
        return _currentHealth;
    }

    /// <summary>
    /// When the current character dies it disables its collider and self
    /// </summary>
    void Die()
    {
        GetComponent<Collider2D>().enabled = false;
        transform.rotation = Quaternion.AngleAxis(90, Vector3.forward);
        this.enabled = false;
    }

    /// <summary>
    /// Heal the current character
    /// </summary>
    /// <param name="heal"></param>
    /// <returns>Health after healing</returns>
    public int Heal(int heal)
    {
        _currentHealth += heal;
        if (_currentHealth > maxHealth)
        {
            _currentHealth = maxHealth;
        }
        return _currentHealth;
    }
}
