using System.Collections;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class ReaperMan1 : Enemy
{
    [SerializeField] private Image hpBar;
    [SerializeField] private float hp = 50f;
    [SerializeField] private float damage = 5f;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float objScale = 0.03f;
    [SerializeField] private float attackRange;


    private GameObject player;
    private PolygonCollider2D _attackCollider;
    private Animator _animator;
    private bool canAttack = true;

    void Start()
    {
        InitStat(hp, damage, speed, objScale, hpBar);
        UpdateHpBar();
        player = GameObject.FindGameObjectWithTag("Player");

        GameObject attackObject = GameObject.FindGameObjectWithTag("ReaperAttack");
        if (attackObject != null)
        {
            _attackCollider = attackObject.GetComponent<PolygonCollider2D>();
            _attackCollider.enabled = false; // Ensure it's disabled initially
        }
        //_attackCollider = GetComponent<Collider2D>();
        _animator = GetComponent<Animator>();
        //_attackCollider.enabled = true;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) > attackRange)
        {
            base.MoveToPlayer();
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= attackRange && canAttack)
        {
            AttackPlayer();

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SwordSkill"))
        {
            TakeDamage(100);
        }
        if (collision.CompareTag("BowSkill"))
        {
            TakeDamage(100);
        }
    }

    private void AttackPlayer()
    {
        canAttack = false;
        _animator.SetBool("isAttacking", true);
        _attackCollider.enabled = true;
        player.GetComponent<PlayerManager>().TakeDamage(5);
        StartCoroutine(DisableAttackAfterCooldown());
    }

    private IEnumerator DisableAttackAfterCooldown()
    {
        yield return new WaitForSeconds(1.5f);
        _attackCollider.enabled = false;
        _animator.SetBool("isAttacking", false);
        canAttack = true;
        yield return new WaitForSeconds(1.5f);
    }
}
