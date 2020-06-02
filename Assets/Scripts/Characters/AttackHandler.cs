using UnityEngine;
using UnityEngine.UI;

public class AttackHandler : MonoBehaviour
{
    //  Public vars
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public Text statsText;

    //  Weapon modifiers
    public int attackModifier;
    public int agilityModifier;
    public int defenseModifier;
    //  Private vars
    float nextAttackTime = 0f;
    PlayerStats playerStats;

    /// <summary>
    /// Initialize components
    /// </summary>
    private void Start()
    {
        playerStats = GetComponent<PlayerStats>();

        statsText.text = "Attack " + playerStats.attack + " | Defense " + playerStats.defense;
    }

    /// <summary>
    /// Handle attack. Check attack times based on agility and apply damage
    /// based on character and weapon stats.
    /// </summary>
    public void OnAttack()
    {
        //  Can i attack at this time?
        if (Time.time < nextAttackTime) return;

        //  Activate "Attack" function on any hero components
        BroadcastMessage("Attack", SendMessageOptions.DontRequireReceiver);

        //  Attack near by enemies
        var enemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (var enemy in enemies)
        {
            enemy.GetComponent<HealthHandler>().TakeDamage(playerStats.attack + attackModifier);
        }

        //  Get next attack time based of agility (weapon or hero)
        if (agilityModifier <= 0) agilityModifier = playerStats.agility;
        nextAttackTime = Time.time + 1f / playerStats.agility;
    }

    /// <summary>
    /// Set the modifiers based of weapon
    /// </summary>
    /// <param name="weaponStats"></param>
    public void SetModifiers(WeaponHandler weaponStats)
    {
        attackModifier = weaponStats.GetAttackModifier();
        agilityModifier = weaponStats.GetAgilityModifier();
        defenseModifier = weaponStats.GetDefenseModifier();

        statsText.text = "Attack " + (playerStats.attack+ attackModifier) + " | Defense " + (playerStats.defense+ defenseModifier);
    }

    /// <summary>
    /// Debug helper function to see attack range
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
