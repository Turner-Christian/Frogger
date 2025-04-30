using UnityEngine;

public class LogSpawner : MonoBehaviour
{
    public GameObject logPrefab; // Prefab of the car to spawn
    public float minSpawnTime = 1f; // Minimum time between spawns
    public float maxSpawnTime = 3f; // Maximum time between spawns
    public float timeUntilNextSpawn; // Time until the next spawn
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        timeUntilNextSpawn = 0f; // Initialize the time until the next spawn to 0
    }

    // Update is called once per frame
    void Update()
    {
        timeUntilNextSpawn -= Time.deltaTime; // Decrease the time until the next spawn

        if (timeUntilNextSpawn <= 0f) // Check if it's time to spawn a new car
        {
            SpawnLog(); // Spawn a new car
            SetTimeUntilNextSpawn(); // Reset the time until the next spawn
        }
    }

    private void SetTimeUntilNextSpawn()
    {
        timeUntilNextSpawn = Random.Range(minSpawnTime, maxSpawnTime); // Set a random time until the next spawn
    }

    private void SpawnLog()
    {
        Instantiate(logPrefab, transform.position, Quaternion.identity); // Instantiate a new car at the spawner's position with no rotation
    }
}
