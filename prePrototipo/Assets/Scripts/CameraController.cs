using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour 
{
	public GameObject target = null;
	public float hSpeed = 10.0f;

	public float height = 0.4f;
	//public float distanceHorizontal = 9.15f;
	public float distanceHorizontal = 5.15f;
	public float distanceHeight = 1.1f;
	public float angle = 12.91f;
	public float playerHeight = 0.9f; //Testeo
	public float viewRangeMax = 5f;
	public float viewRangeMin = 0.5f;

	Transform tr;
	Vector3 positionTarget;
	//Vector3 offsetX;
	//Vector3 offsetY;

	// Use this for initialization
	void Start () 
	{
		tr = this.gameObject.transform;
		positionTarget = new Vector3 ( 0 , distanceHeight, (-distanceHorizontal));
			//tr.position - target.transform.position;
		//tr.position = positionTarget;

		//offsetX = Vector3.zero;
		//offsetY = Vector3.zero;
	}

	void LateUpdate()
	{
		// La cámara gira alrededor del personaje
		//if (Input.GetMouseButton (1)) 
		//{
			

			// Girar con x, sin restricciones
		positionTarget = Quaternion.AngleAxis (Input.GetAxis ("Mouse X") * hSpeed, Vector3.up) * positionTarget;

		Vector3 aux = Quaternion.AngleAxis (Input.GetAxis ("Mouse Y") * hSpeed, Vector3.right) * positionTarget;

		if (aux.y > -viewRangeMin && aux.y < viewRangeMax)
			positionTarget = aux;
			//Vector3 aux = Quaternion.AngleAxis (Input.GetAxis ("Mouse Y") * hSpeed, Vector3.right) * positionTarget;
			//positionTarget = new Vector3 (Mathf.Clamp (aux.x, -viewRange, viewRange), aux.y, aux.z);
		//}

		tr.position = target.transform.position + positionTarget; 
		// Es igual a la funcion por defecto de unity, pero sin problemas después por la posición de la cámara: 
		// tr.RotateAround (target.transform.position, Vector3.up, Input.GetAxis ("Mouse X") * hSpeed); 

		tr.LookAt(target.transform.position);
	}
}
