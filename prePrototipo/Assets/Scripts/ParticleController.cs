using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour 
{
	public ParticleSystem particula = null;
	public int damage = 0;

	// Use this for initialization
	void Start () 
	{
		//particula = GetComponent<pARTICLE>	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetMouseButtonDown (0))
			particula.Emit (0);	
	}
}
