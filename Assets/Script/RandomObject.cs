using System.Collections.Generic;
using UnityEngine;

public class RandomObject : MonoBehaviour
{
    public GameObject[] myObjects;
    public int spawnCount = 6;

    [Header("Spacing Settings")]
    public float minDistance = 15f;
    public int maxAttempts = 30;

    private List<Vector3> spawnedPositions = new List<Vector3>();

    void Start()
    {
        SpawnObjects();
    }

    void SpawnObjects()
    {
        if (myObjects == null || myObjects.Length == 0)
            return;

        for (int i = 0; i < spawnCount; i++)
        {
            Vector3 spawnPosition;
            bool foundValidPosition = TryFindValidPosition(out spawnPosition);

            if (!foundValidPosition)
                continue;

            int randomIndex = Random.Range(0, myObjects.Length);

            Instantiate(
                myObjects[randomIndex],
                spawnPosition,
                Quaternion.identity
            );

            spawnedPositions.Add(spawnPosition);
        }
    }

    bool TryFindValidPosition(out Vector3 result)
    {
        for (int attempt = 0; attempt < maxAttempts; attempt++)
        {
            float randomX = Random.Range(-30f, 30f);
            float randomZ = Random.Range(-75f, 75f);
            float spawnY = 10f;

            Vector3 candidate = new Vector3(randomX, spawnY, randomZ);

            if (IsFarEnough(candidate))
            {
                result = candidate;
                return true;
            }
        }

        result = Vector3.zero;
        return false;
    }

    bool IsFarEnough(Vector3 candidate)
    {
        foreach (Vector3 existingPos in spawnedPositions)
        {
            float distance = Vector2.Distance(
                new Vector2(candidate.x, candidate.z),
                new Vector2(existingPos.x, existingPos.z)
            );

            if (distance < minDistance)
                return false;
        }

        return true;
    }
}