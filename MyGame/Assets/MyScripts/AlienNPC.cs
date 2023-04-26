using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AlienNPC : MonoBehaviour
{
	// The bounds within which the NPC will wander
	public Vector3 boundsMin; // min bounds npc can walk
	public Vector3 boundsMax; // max bounds npc can walk

	// The NPC's movement speed
	public float speed = 5.0f;

	// The distance at which the NPC will start following the player
	public float followDistance = 100.0f;

	// A reference to the player
	private GameObject player;

	private NavMeshAgent agent;



	// The NPC's initial position
	private Vector3 initialPosition; 
	private Vector3 nextDestination;// npc next pos
	private float maxDistance = 300f; // The maximum distance the NPC can wander from its initial position
	private float timer = 0f;   // The timer used for changing destinations
	private float changeDestinationTime = 3f;   // The time interval between destination changes

	private GameManager gameManagerScript; // game manager script reference 
	private HealthManager healthManagerScript; // health manage script rerefence
	 

	void Start()
	{
		// Get a reference to the player
		player = GameObject.FindWithTag("Player");

		// Get the NPC's NavMeshAgent component
		agent = GetComponent<NavMeshAgent>();

		// instatiate all the stuff
		initialPosition = transform.position;
		nextDestination = GetRandomDestination();
		agent.SetDestination(nextDestination);
		gameManagerScript = FindObjectOfType<GameManager>();
		healthManagerScript = FindObjectOfType<HealthManager>();

	}

	void Update()
	{

		timer += Time.deltaTime; // timer here 
		

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

	// randomc generation position cfucntion
	Vector3 GetRandomDestination()
	{

		float randomX = Random.Range(initialPosition.x - maxDistance, initialPosition.x + maxDistance); // get ranbdom x value for pos
		float randomZ = Random.Range(initialPosition.z - maxDistance, initialPosition.z + maxDistance); // get random y z value
		Vector3 randomDestination = new Vector3(randomX, initialPosition.y, randomZ); // add those values to the random dest and return it
		return randomDestination;
	}

	// collision check fucntion
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