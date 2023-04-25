using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DestroyBuildings : MonoBehaviour
{

	public int startingHealth = 100; // starting health of tge building

	public int currentHealth; // current health
	 
	public float minCubeSize = 0.5f; // rand cube size

	public float maxCubeSize = 2.0f;// rand cube size

	public int numCubesToSpawn = 50;
	// Declare a variable to hold the size of the cube's bounds.
	public Vector3 cubeBounds = new Vector3(51.0f, 51.0f, 51.0f);


	private GreenDogAlien dogScript;
	public AudioSource audioSource; // AudioSource component to play the destruction sound

	void Start()
	{
		currentHealth = startingHealth;
		dogScript = FindObjectOfType<GreenDogAlien>();
	}

	void Update()
	{

	}


	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "flyingBullet")
		{

			currentHealth -= 20;

			// Check if the cube's health has reached 0.
			if (currentHealth <= 0)
			{
				// If the cube's health has reached 0, destroy the cube.
				Destroy(gameObject);
				Destroy(other.gameObject);

				createBlocks();
				playDestructionSound(); // play the destruction sound


			}

		}
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

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "fireball" || collision.gameObject.tag == "flyingBullet")
		{

			currentHealth -= 20;

			// Check if the cube's health has reached 0.
			if (currentHealth <= 0)
			{
				// If the cube's health has reached 0, destroy the cube.
				Destroy(gameObject);
				Destroy(collision.gameObject);

				createBlocks();
				playDestructionSound(); // play the destruction sound


			}

		}
		if (collision.gameObject.tag == "redDog")
		{
			currentHealth -= 51;

			// Check if the cube's health has reached 0.
			if (currentHealth <= 0)
			{
				// If the cube's health has reached 0, destroy the cube.
				Destroy(gameObject);
				createBlocks();
				playDestructionSound(); // play the destruction sound


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






	void playDestructionSound()
	{
		
			audioSource.Play();
			Debug.Log("sound");
		
	}

}