using System;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private Weapon[] weapons;

    private int currentWeaponIndex = 0;
    private Weapon currentWeapon;

    private void Start()
    {
        EquipWeapon(0);
    }

    private void EquipWeapon(int index)
    {
        if (currentWeapon != null)
        {
            currentWeapon.gameObject.SetActive(false);
        }

        currentWeapon = weapons[index];
        currentWeapon.gameObject.SetActive(true);
    }

    public void SwitchWeapon()
    {
        currentWeaponIndex = (currentWeaponIndex + 1) % weapons.Length;
        EquipWeapon(currentWeaponIndex);
    }

    public void Attack()
    {
        currentWeapon?.Attack();
    }

    public void StopAttack()
    {
        currentWeapon?.StopAttack();
    }    

    internal void UsingSkill()
    {
        if (currentWeapon.IsHavingSkill())
        {
            currentWeapon.UsingSkill();
        }
    }

    internal void SetStoneToWeapon(string stoneName)
    {
        currentWeapon.AddStone(stoneName);
        currentWeapon.UpdateWeaponStats();
    }
}