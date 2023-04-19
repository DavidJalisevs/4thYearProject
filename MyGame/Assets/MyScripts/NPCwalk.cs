using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCwalk : MonoBehaviour
{
    //public GameObject destinationPoint;
    NavMeshAgent theAgent;

    private Vector3 initialPosition;
    private Vector3 nextDestination;
    private float maxDistance = 300f;
    private float timer = 0f;
    private float changeDestinationTime = 3f;


    void Start()
    {
        theAgent = GetComponent<NavMeshAgent>();
        initialPosition = transform.position;
        nextDestination = GetRandomDestination();
        theAgent.SetDestination(nextDestination);
    }

    void Update()
    {
        //theAgent.SetDestination(destinationPoint.transform.position);

        timer += Time.deltaTime;
        if (timer >= changeDestinationTime)
        {
            nextDestination = GetRandomDestination();
            theAgent.SetDestination(nextDestination);
            timer = 0f;
        }

        if (Vector3.Distance(transform.position, initialPosition) >= maxDistance)
        {
            theAgent.SetDestination(initialPosition);
        }

    }

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("redDog"))
		{
			Destroy(gameObject);
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
