using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdSpawner : MonoBehaviour
{
    [Header("Bird")]
    [SerializeField] private GameObject birdPrefab;

    [Header("Spawn Area")]
    [SerializeField] private BoxCollider2D spawnArea;
    [SerializeField] private BoxCollider2D topWall;

    [Header("Spawn Timing")]
    [SerializeField] private float minimumSpawnDelay = 0.5f;
    [SerializeField] private float maximumSpawnDelay = 1.5f;

    private float bottomPadding = 0.1f;
    private float topPadding = 0.1f;
    private float topExtraHeight = 1f;

    private const int NumberOfLanes = 5;

    private int previousLane = -1;
    private int secondPreviousLane = -1;

    private IEnumerator Start()
    {
        while (true)
        {
            SpawnBird();

            float delay = Random.Range(
                minimumSpawnDelay,
                maximumSpawnDelay
            );

            yield return new WaitForSeconds(delay);
        }
    }

    private void SpawnBird()
    {
        if (birdPrefab == null ||
            spawnArea == null ||
            topWall == null)
        {
            return;
        }

        GameObject bird = Instantiate(
            birdPrefab,
            spawnArea.bounds.center,
            Quaternion.identity
        );

        SpriteRenderer[] birdRenderers =
            bird.GetComponentsInChildren<SpriteRenderer>();

        if (birdRenderers.Length == 0)
        {
            Destroy(bird);
            return;
        }

        Bounds birdBounds = birdRenderers[0].bounds;

        for (int i = 1; i < birdRenderers.Length; i++)
        {
            birdBounds.Encapsulate(
                birdRenderers[i].bounds
            );
        }

        Bounds areaBounds = spawnArea.bounds;

        float spawnX = areaBounds.center.x;
        float rootY = bird.transform.position.y;

        float rootToVisualBottom =
            rootY - birdBounds.min.y;

        float rootToVisualTop =
            birdBounds.max.y - rootY;

        float minimumY =
            areaBounds.min.y
            + bottomPadding
            + rootToVisualBottom;

        float maximumY =
            areaBounds.max.y
            - topWall.bounds.size.y
            - topPadding
            - rootToVisualTop
            + topExtraHeight;

        if (minimumY >= maximumY)
        {
            Destroy(bird);
            return;
        }

        List<int> availableLanes = new List<int>();

        for (int lane = 0; lane < NumberOfLanes; lane++)
        {
            if (lane == previousLane ||
                lane == secondPreviousLane)
            {
                continue;
            }

            availableLanes.Add(lane);
        }

        int randomIndex = Random.Range(
            0,
            availableLanes.Count
        );

        int selectedLane =
            availableLanes[randomIndex];

        float usableHeight =
            maximumY - minimumY;

        float laneHeight =
            usableHeight / NumberOfLanes;

        float laneMinimumY =
            minimumY + laneHeight * selectedLane;

        float laneMaximumY =
            laneMinimumY + laneHeight;

        float spawnY = Random.Range(
            laneMinimumY,
            laneMaximumY
        );

        bird.transform.position = new Vector3(
            spawnX,
            spawnY,
            0f
        );

        secondPreviousLane = previousLane;
        previousLane = selectedLane;
    }
}