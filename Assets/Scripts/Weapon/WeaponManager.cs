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

    public void SetStoneToWeapon(string stoneName)
    {
        if (stoneName == "OracleStone")
        {
            currentWeapon.BuffAttackSpeed(2);
        }
    }
}