using UnityEngine;
using System.Collections;

public class ParticulasScriptTest5 : MonoBehaviour 
{
	ParticleSystem particula;

	// Use this for initialization
	void Start () 
	{
		particula = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetMouseButtonDown (0))
			particula.Emit (1);
	}

	/*void OnParticleCollision (GameObject go)
	{
		Debug.Log ("Colision con" + go.name);
		go.GetComponent<Rigidbody> ().AddExplosionForce (1000, go.transform.position, 5);
	}*/
}
