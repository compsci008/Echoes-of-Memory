using System.Collections.Generic;
using UnityEngine;

public class RandomSpawning : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private GameObject spawnObject;

    [Header("PowerUp Orbs")]
    [SerializeField] private List<GameObject> orbPrefabs = new List<GameObject>();
    [Range(0f, 1f)]
    [SerializeField] private float orbSpawnChance = 0.2f; // 20% chance to spawn an orb

    [Header("Spawn Area")]
    [SerializeField] private float minXPos;
    [SerializeField] private float maxXPos;
    [SerializeField] private float minYPos;
    [SerializeField] private float maxYPos;
    [SerializeField] private float spawnZPos = -1f;

    [Header("Spawn Settings")]
    [SerializeField] private float spawnInterval = 5f;

    private float currentSpawnTimer;

    private void Start()
    {
        currentSpawnTimer = 0f;
    }

    private void Update()
    {
        currentSpawnTimer -= Time.deltaTime;

        if (currentSpawnTimer <= 0f)
        {
            Spawn();
            currentSpawnTimer = spawnInterval;
        }
    }

    private void Spawn()
    {
        float randomX = Random.Range(minXPos, maxXPos);
        float randomY = Random.Range(minYPos, maxYPos);
        Vector3 randomSpawnPosition = new Vector3(randomX, randomY, spawnZPos);

        if (orbPrefabs.Count > 0 && Random.value < orbSpawnChance)
        {
            // Spawn random orb
            GameObject orb = orbPrefabs[Random.Range(0, orbPrefabs.Count)];
            Instantiate(orb, randomSpawnPosition, Quaternion.identity);
        }
        else
        {
            // Spawn target
            Instantiate(spawnObject, randomSpawnPosition, Quaternion.identity);
        }
    }
}