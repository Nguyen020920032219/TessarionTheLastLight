using System;
using System.Buffers.Text;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Slime : Enemy
{
    [SerializeField] private Image healthBar;
    [SerializeField] private Collider2D _collider;
    [SerializeField] private float baseHp = 50f;
    [SerializeField] private float baseDamage = 2f;
    [SerializeField] private float baseMoveSpeed = 1f;
    [SerializeField] private float objScale = 1f;
    [SerializeField] private float distance = 2f;
    [SerializeField] private bool isBoss = false;
    [SerializeField] private GameObject astralStone;

    private GameObject player;
    private bool canAttack = true;
    private Animator animator;
    private Vector3 startPos;

    private void Start()
    {
        if (isBoss && astralStone != null)
        {
            astralStone.SetActive(false);
        }
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        startPos = transform.position;
        InitStat(baseHp, baseDamage, baseMoveSpeed, objScale, healthBar);
        UpdateHpBar();
    }

    void Update()
    {
        if (IsDied())
        {
            if (isBoss && astralStone != null)
            {
                astralStone.SetActive(true);
            }
        }
        float distanceToPlayer = Vector3.Distance(_collider.transform.position, player.transform.position);

        if (distanceToPlayer <= 0.8f && canAttack)
        {
            AttackPlayer();
        }
        if (distanceToPlayer > 0.8f)
        {
            MoveFromPosToPos(startPos, distance);
        }
    }

    private void AttackPlayer()
    {
        canAttack = false;
        animator.SetBool("isAttacking", true);
        _collider.enabled = true;
        player.GetComponent<PlayerManager>().TakeDamage(baseDamage);
        Invoke(nameof(ResetAttack), 2f);
        Invoke(nameof(ResetAnimation), 0.5f);
    }

    private void ResetAnimation()
    {
        animator.SetBool("isAttacking", false);
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
}