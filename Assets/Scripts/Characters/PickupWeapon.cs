using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.GameFoundation;

public class PickupWeapon : MonoBehaviour
{
    AttackHandler attackHandler;

    private void Start()
    {
        attackHandler = transform.parent.GetComponent<AttackHandler>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Weapon")
        {
            Pickup(collision.gameObject);
        }
    }

    void Pickup(GameObject weapon)
    {
        //  Load weapon stats
        WeaponHandler weaponStats = weapon.GetComponent<WeaponHandler>();
        InventoryManager.CreateItem(weaponStats.weapon.inventoryID);

        //  Attach new weapon to player
        weapon.transform.parent = transform;
        weapon.transform.position = transform.position;

        //  Update player stats based on current weapon
        attackHandler.SetModifiers(weaponStats);
    }
}
