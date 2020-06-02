using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthBar : MonoBehaviour
{
    Slider healthBar;

    private void Awake()
    {
        healthBar = GetComponent<Slider>();
    }

    /// <summary>
    /// Update the GUI with the current health
    /// </summary>
    /// <param name="current"></param>
    /// <param name="maxHealth"></param>
    public void UpdateHealthBar(int current, int maxHealth)
    {
        healthBar.maxValue = maxHealth;
        healthBar.value = current;
    }
}
