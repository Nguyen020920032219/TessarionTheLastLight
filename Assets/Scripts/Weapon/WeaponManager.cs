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

    internal void SaveStones()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            string weaponType = (i == 0) ? "Sword" : "Bow";
            string[] stones = weapons[i].GetStones();
            int stoneCount = Mathf.Min(stones.Length, 4);
            PlayerPrefs.SetInt($"Number{weaponType}Stones", stoneCount);

            for (int j = 0; j < stoneCount; j++)
            {
                if (!string.IsNullOrEmpty(stones[j]))
                {
                    PlayerPrefs.SetString($"{weaponType}Stone{j}", stones[j]);
                }
            }
        }

        PlayerPrefs.Save();
    }

    internal void LoadStones()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            string weaponType = (i == 0) ? "Sword" : "Bow";
            if (!PlayerPrefs.HasKey($"Number{weaponType}Stones")) continue;

            int stoneCount = Mathf.Min(PlayerPrefs.GetInt($"Number{weaponType}Stones"), 4);

            for (int j = 0; j < stoneCount; j++)
            {
                string stoneKey = $"{weaponType}Stone{j}";
                if (PlayerPrefs.HasKey(stoneKey))
                {
                    weapons[i].AddStone(PlayerPrefs.GetString(stoneKey));
                }
            }

            weapons[i].UpdateWeaponStats();
        }
    }
}