using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class greenAlienSpawner : MonoBehaviour
{   // The prefab for the aliento be spawned.

	public GameObject greenAlien;
	// The minimum and maximum delay between spawns.

	public float minSpawnDelay = 5f;
	public float maxSpawnDelay = 10f;
	private float nextSpawnTime = 0f;// The time when the next spawn will occur.

	public int maxAliens = 10; // The maximum number of dogs that can exist at any one time.
	private int currentAliens = 0;

	private void Start()
	{
		nextSpawnTime = Time.time + minSpawnDelay;
	}


	private void Update()
	{
		// spawn a new red alien dog and set the next spawn time to a random value between the minimum and maximum spawn delays.

		if (Time.time >= nextSpawnTime && currentAliens < maxAliens)
		{
			SpawnAlien();
			nextSpawnTime = Time.time + Random.Range(minSpawnDelay, maxSpawnDelay);
		}
	}
	// Instantiate a new red alien dog at the spawner's position, increment the current dog count, and set the dog's name.

	private void SpawnAlien()
	{
		GameObject newAlien = Instantiate(greenAlien, transform.position, Quaternion.identity);
		currentAliens++;
		newAlien.name = "alienWalk " + currentAliens;
	}
	// Decrement the current dog count when a red alien dog is destroyed.
	public void DecreaseDogCount()
	{
		currentAliens--;
	}
}
