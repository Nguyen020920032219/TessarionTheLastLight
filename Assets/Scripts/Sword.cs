using UnityEngine;

public class Sword : MonoBehaviour
{
    private Animator animator;
    private bool isAttacking = false;
    private float attackRate = 0.7f; 
    private float nextAttackTime = 0f;

    private void Awake()
    {
        animator = GetComponentInParent<Animator>();
    }

    public void StartAttack()
    {
        isAttacking = true;
    }

    public void StopAttack()
    {
        isAttacking = false;
        animator.SetBool("isAttacking", false);
    }

    private void Update()
    {
        if (isAttacking && Time.time >= nextAttackTime)
        {
            animator.SetBool("isAttacking", true);
            nextAttackTime = Time.time + attackRate;
        }
    }
}