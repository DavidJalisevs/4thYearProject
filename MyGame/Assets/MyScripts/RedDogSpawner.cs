using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedDogSpawner : MonoBehaviour
{
	// The prefab for the red alien dog to be spawned.

	public GameObject redAlienDogPrefab;
	// The minimum and maximum delay between spawns.
	public float minSpawnDelay = 5f;
    public float maxSpawnDelay = 10f;
    public int maxDogs = 10;    // The maximum number of dogs that can exist at any one time.


	private float nextSpawnTime = 0f;    // The time when the next spawn will occur.
	private int currentDogs = 0;

    private void Start()
    {
        nextSpawnTime = Time.time + minSpawnDelay;
    }

	private void Update()
    {
		// spawn a new red alien dog and set the next spawn time to a random value between the minimum and maximum spawn delays.
		if (Time.time >= nextSpawnTime && currentDogs < maxDogs)
        {
            SpawnRedAlienDog();
            nextSpawnTime = Time.time + Random.Range(minSpawnDelay, maxSpawnDelay);
        }
    }
	// Instantiate a new red alien dog at the spawner's position, increment the current dog count, and set the dog's name.
	private void SpawnRedAlienDog()
    {
        GameObject newDog = Instantiate(redAlienDogPrefab, transform.position, Quaternion.identity);
        currentDogs++;
        newDog.name = "RedAlienDog " + currentDogs;
    }

	// Decrement the current dog count when a red alien dog is destroyed.
	public void DecreaseDogCount()
    {
        currentDogs--;
    }
}
