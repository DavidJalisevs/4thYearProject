using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    public float moveSpeed = 5.0f; // speed of movement
    public float rotateSpeed = 5.0f; 
    public float maxDistance = 10.0f; // maximum distance enemy can move
    public float maxflyDistance = 10.0f; // maximum distance enemy can fly
    public float minflyDistance = 5.0f; // minimum distance enemy can fly
    public float minYdistanceWalking = 1.0f; // minimum Y distance enemy can move

    public float minDistance = 2.0f; // minimum distance enemy can move
    public float attackDistance = 1.0f; // distance at which enemy can attack
    public float minattackDistance = 1.0f; // distance at which enemy can attack

    public float avoidanceDistance = 2.0f; // distance at which enemy avoids obstacles
    public float avoidanceForce = 10.0f; // force at which enemy avoids obstacles
    public float flyTime = 5.0f; // duration of flying state
    public float flySpeed = 10.0f; // speed of flight
    public float flyCooldown = 5.0f; // cooldown between flights

    private Transform player; // transform of player
    [HideInInspector]public Vector3 targetPosition; // position of target (player or random position)
    private bool attacking = false; // whether enemy is attacking player or not
    private bool flying = false; // whether enemy is currently flying
    private float flyTimer = 0.0f; // timer for flying state
    private float flyCooldownTimer = 0.0f; // timer for fly cooldown

    public projectilethrower gun;
    public Animator anim_enemy; // animator for enemy model 

    private flyingSpawner flyingSpawnerScript;
    private GameManager gameManagerScript;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // find player object in scene
        targetPosition = GetRandomPosition(); // set initial target position to random position
		flyingSpawnerScript = FindObjectOfType<flyingSpawner>();
		gameManagerScript = FindObjectOfType<GameManager>();
	}

    void Update()
    {
        // update fly timers
        if (flying)
        {
            flyTimer += Time.deltaTime;
            if (flyTimer >= flyTime)
            {
               
                flying = false;
                flyTimer = 0.0f;
                flyCooldownTimer = flyCooldown;
                targetPosition = GetRandomPosition();
            }
        }
        else if (flyCooldownTimer > 0.0f)
        {
            if (transform.position.y > minYdistanceWalking)
                transform.position -= new Vector3(0,flySpeed * Time.deltaTime);

            flyCooldownTimer -= Time.deltaTime;
        }

        Fly();
        if(transform.position.y<minYdistanceWalking)
        {
            anim_enemy.SetBool("isflying", false);

        }else
        {
            anim_enemy.SetBool("isflying", true);

        }
        // if player is in attack distance and not already attacking, attack player
        if (Vector3.Distance(transform.position, player.position) <= attackDistance && !attacking)
        {
            attacking = true;
            StartCoroutine(AttackPlayer());
        }
        else // if not attacking player, move randomly around scene
        {
            // if enemy has reached target position, get new random position
            if (Vector3.Distance(transform.position, targetPosition) <= minDistance)
            {
                if (gun.targetEnemy == null)
                {
                    targetPosition = GetRandomPosition();
                }
            }

            // move towards target position, with different speeds depending on whether flying or not
            if (Vector3.Distance(transform.position, targetPosition) > minattackDistance)
            {
                if (flying)
                {
                    transform.position = Vector3.MoveTowards(transform.position, targetPosition, flySpeed * Time.deltaTime);
                }
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                }
                anim_enemy.SetBool("canmove", true);
            }
            else
            {
                anim_enemy.SetBool("canmove", false);
            }
            // rotate towards target position
            Vector3 direction = (targetPosition - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotateSpeed * Time.deltaTime);

            // avoid obstacles
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, avoidanceDistance))
            {
                if (hit.collider.gameObject.CompareTag("Obstacle") || hit.collider.gameObject.CompareTag("building"))
                {
                    Vector3 avoidanceDirection = transform.position - hit.point;
                    avoidanceDirection.y = 0.0f;
                    Vector3 avoidanceForceVector = avoidanceDirection.normalized * avoidanceForce;
                    transform.position += avoidanceForceVector * Time.deltaTime;
                }
            }
        }
    }

    Vector3 GetRandomPosition()
    {
        // if currently flying, choose random position in air
        if (flying)
        {
            return new Vector3(Random.Range(-maxDistance, maxDistance), Random.Range(minflyDistance, maxflyDistance), Random.Range(-maxDistance, maxDistance));
        }
        else 
        {
            return new Vector3(Random.Range(-maxDistance, maxDistance), minYdistanceWalking, Random.Range(-maxDistance, maxDistance));
        }
    }

    IEnumerator AttackPlayer()
    {
        while (attacking)
        {
            var pos = gun.targetEnemy.transform.position;
            pos.y = minYdistanceWalking;
            // move towards player

            if (Vector3.Distance(transform.position, pos) > minattackDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position, pos, moveSpeed * Time.deltaTime);
                anim_enemy.SetBool("canmove", true);
            }
            else
            {
                anim_enemy.SetBool("canmove", false);
            }
            // rotate towards player
            Vector3 direction = (pos - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotateSpeed * Time.deltaTime);

            // if player is out of attack distance, stop attacking player
            if (Vector3.Distance(transform.position, pos) > attackDistance)
            {
                attacking = false;
                targetPosition = GetRandomPosition();
            }

            yield return null;
        }
    }

    // set flying state to true if not currently on cooldown
    public void Fly()
    {
        if (!flying && flyCooldownTimer <= 0.0f)
        {
            flying = true;
            targetPosition = GetRandomPosition();
        }
    }


	private void OnTriggerEnter(Collider other)
	{
		// Check if the player has pressed the space bar.
		//Check for a match with the specific tag on any GameObject that collides with your GameObject
		if (other.gameObject.tag == "fireball")
		{
			Debug.Log("collision with Flying enemy");
			// If the space bar is pressed, decrease the cube's current health by 20.
			Destroy(other.gameObject);
			Destroy(gameObject);
			flyingSpawnerScript.EnemyDied();
            gameManagerScript.score = gameManagerScript.score + 15;

		}
	}



}
