using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Boar : Enemy
{
    [SerializeField] private float hp = 50f;
    [SerializeField] private float damage = 5f;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float scale = 1f;
    [SerializeField] private Image healthBar;
    [SerializeField] private float distances=3;

    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
        InitStat(hp, damage, moveSpeed, scale, healthBar);
        UpdateHpBar();
    }

    private void Update()
    {
        //MoveFromPosToPos(startPos, distances);
        MoveToPlayer();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SwordSkill"))
        {
            TakeDamage(100);
        }
        if (collision.CompareTag("BowSkill"))
        {
            TakeDamage(100);
        }
    }
}
