using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Dungeon/New Enemy")]
public class EnemyType : ScriptableObject
{
    /// <summary>
    /// Personality
    /// </summary>
    new public string name;
    public string description;
    public Sprite sprite;
    public AnimatorController animator;

    /// <summary>
    /// Battle definitions stats
    /// </summary>
    public int health;
    public int attack;
    public int defense;
    public int agility;
    public int level;

}
