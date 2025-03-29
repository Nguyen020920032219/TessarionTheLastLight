using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.UI;

public class Boar : Enemy
{
    [SerializeField] private float hp = 50f;
    [SerializeField] private float damage = 5f;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float scale = 1f;
    [SerializeField] private Image healthBar;
    [SerializeField] private float distances=3;
    [SerializeField] private Collider2D attackCollider;
    [SerializeField] private GameObject[] stones;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private bool isBoss = false;
    [SerializeField] private float attackRange;
    [SerializeField] private float attackSpeed;
    private bool isRandom = false;
    private GameObject player;
    private Vector3 startPos;
    private bool canAttack = true;

    private void Start()
    {
        attackCollider.enabled = false;
        startPos = transform.position;
        player = GameObject.FindGameObjectWithTag("Player");
        InitStat(hp, damage, moveSpeed, scale, healthBar);
        UpdateHpBar();
    }

    private void Update()
    {
        if (!isRandom && IsDied())
        {
            Died();
            isRandom = true;
        }
        float distanceToPlayer = Vector3.Distance(attackCollider.transform.position, player.transform.position);

        if (distanceToPlayer < 0.8 && canAttack)
        {
            AttackPlayer();
        }
        else if (distanceToPlayer <= attackRange && canAttack)
        {
            SetMoveSpeed(attackSpeed);
            MoveToPlayer();
        }
        else
        {
            SetMoveSpeed(moveSpeed);
            MoveFromPosToPos(startPos, distances);
        }
    }
    private void AttackPlayer()
    {
        canAttack = false;
        attackCollider.enabled = true;
        player.GetComponent<PlayerManager>().TakeDamage(damage);
        Invoke(nameof(ResetAttack), 2f);
    }

    private void ResetAttack()
    {
        canAttack = true;
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
    private void Died()
    {
        if (isBoss)
        {
            var index = UnityEngine.Random.Range(0, stones.Length - 1);
            GameObject selectedStone = stones[index];
            selectedStone.transform.position = spawnPoint.position;
            selectedStone.SetActive(true);
        }
    }

}
