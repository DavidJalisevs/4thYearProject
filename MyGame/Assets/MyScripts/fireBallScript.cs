using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class fireBallScript : MonoBehaviour
{

	public int fireBallCount = 0; // counts the fire balls

	public bool allowButtonHold; // allows to hokld buttonb
	public bool shooting; // bool to shoot

	private float fireballSpeed = 3200f; // speed of fireball

	// Prefab for the fireball
	public GameObject fireballPrefab;
	public SteamVR_Action_Boolean fireAction;

	// Speed of the fireball
	public AudioSource fireballSound; // audio for fire ball

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
		// Set the fireball's position to the current game object's position
		fireball.transform.position = transform.position;

		// Set the fireball's direction to the current game object's forward direction
		var fireballRigidbody = fireball.GetComponent<Rigidbody>();

		fireballRigidbody.AddForce(transform.forward * fireballSpeed, ForceMode.Acceleration);
		fireBallCount = fireBallCount + 1;

		// Add an AudioSource component to the fireball and play the audio clip
		fireballSound.Play();


		Destroy(fireball, 5f);

	}

}
