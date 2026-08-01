using System.Collections.Generic;
using UnityEngine;

public class RandomSpawning : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private GameObject spawnObject;

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
            SpawnTarget();
            currentSpawnTimer = spawnInterval;
        }
    }

    private void SpawnTarget()
    {
        float randomX = Random.Range(minXPos, maxXPos);
        float randomY = Random.Range(minYPos, maxYPos);

        Vector3 randomSpawnPosition = new Vector3(randomX, randomY, spawnZPos);

        GameObject newTarget = Instantiate(spawnObject, randomSpawnPosition, Quaternion.identity);
    }
}