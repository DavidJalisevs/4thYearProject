using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DestroyBuildings : MonoBehaviour
{

	public int startingHealth = 100;

	public int currentHealth;

	public float minCubeSize = 0.5f;

	public float maxCubeSize = 2.0f;

	public int numCubesToSpawn = 50;
	// Declare a variable to hold the size of the cube's bounds.
	public Vector3 cubeBounds = new Vector3(51.0f, 51.0f, 51.0f);

	void Start()
	{
		currentHealth = startingHealth;
	}

	void Update()
	{

	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "fireball" || collision.gameObject.tag == "flyingBullet")
		{
			{
				currentHealth -= 20;

				// Check if the cube's health has reached 0.
				if (currentHealth <= 0)
				{
					// If the cube's health has reached 0, destroy the cube.
					Destroy(gameObject);
					createBlocks();


				}
			}
		}
	}


	//void createBlocks()
	//{
	//	for (int i = 0; i < numCubesToSpawn; i++)
	//	{
	//		// Create a random-sized cube at a random position within the bounds of the destroyed cube.
	//		GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
	//		cube.transform.position = transform.position + new Vector3(
	//			Random.Range(-cubeBounds.x / 2.0f, cubeBounds.x / 2.0f),
	//			Random.Range(-cubeBounds.y / 2.0f, cubeBounds.y / 2.0f),
	//			Random.Range(-cubeBounds.z / 2.0f, cubeBounds.z / 2.0f)
	//		);

	//		cube.AddComponent<Rigidbody>();

	//		float scale = Random.Range(minCubeSize, maxCubeSize);
	//		cube.transform.localScale = new Vector3(scale, scale, scale);

	//		// Destroy the cube after 5 seconds.
	//		//Destroy(cube, 5.0f);
	//	}

	//}

	void createBlocks()
	{
		// Get the bounds of the old cube.
		var bounds = gameObject.GetComponent<Collider>().bounds;

		for (int i = 0; i < numCubesToSpawn; i++)
		{
			// Create a random-sized cube at a random position within the bounds of the destroyed cube.
			GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
			cube.transform.position = transform.position + new Vector3(
				Random.Range(-bounds.extents.x, bounds.extents.x),
				Random.Range(-bounds.extents.y, bounds.extents.y),
				Random.Range(-bounds.extents.z, bounds.extents.z)
			);

			// Add a Rigidbody component to the cube so it can be affected by physics.
			cube.AddComponent<Rigidbody>();

			// Randomize the scale of the cube.
			float scale = Random.Range(minCubeSize, maxCubeSize);
			cube.transform.localScale = new Vector3(scale, scale, scale);

			// Destroy the cube after 5 seconds.
			Destroy(cube, 5.0f);
		}
	}

}