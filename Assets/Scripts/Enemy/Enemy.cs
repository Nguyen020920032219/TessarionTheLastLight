using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using UnityEditor.Timeline;

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
    private bool isMovingRight = true;

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

    public float GetCurrentHP()
    {
        return currentHP;
    }

    public void SetCurrentHP(float hp)
    {
        currentHP = hp;
    }

    protected void MoveToPlayer()
    {
        if (isDead || player == null || isKnockedBack) return;

        Vector3 direction = (player.transform.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;

        if (direction.x > 0) transform.localScale = new Vector3(1f * objectScale, 1f * objectScale, 1f * objectScale);
        else if (direction.x < 0) transform.localScale = new Vector3(-1f * objectScale, 1f * objectScale, 1f * objectScale);
        animator.SetBool("isMoving", true);
    }

    protected void MoveFromPosToPos(Vector3 startPosition, float distanceFromStartPosition)
    {
        float leftBound = startPosition.x - distanceFromStartPosition;
        float rightBound = startPosition.x + distanceFromStartPosition;
        if (isMovingRight)
        {
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            if (transform.position.x >= rightBound)
            {
                isMovingRight = false;
                Flip();
            }
        }
        else
        {
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
            if (transform.position.x <= leftBound)
            {
                isMovingRight = true;
                Flip();
            }
        }
        animator.SetBool("isMoving", true);
    }

    private void Flip()
    {
        Vector3 scaler = transform.localScale;
        scaler.x *= -1; 
        transform.localScale = scaler;
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;
        currentHP -= damage;
        animator.SetBool("isHurting", true);
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
            animator.SetBool("isHurting", false);
        }
    }
    public bool IsDied()
    {
        return isDead;
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