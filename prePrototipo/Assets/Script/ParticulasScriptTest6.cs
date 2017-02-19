using UnityEngine;
using System.Collections;

public class ParticulasScriptTest6 : MonoBehaviour 
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

	void OnParticleCollision (GameObject go)
	{
		Debug.Log ("Colision con " + go.name);
		Renderer rend = go.GetComponent<Renderer>();
		rend.material.color = Color.blue;
	}
}
