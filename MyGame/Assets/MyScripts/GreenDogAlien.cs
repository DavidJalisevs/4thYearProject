using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public enum States
{
	Running = 1,
	Attack = 2,
	Hurt = 3,
    Dead = 4
}


public class GreenDogAlien : MonoBehaviour
{
    //public GameObject destinationPoint;
    NavMeshAgent theAgent;

    private Vector3 initialPosition;
    private Vector3 nextDestination;
    public float maxDistance = 150f;
    private float timer = 0f;
    private float changeDestinationTime = 3f;
    private GameObject player;
    public float speed = 5.0f;
    public float followDistance = 100.0f;
    private RedDogSpawner spawner;
    public float currentHealth = 100.0f;
	public Animator anim;
	public States state;
	private GameManager gameManagerScript;

	void Start()
    {
        theAgent = GetComponent<NavMeshAgent>();
        initialPosition = transform.position;
        nextDestination = GetRandomDestination();
        theAgent.SetDestination(nextDestination);
        // Get a reference to the player
        player = GameObject.FindWithTag("Player");
        spawner = FindObjectOfType<RedDogSpawner>();
		gameManagerScript = FindObjectOfType<GameManager>();

	}
	private void OnDestroy()
    {
        spawner.DecreaseDogCount();
    }
    void Update()
    {
		checkStatesForAnimator();
        if (currentHealth > 0)
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
					Debug.Log("LOOOOOOOOOOOOL");
                }

                // Set the NPC's speed to the specified wander speed
            }
            theAgent.speed = speed * 55.5f;
        }
		if (currentHealth <= 0)
		{
			// If the cube's health has reached 0, destroy the cube.
			//Destroy(gameObject);
			state = States.Dead;
            Destroy(gameObject,2);
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
			currentHealth -= 20;
            Destroy(collision.gameObject);
           

			// Check if the cube's health has reached 0.
			if (currentHealth <= 0)
			{
				// If the cube's health has reached 0, destroy the cube.
				//Destroy(gameObject);
                state = States.Dead;
				gameManagerScript.score = gameManagerScript.score + 25;

			}
		}

		if (collision.gameObject.tag == "building")
		{
			Debug.Log("Building and dog");
			// If the space bar is pressed, decrease the cube's current health by 20.
			state = States.Attack;

		}


	}
	IEnumerator SwitchStateAfterDelay(float delay)
	{
		yield return new WaitForSeconds(delay);
		state = States.Running;
	}

	void checkStatesForAnimator()
	{
		//////
		///Idle animations Conrolls
		//////
		if (state == States.Running)
		{

		
		}


		if (state == States.Dead)
		{


		}

		if (state == States.Attack)
		{

			StartCoroutine(SwitchStateAfterDelay(1.0f));

		}


		//////
		///LEave it hERE DONT TOUCH THIS OR HANDS WILL BE THROWN
		//////
		anim.SetInteger("State", (int)state);
	}



}
