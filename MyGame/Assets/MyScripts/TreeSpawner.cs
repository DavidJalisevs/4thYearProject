using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpawner : MonoBehaviour
{
    public GameObject[] treePrefabs;
    public float spawnRadius = 200f;
    public float density = 0.5f;

    void Start()
    {
        GenerateForest();
    }

    void GenerateForest()
    {
        Vector3 center = transform.position;
        float spawnArea = spawnRadius * spawnRadius * Mathf.PI;
        int numTrees = Mathf.RoundToInt(spawnArea * density);

        for (int i = 0; i < numTrees; i++)
        {
            Vector3 randomPos = Random.insideUnitCircle * spawnRadius;
            randomPos.z = randomPos.y;
            randomPos.y = 0f;
            Instantiate(treePrefabs[Random.Range(0, treePrefabs.Length)], center + randomPos, Quaternion.identity);
        }
    }
}