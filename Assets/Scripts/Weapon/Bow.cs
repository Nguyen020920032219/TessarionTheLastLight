using UnityEngine;

public class Bow : Weapon
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private ObjectPool arrowPool;
    [SerializeField] private GameObject bowSkill;


    private void Start()
    {
        animator.SetLayerWeight(1, 0);
    }

    public override void Attack()
    {
        if (CanAttack())
        {
            animator.SetLayerWeight(1, 1);
            animator.SetBool("isBowShooting", true);
            Invoke("ResetShooting", 0.2f);

            GameObject arrow = arrowPool.GetObject();
            arrow.GetComponent<Arrow>().damage = GetDamage();
            arrow.transform.position = firePoint.position;
            Rigidbody2D arrowRb = arrow.GetComponent<Rigidbody2D>();

            float direction = transform.lossyScale.x > 0 ? 1 : -1;
            arrow.transform.localScale = new Vector3(direction * 0.02f, 0.01f, 0.01f);
            arrowRb.linearVelocity = new Vector2(direction * GetArrowSpeed(), 0);

            ResetAttackCooldown();
        }
    }

    public override void StopAttack()
    {
        animator.SetBool("isBowShooting", false);
        animator.SetLayerWeight(1, 0);
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

            skillRb.linearVelocity = new Vector2(direction * GetArrowSpeed(), 0);

            animator.SetBool("isBowShooting", false);

            Destroy(skillInstance, 10f);
        }
    }


    private void ResetShooting()
    {
        animator.SetBool("isBowShooting", false);
    }
}