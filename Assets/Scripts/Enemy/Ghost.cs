using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Ghost : Enemy
{
    [SerializeField] private Image hpBar;
    [SerializeField] private float hp = 50f;
    [SerializeField] private float damage = 5f;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float objScale = 0.5f;
    [SerializeField] private float distance;
    [SerializeField] private float distanceToStartChasingPlayer;
    private Vector3 startPos;
    [SerializeField] private float immortalTime = 5f;
    [SerializeField] private float canGetDamageTime = 5f;
    [SerializeField] private GameObject[] stones;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private bool isBoss = false;
    private bool isRandom = false;
    private GameObject player;
    private Animator _animator;
    private bool isImmortal = false;
    private bool isChasingPlayer = false;

    void Start()
    {
        startPos = transform.position;
        InitStat(hp, damage, speed, objScale, hpBar);
        UpdateHpBar();
        player = GameObject.FindGameObjectWithTag("Player");
        _animator = GetComponent<Animator>();

        // Start the immortality cycle
        StartCoroutine(ImmortalityCycle());
    }

    void Update()
    {
        if (!isRandom && IsDied())
        {
            Died();
            isRandom = true;
        }
        if (Vector3.Distance(transform.position, player.transform.position) <= distanceToStartChasingPlayer)
        {
            isChasingPlayer = true;
        }

        if (isChasingPlayer)
        {
            MoveToPlayer();
        }
        else
        {
            MoveFromPosToPos(startPos, distance);
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
        if (collision.CompareTag("Player"))
        {
            player.GetComponent<PlayerManager>().TakeDamage(damage);
        }
    }

    private IEnumerator ImmortalityCycle()
    {
        var actualHP = GetCurrentHP();
        var virtualHP = 1000000000;

        yield return new WaitForSeconds(canGetDamageTime); // Wait before first immortality

        while (true)
        {
            isImmortal = true;
            actualHP = GetCurrentHP(); // Save actual HP
            SetCurrentHP(virtualHP);
            base.UpdateHpBar();
            base.SetMoveSpeed(2f);
            yield return new WaitForSeconds(immortalTime);

            isImmortal = false;
            SetCurrentHP(actualHP); // Set HP back to actual value
            base.UpdateHpBar();
            base.SetMoveSpeed(0f);
            yield return new WaitForSeconds(canGetDamageTime);
        }
    }
    private void Died()
    {
        if (isBoss)
        {
            var index = Random.Range(0, stones.Length - 1);
            GameObject selectedStone = stones[index];
            selectedStone.transform.position = spawnPoint.position;
            selectedStone.SetActive(true);
        }
    }
}
