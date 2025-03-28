using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Wizard : Enemy
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
    [SerializeField] private GameObject chest;
    [SerializeField] private GameObject stone;

    private Animator animator;
    private Transform playerTransform;

    void Start()
    {
        InitStat(hp, damage, speed, objScale, hpBar);
        UpdateHpBar();
        animator = GetComponent<Animator>();
        chest.SetActive(false);
        stone.SetActive(false);

        Debug.Log("Chest active after Start(): " + chest.activeSelf);
        Debug.Log("Stone active after Start(): " + stone.activeSelf);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
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
                CastSkill1();
            }
            else if (indexSkill == 1)
            {
                CastSkill2();
            }
            else if (indexSkill == 2)
            {
                CastSkill3();

            }
        }
    }

    public void CastSkill1()
    {
        if (playerTransform != null)
        {
            animator.SetInteger("WizardSkill1", 0);
            Vector2 direction = (playerTransform.position - spawnPoint.position).normalized;
            GameObject skill = Instantiate(skillPrefabs[0], spawnPoint.position, Quaternion.identity);
            skill.GetComponent<Rigidbody2D>().linearVelocity = direction * speedSkill;
        }
    }
    public void CastSkill2()
    {
        if (playerTransform != null)
        {
            animator.SetInteger("WizardSkill2", 1);
            Vector2 direction = (playerTransform.position - spawnPoint.position).normalized;
            GameObject skill = Instantiate(skillPrefabs[1], spawnPoint.position, Quaternion.identity);
            skill.GetComponent<Rigidbody2D>().linearVelocity = direction * speedSkill;
        }
    }

    public void CastSkill3()
    {
        animator.SetInteger("WizardSkill3", 2);
        Vector2 diagonalDirection = new Vector2(-1, -1).normalized;
        foreach (Transform point in spawnPoints2)
        {
            GameObject skill = Instantiate(skillPrefabs[2], point.position, Quaternion.identity);

            skill.GetComponent<Rigidbody2D>().linearVelocity = diagonalDirection * speedSkill;
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
        if (IsDied())
        {
            chest.SetActive(true);
            stone.SetActive(true);
        }
        
    }
}
