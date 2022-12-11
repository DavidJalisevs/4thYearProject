using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startParticle : MonoBehaviour
{
	// Start is called before the first frame update
	public ParticleSystem particleSys;

	void Start()
    {
		//particleSys = new ParticleSystem();
		particleSys.Play();

	}

	// Update is called once per frame
	void Update()
    {

	}
}
