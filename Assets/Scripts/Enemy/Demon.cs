using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Demon : Enemy
{
    [SerializeField] private Image hpBar;
    [SerializeField] private float hp = 500f;
    [SerializeField] private float damage = 5f;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float objScale = 1f;

    [SerializeField] private GameObject skill1Prefabs;
    [SerializeField] private GameObject skill2Prefabs;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform[] spawnPoints2;
    [SerializeField] private float speedSkill = 5f;

    private Animator animator;
    private Transform playerTransform;

    void Start()
    {
        InitStat(hp, damage, speed, objScale, hpBar);
        UpdateHpBar();
        animator = GetComponent<Animator>();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }

        StartCoroutine(RandomSkill());
    }
    
    private IEnumerator RandomSkill()
    {
        while (true)
        {
            yield return new WaitForSeconds(4);
            int indexSkill = Random.Range(0, spawnPoints2.Length - 1);
            
            if (indexSkill == 0)
            {
                animator.SetTrigger("Attack");

            }else if (indexSkill == 1)
            {
                animator.SetTrigger("Attack_1");
            }
        }
    }

    public void CastSkill1()
    {
        if(playerTransform != null)
        {
            Vector2 direction = (playerTransform.position - spawnPoint.position).normalized;
            GameObject skill = Instantiate(skill1Prefabs, spawnPoint.position, Quaternion.identity);
            skill.GetComponent<Rigidbody2D>().linearVelocity = direction * speedSkill;
        }
    }

    public void CastSkill2()
    {
        foreach (Transform point in spawnPoints2)
        {
            Instantiate(skill2Prefabs, point.position, Quaternion.identity);
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
}
