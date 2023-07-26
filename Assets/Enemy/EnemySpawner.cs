using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject defaultEnemy;
    public List<GameObject> enemyPrefabs; // Assign your enemy prefabs in the Unity editor
    public float spawnInterval = 10f; // Time between each spawn

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            // Choose a random enemy prefab from the list
            int prob = Random.Range(0, 100);
            if (prob <= 10)
            {
                GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
                Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                GameObject enemyPrefab = defaultEnemy;
                Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            }
            

            ;
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.5f);
    }
}
