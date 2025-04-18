using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Demon : Enemy
{
    [SerializeField] private Image hpBar;
    [SerializeField] private float hp = 500f;
    [SerializeField] private float damage = 5f;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float objScale = 1f;
    [SerializeField] private GameObject[] skillPrefabs;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform[] spawnPoints2;
    [SerializeField] private float speedSkill = 5f;
    [SerializeField] private GameObject foreground;

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
        foreground.gameObject.SetActive(false);
        StartCoroutine(RandomSkill());
    }

    private void Update()
    {
        Died();
    }

    private IEnumerator RandomSkill()
    {
        while (true)
        {
            yield return new WaitForSeconds(4);
            int indexSkill = Random.Range(0, skillPrefabs.Length);
            Debug.Log("Random Skill Index: " + indexSkill);

            if (indexSkill == 0)
            {
                Debug.Log("Setting Attack Trigger");
                CastSkill1();
            }
            else if (indexSkill == 1)
            {
                Debug.Log("Setting Attack_1 Trigger");
                CastSkill2();

            }
        }
    }

    public void CastSkill1()
    {
        if (playerTransform != null)
        {
            animator.SetInteger("DemonSkill1", 0);
            Vector2 direction = (playerTransform.position - spawnPoint.position).normalized;
            GameObject skill = Instantiate(skillPrefabs[0], spawnPoint.position, Quaternion.identity);
            skill.GetComponent<Rigidbody2D>().linearVelocity = direction * speedSkill;
        }
    }

    public void CastSkill2()
    {
        animator.SetInteger("DemonSkill2", 1);
        foreach (Transform point in spawnPoints2)
        {
            Instantiate(skillPrefabs[1], point.position, Quaternion.identity);
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
    private void Died()
    {
        if (!IsDied()) return;
        if (foreground != null)
        {
            foreground.gameObject.SetActive(true);
        }
    }
}
