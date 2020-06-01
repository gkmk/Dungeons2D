using UnityEngine;

public class AttackHandler : MonoBehaviour
{
    //  Public vars
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    //  Weapon modifiers
    public int attackModifier;
    public int agilityModifier;
    //  Private vars
    float nextAttackTime = 0f;
    PlayerStats playerStats;

    /**
     * Initialize components
     * 
     */
    private void Start()
    {
        playerStats = GetComponent<PlayerStats>();
    }

    /**
     * Handle attack
     * 
     */
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
            enemy.GetComponent<Health>().TakeDamage(playerStats.attack + attackModifier);
        }

        //  Get next attack time based of agility (weapon or hero)
        if (agilityModifier <= 0) agilityModifier = playerStats.agility;
        nextAttackTime = Time.time + 1f / playerStats.agility;
    }

    /**
     * Set the modifiers based of weapon
     * 
     */
    public void SetModifiers(WeaponHandler weaponStats)
    {
        attackModifier = weaponStats.GetAttackModifier();
        agilityModifier = weaponStats.GetAgilityModifier();
    }

    /**
     * Debug helper function to see attack range
     * 
     */
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
