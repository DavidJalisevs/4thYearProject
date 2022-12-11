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

	void Start()
	{
		// Get a reference to the player
		player = GameObject.FindWithTag("Player");

		// Get the NPC's NavMeshAgent component
		agent = GetComponent<NavMeshAgent>();

		// Set the NPC's initial destination to a random point within the specified bounds
		destination = new Vector3(
			Random.Range(boundsMin.x, boundsMax.x),
			Random.Range(boundsMin.y, boundsMax.y),
			Random.Range(boundsMin.z, boundsMax.z)
		);
	}

	void Update()
	{
		// If the player is within the follow distance, start following them
		if (Vector3.Distance(transform.position, player.transform.position) < followDistance)
		{
			// Set the NPC's destination to the player's position
			destination = player.transform.position;
			agent.speed = speed;
		}
		else
		{
			// If the NPC has reached current destination, set a new random destination within the bounds
			if (Vector3.Distance(transform.position, destination) < 0.1f)
			{
				destination = new Vector3(
					Random.Range(boundsMin.x, boundsMax.x),
					Random.Range(boundsMin.y, boundsMax.y),
					Random.Range(boundsMin.z, boundsMax.z)
				);
			}

			// Set the NPC's speed to the specified wander speed
			agent.speed = speed * 0.5f;
		}

		// Set the NPC's destination
		agent.destination = destination;
	}
}