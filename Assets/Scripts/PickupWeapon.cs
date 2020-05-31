using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupWeapon : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Weapon")
        {
            Pickup(collision.gameObject);
        }
        Debug.Log(collision.name);
    }

    void Pickup(GameObject weapon)
    {
        //InventoryManager.CreateItem(weapon.inventoryID);
        weapon.transform.parent = transform;
        weapon.transform.position = transform.position;
    }
}
