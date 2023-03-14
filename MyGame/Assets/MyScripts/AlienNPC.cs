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

	private Vector3 destination;
	private NavMeshAgent agent;




	private Vector3 initialPosition;
	private Vector3 nextDestination;
	private float maxDistance = 300f;
	private float timer = 0f;
	private float changeDestinationTime = 3f;

	void Start()
	{
		// Get a reference to the player
		player = GameObject.FindWithTag("Player");

		// Get the NPC's NavMeshAgent component
		agent = GetComponent<NavMeshAgent>();

		initialPosition = transform.position;
		nextDestination = GetRandomDestination();
		agent.SetDestination(nextDestination);
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
}