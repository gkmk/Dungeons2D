using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor.Animations;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyType enemyType;

    // Private vars
    SpriteRenderer spriteRenderer;
    Animator animator;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = enemyType.sprite;

        animator = GetComponent<Animator>();
        if (enemyType.animator != null)
        {
            animator.runtimeAnimatorController = enemyType.animator;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.transform.GetComponent<HealthHandler>().TakeDamage(7);
        }
    }
}
