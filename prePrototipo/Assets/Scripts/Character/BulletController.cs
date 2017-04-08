using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour 
{
	public float lifeTime; // Tiempo de vida de la bala

	float counter; // Contador de vida restante

	// Use this for initialization
	void Start () 
	{
		counter = lifeTime;
	}
	
	// Update is called once per frame
	void Update () 
	{
		counter -= Time.deltaTime;

		if (counter <= 0) 
		{
			counter = lifeTime;
			this.gameObject.SetActive (false); //Object Pool
		}
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Enemy") 
		{
			EnemyController ec = collision.gameObject.GetComponent<EnemyController> ();
			ec.getHit (1);
		}

		this.gameObject.SetActive (false);
	}

}
