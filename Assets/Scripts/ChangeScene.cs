using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy")) return;
        if(collision.CompareTag("Arrow")) return;
        SceneManager.LoadScene(3);
    }
}