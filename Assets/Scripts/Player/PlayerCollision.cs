using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private GameObject player;
    private PlayerManager playerManager;
    private PlayerController playerController;
    private WeaponManager weaponManager;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerManager = player.GetComponent<PlayerManager>();
            playerController = player.GetComponent<PlayerController>();
            weaponManager = player.GetComponent<WeaponManager>();
        }
    }

    private bool isEnterOraccleStone = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("AAAAAAAAAA");
            playerManager.TakeDamage(5);
        }
        if (collision.CompareTag("OracleStone"))
        {
            if (!isEnterOraccleStone)
            {
                Debug.Log("Oracle Stone!!!!");
                weaponManager.SetStoneToWeapon("OracleStone");
                isEnterOraccleStone = true;
            }
            else
            {
                return;
            }
        }
    }
}