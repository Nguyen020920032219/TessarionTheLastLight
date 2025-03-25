using Unity.VisualScripting;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private Collider2D arrowCollider;
    [SerializeField] public float damage { get; set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                Debug.Log("enemy take: " + damage + " damage");
            }
            FindAnyObjectByType<ObjectPool>().ReturnObject(gameObject);
        }
    }
}