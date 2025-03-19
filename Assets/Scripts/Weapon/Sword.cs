using UnityEngine;

public class Sword : Weapon
{
    [SerializeField] private Collider2D swordCollider;

    protected override void Awake()
    {
        base.Awake();
        if (swordCollider != null)
            swordCollider.enabled = false;
    }

    public override void Attack()
    {
        if (!CanAttack()) return;

        animator.SetBool("isSwordAttacking", true);
        swordCollider.enabled = true;
        ResetAttackCooldown();
        Invoke(nameof(DisableCollider), 0.2f);
    }

    public override void StopAttack()
    {
        animator.SetBool("isSwordAttacking", false);
        DisableCollider();
    }

    private void DisableCollider()
    {
        swordCollider.enabled = false;
        animator.SetBool("isSwordAttacking", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null && swordCollider.enabled) 
            {
                enemy.TakeDamage(GetDamage());
                swordCollider.enabled = false; 
            }
        }
    }
}