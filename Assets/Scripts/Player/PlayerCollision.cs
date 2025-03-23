using UnityEngine;

public class Player : MonoBehaviour
{
    private GameObject player;
    private PlayerManager playerManager;
    private WeaponManager weaponManager;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerManager = player.GetComponent<PlayerManager>();
            weaponManager = player.GetComponent<WeaponManager>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Enemy"))
        {
                Debug.Log("AAAAAAAAAA");
                playerManager.TakeDamage(5);
        }
        if (collision.CompareTag("AstralStone"))
        {
            Debug.Log("Astral Stone!!!!");
            weaponManager.GetCurrentWeapon().AddStone("Astral");
            weaponManager.GetCurrentWeapon().UpdateWeaponStats();
        }
        if (collision.CompareTag("IgnisStone"))
        {
            Debug.Log("Ignis Stone!!!!");
            weaponManager.GetCurrentWeapon().AddStone("Ignis");
            weaponManager.GetCurrentWeapon().UpdateWeaponStats();
        }
        if (collision.CompareTag("VitalisStone"))
        {
            Debug.Log("Vitalis Stone!!!!");
            weaponManager.GetCurrentWeapon().AddStone("Vitalis");
            weaponManager.GetCurrentWeapon().UpdateWeaponStats();
        }
        if (collision.CompareTag("AegisStone"))
        {
            Debug.Log("Aegis Stone!!!!");
            weaponManager.GetCurrentWeapon().AddStone("Aegis");
            weaponManager.GetCurrentWeapon().UpdateWeaponStats();
        }
    }
}