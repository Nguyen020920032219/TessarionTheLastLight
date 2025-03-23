using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;

public class Enemy : MonoBehaviour
{
    private float health;
    private float damge;
    private float moveSpeed;
    private float knockbackForce = 5f;
    private float knockbackDuration = 0.3f;
    private float objectScale;
    private Image hpBar;

    private GameObject player;
    private Animator animator;
    private Collider2D enemyCollider;
    private Rigidbody2D rb;
    private float currentHP;
    private bool isDead = false;
    private bool isKnockedBack = false;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        enemyCollider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    protected void InitStat(float baseHp, float baseDamage, float baseMoveSpeed, float objScale, Image healthBar)
    {
        health = baseHp;
        currentHP = health;
        damge = baseDamage;
        moveSpeed = baseMoveSpeed;
        objectScale = objScale;
        hpBar = healthBar;
        UpdateHpBar();
    }

    public float GetDamage()
    {
        return damge;
    }

    protected void MoveToPlayer()
    {
        if (isDead || player == null || isKnockedBack) return;

        Vector3 direction = (player.transform.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;

        if (direction.x > 0) transform.localScale = new Vector3(1f * objectScale, 1f * objectScale, 1f * objectScale);
        else if (direction.x < 0) transform.localScale = new Vector3(-1f * objectScale, 1f * objectScale, 1f * objectScale);
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;
        currentHP -= damage;
        currentHP = Mathf.Max(currentHP, 0);
        StartCoroutine(Knockback());
        UpdateHpBar();
        if (currentHP <= 0)
        {
            Die();
        }
    }

    protected void UpdateHpBar()
    {
        if (hpBar != null)
        {
            hpBar.fillAmount = currentHP / health;
        }
    }

    private IEnumerator Knockback()
    {
        if (rb != null && player != null)
        {
            isKnockedBack = true;

            Vector2 knockbackDirection = (transform.position - player.transform.position).normalized;
            rb.linearVelocity = Vector2.zero;
            rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);

            yield return new WaitForSeconds(knockbackDuration);

            rb.linearVelocity = Vector2.zero;
            isKnockedBack = false;
        }
    }

    private void Die()
    {
        isDead = true;
        enemyCollider.enabled = false;
        animator.SetBool("isDying", true);
        rb.linearVelocity = Vector2.zero;
        Destroy(gameObject, 1.5f);
    }
}