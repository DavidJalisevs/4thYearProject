using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AlienNPC : MonoBehaviour
{
	// The bounds within which the NPC will wander
	public Vector3 boundsMin;
	public Vector3 boundsMax;

	// The NPC's movement speed
	public float speed = 5.0f;

	// The distance at which the NPC will start following the player
	public float followDistance = 100.0f;

	// A reference to the player
	private GameObject player;

	private NavMeshAgent agent;




	private Vector3 initialPosition;
	private Vector3 nextDestination;
	private float maxDistance = 300f;
	private float timer = 0f;
	private float changeDestinationTime = 3f;
	private GameManager gameManagerScript;
	private HealthManager healthManagerScript;


	void Start()
	{
		// Get a reference to the player
		player = GameObject.FindWithTag("Player");

		// Get the NPC's NavMeshAgent component
		agent = GetComponent<NavMeshAgent>();

		initialPosition = transform.position;
		nextDestination = GetRandomDestination();
		agent.SetDestination(nextDestination);
		gameManagerScript = FindObjectOfType<GameManager>();
		healthManagerScript = FindObjectOfType<HealthManager>();

	}

	void Update()
	{

		timer += Time.deltaTime;
		
		if (Vector3.Distance(transform.position, initialPosition) >= maxDistance)
		{
			agent.SetDestination(initialPosition);
		}

		// If the player is within the follow distance, start following them
		if (Vector3.Distance(transform.position, player.transform.position) < followDistance)
		{
			// Set the NPC's destination to the player's position
			agent.SetDestination(player.transform.position);
			agent.speed = speed;
		}
		else
		{
			if (timer >= changeDestinationTime)
			{
				nextDestination = GetRandomDestination();
				agent.SetDestination(nextDestination);
				timer = 0f;
			}

			// Set the NPC's speed to the specified wander speed
			agent.speed = speed * 0.5f;
		}
	}

	Vector3 GetRandomDestination()
	{
		float randomX = Random.Range(initialPosition.x - maxDistance, initialPosition.x + maxDistance);
		float randomZ = Random.Range(initialPosition.z - maxDistance, initialPosition.z + maxDistance);
		Vector3 randomDestination = new Vector3(randomX, initialPosition.y, randomZ);
		return randomDestination;
	}

	void OnCollisionEnter(Collision collision)
	{
		// Check if the player has pressed the space bar.
		//Check for a match with the specific tag on any GameObject that collides with your GameObject
		if (collision.gameObject.tag == "fireball")
		{
			Debug.Log("collision with fireball and dog");
			// If the space bar is pressed, decrease the cube's current health by 20.
			Destroy(collision.gameObject);
			Destroy(gameObject);
			gameManagerScript.score = gameManagerScript.score + 35;


		}

		if (collision.gameObject.tag == "Player")
		{
			Debug.Log("Building and dog");
			// If the space bar is pressed, decrease the cube's current health by 20.
			healthManagerScript.takeDamage(20);

		}

	}


}