using UnityEngine;
using UnityEngine.GameFoundation;

public class WeaponHandler : MonoBehaviour
{
    //  Scriptable Object
    public Weapon weapon;
    //  Public vars
    public Animator animator;
    public Transform attackPoint;
    public LayerMask enemyLayers;
    //  Weapon stats
    int attack;
    int defense;
    int agility;
    int level;
    float attackRange;
    //  Private vars
    float nextAttackTime = 0f;
    // Cache properties
    SpriteRenderer spriteRender;
    new BoxCollider2D collider;
    AudioSource hitAudio;

    // Start is called before the first frame update
    void Awake()
    {
        //  Initialize weapon sprite
        spriteRender = GetComponent<SpriteRenderer>();
        spriteRender.sprite = weapon.icon;

        //  Assign and recalculate new collider
        collider = GetComponent<BoxCollider2D>();
        collider.size = spriteRender.sprite.bounds.size;

        //  Setup audio hit clip
        hitAudio = GetComponent<AudioSource>();
        hitAudio.clip = weapon.hitAudio;

        //  Load weapon stats
        GameEvents.Current.OnGFInitialized += LoadWeaponStats;
    }

    private void LoadWeaponStats()
    {
        var item = GameFoundation.catalogs.inventoryCatalog.FindItem(weapon.inventoryID);
        var stats = item.GetDetail<StatDetail>();
        attack = stats.GetDefaultValue("attack");
        attackRange = stats.GetDefaultValue("attackRange");
        defense = stats.GetDefaultValue("defense");
        agility = stats.GetDefaultValue("agility");
        level = stats.GetDefaultValue("level");
        Debug.Log("weapon stats loaded" + attack);
    }

    public void OnAttack()
    {
        if (Time.time < nextAttackTime) return;

        animator.SetTrigger("attack");
        hitAudio.Play();

        var enemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (var enemy in enemies)
        {
            Debug.Log("HIT: " + enemy.name);
            enemy.GetComponent<Health>().TakeDamage(attack);
        }
        nextAttackTime = Time.time + 1f / agility;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    //private void Update()
    //{
    //    if (hasWeapon)
    //    {
    //        Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.parent.position);
    //        float angle = Mathf.Atan2(dir.y, dir.x);
    //        transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg - 90, Vector3.forward);
    //        transform.position = transform.parent.position + new Vector3(Mathf.Cos(angle)/2, Mathf.Sin(angle)/2, 0f);
    //    }
    //}

}
