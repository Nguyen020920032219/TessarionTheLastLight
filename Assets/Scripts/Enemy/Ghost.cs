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
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
        InitStat(hp, damage, speed, objScale, hpBar);
        UpdateHpBar();
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
}
