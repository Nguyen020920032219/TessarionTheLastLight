using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public GameObject[] weapons;
    private int currentWeaponIndex = 0;
    private Animator animator;
    private Sword sword;
    private Bow bow;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        sword = GetComponentInChildren<Sword>();
        bow = GetComponentInChildren<Bow>();
    }

    void Start()
    {
        UpdateWeapon();
    }

    public void SwitchWeapon()
    {
        currentWeaponIndex = (currentWeaponIndex + 1) % weapons.Length;
        UpdateWeapon();
    }

    void UpdateWeapon()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(i == currentWeaponIndex);
        }
    }

    public void Attack()
    {
        if (currentWeaponIndex == 0)
        {
            sword?.StartAttack();
        }
        else if (currentWeaponIndex == 1)
        {
            bow?.StartShooting();
        }
    }

    public void StopAttack()
    {
        if (currentWeaponIndex == 0)
        {
            sword?.StopAttack();
        }
        else if (currentWeaponIndex == 1)
        {
            bow?.StopShooting();
        }
    }
}