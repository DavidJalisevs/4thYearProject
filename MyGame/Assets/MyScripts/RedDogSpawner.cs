using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedDogSpawner : MonoBehaviour
{
    public GameObject redAlienDogPrefab;
    public float minSpawnDelay = 5f;
    public float maxSpawnDelay = 10f;
    public int maxDogs = 10;

    private float nextSpawnTime = 0f;
    private int currentDogs = 0;

    private void Start()
    {
        nextSpawnTime = Time.time + minSpawnDelay;
    }


    private void Update()
    {
        if (Time.time >= nextSpawnTime && currentDogs < maxDogs)
        {
            SpawnRedAlienDog();
            nextSpawnTime = Time.time + Random.Range(minSpawnDelay, maxSpawnDelay);
        }
    }

    private void SpawnRedAlienDog()
    {
        GameObject newDog = Instantiate(redAlienDogPrefab, transform.position, Quaternion.identity);
        currentDogs++;
        newDog.name = "RedAlienDog " + currentDogs;
    }

    public void DecreaseDogCount()
    {
        currentDogs--;
    }
}
