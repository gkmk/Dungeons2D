﻿using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Dungeon/New Weapon")]
public class Weapon : ScriptableObject
{
    //  Prefab Attributes
    public new string name;
    public Sprite icon;

    // Inventory System Params
    public string inventoryID;
}
