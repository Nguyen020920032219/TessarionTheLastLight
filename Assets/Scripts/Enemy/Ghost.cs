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
    [SerializeField] private Vector3 startPos;
    [SerializeField] private float immortalTime = 5f;
    [SerializeField] private float canGetDamageTime = 5f;

    private GameObject player;
    private Animator _animator;
    private bool isImmortal = false;

    void Start()
    {
        InitStat(hp, damage, speed, objScale, hpBar);
        UpdateHpBar();
        player = GameObject.FindGameObjectWithTag("Player");
        _animator = GetComponent<Animator>();

        // Start the immortality cycle
        StartCoroutine(ImmortalityCycle());
    }

    void Update()
    {
        //MoveToPlayer();
        MoveFromPosToPos(startPos, distance);
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

            yield return new WaitForSeconds(immortalTime);

            isImmortal = false;
            SetCurrentHP(actualHP); // Set HP back to actual value
            base.UpdateHpBar();
            yield return new WaitForSeconds(canGetDamageTime);
        }
    }
}
