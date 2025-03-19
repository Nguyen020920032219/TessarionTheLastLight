using UnityEngine;

public class Bow : Weapon
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private float arrowSpeed;
    [SerializeField] private ObjectPool arrowPool;

    public override void Attack()
    {
        if (CanAttack())
        {
            animator.SetBool("isBowShooting", true);
            Invoke("ResetShooting", 0.2f);

            GameObject arrow = arrowPool.GetObject();
            arrow.transform.position = firePoint.position; 
            Rigidbody2D arrowRb = arrow.GetComponent<Rigidbody2D>();

            float direction = transform.lossyScale.x > 0 ? 1 : -1;
            arrow.transform.localScale = new Vector3(direction * 0.02f, 0.01f, 0.01f);
            arrowRb.linearVelocity = new Vector2(direction * arrowSpeed, 0);

            ResetAttackCooldown();
        }
    }

    public override void StopAttack()
    {
        animator.SetBool("isBowShooting", false);
    }

    private void ResetShooting()
    {
        animator.SetBool("isBowShooting", false);
    }
}