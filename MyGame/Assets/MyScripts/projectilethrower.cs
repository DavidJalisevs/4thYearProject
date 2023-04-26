using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class projectilethrower : MonoBehaviour
{
    public GameObject projectilePrefab; // The projectile to throw
    public float range = 10f; // The range at which to start throwing
    public float throwForce = 10f; // The force with which to throw the projectile
    public float throwInterval = 2f; // The interval at which to throw projectiles
    public Transform spawnpoint;
    private float throwTimer = 0f; // The current timer for throwing projectiles
    public GameObject targetEnemy; // The closest enemy with the tag "Enemy"
    public FlyingEnemy flyinghandler;
    // Update is called once per frame
    void Update()
    {
        // Find the closest enemy with the tags
        List<GameObject> enemies=new List<GameObject>();
       enemies = GameObject.FindGameObjectsWithTag("npc").ToList();
        enemies.Add(GameObject.FindGameObjectWithTag("Player"));
        float closestDistance = Mathf.Infinity;
        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                targetEnemy = enemy;
            }
        }

        // If there is a target enemy within range, start shooting projectiles
        if (targetEnemy != null && closestDistance <= range)
        {
            flyinghandler.targetPosition = targetEnemy.transform.position;
            throwTimer += Time.deltaTime;

            // If enough time has passed, throw a projectile
            if (throwTimer >= throwInterval)
            {
                // Calculate the direction to throw the projectile
                Vector3 direction = (targetEnemy.transform.position - transform.position).normalized;

                // Instantiate the projectile and throw it
                GameObject projectile = Instantiate(projectilePrefab, spawnpoint.transform.position, Quaternion.identity);
                Rigidbody rb = projectile.GetComponent<Rigidbody>();
                rb.AddForce(direction * throwForce, ForceMode.Impulse);
                flyinghandler.anim_enemy.SetTrigger("shoot");

                throwTimer = 0f; // Reset the throw timer
            }
        }
        else
        {
            throwTimer = 0f; // Reset the throw timer if there is no target enemy within range
        }
    }
}
