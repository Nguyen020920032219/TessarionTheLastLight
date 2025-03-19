using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] private float attackDamage;
    [SerializeField] private float attackSpeed;
    private float nextAttackTime=0f;

    protected Animator animator;

    protected virtual void Awake()
    {
        animator = GetComponentInParent<Animator>();
    }

    public float GetDamage()
    {
        return attackDamage;
    }

    protected bool CanAttack()
    {
        return Time.time >= nextAttackTime;
    }

    protected void ResetAttackCooldown()
    {
        nextAttackTime = Time.time + (1f / attackSpeed);
    }

    public void BuffAttackDamage(float damage)
    {
        attackDamage += damage;
    }
    public void BuffAttackSpeed(float speed)
    {
        attackSpeed += speed;
    }

    public abstract void Attack();
    public abstract void StopAttack();
}