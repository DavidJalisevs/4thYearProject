using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class fireBallScript : MonoBehaviour
{
	// Prefab for the fireball
	public GameObject fireballPrefab;
	public SteamVR_Action_Boolean fireAction;
	public bool allowButtonHold;
	public bool shooting;
	// Speed of the fireball
	public float fireballSpeed = 50f;
	public int fireBallCount = 0;
	// Start is called before the first frame update
	void Start()
    {
        
    }


	// Update is called once per frame
	void Update()
	{
		// Check if the player has pressed the space bar
		//if (allowButtonHold) shooting = fireAction.state;
		 shooting = fireAction.stateDown;


		if (shooting)
		{
			shootFireBall();
		}
	}



	void shootFireBall()
	{
		// Instantiate a new fireball game object
		var fireball = Instantiate(fireballPrefab);
		fireBallCount = fireBallCount + 1;
		// Set the fireball's position to the current game object's position
		fireball.transform.position = transform.position;

		// Set the fireball's direction to the current game object's forward direction
		var fireballRigidbody = fireball.GetComponent<Rigidbody>();

		fireballRigidbody.AddForce(transform.forward * fireballSpeed, ForceMode.Acceleration);

		Destroy(fireball, 5f);

	}

}
