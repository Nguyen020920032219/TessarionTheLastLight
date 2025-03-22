using UnityEngine;

public class Bow : Weapon
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private float arrowSpeed = 5f;
    [SerializeField] private ObjectPool arrowPool;
    [SerializeField] private GameObject bowSkill;

    public override void Attack()
    {
        if (CanAttack())
        {
            animator.SetBool("isBowShooting", true);
            Invoke("ResetShooting", 0.2f);

            GameObject arrow = arrowPool.GetObject();
            arrow.GetComponent<Arrow>().damage = currentDamage;
            arrow.transform.position = firePoint.position;
            Rigidbody2D arrowRb = arrow.GetComponent<Rigidbody2D>();

            float direction = transform.lossyScale.x > 0 ? 1 : -1;
            arrow.transform.localScale = new Vector3(direction * 0.02f, 0.01f, 0.01f);
            arrowRb.linearVelocity = new Vector2(direction * arrowSpeed, 0);

            Destroy(arrow, 10f);
            ResetAttackCooldown();
        }
    }

    public override void StopAttack()
    {
        animator.SetBool("isBowShooting", false);
    }

    public override void UsingSkill()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PlayerManager playerManager = player.GetComponent<PlayerManager>();
        if (playerManager.CanUseSkill(10f))
        {
            animator.SetBool("isBowShooting", true);

            GameObject skillInstance = Instantiate(bowSkill, firePoint.position, Quaternion.identity);
            Rigidbody2D skillRb = skillInstance.GetComponent<Rigidbody2D>();

            float direction = transform.lossyScale.x > 0 ? 1 : -1;
            skillInstance.transform.localScale = new Vector3(direction * 3f, 3f, 3f);

            skillRb.linearVelocity = new Vector2(direction * arrowSpeed, 0);

            animator.SetBool("isBowShooting", false);

            Destroy(skillInstance, 10f);
        }
    }


    private void ResetShooting()
    {
        animator.SetBool("isBowShooting", false);
    }
}