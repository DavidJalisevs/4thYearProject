using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletscript : MonoBehaviour
{
	private HealthManager healthManagerScript;

	// Start is called before the first frame update
	void Start()
    {
		healthManagerScript = FindObjectOfType<HealthManager>();

	}

	// Update is called once per frame
	void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other!=null)
        {
            if(other.CompareTag("npc"))
            {
                Destroy(other.gameObject);
            }

            Destroy(gameObject);
        }


		if (other.gameObject.tag == "Player")
		{
			Debug.Log("Building and dog");
			// If the space bar is pressed, decrease the cube's current health by 20.
			healthManagerScript.takeDamage(10);

		}

	}
}
