using UnityEngine;

public class Sword : Weapon
{
    [SerializeField] private Collider2D swordCollider;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject swordSkill;

    protected override void Awake()
    {
        base.Awake();
        if (swordCollider != null)
        {
            swordCollider.enabled = false;
        }
        animator.SetLayerWeight(1, 0);
    }

    public override void Attack()
    {
        if (!CanAttack()) return;
        animator.SetLayerWeight(1, 1);
        animator.SetBool("isSwordAttacking", true);
        swordCollider.enabled = true;
        ResetAttackCooldown();
        Invoke(nameof(DisableCollider), 0.2f);
    }

    public override void StopAttack()
    {
        animator.SetBool("isSwordAttacking", false);
        animator.SetLayerWeight(1, 0);
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

    public override void UsingSkill()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PlayerManager playerManager = player.GetComponent<PlayerManager>();
        if (playerManager.CanUseSkill(10f))
        {
            animator.SetBool("isSwordAttacking", true);

            GameObject skillInstance = Instantiate(swordSkill, firePoint.position, Quaternion.identity);
            Rigidbody2D skillRb = skillInstance.GetComponent<Rigidbody2D>();

            float direction = transform.lossyScale.x > 0 ? 1 : -1;
            skillInstance.transform.localScale = new Vector3(direction * 3f, 3f, 3f);

            skillRb.linearVelocity = new Vector2(direction * 5f, 0);

            animator.SetBool("isSwordAttacking", false);

            Destroy(skillInstance, 1f);
        }
    }
}