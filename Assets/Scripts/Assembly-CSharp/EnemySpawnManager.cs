using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;  // Enemy prefab to be spawned
    public Transform spawnParent;   // Parent object that holds all spawn points
    private Transform[] spawnPoints;  // Array of spawn points

    public float spawnDelay = 2f;  // Delay between each spawn

    private void Start()
    {
        // Get all child transforms (spawn points) under the parent object
        spawnPoints = spawnParent.GetComponentsInChildren<Transform>();

        // Start spawning enemies
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        // Here we assume we are spawning enemies with fixed IDs 1 and 2 for this example

        // Loop to spawn 1st enemy (ID 1) and 2nd enemy (ID 2)
        for (int i = 0; i < 2; i++)
        {
            int enemyId = (i == 0) ? 1 : 2;  // Set enemy ID to 1 for first spawn, and 2 for second spawn

            // Get a random spawn point (skip the first one because it's the parent object)
            Transform spawnPoint = spawnPoints[Random.Range(1, spawnPoints.Length)];
            
            // Spawn the enemy at the spawn point
            GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);

            // Set specific properties for each enemy ID
            // If you need to reference specific properties or behavior based on the ID, you can do so here

            if (enemyId == 1)
            {
                // Example: Set some properties based on enemy ID 1 (you can customize this as needed)
                // This is where you'd set properties specific to this enemy ID if needed.
                Debug.Log("Spawned Enemy ID 1");
            }
            else if (enemyId == 2)
            {
                // Example: Set some properties based on enemy ID 2 (you can customize this as needed)
                // This is where you'd set properties specific to this enemy ID if needed.
                Debug.Log("Spawned Enemy ID 2");
            }

            // Wait before spawning the next enemy
            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
