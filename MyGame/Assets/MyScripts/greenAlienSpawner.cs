using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class greenAlienSpawner : MonoBehaviour
{
	public GameObject greenAlien;
	public float minSpawnDelay = 5f;
	public float maxSpawnDelay = 10f;
	public int maxAliens = 10;

	private float nextSpawnTime = 0f;
	private int currentAliens = 0;

	private void Start()
	{
		nextSpawnTime = Time.time + minSpawnDelay;
	}


	private void Update()
	{
		if (Time.time >= nextSpawnTime && currentAliens < maxAliens)
		{
			SpawnRedAlienDog();
			nextSpawnTime = Time.time + Random.Range(minSpawnDelay, maxSpawnDelay);
		}
	}

	private void SpawnRedAlienDog()
	{
		GameObject newDog = Instantiate(greenAlien, transform.position, Quaternion.identity);
		currentAliens++;
		newDog.name = "RedAlienDog " + currentAliens;
	}

	public void DecreaseDogCount()
	{
		currentAliens--;
	}
}
