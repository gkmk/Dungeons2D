using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.GameFoundation;

public class PickupWeapon : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Weapon")
        {
            Pickup(collision.gameObject);
        }
    }

    void Pickup(GameObject weapon)
    {
        InventoryManager.CreateItem(weapon.GetComponent<WeaponHandler>().weapon.inventoryID);
        weapon.transform.parent = transform;
        weapon.transform.position = transform.position;
    }
}
