using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private GameObject player;
    private PlayerManager playerManager;
    private WeaponManager weaponManager;
    [SerializeField] private GameObject menuWin;

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
        if (collision.CompareTag("BossSkill"))
        {
            playerManager.TakeDamage(25);
        }
        if (collision.CompareTag("AstralStone"))
        {
            Debug.Log("Astral Stone!!!!");
            weaponManager.SetStoneToWeapon("Astral");
        }
        if (collision.CompareTag("IgnisStone"))
        {
            Debug.Log("Ignis Stone!!!!");
            weaponManager.SetStoneToWeapon("Ignis");
        }
        if (collision.CompareTag("VitalisStone"))
        {
            Debug.Log("Vitalis Stone!!!!");
            weaponManager.SetStoneToWeapon("Vitalis");
        }
        if (collision.CompareTag("AegisStone"))
        {
            Debug.Log("Aegis Stone!!!!");
            weaponManager.SetStoneToWeapon("Aegis");
        }
        if (collision.CompareTag("GateWay"))
        {
            SceneManager.LoadScene("Level_5_2");
        }
        if (collision.CompareTag("GenesisStone"))
        {
            Time.timeScale = 0;
            menuWin.SetActive(true);
        }
        if (collision.CompareTag("Stone"))
        {
            SceneManager.LoadScene("Level_3");
        }
        if (collision.CompareTag("Stone3"))
        {
            SceneManager.LoadScene("Level_4");
        }
        if (collision.CompareTag("Stone4"))
        {
            SceneManager.LoadScene("Level_5_1");
        }
    }
}