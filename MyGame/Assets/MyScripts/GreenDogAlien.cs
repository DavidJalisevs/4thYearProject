using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GreenDogAlien : MonoBehaviour
{
    //public GameObject destinationPoint;
    NavMeshAgent theAgent;

    private Vector3 initialPosition;
    private Vector3 nextDestination;
    private float maxDistance = 150f;
    private float timer = 0f;
    private float changeDestinationTime = 3f;
    private GameObject player;
    public float speed = 5.0f;
    public float followDistance = 100.0f;


    void Start()
    {
        theAgent = GetComponent<NavMeshAgent>();
        initialPosition = transform.position;
        nextDestination = GetRandomDestination();
        theAgent.SetDestination(nextDestination);
        // Get a reference to the player
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (Vector3.Distance(transform.position, initialPosition) >= maxDistance)
        {
            theAgent.SetDestination(initialPosition);
        }


        // If the player is within the follow distance, start following them
        if (Vector3.Distance(transform.position, player.transform.position) < followDistance)
        {
            // Set the NPC's destination to the player's position
            theAgent.SetDestination(player.transform.position);
            //theAgent.speed = speed;
        }
        else
        {
            if (timer >= changeDestinationTime)
            {
                nextDestination = GetRandomDestination();
                theAgent.SetDestination(nextDestination);
                timer = 0f;
            }

            // Set the NPC's speed to the specified wander speed
        }
        theAgent.speed = speed * 55.5f;

    }

    Vector3 GetRandomDestination()
    {
        float randomX = Random.Range(initialPosition.x - maxDistance, initialPosition.x + maxDistance);
        float randomZ = Random.Range(initialPosition.z - maxDistance, initialPosition.z + maxDistance);
        Vector3 randomDestination = new Vector3(randomX, initialPosition.y, randomZ);
        return randomDestination;
    }
}
