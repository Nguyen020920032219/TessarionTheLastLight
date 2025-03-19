using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float maxHP = 50f;
    [SerializeField] private float moveSpeed = 1f;
    private GameObject player;
    private float currentHP;
    private Animator animator;
    private bool isDead = false;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        currentHP = maxHP;
    }

    private void Update()
    {
        if (isDead || player == null) return;

        Vector3 direction = (player.transform.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;

        if (direction.x > 0) transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        else if (direction.x < 0) transform.localScale = new Vector3(-0.5f, 0.5f, 0.5f);
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;
        currentHP -= damage;
        if (currentHP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        animator.SetBool("isDying", true);
        Destroy(gameObject, 1.5f);
    }
}