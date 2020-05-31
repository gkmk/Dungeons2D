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
        if (_currentHealth > 0)
        {
            _currentHealth -= damage;
            return _currentHealth;
        }
        return 0;
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
