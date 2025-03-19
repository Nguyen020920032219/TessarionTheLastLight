using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform player;
    [SerializeField] private float spawnRadius = 5f;
    private bool hasSpawned = false;

    private void Update()
    {
        if (!hasSpawned)
        {
            float distance = Vector3.Distance(transform.position, player.position);
            if (distance <= spawnRadius)
            {
                SpawnEnemy();
                hasSpawned = true; 
            }
        }
    }

    private void SpawnEnemy()
    {
        Vector3 spawnPosition = transform.position + new Vector3(0, 1, 0);
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}