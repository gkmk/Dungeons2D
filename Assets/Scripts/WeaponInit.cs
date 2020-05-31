using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.GameFoundation;
using UnityEngine.InputSystem;

public class WeaponInit : MonoBehaviour
{
    //  Scriptable Object
    public Weapon weapon;
    //  Weapon stats
    int attack;
    int defense;
    int agility;
    int level;
    // Cache properties
    SpriteRenderer spriteRender;
    new BoxCollider2D collider;

    // Start is called before the first frame update
    void Awake()
    {
        //  Initialize weapon sprite
        spriteRender = GetComponent<SpriteRenderer>();
        spriteRender.sprite = weapon.icon;

        //  Assign and recalculate new collider
        collider = GetComponent<BoxCollider2D>();
        collider.size = spriteRender.sprite.bounds.size;

        //  Load weapon stats
        GameEvents.Current.OnGFInitialized += LoadWeaponStats;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !hasWeapon)
        {
            Pickup(collision.gameObject);
        }
        if (collision.tag == "Enemy" && hasWeapon)
        {
            //Pickup(collision.gameObject);
        }
        Debug.Log(collision.name);
    }

    private void Update()
    {
        if (hasWeapon)
        {
            Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.parent.position);
            float angle = Mathf.Atan2(dir.y, dir.x);
            transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg - 90, Vector3.forward);
            transform.position = transform.parent.position + new Vector3(Mathf.Cos(angle)/2, Mathf.Sin(angle)/2, 0f);
        }
    }

    bool hasWeapon = false;
    void Pickup(GameObject player)
    {
        InventoryManager.CreateItem(weapon.inventoryID);
        //StatManager.AdjustValue("playerHero", "attack", )
        //transform.position = Vector3.zero;
        transform.parent = player.transform;
        transform.position = player.transform.position;
        hasWeapon = true;
        //transform.LookAt(new Vector3(Mouse.current.position.x.ReadValue(), Mouse.current.position.y.ReadValue(), 0f));
    }
}
