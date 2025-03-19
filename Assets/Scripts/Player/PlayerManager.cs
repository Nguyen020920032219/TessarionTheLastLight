using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private float Hp = 20f;
    [SerializeField] private float Mp = 20f;
    [SerializeField] private float Speed = 20f;
    [SerializeField] private float Defend = 20f;
    [SerializeField] private float Strength = 20f;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private ManaBar manaBar;

    private GameObject player;
    private PlayerController playerController;
    private float currentHealth;
    private float currentMana;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerController = player.GetComponent<PlayerController>();

            currentHealth = Hp;
            currentMana = Mp;
            healthBar.SetMaxHealth(Hp);
            manaBar.SetMaxMana(Mp);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth-=damage;
        healthBar.SetHealth(currentHealth);
        if(currentHealth <= 0)
        {
            playerController.Die();
        }
    }

    public void ResetHp()
    {
        currentHealth = Hp;
    }
}
