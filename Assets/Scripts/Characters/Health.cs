using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    private int _currentHealth;

    private void Start()
    {
        _currentHealth = maxHealth;
    }

    public int TakeDamage(int damage)
    {
        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            Die();
        }
        return _currentHealth;
    }

    void Die()
    {
        GetComponent<Collider2D>().enabled = false;
        transform.rotation = Quaternion.AngleAxis(90, Vector3.forward);
        this.enabled = false;
    }

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
