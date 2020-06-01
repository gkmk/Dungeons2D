using UnityEngine;
using UnityEngine.GameFoundation;

public class WeaponHandler : MonoBehaviour
{
    //  Scriptable Object
    public Weapon weapon;
    //  Public vars
    public Animator animator;
    //  Weapon stats
    int attack;
    int defense;
    int agility;
    int level;
    //  Private vars
    
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

    public int GetAttackModifier()
    {
        return attack;
    }

    public int GetAgilityModifier()
    {
        return agility;
    }
    
    public int GetDefenseModifier()
    {
        return defense;
    }

    public int GetRequiredLevel()
    {
        return level;
    }

    private void LoadWeaponStats()
    {
        var item = GameFoundation.catalogs.inventoryCatalog.FindItem(weapon.inventoryID);
        var stats = item.GetDetail<StatDetail>();
        attack = stats.GetDefaultValue("attack");
        defense = stats.GetDefaultValue("defense");
        agility = stats.GetDefaultValue("agility");
        level = stats.GetDefaultValue("level");
        Debug.Log("weapon stats loaded" + attack);
    }

    public void Attack()
    {
        animator.SetTrigger("attack");
        hitAudio.Play();
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
