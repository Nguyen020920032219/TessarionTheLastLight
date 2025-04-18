using System.Collections;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class ReaperMan3 : Enemy
{
    [SerializeField] private Image hpBar;
    [SerializeField] private float hp = 50f;
    [SerializeField] private float damage = 5f;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float objScale = 0.03f;
    [SerializeField] private float canCallReaperMan1Range;
    [SerializeField] private float canUseUltilmateRange;
    [SerializeField] private float limitHpRemainToCanCallReaperMan1;
    [SerializeField] private ReaperMan1 reaperMan1Prefab;
    [SerializeField] private GameObject[] stones;
    [SerializeField] private Transform spawnPoint;
    private bool isRandom = false;
    private GameObject player;
    private Animator _animator;
    private bool canCallReaperMan1 = true;

    void Start()
    {
        InitStat(hp, damage, speed, objScale, hpBar);
        UpdateHpBar();
        player = GameObject.FindGameObjectWithTag("Player");
        _animator = GetComponent<Animator>();


        var index = Random.Range(0, stones.Length);
        GameObject selectedStone = Instantiate(stones[index], spawnPoint.position, Quaternion.identity);
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= canCallReaperMan1Range && canCallReaperMan1 && GetCurrentHP() > limitHpRemainToCanCallReaperMan1)
        {
            CallReaperMan1();
        }

        if (distanceToPlayer <= canUseUltilmateRange)
        {
            StartCoroutine(UseUltilmate());
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

    private void CallReaperMan1()
    {
        canCallReaperMan1 = false;
        _animator.SetBool("isAttacking", true);
        // Spawn new enemy near player
        Vector3 spawnPosition = player.transform.position + new Vector3(5f, 2f, 0);
        ReaperMan1 newReaper = Instantiate(reaperMan1Prefab, spawnPosition, Quaternion.identity);
        base.TakeDamage(10);
        StartCoroutine(DisableAttackAfterCooldown());
    }

    private IEnumerator DisableAttackAfterCooldown()
    {
        yield return new WaitForSeconds(10f);
        _animator.SetBool("isAttacking", false);
        canCallReaperMan1 = true;
        yield return new WaitForSeconds(10f);
    }

    private IEnumerator UseUltilmate()
    {
        while (GetCurrentHP() > 0)
        {
            Vector3 spawnPosition = player.transform.position + new Vector3(5f, 0f, 0);
            ReaperMan1 newReaperFrontPlayer = Instantiate(reaperMan1Prefab, spawnPosition, Quaternion.identity);

            Vector3 spawnPosition1 = player.transform.position + new Vector3(-5f, 0f, 0);
            ReaperMan1 newReaperBehindPlayer = Instantiate(reaperMan1Prefab, spawnPosition1, Quaternion.identity);

            base.TakeDamage(20);
            yield return new WaitForSeconds(0.5f);
        }

    }

}
