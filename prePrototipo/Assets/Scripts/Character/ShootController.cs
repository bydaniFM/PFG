using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour 
{
	public GameObject bullet; // GameObject de base para las balas
	public int size; // Tamaño del array de balas
	public int cooldownTime; // Tiempo de cooldown
	public float speed; // Velocidad de las balas

	public GameObject[] bullets; // Array de balas
	public bool inCooldown; // Estamos en el cooldown?

	// Use this for initialization
	void Start () 
	{
		inCooldown = false;

		bullets = new GameObject[size];

		for (int i = 0; i < size; i++) 
		{
			bullets [i] = (GameObject)Instantiate (bullet, this.transform.position, this.transform.rotation); 
			bullets [i].gameObject.SetActive (false); // Object Pooler
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!inCooldown) // Si no estamos en el cooldown
		{
			if (Input.GetMouseButtonDown (0)) // Click izquierdo
			{
				bool done = false;

				for (int i = 0; i < bullets.Length && !done; i++) 
				{
					if (!bullets [i].activeSelf) // Busqueda de una bala disponible en el array
					{
						// Posición, dirección y activar
						bullets [i].transform.position = this.transform.position;
						bullets [i].transform.rotation = this.transform.rotation;
						bullets [i].SetActive (true);

						// Lanzamiento de la bala
						Rigidbody rb = bullets [i].GetComponent<Rigidbody> ();
						rb.velocity = this.transform.forward * speed;

						// Fin de la búsqueda y cooldown
						done = true;
						inCooldown = true;
						StartCoroutine (cooldown ());
					}
				}
			}
		}	
	}

	IEnumerator cooldown ()
	{
		yield return new WaitForSeconds(cooldownTime);
		inCooldown = false;
	}
}
