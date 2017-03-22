using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour 
{
	public int lives = 1;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void getHit(int damage)
	{
		lives -= damage;

		Debug.Log ("Disparado");

		if (lives <= 0)
			Destroy (this.gameObject);
	}
}
